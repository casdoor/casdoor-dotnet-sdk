using IdentityModel.Client;

namespace Casdoor.Client;

public interface ICasdoorTokenClient
{
    public Task<TokenResponse> GetTokenAsync();
}
