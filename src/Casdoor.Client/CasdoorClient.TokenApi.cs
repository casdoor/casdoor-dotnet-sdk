// Copyright 2022 The Casdoor Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Text.Json;
using IdentityModel.Client;

namespace Casdoor.Client;

public partial class CasdoorClient
{
    private readonly ClientCredentialsTokenRequest _credentialsTokenRequest = new ClientCredentialsTokenRequest();

    private async Task<T> ApplyConfigurationAsync<T>(T request, CancellationToken cancellationToken = default) where T : TokenRequest
    {
        var configuration = await _options.GetOpenIdConnectConfigurationAsync(cancellationToken: cancellationToken);
        request.Address = configuration.TokenEndpoint;
        request.ClientId = _options.ClientId;
        request.ClientSecret = _options.ClientSecret;
        return request;
    }

    public virtual async Task<TokenResponse> RequestClientCredentialsTokenAsync(CancellationToken cancellationToken = default)
    {
        var request = _credentialsTokenRequest;
        request.Scope = _options.Scope;
        request = await ApplyConfigurationAsync(request, cancellationToken);
        return await _httpClient.RequestClientCredentialsTokenAsync(request, cancellationToken: cancellationToken);
    }

    public virtual async Task<TokenResponse> RequestPasswordTokenAsync(string username, string password, CancellationToken cancellationToken = default)
    {
        var request = new PasswordTokenRequest {UserName = username, Password = password};
        request = await ApplyConfigurationAsync(request, cancellationToken);
        return await _httpClient.RequestPasswordTokenAsync(request, cancellationToken: cancellationToken);
    }

    public virtual async Task<TokenResponse> RequestAuthorizationCodeTokenAsync(string code, string redirectUri,
        string codeVerifier = "", CancellationToken cancellationToken = default)
    {
        var request = new AuthorizationCodeTokenRequest
        {
            Code = code, RedirectUri = redirectUri, CodeVerifier = codeVerifier
        };
        request = await ApplyConfigurationAsync(request, cancellationToken);
        return await _httpClient.RequestAuthorizationCodeTokenAsync(request, cancellationToken: cancellationToken);
    }

    public virtual async Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        var request = new RefreshTokenRequest {RefreshToken = refreshToken};
        request = await ApplyConfigurationAsync(request, cancellationToken);
        return await _httpClient.RequestRefreshTokenAsync(request, cancellationToken: cancellationToken);
    }

    public virtual CasdoorUser? ParseJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);
        var payload = jsonToken.Payload;
        var result = JsonSerializer.Deserialize<CasdoorUser>(payload.SerializeToJson());
        return result;
    }
}
