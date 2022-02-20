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

        private static CasdoorClientOptions opts;

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
                        Address = $"{opts.TokenEndpoint}",
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
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
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

        public string getSignInUrl(string redirectUrl)
        {
            string scope = "read";
            string state = _config.applicationName ?? throw new ArgumentNullException(nameof(_config.applicationName));
            try
            {
                return $"{opts.AuthorizeEndpoint}?client_id={_config.clientId}&response_type=code" +
                    $"&redirect_uri={HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8)}&scope={scope}&state={state}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string getSignUpUrl()
        {
            return getSignUpUrl(true, "");
        }

        public string getSignUpUrl(string redirectUrl)
        {
            return getSignUpUrl(false, redirectUrl);
        }

        private string getSignUpUrl(bool enablePassWord, string redirectUrl)
        {
            return enablePassWord
                ? $"{_config.endPoint}/signup/{_config.applicationName}"
                : getSignUpUrl(redirectUrl).Replace("/login/oauth/authorize", "/signup/oauth/authorize");
        }

        public string getUserProfileUrl(string userName, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            string param = "";
            if (!string.IsNullOrEmpty(accessToken.Trim()))
            {
                param = "?access_token=" + accessToken;
            }
            return $"{_config.endPoint}/users/{_config.orginazationName}/{userName}{param}";
        }

        public string getMyProfileUrl(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            string param = "";
            if (!string.IsNullOrEmpty(accessToken.Trim()))
            {
                param = "?access_token=" + accessToken;
            }
            return $"{_config.endPoint}/account{param}";
        }
    }
}
