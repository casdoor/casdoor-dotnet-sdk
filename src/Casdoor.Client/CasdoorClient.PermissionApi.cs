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
    public virtual Task<IEnumerable<CasdoorPermission>?> GetPermissionsAsync(CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", _options.OrganizationName).QueryMap;
        string url = _options.GetActionUrl("get-permissions", queryMap);
        return GetFromJsonAsync<IEnumerable<CasdoorPermission>>(url, cancellationToken);
    }

    public virtual Task<CasdoorResponse<IEnumerable<CasdoorPermission>>?> GetPermissionsByRoleAsync(string name, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", $"{_options.OrganizationName}/{name}").QueryMap;
        string url = _options.GetActionUrl("get-permissions-by-role", queryMap);
        return GetFromJsonAsync<CasdoorResponse<IEnumerable<CasdoorPermission>>>(url, cancellationToken);
    }
}
