using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Casdoor.Client.Client;

public class CasdoorSmsClient
{
    private readonly CasdoorClientOptions _clientOptions;

    public CasdoorSmsClient(CasdoorClientOptions? options)
    {
        _clientOptions = options ?? throw new ArgumentNullException(nameof(options));
    }


    public virtual async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new CasdoorSmsForm(string.Concat("admin/", _clientOptions.OrganizationName), content, receivers);
        byte[] postBytes = JsonSerializer.SerializeToUtf8Bytes(form);

        CasdoorResponse resp = await CasdoorHttpClient.DoPostAsync(_clientOptions,"send-sms", null, postBytes, false);
        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }
    }
}
