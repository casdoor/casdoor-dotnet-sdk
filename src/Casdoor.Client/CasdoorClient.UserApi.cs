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
    public virtual Task<IEnumerable<CasdoorUser>?> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("owner", _options.OrganizationName).GetMap();
        string url = _options.GetActionUrl("get-users", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorUser>>(url, cancellationToken);
    }

    public virtual Task<IEnumerable<CasdoorUser>?> GetSortedUsersAsync(string sorter, int limit, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder()
            .Add("owner", _options.OrganizationName)
            .Add("sorter", sorter)
            .Add("limit", limit.ToString())
            .GetMap();
        string url = _options.GetActionUrl("get-sorted-users", queryMap);
        return _httpClient.GetFromJsonAsync<IEnumerable<CasdoorUser>>(url, cancellationToken);
    }

    public virtual Task<CasdoorUser?> GetUserAsync(string name, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", $"{_options.OrganizationName}/{name}").GetMap();
        string url = _options.GetActionUrl("get-user", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorUser>(url, cancellationToken);
    }

    public virtual Task<CasdoorUser?> GetUseByIdrAsync(string id, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder().Add("id", id).GetMap();
        string url = _options.GetActionUrl("get-user", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorUser>(url, cancellationToken);
    }

    public virtual Task<CasdoorUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder()
            .Add("owner", _options.OrganizationName)
            .Add("email", email)
            .GetMap();
        string url = _options.GetActionUrl("get-user", queryMap);
        return _httpClient.GetFromJsonAsync<CasdoorUser>(url, cancellationToken);
    }

    public virtual Task<CasdoorResponse?> AddUserAsync(CasdoorUser user, CancellationToken cancellationToken = default)
        => ModifyUserAsync("add-user", user, cancellationToken);

    public virtual Task<CasdoorResponse?> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
        => UpdateUserAsync(user, default, propertyNames);

    public virtual Task<CasdoorResponse?> UpdateUserAsync(CasdoorUser user, CancellationToken cancellationToken,
        params string[] propertyNames)
        => ModifyUserAsync("update-user", user, cancellationToken, propertyNames);

    public virtual async Task<CasdoorResponse?> DeleteUserAsync(string name, CancellationToken cancellationToken = default)
    {
        CasdoorUser? user = await GetUserAsync(name, cancellationToken);
        if (user is null)
        {
            return null;
        }
        return await ModifyUserAsync("delete-user", user, cancellationToken);
    }

    public virtual async Task<CasdoorResponse?> CheckUserPasswordAsync(string name, CancellationToken cancellationToken = default)
    {
        CasdoorUser? user = await GetUserAsync(name, cancellationToken);
        if (user is null)
        {
            return null;
        }
        return await ModifyUserAsync("check-user-password", user, cancellationToken);
    }

    private Task<CasdoorResponse?> ModifyUserAsync(string action, CasdoorUser user, CancellationToken cancellationToken, params string[] columns)
    {
        var queryMapBuilder = new QueryMapBuilder().Add("id", $"{user.Owner}/{user.Name}");

        if (columns.Length != 0)
        {
            queryMapBuilder.Add("columns", string.Join(",", columns));
        }

        user.Owner = _options.OrganizationName;
        string url = _options.GetActionUrl(action, queryMapBuilder.GetMap());
        return PostAsJsonAsync(url, user, cancellationToken);
    }
}
