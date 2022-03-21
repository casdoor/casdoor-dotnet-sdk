using Casdoor.Client;
using Casdoor.UnitTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace Casdoor.UnitTests;

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
        var tokenHandler = _servicesFixture.ServiceProvider.GetService<CasdoorJsonWebTokenTokenHandler>();
        Assert.NotNull(tokenHandler);
    }
}
