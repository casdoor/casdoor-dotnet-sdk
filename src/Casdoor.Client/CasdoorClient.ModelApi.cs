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
    public virtual async Task<CasdoorResponse?> AddModelAsync(CasdoorModel model, CancellationToken cancellationToken = default) =>
        await PostAsJsonAsync(_options.GetActionUrl("add-model"), model, cancellationToken);

    public virtual async Task<CasdoorResponse?> UpdateModelAsync(CasdoorModel model, string modelId,
        CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("update-model", new QueryMapBuilder().Add("id", modelId).QueryMap);
        return await PostAsJsonAsync(url, model, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> DeleteModelAsync(CasdoorModel model, CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("delete-model");
        return await PostAsJsonAsync(url, model, cancellationToken);
    }

    public virtual async Task<CasdoorModel?> GetModelAsync(string name, string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", $"{owner ?? _options.OrganizationName}/{name}").QueryMap;
        string url = _options.GetActionUrl("get-model", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<CasdoorModel?>();
    }

    public virtual async Task<IEnumerable<CasdoorModel>?> GetModelsAsync(string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner ?? _options.OrganizationName).QueryMap;
        string url = _options.GetActionUrl("get-models", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorModel>?>();
    }
}
