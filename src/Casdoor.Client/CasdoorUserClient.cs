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

public class CasdoorUserClient : ICasdoorUserClient
{
    private readonly CasdoorApiClient _apiClient;
    private readonly CasdoorOptions _options;

    public CasdoorUserClient(CasdoorApiClient apiClient, CasdoorOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _apiClient = apiClient;
    }

    public virtual Task<IEnumerable<CasdoorUser>?> GetUsersAsync()
    {
        IEnumerable<KeyValuePair<string, string?>> queryMap =
            new KeyValuePair<string,string?>[]
            {
                new("owner", _options.OrganizationName)
            };
        string url = _options.GetActionUrl("get-users", queryMap);
        return _apiClient.GetFromJsonAsync<IEnumerable<CasdoorUser>>(url);
    }

    public virtual Task<IEnumerable<CasdoorUser>?> GetSortedUsersAsync(string sorter, int limit)
    {
        IEnumerable<KeyValuePair<string, string?>> queryMap =
            new KeyValuePair<string, string?>[]
            {
                new("owner", _options.OrganizationName),
                new("sorter", sorter), new("limit", limit.ToString())
            };
        string url = _options.GetActionUrl("get-sorted-users", queryMap);
        return _apiClient.GetFromJsonAsync<IEnumerable<CasdoorUser>>(url);
    }

    public virtual Task<CasdoorUser?> GetUserAsync(string name)
    {
        IEnumerable<KeyValuePair<string, string?>> queryMap =
            new KeyValuePair<string, string?>[]
            {
                new("id", string.Concat(_options.OrganizationName, "/", name))
            };
        string url = _options.GetActionUrl("get-user", queryMap);
        return _apiClient.GetFromJsonAsync<CasdoorUser>(url);
    }

    public virtual Task<CasdoorUser?> GetUserByEmailAsync(string email)
    {
        IEnumerable<KeyValuePair<string, string?>> queryMap =
            new KeyValuePair<string, string?>[]
            {
                new("owner", _options.OrganizationName), new("email", email)
            };
        string url = _options.GetActionUrl("get-user", queryMap);
        return _apiClient.GetFromJsonAsync<CasdoorUser>(url);
    }

    public virtual Task<CasdoorResponse?> AddUserAsync(CasdoorUser user)
        => ModifyUserAsync("add-user", user);

    public virtual Task<CasdoorResponse?> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
        => ModifyUserAsync("update-user", user, propertyNames);

    public virtual async Task<CasdoorResponse?> DeleteUserAsync(string name)
    {
        CasdoorUser? user = await GetUserAsync(name);
        if (user is null)
        {
            return null;
        }
        return await ModifyUserAsync("delete-user", user);
    }

    public virtual async Task<CasdoorResponse?> CheckUserPasswordAsync(string name)
    {
        CasdoorUser? user = await GetUserAsync(name);
        if (user is null)
        {
            return null;
        }
        return await ModifyUserAsync("check-user-password", user);
    }

    // TODO: what are `createdTime` and `description` for?
    public virtual Task<CasdoorResponse?> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream, string createdTime = "", string description = "")
    {
        IEnumerable<KeyValuePair<string, string?>> queryMap =
            new KeyValuePair<string, string?>[]
        {
            new("owner", _options.OrganizationName),
            new("user", user),
            new("application", _options.ApplicationName),
            new("tag", tag),
            new("parent", parent),
            new("fullFilePath", fullFilePath)
        };
        string url = _options.GetActionUrl("upload-resource", queryMap);
        return _apiClient.PostFileAsync(url, new StreamContent(fileStream));
    }

    public virtual Task<CasdoorResponse?> DeleteResourceAsync(string name)
    {
        CasdoorUserResource resource = new() {Owner = _options.OrganizationName, Name = name};
        return _apiClient.PostAsJsonAsync("delete-resource", resource);
    }

    public virtual Task<CasdoorResponse?> SendSmsAsync(string content, params string[] receivers)
    {
        CasdoorSmsForm form = new()
        {
            OrganizationId = string.Concat("admin/", _options.OrganizationName),
            Content = content,
            Receivers = receivers,
        };
        string url = _options.GetActionUrl("send-sms");
        return _apiClient.PostAsJsonAsync(url, form);
    }

    public virtual Task<CasdoorResponse?> SendEmailAsync(string title, string content, string sender,
        string[] receivers)
    {
        CasdoorEmailForm form = new()
        {
            Title = title, Content = content, Receivers = receivers, Sender = sender
        };
        string url = _options.GetActionUrl("send-email");
        return _apiClient.PostAsJsonAsync(url, form);
    }

    private Task<CasdoorResponse?> ModifyUserAsync(
        string action, CasdoorUser user, params string[] columns)
    {
        List<KeyValuePair<string, string?>> queryMap = new()
        {
            new KeyValuePair<string, string?>("id", $"{user.Owner}/{user.Name}")
        };
        if (columns.Length != 0)
        {
            queryMap.Add(new KeyValuePair<string, string?>("columns", string.Join(",", columns)));
        }
        user.Owner = _options.OrganizationName;
        string url = _options.GetActionUrl(action, queryMap);
        return _apiClient.PostAsJsonAsync(url, user);
    }
}
