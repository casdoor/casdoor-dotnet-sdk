using Casdoor.Client.Config;
using Casdoor.Client.Entity;
using Casdoor.Client.Exception;
using Casdoor.Client.Util;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Casdoor.Client;

public class CasdoorUserClient : ICasdoorUserClient
{
    private readonly CasdoorClientOptions _options;

    public CasdoorUserClient(CasdoorClientOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // TODO: unit test
    public virtual async Task<IEnumerable<CasdoorUser>> GetUsersAsync()
    {
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}};

        string url = CasdoorHttpClient.GetUrl(_options, "get-users", queryMap);
        try
        {
            string str = await CasdoorHttpClient.DoGetStringAsync(_options, url);
            HashSet<CasdoorUser>? users = JsonConvert.DeserializeObject<HashSet<CasdoorUser>>(str);
            return users ?? new HashSet<CasdoorUser>();
        }
        catch (System.Exception ignored)
        {
            return new HashSet<CasdoorUser>();
        }
    }

    public virtual async Task<IEnumerable<CasdoorUser>> GetSortedUsersAsync(string sorter, int limit)
    {
        Dictionary<string, string> queryMap = new()
        {
            {"owner", _options.OrganizationName}, {"sorter", sorter}, {"limit", limit.ToString()},
        };

        string url = CasdoorHttpClient.GetUrl(_options, "get-sorted-users", queryMap);
        try
        {
            string str = await CasdoorHttpClient.DoGetStringAsync(_options, url);
            CasdoorUser[]? users = JsonConvert.DeserializeObject<CasdoorUser[]>(str);
            return users ?? Array.Empty<CasdoorUser>();
        }
        catch (System.Exception ignored)
        {
            return Array.Empty<CasdoorUser>();
        }
    }

    public virtual async Task<CasdoorUser> GetUserAsync(string name)
    {
        Dictionary<string, string> queryMap = new() {{"id", string.Concat(_options.OrganizationName, "/", name)}};

        string url = CasdoorHttpClient.GetUrl(_options, "get-user", queryMap);
        try
        {
            string str = await CasdoorHttpClient.DoGetStringAsync(_options, url);
            CasdoorUser? users = JsonConvert.DeserializeObject<CasdoorUser>(str);
            return users ?? new CasdoorUser(); // TODO: or return null?
        }
        catch (System.Exception ignored)
        {
            return new CasdoorUser();
        }
    }

    public virtual async Task<CasdoorUser> GetUserByEmailAsync(string email)
    {
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}, {"email", email},};

        string url = CasdoorHttpClient.GetUrl(_options, "get-user", queryMap);
        try
        {
            string str = await CasdoorHttpClient.DoGetStringAsync(_options, url);
            CasdoorUser? users = JsonConvert.DeserializeObject<CasdoorUser>(str);
            return users ?? new CasdoorUser();
        }
        catch (System.Exception ignored)
        {
            return new CasdoorUser();
        }
    }

    public virtual async Task<bool> AddUserAsync(CasdoorUser user)
    {
        (_, bool ok) = await CasdoorHttpClient.ModifyUserAsync(_options, "add-user", user, null);
        return ok;
    }

    public virtual async Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
    {
        (_, bool ok) = await CasdoorHttpClient.ModifyUserAsync(_options, "update-user", user, propertyNames);
        return ok;
    }

    public virtual async Task<bool> DeleteUserAsync(string name)
    {
        var user = await GetUserAsync(name);
        (_, bool ok) = await CasdoorHttpClient.ModifyUserAsync(_options, "delete-user", user, null);
        return ok;
    }

    // TODO: what are `createdTime` and `description` for
    public Task<CasdoorUserResource> UploadResourceAsync(
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
        fileStream.CopyTo(ms);
        byte[] fileBytes = ms.ToArray();

        var respT = CasdoorHttpClient.DoPostAsync(_options,
            "upload-resource", queryMap, fileBytes, true);

        return new Task<CasdoorUserResource>(() =>
        {
            var resp = respT.GetAwaiter().GetResult();
            if (!"ok".Equals(resp.Status))
            {
                throw new CasdoorException(resp.Msg);
            }

            // FIXME: Data2 ?= user
            return new CasdoorUserResource(
                (string)(resp.Data2 ?? string.Empty),
                (string)(resp.Data ?? string.Empty));
        });
    }

    public virtual async Task<bool> CheckUserPasswordAsync(string name)
    {
        var user = await GetUserAsync(name);
        (CasdoorResponse resp, _) =
            await CasdoorHttpClient.ModifyUserAsync(_options, "check-user-password", user, null);
        return "ok".Equals(resp.Status);
    }

    public async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new CasdoorSmsForm(string.Concat("admin/", _options.OrganizationName), content, receivers);
        byte[] postBytes = JsonSerializer.SerializeToUtf8Bytes(form);


        CasdoorResponse resp = await CasdoorHttpClient.DoPostAsync(_options, "send-sms", null, postBytes, false);
        if (!"ok".Equals(resp.Status))
        {
            throw new CasdoorException(resp.Msg);
        }
    }
}
