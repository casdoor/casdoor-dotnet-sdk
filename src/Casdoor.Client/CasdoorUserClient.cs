using System.Text;
using System.Text.Json;
using Casdoor.Client.Abstractions;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;

namespace Casdoor.Client;

public class CasdoorUserClient : ICasdoorUserClient
{
    private readonly CasdoorClientOptions _options;
    private readonly CasdoorHttpClient _httpClient;

    public CasdoorUserClient(CasdoorClientOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient = new CasdoorHttpClient(_options);
    }


    public virtual async Task<IEnumerable<CasdoorUser>> GetUsersAsync()
    {
        var queryMap = new List<KeyValuePair<string, string>> {new("owner", _options.OrganizationName)};

        string url = _httpClient.GetUrl("get-users", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        return (await JsonSerializer.DeserializeAsync<List<CasdoorUser>>(ms))!;
    }

    public virtual async Task<IEnumerable<CasdoorUser>> GetSortedUsersAsync(string sorter, int limit)
    {
        var queryMap = new List<KeyValuePair<string, string>>()
        {
            new("owner", _options.OrganizationName), new("sorter", sorter), new("limit", limit.ToString())
        };

        string url = _httpClient.GetUrl("get-sorted-users", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        return (await JsonSerializer.DeserializeAsync<CasdoorUser[]>(ms))!;
    }

    public virtual async Task<CasdoorUser> GetUserAsync(string name)
    {
        var queryMap =
            new List<KeyValuePair<string, string>> {new("id", string.Concat(_options.OrganizationName, "/", name))};

        string url = _httpClient.GetUrl("get-user", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        return (await JsonSerializer.DeserializeAsync<CasdoorUser>(ms))!;
    }

    public virtual async Task<CasdoorUser> GetUserByEmailAsync(string email)
    {
        var queryMap =
            new List<KeyValuePair<string, string>> {new("owner", _options.OrganizationName), new("email", email)};

        string url = _httpClient.GetUrl("get-user", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        return (await JsonSerializer.DeserializeAsync<CasdoorUser>(ms))!;
    }

    public virtual async Task<bool> AddUserAsync(CasdoorUser user)
    {
        var resp = await _httpClient.ModifyUserAsync("add-user", user, null);
        return resp.Data is "Affected";
    }

    public virtual async Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
    {
        var resp = await _httpClient.ModifyUserAsync("update-user", user, propertyNames);
        return resp.Data is "Affected";
    }

    public virtual async Task<bool> DeleteUserAsync(string name)
    {
        var user = await GetUserAsync(name);
        var resp = await _httpClient.ModifyUserAsync("delete-user", user, null);
        return resp.Data is "Affected";
    }

    public virtual async Task<bool> CheckUserPasswordAsync(string name)
    {
        var user = await GetUserAsync(name);
        CasdoorResponse resp =
            await _httpClient.ModifyUserAsync("check-user-password", user, null);
        return resp.Status is "ok";
    }


    // FIXME: what are `createdTime` and `description` for?
    public virtual async Task<CasdoorUserResource> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream,
        string createdTime = "", string description = "")
    {
        var queryMap = new List<KeyValuePair<string, string>>
        {
            new("owner", _options.OrganizationName),
            new("user", user),
            new("application", _options.ApplicationName),
            new("tag", tag),
            new("parent", parent),
            new("fullFilePath", fullFilePath)
        };

        var resp = await _httpClient.DoPostFileAsync(
            "upload-resource", queryMap, new StreamContent(fileStream));

        if (resp.Status is not "ok")
        {
            throw new CasdoorApiException(resp.Msg);
        }

        return new CasdoorUserResource(
            (string)(resp.Data2 ?? string.Empty),
            (string)(resp.Data ?? string.Empty));
    }

    public virtual async Task<CasdoorResponse> DeleteResourceAsync(string name)
    {
        CasdoorUserResource resource = new(_options.OrganizationName, name);
        string resStr = JsonSerializer.Serialize(resource);

        return await _httpClient.DoPostStringAsync("delete-resource",
            null, resStr);
    }


    public virtual async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new CasdoorSmsForm(string.Concat("admin/", _options.OrganizationName), content, receivers);
        string smsFormStr = JsonSerializer.Serialize(form);

        CasdoorResponse resp = await _httpClient.DoPostStringAsync("send-sms", null, smsFormStr);
        if (resp.Status is not "ok")
        {
            throw new CasdoorApiException(resp.Msg);
        }
    }

    public virtual async Task SendEmailAsync(string title, string content, string sender, string[] receivers)
    {
        CasdoorEmailForm casdoorEmailForm = new(title, content, sender, receivers);
        string emailFormStr = JsonSerializer.Serialize(casdoorEmailForm);

        CasdoorResponse resp = await _httpClient.DoPostStringAsync("send-email", null, emailFormStr);
        if (resp.Status is not "ok")
        {
            throw new CasdoorApiException(resp.Msg);
        }
    }
}
