using Casdoor.Client.Entity;
using IdentityModel.Client;

namespace Casdoor.Client.Abstractions;

public interface ICasdoorTokenClient
{
    public Task<TokenResponse> GetTokenAsync();
    public CasdoorUser? ParseJwtToken(string token);
}
