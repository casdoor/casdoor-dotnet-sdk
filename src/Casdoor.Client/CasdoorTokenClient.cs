using IdentityModel.Client;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    public readonly TokenClient _tokenClient;
    public readonly CasdoorClientOptions _options;

    public CasdoorTokenClient(TokenClient tokenClient, CasdoorClientOptions options)
    {
        _tokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public virtual Task<TokenResponse> GetTokenAsync(string code) => throw new NotImplementedException();
}
