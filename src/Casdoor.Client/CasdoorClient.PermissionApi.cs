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
    public virtual async Task<CasdoorResponse?> AddPermissionAsync(CasdoorPermission permission,
        CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("add-permission");
        return await PostAsJsonAsync(url, permission, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> UpdatePermissionAsync(CasdoorPermission permission, string permissionId,
        CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("update-permission", new QueryMapBuilder().Add("id", permissionId).QueryMap);
        return await PostAsJsonAsync(url, permission, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> DeletePermissionAsync(CasdoorPermission permission, CancellationToken cancellationToken = default)
    {
        string url = _options.GetActionUrl("delete-permission");
        return await PostAsJsonAsync(url, permission, cancellationToken);
    }

     public virtual async Task<IEnumerable<CasdoorPermission>?> GetPermissionsAsync(string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner ?? _options.OrganizationName).QueryMap;
        string url = _options.GetActionUrl("get-permissions", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorPermission>?>();
    }

    public virtual async Task<IEnumerable<CasdoorPermission>?> GetPermissionsByRoleAsync(string name, string? owner = null, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", $"{owner ?? _options.OrganizationName}/{name}").QueryMap;
        string url = _options.GetActionUrl("get-permissions-by-role", queryMap);
        var result = await _httpClient.GetFromJsonAsync<CasdoorResponse?>(url, cancellationToken: cancellationToken);
        return result.DeserializeData<IEnumerable<CasdoorPermission>?>();
    }
}
