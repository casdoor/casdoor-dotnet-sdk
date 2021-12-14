using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Casdoor.Client;

public static class ServiceExtensions
{
    public static IServiceCollection AddCasdoorClient(this IServiceCollection services,
        Action<CasdoorClientOptions> optionAction)
    {
        var casdoorClientOptions = new CasdoorClientOptions();
        optionAction(casdoorClientOptions);
        services.Configure(optionAction);
        services.Configure<TokenClientOptions>(options =>
        {
            options.Address = casdoorClientOptions.TokenEndpoint;
            options.ClientId = casdoorClientOptions.ClientId;
            options.ClientSecret = casdoorClientOptions.ClientSecret;
        });
        services.AddTransient(p => p.GetRequiredService<IOptions<TokenClientOptions>>().Value);
        services.AddHttpClient<TokenClient>();
        services.AddTransient(p => p.GetRequiredService<IOptions<CasdoorClientOptions>>().Value);
        // TODO: Add casdoor client
        return services;
    }
}
