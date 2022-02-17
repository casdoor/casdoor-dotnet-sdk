using System.Net;
using System.Text;
using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;

namespace Casdoor.Client;

public class CasdoorHttpClient
{
    private readonly CasdoorClientOptions _options;
    private readonly HttpClient _client;

    public CasdoorHttpClient(CasdoorClientOptions? options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _client = new HttpClient(
            new HttpClientHandler {Credentials = new NetworkCredential(_options.ClientId, _options.ClientSecret)});
    }

    public async Task<string> DoGetStringAsync(string url)
    {
        return await _client.GetStringAsync(url);
    }

    public async Task<CasdoorResponse> DoPostStringAsync(string action,
        List<KeyValuePair<string, string>>? queryMap, string postString)
    {
        string url = GetUrl(action, queryMap);
        StringContent postContent = new(
            postString,
            Encoding.UTF8,
            "text/plain");

        var resp = await _client.PostAsync(url, postContent);
        var cs = await resp.Content.ReadAsStreamAsync();
        return (await JsonSerializer.DeserializeAsync<CasdoorResponse>(cs))!;
    }

    public async Task<CasdoorResponse> DoPostFileAsync(string action,
        List<KeyValuePair<string, string>>? queryMap, StreamContent postStream)
    {
        string url = GetUrl(action, queryMap);
        using var formData = new MultipartFormDataContent();
        formData.Add(postStream, "file", "file");

        var resp = await _client.PostAsync(url, formData);
        var cs = await resp.Content.ReadAsStreamAsync();
        return (await JsonSerializer.DeserializeAsync<CasdoorResponse>(cs))!;
    }

    public async Task<CasdoorResponse> ModifyUserAsync(
        string action, CasdoorUser user, string[]? columns)
    {
        var queryMap = new List<KeyValuePair<string, string>> {new("id", $"{user.Owner}/{user.Name}")};
        if (columns != null && columns.Length != 0)
        {
            queryMap.Add(new KeyValuePair<string, string>("columns", string.Join(",", columns)));
        }

        user.Owner = _options.OrganizationName;
        string serializedUser = JsonSerializer.Serialize(user);

        return await DoPostStringAsync(action, queryMap, serializedUser);
    }

    public string GetUrl(string action, List<KeyValuePair<string, string>>? queryMap)
    {
        StringBuilder queryBuilder = new();
        if (queryMap is not null)
        {
            foreach (var q in queryMap)
            {
                queryBuilder.Append($"{q.Key}={q.Value}&");
            }
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1); // remove the last (redundant) `&`
        string query = queryBuilder.ToString();

        return $"{_options.Endpoint}/api/{action}?{query}";
    }
}
