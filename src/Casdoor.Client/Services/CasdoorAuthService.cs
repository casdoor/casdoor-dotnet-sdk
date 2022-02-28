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
        private static CasdoorConfig? _config;

        private TokenClient _client;

        private CasdoorClientOptions _options;

        private TokenRequest? _authCodeReq;


        public CasdoorAuthService(CasdoorTokenClient cli)
        {
            _client = cli._tokenClient ?? throw new ArgumentNullException(nameof(_client));
            _options = cli._options ?? throw new ArgumentNullException(nameof(_options));
            _authCodeReq = new TokenRequest
            {
                Address = $"{_options.TokenEndpoint}",
                ClientId = _config?.clientId,
                ClientSecret = _config?.clientSecret,
            };
        }

        public Task<TokenResponse> getOauthToken(string code, string state = "")
        {
            using (var _httpClient = new HttpClient())
            using (AuthorizationCodeTokenRequest req1 = (_authCodeReq.Clone()) as AuthorizationCodeTokenRequest)
            {
                try
                {
                    req1.Code = code;
                    return _client.RequestAuthorizationCodeTokenAsync(req1.Code, req1.RedirectUri);
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
                var parsed = handler.ReadJwtToken(token.Trim());
                X509Certificate cert = new X509Certificate2(Encoding.UTF8.GetBytes(_config?.jwtPublicKey));
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(cert.GetPublicKey())
                }, out SecurityToken tem);

                var claims = new List<KeyValuePair<string, object>>();
                foreach (var item in parsed.Claims)
                {
                    claims.Add(new KeyValuePair<string, object>(item.ValueType, item.Value));
                }
                return CastUtil.classify(claims, new CasdoorUser());
            }
            catch (Exception exp)
            {
                throw new CasdoorAuthException(exp.Message);
            }
        }

        public string getSignInUrl(string redirectUrl)
        {
            string scope = "read";
            string state = _config?.applicationName ?? throw new ArgumentNullException(nameof(_config.applicationName));
            try
            {
                return $"{_options.AuthorizeEndpoint}?client_id={_config?.clientId}&response_type=code" +
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
                ? $"{_config?.endPoint}/signup/{_config?.applicationName}"
                : getSignUpUrl(redirectUrl).Replace("/login/oauth/authorize", "/signup/oauth/authorize");
        }

        public string getUserProfileUrl(string userName, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return $"{_config?.endPoint}/users/{_config?.orginazationName}/{userName}{string.Concat("?access_token=", accessToken.Trim())}";
        }

        public string getMyProfileUrl(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return $"{_config?.endPoint}/account{string.Concat("?access_token=", accessToken.Trim())}";
        }
    }
}
