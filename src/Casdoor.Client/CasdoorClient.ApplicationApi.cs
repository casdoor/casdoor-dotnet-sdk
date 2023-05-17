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
    public virtual async Task<CasdoorResponse?> AddApplicationAsync(CasdoorApplication application)
    {
        if (string.IsNullOrEmpty(application.Owner))
        {
            application.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        
        var url = _options.GetActionUrl("add-application");
        return await PostAsJsonAsync(url, application);
    }

    public virtual async Task<CasdoorResponse?> DeleteApplicationAsync(string name)
    {
        var application = new CasdoorApplication {Owner = CasdoorConstants.DefaultCasdoorOwner, Name = name};
        var url = _options.GetActionUrl("delete-application");
        return await PostAsJsonAsync(url, application);
    }

    public virtual async Task<CasdoorResponse?> UpdateApplicationAsync(string id, CasdoorApplication newApplication)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();

        if (string.IsNullOrEmpty(newApplication.Owner))
        {
            newApplication.Owner = CasdoorConstants.DefaultCasdoorOwner;
        }
        
        var url = _options.GetActionUrl("update-application", queryMap);
        return await PostAsJsonAsync(url, newApplication);
    }

    public virtual Task<CasdoorApplication?> GetApplicationAsync(string id)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        var url = _options.GetActionUrl("get-application", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorApplication>(url);
    }

    public virtual Task<IEnumerable<CasdoorApplication>?> GetApplicationsAsync(string owner)
    {
        var queryMap = new QueryMapBuilder().Add("owner", owner).GetMap();
        var url = _options.GetActionUrl("get-applications", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorApplication>>(url);
    }

    public virtual Task<IEnumerable<CasdoorApplication>?> GetOrganizationApplicationsAsync(string organization)
    {
        var queryMap = new QueryMapBuilder().Add("organization", organization).GetMap();
        var url = _options.GetActionUrl("get-organization-applications", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorApplication>>(url);
    }

    public virtual Task<CasdoorApplication?> GetUserApplicationAsync(string id)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        var url = _options.GetActionUrl("get-user-application", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorApplication>(url);
    }
}