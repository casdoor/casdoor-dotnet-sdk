// Copyright 2023 The Casdoor Authors. All Rights Reserved.
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

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using IdentityModel.Client;

namespace Casdoor.Client;

public partial class CasdoorClient
{
    public virtual Task<CasdoorResponse<bool>> EnforceAsync(CasdoorPermissionRule permissionRule)
    {
        return DoEnforceAsync<bool>("enforce",  JsonSerializer.Serialize(permissionRule));
    }

    public virtual Task<CasdoorResponse<IEnumerable<bool>>> BatchEnforceAsync(IEnumerable<CasdoorPermissionRule> permissionRule)
    {
        return DoEnforceAsync<IEnumerable<bool>>("batch-enforce", JsonSerializer.Serialize(permissionRule));
    }

    private async Task<CasdoorResponse<T>> DoEnforceAsync<T>(string url, string data)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_options.GetActionUrl(url)),
            Content = new StringContent(
                data,
                Encoding.UTF8,
                "application/json")
        };
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        request.SetBasicAuthentication(_options.ClientId, _options.ClientSecret);

        var response = await _httpClient.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();

        var result = new CasdoorResponse<T>
        {
            Status = response.StatusCode.ToString(),
            Msg = responseContent
        };

        if (!response.IsSuccessStatusCode)
        {
            result.Msg = responseContent;
            return result;
        }

        result.Data = JsonSerializer.Deserialize<T>(responseContent);
        return result;
    }
}
