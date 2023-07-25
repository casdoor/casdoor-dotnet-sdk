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

public partial class CasdoorClient
{
    public virtual async Task<CasdoorResponse?> AddProviderAsync(CasdoorProvider provider, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(provider.Owner))
        {
            provider.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("add-provider");
        return await PostAsJsonAsync(url, provider, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> DeleteProviderAsync(string name, CancellationToken cancellationToken = default)
    {
        var provider = new CasdoorProvider {Owner = CasdoorConstants.DefaultCasdoorOwner, Name = name};
        var url = _options.GetActionUrl("delete-provider");
        return await PostAsJsonAsync(url, provider, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> UpdateProviderAsync(string id, CasdoorProvider newProvider, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).QueryMap;
        if (string.IsNullOrEmpty(newProvider.Owner))
        {
            newProvider.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("update-provider", queryMap);
        return await PostAsJsonAsync(url, newProvider, cancellationToken);
    }

    public virtual async Task<CasdoorProvider?> GetProviderAsync(string id, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).QueryMap;
        var url = _options.GetActionUrl("get-provider", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<CasdoorProvider?>();
    }

    public virtual async Task<IEnumerable<CasdoorProvider>?> GetProvidersAsync(string owner, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner).QueryMap;
        var url = _options.GetActionUrl("get-providers", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorProvider>?>();
    }

    public virtual async Task<IEnumerable<CasdoorProvider>?> GetGlobalProvidersAsync(CancellationToken cancellationToken = default)
    {
        var url = _options.GetActionUrl("get-global-providers");
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorProvider>?>();
    }
}
