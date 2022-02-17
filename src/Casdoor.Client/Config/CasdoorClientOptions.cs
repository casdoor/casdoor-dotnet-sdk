using System.Text;
using System.Web;

namespace Casdoor.Client.Config;

/// <summary>
/// CasdoorClientOptions is the core configuration.
/// The first step to use this SDK is to config an instance of CasdoorClientOptions.
/// </summary>
public class CasdoorClientOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string JwtPublicKey { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;

    public string ApiPath { get; set; } = "/api";
    public string AuthorizePath { get; set; } = "/api/login/oauth/authorize";
    public string TokenPath { get; set; } = "/api/login/oauth/access_token";

    public string ApiEndpoint => $"{Endpoint}{ApiPath}";
    public string AuthorizeEndpoint => $"{Endpoint}{AuthorizePath}";
    public string TokenEndpoint => $"{Endpoint}{TokenPath}";


    public string GetSigninUrl(string redirectUrl)
    {
        string scope = "read";
        string state = ApplicationName;
        return
            $"{Endpoint}/login/oauth/authorize?client_id={ClientId}&response_type=code&redirect_uri={HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8)}&scope={scope}&state={state}";
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
            ? $"{Endpoint}/signup/{ApplicationName}"
            : GetSigninUrl(redirectUrl).Replace("/login/oauth/authorize", "/signup/oauth/authorize");
    }

    public string GetUserProfileUrl(string username, string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{Endpoint}/users/{OrganizationName}/{username}{param}";
    }

    public string GetMyProfileUrl(string? accessToken)
    {
        string param = "";
        if (accessToken != null && accessToken.Trim().Length != 0)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{Endpoint}/account{param}";
    }
}
