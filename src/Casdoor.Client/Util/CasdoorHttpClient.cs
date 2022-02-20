using System.Net;
using System.Text;
using System.Xml.Serialization;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;

namespace Casdoor.Client.Util;

public static class CasdoorHttpClient
{
    private static readonly XmlSerializer s_objMapper = new(typeof(CasdoorResponse));

    public static Task<string> DoGetStringAsync(CasdoorClientOptions clientOptions, string url)
    {
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(clientOptions.ClientId, clientOptions.ClientSecret)
        };
        using var client = new HttpClient(handler);
        return client.GetStringAsync(url);
    }

    public static Task<CasdoorResponse> DoPostAsync(CasdoorClientOptions clientOptions,
        string action, Dictionary<string, string>? queryMap, byte[] postBytes, bool isFile)
    {
        string url = GetUrl(clientOptions, action, queryMap ?? new Dictionary<string, string>());
        using var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(clientOptions.ClientId, clientOptions.ClientSecret)
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
                throw new CasdoorException("postBytes.ToString() returns null", new IOException()),
                Encoding.UTF8,
                "text/plain");
            postTask = httpClient.PostAsync(url, postContent);
        }

        return new Task<CasdoorResponse>(() =>
        {
            var resp = postTask.Result;
            using MemoryStream ms = new(Encoding.UTF8.GetBytes(resp.Content.ToString()
                                                               ?? throw new CasdoorException(
                                                                   "unable to deserialize http response")));
            return s_objMapper.Deserialize(ms) as CasdoorResponse
                   ?? throw new CasdoorException("unable to deserialize http response");
        });
    }

    public static Task<Tuple<CasdoorResponse, bool>> ModifyUserAsync(
        CasdoorClientOptions clientOptions, string action, CasdoorUser user, string[]? columns)
    {
        var queryMap = new Dictionary<string, string> {{"id", $"{user.Owner}/{user.Name}"}};
        if (columns != null && columns.Length != 0)
        {
            queryMap.Add("columns", string.Join(",", columns));
        }

        user.Owner = clientOptions.OrganizationName;

        using StringWriter textWriter = new();
        try
        {
            s_objMapper.Serialize(textWriter, user);
        }
        catch (System.Exception e)
        {
            throw new CasdoorException("cannot serialize user", e);
        }

        string serializedUser = textWriter.ToString();


        var respTask = DoPostAsync(clientOptions, action, queryMap, Encoding.UTF8.GetBytes(serializedUser), false);

        return new Task<Tuple<CasdoorResponse, bool>>(() =>
        {
            var resp = respTask.Result;
            return new Tuple<CasdoorResponse, bool>(resp, "Affected".Equals(resp.Data));
        });
    }

    public static string GetUrl(CasdoorClientOptions clientOptions, string action, Dictionary<string, string> queryMap)
    {
        StringBuilder queryBuilder = new StringBuilder();
        foreach (var q in queryMap)
        {
            queryBuilder.Append($"{q.Key}={q.Value}&");
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1); // remove the last (a.k.a. redundant) `&`
        string query = queryBuilder.ToString();

        string url = $"{clientOptions.Endpoint}/api/{action}?{query}";
        return url;
    }
}
