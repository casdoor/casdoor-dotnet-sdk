using System.Text;
using System.Web;
using Casdoor.Client.Config;

namespace Casdoor.Client;

public class CasdoorAuthClient
{
    private readonly CasdoorClientOptions _options;

    public CasdoorAuthClient(CasdoorClientOptions? options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public string GetSigninUrl(string redirectUrl)
    {
        string scope = "read";
        string state = _options.ApplicationName;
        return
            $"{_options.Endpoint}/login/oauth/authorize?client_id={_options.ClientId}&response_type=code&redirect_uri={HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8)}&scope={scope}&state={state}";
    }

    public string GetSignupUrl()
    {
        return GetSignupUrl(true, "");
    }

    public string GetSignupUrl(string redirectUrl)
    {
        return GetSignupUrl(false, redirectUrl);
    }

    private string GetSignupUrl(bool enablePassword, string redirectUrl)
    {
        return enablePassword
            ? $"{_options.Endpoint}/signup/{_options.ApplicationName}"
            : GetSigninUrl(redirectUrl).Replace("/login/oauth/authorize", "/signup/oauth/authorize");
    }

    public string GetUserProfileUrl(string username, string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{_options.Endpoint}/users/{_options.OrganizationName}/{username}{param}";
    }

    public string GetMyProfileUrl(string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{_options.Endpoint}/account{param}";
    }
}
