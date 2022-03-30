using Casdoor.Client.UnitTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Casdoor.Client.UnitTests.ApiClientTests;

public class DependencyInjectionTest : IClassFixture<ServicesFixture>
{
    private readonly ServicesFixture _servicesFixture;

    public DependencyInjectionTest(ServicesFixture servicesFixture)
    {
        _servicesFixture = servicesFixture;
    }

    [Fact]
    public void ShouldGetClient()
    {
        var userClient = _servicesFixture.ServiceProvider.GetService<ICasdoorClient>();
        Assert.NotNull(userClient);
    }
}
