using System.Text;
using System.Text.Json;
using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;

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
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}};

        string url = _httpClient.GetUrl("get-users", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        List<CasdoorUser> users = await JsonSerializer.DeserializeAsync<List<CasdoorUser>>(ms)
                                  ?? throw new CasdoorException("unable to deserialize json");
        return users;
    }

    public virtual async Task<IEnumerable<CasdoorUser>> GetSortedUsersAsync(string sorter, int limit)
    {
        Dictionary<string, string> queryMap = new()
        {
            {"owner", _options.OrganizationName}, {"sorter", sorter}, {"limit", limit.ToString()},
        };

        string url = _httpClient.GetUrl("get-sorted-users", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        CasdoorUser[] users = await JsonSerializer.DeserializeAsync<CasdoorUser[]>(ms)
                              ?? throw new CasdoorException("unable to deserialize json");
        return users;
    }

    public virtual async Task<CasdoorUser> GetUserAsync(string name)
    {
        Dictionary<string, string> queryMap = new() {{"id", string.Concat(_options.OrganizationName, "/", name)}};

        string url = _httpClient.GetUrl("get-user", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        CasdoorUser users = await JsonSerializer.DeserializeAsync<CasdoorUser>(ms)
                            ?? throw new CasdoorException("unable to deserialize json");
        return users;
    }

    public virtual async Task<CasdoorUser> GetUserByEmailAsync(string email)
    {
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}, {"email", email},};

        string url = _httpClient.GetUrl("get-user", queryMap);
        string str = await _httpClient.DoGetStringAsync(url);

        using MemoryStream ms = new(Encoding.UTF8.GetBytes(str));
        CasdoorUser users = await JsonSerializer.DeserializeAsync<CasdoorUser>(ms)
                            ?? throw new CasdoorException("unable to deserialize json");
        return users;
    }

    public virtual async Task<bool> AddUserAsync(CasdoorUser user)
    {
        (_, bool ok) = await _httpClient.ModifyUserAsync("add-user", user, null);
        return ok;
    }

    public virtual async Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
    {
        (_, bool ok) = await _httpClient.ModifyUserAsync("update-user", user, propertyNames);
        return ok;
    }

    public virtual async Task<bool> DeleteUserAsync(string name)
    {
        var user = await GetUserAsync(name);
        (_, bool ok) = await _httpClient.ModifyUserAsync("delete-user", user, null);
        return ok;
    }

    // TODO: what are `createdTime` and `description` for?
    public async Task<CasdoorUserResource> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream,
        string createdTime = "", string description = "")
    {
        var queryMap = new Dictionary<string, string>
        {
            {"owner", _options.OrganizationName},
            {"user", user},
            {"application", _options.ApplicationName},
            {"tag", tag},
            {"parent", parent},
            {"fullFilePath", fullFilePath},
        };

        using MemoryStream ms = new();
        await fileStream.CopyToAsync(ms);
        byte[] fileBytes = ms.ToArray();

        var resp = await _httpClient.DoPostAsync(
            "upload-resource", queryMap, fileBytes, true);

        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }

        // FIXME: Data2 ?= user
        return new CasdoorUserResource(
            (string)(resp.Data2 ?? string.Empty),
            (string)(resp.Data ?? string.Empty));
    }

    public virtual async Task<bool> CheckUserPasswordAsync(string name)
    {
        var user = await GetUserAsync(name);
        (CasdoorResponse resp, _) =
            await _httpClient.ModifyUserAsync("check-user-password", user, null);
        return "ok".Equals(resp.Status);
    }

    public async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new CasdoorSmsForm(string.Concat("admin/", _options.OrganizationName), content, receivers);
        byte[] postBytes = JsonSerializer.SerializeToUtf8Bytes(form);

        CasdoorResponse resp = await _httpClient.DoPostAsync("send-sms", null, postBytes, false);
        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }
    }
}
