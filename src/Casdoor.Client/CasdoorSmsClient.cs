using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;

namespace Casdoor.Client;

public class CasdoorSmsClient
{
    private readonly CasdoorClientOptions _options;
    private readonly CasdoorHttpClient _httpClient;

    public CasdoorSmsClient(CasdoorClientOptions? options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = new CasdoorHttpClient(_options);
    }


    public virtual async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new CasdoorSmsForm(string.Concat("admin/", _options.OrganizationName), content, receivers);
        byte[] postBytes = JsonSerializer.SerializeToUtf8Bytes(form);

        var resp = await _httpClient.DoPostAsync("send-sms", null, postBytes, false);
        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }
    }
}
