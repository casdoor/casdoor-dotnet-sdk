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
    public virtual Task<CasdoorResponse<bool>> EnforceAsync(CasdoorEnforceData enforceData, string permissionId, CancellationToken cancellationToken = default) =>
        DoEnforceAsync<bool>("enforce", enforceData.ToJsonArray(), permissionId, cancellationToken);

    public virtual Task<CasdoorResponse<bool>> BatchEnforceAsync(IEnumerable<CasdoorEnforceData> enforceData, string permissionId, CancellationToken cancellationToken = default) =>
        DoEnforceAsync<bool>("batch-enforce", enforceData.ToJsonArray(), permissionId, cancellationToken);

    public virtual Task<CasdoorResponse<string>> GetAllObjectsAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync<string>("get-all-objects", cancellationToken);

    public virtual Task<CasdoorResponse<string>> GetAllActionsAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync<string>("get-all-actions", cancellationToken);

    public virtual Task<CasdoorResponse<string>> GetAllRolesAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync<string>("get-all-roles", cancellationToken);

    private async Task<CasdoorResponse<T>> DoEnforceAsync<T>(string url, string data, string permissionId,
        CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("permissionId", permissionId).QueryMap;
        var response = await SendRequestAsync(HttpMethod.Post,
            new Uri(_options.GetActionUrl(url, queryMap)),
            new StringContent(
                data,
                Encoding.UTF8,
                "application/json"),
            cancellationToken);
        string responseContent = await response.Content.ReadAsStringAsync(); // netstandard2.0 does not support cancellationToken

        if (!response.IsSuccessStatusCode)
        {
            return new CasdoorResponse<T> { Status = response.StatusCode.ToString(), Msg = responseContent };
        }

        var deserializedResponse = new CasdoorResponse<T>();
        deserializedResponse.DeserializeFromJson(responseContent);
        return deserializedResponse;
    }

    private async Task<CasdoorResponse<T>> GetAllAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        var response = await SendRequestAsync(HttpMethod.Get, new Uri(_options.GetActionUrl(url)),
            cancellationToken: cancellationToken);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new CasdoorResponse<T> { Status = response.StatusCode.ToString(), Msg = responseContent };
        }

        var deserializedResponse = new CasdoorResponse<T>();
        deserializedResponse.DeserializeFromJson(responseContent);
        return deserializedResponse;
    }

    private Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, Uri uri, HttpContent? content = default,
        CancellationToken cancellationToken = default)
    {
        var request = new HttpRequestMessage
        {
            Method = method,
            RequestUri = uri,
            Content = content
        };
        request.SetBasicAuthentication(_options.ClientId, _options.ClientSecret);

        return _httpClient.SendAsync(request, cancellationToken);
    }
}
