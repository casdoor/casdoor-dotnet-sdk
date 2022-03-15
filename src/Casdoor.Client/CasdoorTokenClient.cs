using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Casdoor.Client.Abstractions;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    private readonly TokenClient _tokenClient;
    private readonly CasdoorClientOptions _options;
    private readonly JwtSecurityTokenHandler _jwtHandler;
    private readonly X509Certificate _x509Cert;
    private readonly RsaSecurityKey _issuerSigningKey;

    private SecurityToken _securityToken;

    public CasdoorTokenClient(TokenClient tokenClient, CasdoorClientOptions options)
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

    public virtual Task<TokenResponse> GetTokenAsync()
    {
        return _tokenClient.RequestTokenAsync(
            "Authorization_Code"
            // FIXME: borrowed from https://github.com/casdoor/casdoor-java-sdk/blob/master/src/main/java/org/casbin/casdoor/service/CasdoorAuthService.java#L57
            // new Parameters()
        );
    }

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
