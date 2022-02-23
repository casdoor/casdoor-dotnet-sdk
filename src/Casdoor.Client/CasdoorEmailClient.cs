using System.Text;
using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Util;

namespace Casdoor.Client;

public class CasdoorEmailClient
{
    private readonly CasdoorHttpClient _httpClient;
    public CasdoorEmailClient(CasdoorClientOptions? options)
    {
        CasdoorClientOptions opts = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = new CasdoorHttpClient(opts);
    }

    public async Task<CasdoorResponse> SendEmailAsync(string title, string content, string sender, string[] receivers)
    {
        CasdoorEmailForm casdoorEmailForm = new(title, content, sender, receivers);
        string emailFormStr = JsonSerializer.Serialize(casdoorEmailForm);

        return await _httpClient.DoPostAsync("send-email", null,
            Encoding.UTF8.GetBytes(emailFormStr), false);
    }
}
