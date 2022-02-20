using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using Casdoor.Client.Config;
using Casdoor.Client.Exceptions;
using Casdoor.Client.Utils;
using IdentityModel.Client;
using Microsoft.IdentityModel.Tokens;

namespace Casdoor.Client.Services
{
    public class CasdoorAuthService
    {
        private static HttpClient? _httpClient;

        private CasdoorConfig? _config;

        private static TokenRequest? _request;

        public CasdoorAuthService(CasdoorConfig configIn)
        {
            _config = configIn;
        }

        public string getOauthToken(string code, string state)
        {
            using (_httpClient = new HttpClient())
            {
                try
                {
                    var response = _httpClient.RequestTokenAsync(new AuthorizationCodeTokenRequest()
                    {
                        Address = $"{_config.endPoint}/api/login/oauth/access_token",
                        ClientId = _config.clientId,
                        ClientSecret = _config.clientSecret,
                        Code = code,
                    });
                    return response.Result.AccessToken;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public CasdoorUser parseJwtToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var parsed = handler.ReadJwtToken(token);
                X509Certificate cert = new X509Certificate2(Encoding.UTF8.GetBytes(_config.jwtPublicKey));
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(cert.GetPublicKey())
                }, out SecurityToken tem);

                Dictionary<string, object> claims = new Dictionary<string, object>(parsed.Claims.Count());
                foreach (var item in parsed.Claims)
                {
                    claims.Add(item.ValueType, item.Value);
                }
                CasdoorUser casdoorInstance = new CasdoorUser();
                return CastUtil.classify(claims, casdoorInstance);
            }
            catch (Exception exp)
            {
                throw new CasdoorAuthException(exp.Message);
            }
        }

        public string getSingInUrl(string redirectUri)
        {
            string scope = "read";
            string state = _config.applicationName ?? throw new ArgumentNullException(nameof(_config.applicationName));
            try
            {
                return $"{_config.endPoint}/login/oauth/authorize?client_id={_config.clientId}%response_type=code" +
                    $"%redirect_uri={HttpUtility.UrlEncode(redirectUri, Encoding.UTF8)}%scope={scope}%state={state}";
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
