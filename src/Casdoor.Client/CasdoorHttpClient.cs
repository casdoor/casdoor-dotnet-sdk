using System.Net;
using System.Text;
using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;

namespace Casdoor.Client;

public class CasdoorHttpClient
{
    private readonly CasdoorClientOptions _options;

    public CasdoorHttpClient(CasdoorClientOptions? options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<string> DoGetStringAsync(string url)
    {
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(_options.ClientId, _options.ClientSecret)
        };
        using var client = new HttpClient(handler);
        return await client.GetStringAsync(url);
    }

    public async Task<CasdoorResponse> DoPostAsync(string action,
        Dictionary<string, string>? queryMap, byte[] postBytes, bool isFile)
    {
        string url = GetUrl(action, queryMap ?? new Dictionary<string, string>());
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(_options.ClientId, _options.ClientSecret)
        };
        using HttpClient httpClient = new(handler);
        Task<HttpResponseMessage> postTask;

        if (isFile) // postFileAsync
        {
            using var formData = new MultipartFormDataContent();
            formData.Add(new ByteArrayContent(postBytes), "file", "file");
            postTask = httpClient.PostAsync(url, formData);
        }
        else // postStringAsync
        {
            StringContent postContent = new(
                postBytes.ToString() ??
                throw new CasdoorException("postBytes.ToString() returns null"),
                Encoding.UTF8,
                "text/plain");
            postTask = httpClient.PostAsync(url, postContent);
        }

        var resp = await postTask;
        using MemoryStream ms = new(Encoding.UTF8.GetBytes(resp.Content.ToString()
                                                           ?? throw new CasdoorException(
                                                               "unable to deserialize http response")));
        return await JsonSerializer.DeserializeAsync<CasdoorResponse>(ms)
               ?? throw new CasdoorException("unable to deserialize http response");
    }

    public async Task<Tuple<CasdoorResponse, bool>> ModifyUserAsync(
        string action, CasdoorUser user, string[]? columns)
    {
        var queryMap = new Dictionary<string, string> {{"id", $"{user.Owner}/{user.Name}"}};
        if (columns != null && columns.Length != 0)
        {
            queryMap.Add("columns", string.Join(",", columns));
        }

        user.Owner = _options.OrganizationName;
        string serializedUser = JsonSerializer.Serialize(user);

        var resp = await DoPostAsync(action, queryMap, Encoding.UTF8.GetBytes(serializedUser), false);
        return new Tuple<CasdoorResponse, bool>(resp, "Affected".Equals(resp.Data));
    }

    public string GetUrl(string action, Dictionary<string, string> queryMap)
    {
        StringBuilder queryBuilder = new StringBuilder();
        foreach (var q in queryMap)
        {
            queryBuilder.Append($"{q.Key}={q.Value}&");
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1); // remove the last (a.k.a. redundant) `&`
        string query = queryBuilder.ToString();

        string url = $"{_options.Endpoint}/api/{action}?{query}";
        return url;
    }
}
