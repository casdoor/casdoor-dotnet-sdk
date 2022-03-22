using Casdoor.Client;
using Casdoor.Client.UnitTests.Fixtures;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Casdoor.Client.UnitTests;

public class CasdoorTokenClientTest : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _servicesFixture;

    public CasdoorTokenClientTest(ServicesFixture servicesFixture)
    {
        _servicesFixture = servicesFixture;
    }

    [Fact]
    public void ShouldParseToken()
    {
        var tokenHandler = _servicesFixture.ServiceProvider.GetService<JsonWebTokenHandler>();
        Assert.NotNull(tokenHandler);
    }
}
