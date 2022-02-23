using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using IdentityModel.Client;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    private readonly TokenClient _tokenClient;
    private readonly CasdoorClientOptions _options;

    public CasdoorTokenClient(TokenClient tokenClient, CasdoorClientOptions options)
    {
        _tokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // TODO: impl
    public Task<TokenResponse> GetTokenAsync() => throw new NotImplementedException();

    public CasdoorUser ParseJwtToken(string token)
    {
        // parse jwt token
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken? decodedJwt = null;
        decodedJwt = handler.ReadJwtToken(token);
        if (decodedJwt == null)
        {
            throw new CasdoorException("decoded JWT is null");
        }

        // verify the jwt public key
        // RSACryptoServiceProvider rsa = new();
        // rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_options.JwtPublicKey), out _); // X.509 encoded
        // rsa.ImportParameters(new RSAParameters());

        // var tokenHandler = new JwtSecurityTokenHandler();
        // try
        // {
        //     tokenHandler.ValidateToken(_options.JwtPublicKey,
        //         new TokenValidationParameters
        //         {
        //             ValidateAudience = false,
        //             ValidateLifetime = false,
        //             ValidateIssuer = false,
        //             IssuerSigningKey = new RsaSecurityKey(rsa)
        //         },
        //         out _);
        // }

        X509Certificate _ = new(_options.JwtPublicKey);

        // convert to CasdoorUser
        CasdoorUser casdoorUser = CopyProperties(decodedJwt.Claims, new CasdoorUser());
        return casdoorUser;
    }

    /// <summary>
    /// adapted from https://stackoverflow.com/questions/36054547/how-to-copy-properties-from-one-net-object-to-another
    /// </summary>
    private static TTarget CopyProperties<TSource, TTarget>(TSource source, TTarget target)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        foreach (var sProp in source.GetType().GetProperties())
        {
            bool isMatched = target != null && target.GetType().GetProperties().Any(tProp =>
                tProp.Name == sProp.Name && tProp.GetType() == sProp.GetType() && tProp.CanWrite);
            if (!isMatched)
            {
                continue;
            }

            object? value = sProp.GetValue(source);
            PropertyInfo? propertyInfo = target?.GetType().GetProperty(sProp.Name);
            propertyInfo?.SetValue(target, value);
        }

        return target;
    }
}
