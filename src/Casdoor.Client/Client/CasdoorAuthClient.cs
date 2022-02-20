using System.Text;
using System.Web;
using Casdoor.Client.Config;

namespace Casdoor.Client.Client;

public class CasdoorAuthClient
{
    private readonly CasdoorClientOptions _option;

    public CasdoorAuthClient(CasdoorClientOptions? options)
    {
        _option = options ?? throw new ArgumentNullException(nameof(options));
    }

    public string GetSigninUrl(string redirectUrl)
    {
        string scope = "read";
        string state = _option.ApplicationName;
        return
            $"{_option.Endpoint}/login/oauth/authorize?client_id={_option.ClientId}&response_type=code&redirect_uri={HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8)}&scope={scope}&state={state}";
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
            ? $"{_option.Endpoint}/signup/{_option.ApplicationName}"
            : GetSigninUrl(redirectUrl).Replace("/login/oauth/authorize", "/signup/oauth/authorize");
    }

    public string GetUserProfileUrl(string username, string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{_option.Endpoint}/users/{_option.OrganizationName}/{username}{param}";
    }

    public string GetMyProfileUrl(string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{_option.Endpoint}/account{param}";
    }
}
