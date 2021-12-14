using IdentityModel.Client;

namespace Casdoor.Client;

public class CasdoorTokenClient : ICasdoorTokenClient
{
    private readonly TokenClient _tokenClient;
    private readonly CasdoorClientOptions _options;

    public CasdoorTokenClient(TokenClient tokenClient, CasdoorClientOptions options)
    {
        _tokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public Task<TokenResponse> GetTokenAsync()
    {
        throw new NotImplementedException();
    }
}
