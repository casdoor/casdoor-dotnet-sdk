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
    public virtual async Task<CasdoorResponse?> AddOrganizationAsync(CasdoorOrganization organization)
    {
        if (organization.Owner is null || organization.Owner == "")
        {
            organization.Owner = "admin";
        }
        string url = _options.GetActionUrl("add-organization");
        return await PostAsJsonAsync(url, organization);
    }

    public virtual async Task<CasdoorResponse?> DeleteOrganizationAsync(string name)
    {
        CasdoorOrganization organization = new CasdoorOrganization() {Owner = "admin", Name = name};
        string url = _options.GetActionUrl("delete-organization");
        return await PostAsJsonAsync(url, organization);
    }

    public virtual async Task<CasdoorResponse?> UpdateOrganizationAsync(string id, CasdoorOrganization newOrganization)
    {
        List<KeyValuePair<string, string?>> queryMap = new()
        {
            new KeyValuePair<string, string?>("id", id)
        };
        if (newOrganization.Owner is null || newOrganization.Owner == "")
        {
            newOrganization.Owner = "admin";
        }
        string url = _options.GetActionUrl("update-organization", queryMap);
        return await PostAsJsonAsync(url, newOrganization);
    }

    public virtual Task<CasdoorOrganization?> GetOrganizationAsync(string id)
    {
        List<KeyValuePair<string, string?>> queryMap = new()
        {
            new KeyValuePair<string, string?>("id", id)
        };
        string url = _options.GetActionUrl("get-organization", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorOrganization>(url);
    }

    public virtual Task<IEnumerable<CasdoorOrganization>?> GetOrganizationsAsync(string owner)
    {
        List<KeyValuePair<string, string?>> queryMap = new()
        {
            new KeyValuePair<string, string?>("owner", owner)
        };
        string url = _options.GetActionUrl("get-organizations", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorOrganization>>(url);
    }
}
