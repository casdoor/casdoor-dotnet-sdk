using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Casdoor.Client;

public class CasdoorUserClient : ICasdoorUserClient
{
    private readonly CasdoorClientOptions _options;

    public string _clientId
    {
        get => _options.ClientId;
    }
    public string _clientSecret
    {
        get => _options.ClientSecret;
    }
    public string _endpoint
    {
        get => _options.Endpoint;
    }

    public CasdoorUserClient(CasdoorClientOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // TODO: unit test
    public virtual async Task<IEnumerable<CasdoorUser>> GetUsersAsync()
    {
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}};

        string url = Util.GetUrl("get-users", queryMap);
        try
        {
            string str = await Base.DoGetStringAsync(this, url);
            HashSet<CasdoorUser>? users = JsonConvert.DeserializeObject<HashSet<CasdoorUser>>(str);
            return users ?? new HashSet<CasdoorUser>();
        }
        catch (Exception ignored)
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

        string url = Util.GetUrl("get-sorted-users", queryMap);
        try
        {
            string str = await Base.DoGetStringAsync(this, url);
            CasdoorUser[]? users = JsonConvert.DeserializeObject<CasdoorUser[]>(str);
            return users ?? Array.Empty<CasdoorUser>();
        }
        catch (Exception ignored)
        {
            return Array.Empty<CasdoorUser>();
        }
    }

    public virtual async Task<CasdoorUser> GetUserAsync(string name)
    {
        Dictionary<string, string> queryMap = new() {{"id", string.Concat(_options.OrganizationName, "/", name)}};

        string url = Util.GetUrl("get-user", queryMap);
        try
        {
            string str = await Base.DoGetStringAsync(this, url);
            CasdoorUser? users = JsonConvert.DeserializeObject<CasdoorUser>(str);
            return users ?? new CasdoorUser(); // TODO: or return null?
        }
        catch (Exception ignored)
        {
            return new CasdoorUser();
        }
    }

    public virtual async Task<CasdoorUser> GetUserByEmailAsync(string email)
    {
        Dictionary<string, string> queryMap = new() {{"owner", _options.OrganizationName}, {"email", email},};

        string url = Util.GetUrl("get-user", queryMap);
        try
        {
            string str = await Base.DoGetStringAsync(this, url);
            CasdoorUser? users = JsonConvert.DeserializeObject<CasdoorUser>(str);
            return users ?? new CasdoorUser();
        }
        catch (Exception ignored)
        {
            return new CasdoorUser();
        }
    }

    public virtual async Task<bool> AddUserAsync(CasdoorUser user)
    {
        (_, bool ok) = await Base.ModifyUserAsync("add-user", ref user, null);
        return ok;
    }

    public virtual async Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames)
    {
        (_, bool ok) = await Base.ModifyUserAsync("update-user", ref user, propertyNames);
        return ok;
    }

    public virtual async Task<bool> DeleteUserAsync(string name)
    {
        var user = await GetUserAsync(name);
        (_, bool ok) = await Base.ModifyUserAsync("delete-user", ref user, null);
        return ok;
    }

    public virtual Task<UserResource> UploadResourceAsync(
        string user, string tag, string parent,
        string fullFilePath, Stream fileStream,
        string createdTime = "", string description = "")
    {
        return Task.FromResult(new UserResource());
    }

    public virtual async Task<bool> CheckUserPasswordAsync(string name)
    {
        var user = await GetUserAsync(name);
        (Response resp, _) = await Base.ModifyUserAsync("check-user-password", ref user, null);
        return "ok".Equals(resp.Status);
    }

    private class SmsForm
    {
        public string content;
        public string[] receivers;

        public SmsForm(string content, string[] receivers)
        {
            this.content = content;
            this.receivers = receivers;
        }
    }

    public virtual async Task SendSmsAsync(string content, params string[] receivers)
    {
        var form = new SmsForm(content, receivers);
        byte[] postBytes = JsonSerializer.SerializeToUtf8Bytes(form);

        Response resp = await Base.DoPostAsync("send-sms", null, postBytes, false);
        if (!"ok".Equals(resp.Status))
        {
            throw new Exception(resp.Msg);
        }
    }
}
