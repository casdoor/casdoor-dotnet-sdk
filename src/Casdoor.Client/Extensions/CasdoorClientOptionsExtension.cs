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

using System.Text;
using System.Web;

namespace Casdoor.Client;

public static class CasdoorClientOptionsExtension
{
    public static string GetSigninUrl(this CasdoorClientOptions options, string redirectUrl)
    {
        const string scope = "read";
        string state = options.ApplicationName;
        string urlEncodeRedirectUrl = HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8);
        return
            $"{options.LoginAuthorizePath}?client_id={options.ClientId}&response_type=code&redirect_uri={urlEncodeRedirectUrl}&scope={scope}&state={state}";
    }

    public static string GetActionUrl(this CasdoorClientOptions options, string action,
        List<KeyValuePair<string, string>>? queryMap = null)
    {
        StringBuilder queryBuilder = new();
        if (queryMap is not null)
        {
            foreach (KeyValuePair<string, string> q in queryMap)
            {
                queryBuilder.Append($"{q.Key}={q.Value}&");
            }
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1); // remove the last (redundant) `&`
        string query = queryBuilder.ToString();

        return $"{options.Endpoint}/api/{action}?{query}";
    }

    public static string GetSignupUrl(this CasdoorClientOptions options) => options.GetSignupUrl("", true);

    private static string GetSignupUrl(this CasdoorClientOptions options, string redirectUrl,
        bool enablePassword = false)
    {
        if (enablePassword)
        {
            return $"{options.Endpoint}/signup/{options.ApplicationName}";
        }

        const string scope = "read";
        string state = options.ApplicationName;
        string urlEncodeRedirectUrl = HttpUtility.UrlEncode(redirectUrl, Encoding.UTF8);
        return
            $"{options.SignupAuthorizePath}?client_id={options.ClientId}&response_type=code&redirect_uri={urlEncodeRedirectUrl}&scope={scope}&state={state}";
    }

    public static string GetUserProfileUrl(this CasdoorClientOptions options, string username, string accessToken = "")
    {
        string param = "";
        if (string.IsNullOrWhiteSpace(accessToken) is false)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{options.Endpoint}/users/{options.OrganizationName}/{username}{param}";
    }

    public static string GetMyProfileUrl(this CasdoorClientOptions options, string accessToken = "")
    {
        string param = "";
        if (string.IsNullOrWhiteSpace(accessToken) is false)
        {
            param = "?access_token=" + accessToken;
        }

        return $"{options.Endpoint}/account{param}";
    }
}
