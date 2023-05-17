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
    public virtual async Task<CasdoorResponse?> AddProviderAsync(CasdoorProvider provider)
    {
        if (string.IsNullOrEmpty(provider.Owner))
        {
            provider.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("add-provider");
        return await PostAsJsonAsync(url, provider);
    }

    public virtual async Task<CasdoorResponse?> DeleteProviderAsync(string name)
    {
        var provider = new CasdoorProvider {Owner = CasdoorConstants.DefaultCasdoorOwner, Name = name};
        var url = _options.GetActionUrl("delete-provider");
        return await PostAsJsonAsync(url, provider);
    }

    public virtual async Task<CasdoorResponse?> UpdateProviderAsync(string id, CasdoorProvider newProvider)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        if (string.IsNullOrEmpty(newProvider.Owner))
        {
            newProvider.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("update-provider", queryMap);
        return await PostAsJsonAsync(url, newProvider);
    }

    public virtual Task<CasdoorProvider?> GetProviderAsync(string id)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        var url = _options.GetActionUrl("get-provider", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorProvider>(url);
    }

    public virtual Task<IEnumerable<CasdoorProvider>?> GetProvidersAsync(string owner)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner).GetMap();
        var url = _options.GetActionUrl("get-providers", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorProvider>>(url);
    }

    public virtual Task<IEnumerable<CasdoorProvider>?> GetGlobalProvidersAsync()
    {
        var url = _options.GetActionUrl("get-global-providers");
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorProvider>>(url);
    }
}