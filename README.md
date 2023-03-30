# Casdoor .NET SDK
[![Actions Status](https://github.com/casdoor/casdoor-dotnet-sdk/workflows/Build/badge.svg)](https://github.com/casdoor/casdoor-dotnet-sdk/actions)
[![GitHub](https://img.shields.io/github/license/casdoor/casdoor-dotnet-sdk)](https://github.com/casdoor/casdoor-dotnet-sdk/blob/master/LICENSE)

The Casdoor's SDK for .NET/ASP.NET Core, which will allow you to easily connect your application to the Casdoor authentication system without having to implement it from scratch.

**Note:** This SDK is currently in beta and is not yet ready for use.

## Packages
This SDK is built using the following packages for different platforms:

| Package Name        | Develop (MyGet)                                                                                                                                                  | Release (NuGet)                                                                                                                         | Description          | Supported frameworks                   |
|---------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|----------------------|----------------------------------------|
| `Cadoor.Client`     | [![MyGet](https://img.shields.io/casdoor.myget/casdoor/v/Casdoor.Client?label=Version)](https://www.myget.org/feed/casdoor/package/nuget/Casdoor.Client)         | [![NuGet](https://buildstats.info/nuget/Casdoor.Client?includePreReleases=true)](https://www.nuget.org/packages/Casdoor.Client)         | SDK for .NET         | .NET Standard 2.0/.NET 4.6.1 and newer |
| `Cadoor.AspNetCore` | [![MyGet](https://img.shields.io/casdoor.myget/casdoor/v/Casdoor.AspNetCore?label=Version)](https://www.myget.org/feed/casdoor/package/nuget/Casdoor.AspNetCore) | [![NuGet](https://buildstats.info/nuget/Casdoor.AspNetCore?includePreReleases=true)](https://www.nuget.org/packages/Casdoor.AspNetCore) | SDK for ASP.NET Core | .NET Core 3.1 and newer                |
| `Cadoor.Native`     | `wait develop`                                                                                                                                                   | `wait publish`                                                                                                                          | SDK for WPF or Maui  | -                                      |

## Casdoor Client
Casdoor.Client is a base package for the specific platform SDKs. It contains follow features.
- CasdoorClient: A API client implementation for the Casdoor. You can use to call the Casdoor APIs.
##### Usage
```cs
var httpClient = new HttpClient();
var client = new CasdoorClient(HttpClient, new CasdoorOptions{
    Endpoint = "https://door.casdoor.com",
    OrganizationName = "build-in",
    ApplicationName = "app-build-in",
    ApplicationType = "native", // webapp, webapi or native
    ClientId = "<your client id>",
    ClientSecret = "<your client secret>",
});

// Request tokens (You should enable credentials flow in your Casdoor application)
var token = await client.RequestClientCredentialsTokenAsync();
client.SetBearerToken(token);

var currentUser = client.ParseJwtToken(token.AccessToken);

// Request user info
var users = await client.GetUsersAsync();
// Request roles 
var roles = await client.GetRolesAsync();
// Request Permissions
var persmissions = await client.GetPermissionsAsync();

var policy = new CasdoorPermissionRule()
{
    Id = $"{currentUser.Owner}/{persmissions.First().Name}",
    V0 = $"{currentUser.Owner}/{currentUser.Name}",
    V1 = "example-resource",
    V2 = "example-action" 
};

var isPermissionAvaliable = await client.EnforceAsync(policy);

```
- CasdoorOptions (Updating)
- Extensions (Updating)

## Casdoor AspNetCore
### Getting started
1. Add the following settings about casdoor in your appsettings.json file:
```json5
{
    "Casdoor": {
        "Endpoint": "<your casdoor host>",
        "OrganizationName": "<your organization name>",
        "ApplicationName": "<your application name>",
        "ApplicationType": "webapp", // webapp or webapi (webapi is not yet supported)
        "ClientId": "<your client id>",
        "ClientSecret": "<your client secret>",

        // Optional: The callback path that the client will be redirected to
        // after the user has authenticated. default is "/casdoor/signin-callback"
        "CallbackPath": "/callback",
        // Optional: Whether require https for casdoor endpoint
        "RequireHttpsMetadata": false,
        // Optional: The scopes that the client will request
        "Scopes": [
            "openid",
            "profile",
            "email",
        ]
    }
}
```

2. Add the following code to your webapp:
```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCasdoor(builder.Configuration.GetSection("Casdoor"))
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
```

3. Now you can use the Casdoor authentication scheme in your webapp!

### Samples
#### 1. [Mvc sample](https://github.com/casdoor/casdoor-dotnet-sdk/tree/master/samples/MvcApp)
It is a mvc webapp that uses Casdoor authentication.
The default settings use the public demo Casdoor and Casnode configuration, you can directly start the webapp by running:
```bash
dotnet run
```
Or change the settings in the appsettings.json according to your deployed casdoor configuration.

## License
This project is licensed under the [Apache 2.0 license](LICENSE).
