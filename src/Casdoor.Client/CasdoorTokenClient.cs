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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Casdoor.Client.Exception;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    private readonly RsaSecurityKey _issuerSigningKey;
    private readonly JwtSecurityTokenHandler _jwtHandler;
    private readonly CasdoorClientOptions _options;
    private readonly TokenClient _tokenClient;
    private readonly X509Certificate _x509Cert;

    private SecurityToken _securityToken;

    public CasdoorTokenClient(HttpClient httpClient, TokenClient tokenClient, CasdoorClientOptions options)
    {
        _tokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _jwtHandler = new JwtSecurityTokenHandler();
        _x509Cert = new X509Certificate(_options.JwtPublicKey);
        _securityToken = new JwtSecurityToken();

        // verify the jwt public key once
        // X.509 encoded
        RSACryptoServiceProvider rsaServiceProvider = new();
        rsaServiceProvider.ImportCspBlob(Encoding.UTF8.GetBytes(_options.JwtPublicKey));
        rsaServiceProvider.ImportParameters(new RSAParameters());

        _issuerSigningKey = new RsaSecurityKey(rsaServiceProvider);
    }

    public virtual Task<TokenResponse> GetTokenAsync(string code, string state = "") =>
        _tokenClient.RequestAuthorizationCodeTokenAsync(code, "");

    public virtual CasdoorUser? ParseJwtToken(string token)
    {
        // parse jwt token
        JwtSecurityToken? decodedJwt = _jwtHandler.ReadJwtToken(token);
        if (decodedJwt is null)
        {
            throw new CasdoorApiException("decoded JWT is null");
        }

        _jwtHandler.ValidateToken(_options.JwtPublicKey,
            new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuer = false,
                IssuerSigningKey = _issuerSigningKey
            },
            out _securityToken);

        return JsonSerializer.Deserialize<CasdoorUser>(decodedJwt.RawData);
    }
}
