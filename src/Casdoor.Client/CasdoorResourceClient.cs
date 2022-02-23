using System.Text;
using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;

namespace Casdoor.Client;

public class CasdoorResourceClient
{
    private readonly CasdoorClientOptions _options;
    private readonly CasdoorHttpClient _httpClient;

    public CasdoorResourceClient(CasdoorClientOptions? options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = new CasdoorHttpClient(_options);
    }

    // CasdoorUserClient.cs
    // TODO: what are `createdTime` and `description` for
    public async Task<CasdoorUserResource> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream,
        string createdTime = "", string description = "")
    {
        var queryMap = new Dictionary<string, string>
        {
            {"owner", _options.OrganizationName},
            {"user", user},
            {"application", _options.ApplicationName},
            {"tag", tag},
            {"parent", parent},
            {"fullFilePath", fullFilePath},
        };

        using MemoryStream ms = new();
        await fileStream.CopyToAsync(ms);
        byte[] fileBytes = ms.ToArray();

        var resp = await _httpClient.DoPostAsync(
            "upload-resource", queryMap, fileBytes, true);

        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }

        // FIXME: Data2 ?= user
        return new CasdoorUserResource(
            (string)(resp.Data2 ?? string.Empty),
            (string)(resp.Data ?? string.Empty));
    }

    public async Task<CasdoorResponse> DeleteResourceAsync(string name)
    {
        CasdoorUserResource resource = new(_options.OrganizationName, name);
        string resStr = JsonSerializer.Serialize(resource);

        return await _httpClient.DoPostAsync("delete-resource",
            null, Encoding.UTF8.GetBytes(resStr), false);
    }
}
