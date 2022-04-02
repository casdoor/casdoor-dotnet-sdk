using Casdoor.Client;
using ConsoleApp;
using Microsoft.IdentityModel.Logging;

var httpClient = new HttpClient();
var options = new CasdoorOptions
{
    // Require: Basic options
    Endpoint = "https://door.casdoor.com",
    OrganizationName = "build-in",
    ApplicationName = "app-build-in",
    ApplicationType = "native", // webapp, webapi or native
    ClientId = "541738959670d221d59d",
    ClientSecret = "66863369a64a5863827cf949bab70ed560ba24bf",

    // Optional: The callback path that the client will be redirected to
    // after the user has authenticated. default is "/casdoor/signin-callback"
    CallbackPath = "/callback",
    // Optional: Whether require https for casdoor endpoint
    RequireHttpsMetadata = true,
    // Optional: The scopes that the client is requesting.
    Scope = "openid profile email"

    // More options can be found at README.md
    // https://github.com/casdoor/casdoor-dotnet-sdk/blob/master/README.md
};

var client = new CasdoorClient(httpClient, options);

// If you want look PII in logs or exception, you can set the following
IdentityModelEventSource.ShowPII = true;

var configuration = await options.GetOpenIdConnectConfigurationAsync();
ConsoleExtension.WriteLine("Auto fetching OpenIdConnectConfiguration...");
ConsoleExtension.JsonWriteLine(new
{
    configuration.Issuer,
    configuration.JwksUri,
    configuration.TokenEndpoint,
    configuration.AuthorizationEndpoint,
    configuration.UserInfoEndpoint,
}, ConsoleColor.DarkBlue);

var token = await client.RequestPasswordTokenAsync("admin", "123");
ConsoleExtension.WriteLine("Get tokens by username and password...");
if (token.IsError is false)
{
    ConsoleExtension.JsonWriteLine(new
    {
        token.AccessToken,
        token.RefreshToken,
        token.IdentityToken,
        token.Scope,
        token.ExpiresIn,
        token.TokenType,
    }, ConsoleColor.DarkGreen);
}
else
{
    ConsoleExtension.WriteLine(token.Error, ConsoleColor.Red);
}
