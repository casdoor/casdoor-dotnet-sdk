namespace Casdoor.Client;

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
}
