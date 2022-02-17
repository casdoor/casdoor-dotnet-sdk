using System.Net;
using Newtonsoft.Json;

namespace Casdoor.Client;

public class Response
{
    [JsonProperty("status")] public string? Status { get; set; }
    [JsonProperty("msg")] public string? Msg { get; set; }
    [JsonProperty("data")] public object? Data { get; set; }
    [JsonProperty("data2")] public object? Data2 { get; set; }
}

public static class Base
{
    public static async Task<string> DoGetStringAsync(CasdoorUserClient casdoorClient, string url)
    {
        WebRequest req = WebRequest.Create(url);


        // TODO: access the fields in <CasdoorUserClient._options>
        // [base.go: 43] req.SetBasicAuth(authConfig.ClientId, authConfig.ClientSecret)
        req.Credentials = new NetworkCredential(casdoorClient._clientId, casdoorClient._clientSecret);

        using WebResponse resp = await req.GetResponseAsync();
        return resp.ToString() ?? "";
    }

    public static async Task<Response>
        DoPostAsync(string action, Dictionary<string, string>? queryMap, byte[] postBytes, bool isFile) =>
        throw new NotImplementedException();

    // TODO: ref to a Tuple is right?
    public static ref Task<Tuple<Response, bool>> ModifyUserAsync(string action, ref CasdoorUser user, string[]? columns) =>
        throw new NotImplementedException();
}
