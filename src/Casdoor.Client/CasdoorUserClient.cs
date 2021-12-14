using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Casdoor.Client;

public class CasdoorUserClient : ICasdoorUserClient
{
    private readonly CasdoorClientOptions _options;
    public CasdoorUserClient(CasdoorClientOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public virtual Task<IEnumerable<CasdoorUser>> GetUsersAsync() => throw new NotImplementedException();

    public virtual Task<IEnumerable<CasdoorUser>> GetSortedUsersAsync(string sorter, int limit) => throw new NotImplementedException();

    public virtual Task<CasdoorUser> GetUserAsync(string name) => throw new NotImplementedException();

    public virtual Task<CasdoorUser> GetUserByEmailAsync(string email) => throw new NotImplementedException();

    public virtual Task<bool> AddUserAsync(CasdoorUser user) => throw new NotImplementedException();

    public virtual Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames) => throw new NotImplementedException();

    public virtual Task<bool> DeleteUserAsync(string name) => throw new NotImplementedException();

    public virtual Task<UserResource> UploadResourceAsync(string user, string tag, string parent, string fullFilePath, Stream fileStream,
        string createdTime = "", string description = "")
    {
        return Task.FromResult(new UserResource());
    }

    public virtual Task<bool> CheckUserPasswordAsync(string name) => throw new NotImplementedException();

    public virtual Task SendSmsAsync(string content, params string[] receivers) => throw new NotImplementedException();
}
