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

using System.Net.Http.Json;

namespace Casdoor.Client;

public class CasdoorApiClient
{
    private readonly HttpClient _httpClient;

    public CasdoorApiClient(HttpClient httpClient, CasdoorOptions options)
    {
        _httpClient = httpClient;
        _httpClient.SetCasdoorAuthentication(options);
    }

    public Task<TValue?> GetFromJsonAsync<TValue>(string? requestUri, CancellationToken cancellationToken = default)
        => _httpClient.GetFromJsonAsync<TValue>(requestUri, cancellationToken);

    public async Task<CasdoorResponse?> PostAsJsonAsync<TValue>(string? requestUri, TValue value, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage resp = await _httpClient.PostAsJsonAsync(requestUri, value, cancellationToken);
        return await resp.ToCasdoorResponse();
    }

    internal Task<CasdoorResponse?> PostFileAsync(string? requestUri, StreamContent postStream)
        => _httpClient.PostFileAsync(requestUri, postStream);
}
