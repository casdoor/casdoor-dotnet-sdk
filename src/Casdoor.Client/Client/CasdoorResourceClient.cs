using System.Text;
using System.Xml.Serialization;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;

namespace Casdoor.Client.Client;

public class CasdoorResourceClient
{
    private readonly CasdoorClientOptions _option;
    private readonly XmlSerializer _objMapper = new(typeof(CasdoorResponse));

    public CasdoorResourceClient(CasdoorClientOptions? options)
    {
        _option = options ?? throw new ArgumentNullException(nameof(options));
    }

    // CasdoorUserClient.cs
    // TODO: what are `createdTime` and `description` for
    public Task<CasdoorUserResource> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream,
        string createdTime = "", string description = "")
    {
        var queryMap = new Dictionary<string, string>
        {
            {"owner", _option.OrganizationName},
            {"user", user},
            {"application", _option.ApplicationName},
            {"tag", tag},
            {"parent", parent},
            {"fullFilePath", fullFilePath},
        };

        using MemoryStream ms = new();
        fileStream.CopyTo(ms);
        byte[] fileBytes = ms.ToArray();

        var respT = CasdoorHttpClient.DoPostAsync(_option,
            "upload-resource", queryMap, fileBytes, true);

        return new Task<CasdoorUserResource>(() =>
        {
            var resp = respT.GetAwaiter().GetResult();
            if (!"ok".Equals(resp.Status))
            {
                throw new CasdoorException(resp.Msg);
            }

            // FIXME: Data2 ?= user
            return new CasdoorUserResource(
                (string)(resp.Data2 ?? string.Empty),
                (string)(resp.Data ?? string.Empty));
        });
    }

    public Task<CasdoorResponse> DeleteResourceAsync(string name)
    {
        string targetUrl =
            $"{_option.Endpoint}/api/delete-resource?clientId={_option.ClientId}&clientSecret={_option.ClientSecret}";
        CasdoorUserResource resource = new(_option.OrganizationName, name);

        using StringWriter textWriter = new();
        _objMapper.Serialize(textWriter, resource);
        string resStr = textWriter.ToString();

        return CasdoorHttpClient.DoPostAsync(_option, "delete-resource",
            null, Encoding.UTF8.GetBytes(resStr), false);
    }
}
