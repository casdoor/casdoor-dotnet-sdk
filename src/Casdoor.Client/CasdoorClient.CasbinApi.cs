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

using System.Net.Http.Json;

namespace Casdoor.Client;

public partial class CasdoorClient
{
    public virtual async Task<CasdoorEnforceResult?> EnforceAsync(
        CasdoorEnforceData enforceData,
        string? permissionId,
        string? modelId,
        string? resourceId,
        string? enforcerId,
        CancellationToken cancellationToken = default)
    {
        var queryMapBuilder = new QueryMapBuilder();

        if (!string.IsNullOrEmpty(permissionId))
        {
            queryMapBuilder.Add(permissionId!);
        }

        if (!string.IsNullOrEmpty(modelId))
        {
            queryMapBuilder.Add(modelId!);
        }

        if (!string.IsNullOrEmpty(resourceId))
        {
            queryMapBuilder.Add(resourceId!);
        }

        if (!string.IsNullOrEmpty(enforcerId))
        {
            queryMapBuilder.Add(enforcerId!);
        }

        var queryMap = queryMapBuilder.QueryMap;
        var response = await PostAsJsonAsync(_options.GetActionUrl("enforce", queryMap), enforceData.Data, cancellationToken);
        return new CasdoorEnforceResult { Result = response.DeserializeData<IEnumerable<bool>>() };
    }

    public virtual async Task<IEnumerable<CasdoorEnforceResult>?> BatchEnforceAsync(
        IEnumerable<CasdoorEnforceData> enforceData,
        string? permissionId,
        string? modelId,
        string? enforcerId,
        CancellationToken cancellationToken = default)
    {
        var queryMapBuilder = new QueryMapBuilder();

        if (!string.IsNullOrEmpty(permissionId))
        {
            queryMapBuilder.Add(permissionId!);
        }

        if (!string.IsNullOrEmpty(modelId))
        {
            queryMapBuilder.Add(modelId!);
        }

        if (!string.IsNullOrEmpty(enforcerId))
        {
            queryMapBuilder.Add(enforcerId!);
        }

        var queryMap = queryMapBuilder.QueryMap;
        var data = enforceData.Select(data => data.Data);
        var response = await PostAsJsonAsync(_options.GetActionUrl("batch-enforce", queryMap), data, cancellationToken);
        var results = response.DeserializeData<IEnumerable<IEnumerable<bool>>>();
        return results!.Select(result => new CasdoorEnforceResult { Result = result });
    }

    public virtual Task<IEnumerable<string>?> GetAllObjectsAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync("get-all-objects", cancellationToken);

    public virtual Task<IEnumerable<string>?> GetAllActionsAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync("get-all-actions", cancellationToken);

    public virtual Task<IEnumerable<string>?> GetAllRolesAsync(CancellationToken cancellationToken = default) =>
        GetAllAsync("get-all-roles", cancellationToken);

    private async Task<IEnumerable<string>?> GetAllAsync(string url, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(_options.GetActionUrl(url), cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<string>>();
    }
}
