using Casdoor.Client.Entity;

namespace Casdoor.Client.Abstractions;

public interface ICasdoorUserClient
{
    public Task<IEnumerable<CasdoorUser>> GetUsersAsync();
    public Task<IEnumerable<CasdoorUser>> GetSortedUsersAsync(string sorter, int limit);
    public Task<CasdoorUser> GetUserAsync(string name);
    public Task<CasdoorUser> GetUserByEmailAsync(string email);
    public Task<bool> AddUserAsync(CasdoorUser user);
    public Task<bool> UpdateUserAsync(CasdoorUser user, params string[] propertyNames);
    public Task<bool> DeleteUserAsync(string name);
    public Task<bool> CheckUserPasswordAsync(string name);

    public Task<CasdoorUserResource> UploadResourceAsync(string user, string tag, string parent, string fullFilePath, Stream fileStream,
        string createdTime = "", string description = "");
    public Task<CasdoorResponse> DeleteResourceAsync(string name);

    public Task SendSmsAsync(string content, params string[] receivers);
    public Task SendEmailAsync(string title, string content, string sender, string[] receivers);
}
