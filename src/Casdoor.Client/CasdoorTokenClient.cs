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

using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    private readonly CasdoorOptions _options;
    private readonly TokenClient _tokenClient;

    public CasdoorTokenClient(TokenClient tokenClient, CasdoorOptions options)
    {
        _tokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    public virtual Task<TokenResponse> RequestClientCredentialsTokenAsync()
    {
        return _tokenClient.RequestClientCredentialsTokenAsync();
    }

    public virtual Task<TokenResponse> RequestPasswordTokenAsync(string username, string password)
    {
        return _tokenClient.RequestPasswordTokenAsync(username, password);
    }

    public virtual Task<TokenResponse> RequestAuthorizationCodeTokenAsync(string code, string redirectUri, string codeVerifier = "")
    {
        return _tokenClient.RequestAuthorizationCodeTokenAsync(code, redirectUri, codeVerifier);
    }

    public virtual Task<TokenResponse> RequestRefreshTokenAsync(string refreshToken)
    {
        return _tokenClient.RequestRefreshTokenAsync(refreshToken);
    }
}
