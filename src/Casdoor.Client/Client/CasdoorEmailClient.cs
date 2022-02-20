using System.Text;
using System.Xml.Serialization;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Util;

namespace Casdoor.Client.Client;

public class CasdoorEmailClient
{
    private readonly CasdoorClientOptions _option;
    private readonly XmlSerializer _objMapper = new(typeof(CasdoorResponse));

    public CasdoorEmailClient(CasdoorClientOptions? options)
    {
        _option = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<CasdoorResponse> SendEmailAsync(string title, string content, string sender, string[] receivers)
    {
        string targetUrl =
            $"{_option.Endpoint}/api/send-email?clientId={_option.ClientId}&clientSecret={_option.ClientSecret}";
        CasdoorEmailForm casdoorEmailForm = new(title, content, sender, receivers);

        using StringWriter textWriter = new();
        _objMapper.Serialize(textWriter, casdoorEmailForm);
        string emailFormStr = textWriter.ToString();

        return CasdoorHttpClient.DoPostAsync(_option, "send-email", null,
            Encoding.UTF8.GetBytes(emailFormStr), false);
    }
}
