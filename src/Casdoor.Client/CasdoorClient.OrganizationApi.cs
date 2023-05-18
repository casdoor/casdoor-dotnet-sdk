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
    public virtual async Task<CasdoorResponse?> AddOrganizationAsync(CasdoorOrganization organization, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(organization.Owner))
        {
            organization.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("add-organization");
        return await PostAsJsonAsync(url, organization, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> DeleteOrganizationAsync(string name, CancellationToken cancellationToken = default)
    {
        var organization = new CasdoorOrganization {Owner = CasdoorConstants.DefaultCasdoorOwner, Name = name};
        var url = _options.GetActionUrl("delete-organization");
        return await PostAsJsonAsync(url, organization, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> UpdateOrganizationAsync(string id, CasdoorOrganization newOrganization, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        if (string.IsNullOrEmpty(newOrganization.Owner))
        {
            newOrganization.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        var url = _options.GetActionUrl("update-organization", queryMap);
        return await PostAsJsonAsync(url, newOrganization, cancellationToken);
    }

    public virtual Task<CasdoorOrganization?> GetOrganizationAsync(string id, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        var url = _options.GetActionUrl("get-organization", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorOrganization>(url, cancellationToken: cancellationToken);
    }

    public virtual Task<IEnumerable<CasdoorOrganization>?> GetOrganizationsAsync(string owner, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner).GetMap();
        var url = _options.GetActionUrl("get-organizations", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorOrganization>>(url, cancellationToken: cancellationToken);
    }
}
