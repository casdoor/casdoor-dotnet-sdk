using System;
using System.Diagnostics;
using Casdoor.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Casdoor.AspNetCore.Authentication
{
    public static class ServiceExtension
    {
        public static AuthenticationBuilder AddCasdoor(this AuthenticationBuilder builder,
            IConfigurationSection configurationSection)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (configurationSection is null)
            {
                throw new ArgumentNullException(nameof(configurationSection));
            }
            return builder.AddCasdoor(configurationSection.Bind);
        }

        public static AuthenticationBuilder AddCasdoor(this AuthenticationBuilder builder, Action<CasdoorOptions> optionAction)
        {
            builder.Services.AddCasdoorClient(optionAction);
            CasdoorOptions casdoorOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<CasdoorOptions>>().Value;
            return casdoorOptions.ApplicationType switch {
                CasdoorDefaults.WebAppApplicationType => builder.AddCasdoorWebApp(casdoorOptions, options => {}),
                CasdoorDefaults.WebApiApplicationType => builder.AddCasdoorWebApi(casdoorOptions, options => {}),
                _ => throw new ArgumentOutOfRangeException(nameof(casdoorOptions.ApplicationType))
            };
        }

        public static AuthenticationBuilder AddCasdoorWebApp(this AuthenticationBuilder builder,
            CasdoorOptions casdoorOptions,
            Action<OpenIdConnectOptions> openIdOptionAction)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CasdoorDefaults.AuthenticationScheme;
            });
            builder.AddOpenIdConnect(CasdoorDefaults.AuthenticationScheme, options =>
            {
                options.Authority = casdoorOptions.Protocols.Authority;
                options.CallbackPath = casdoorOptions.CallBackPath;
                options.ClientId = casdoorOptions.ClientId;
                options.ClientSecret = casdoorOptions.ClientSecret;
                options.RequireHttpsMetadata = casdoorOptions.RequireHttpsMetadata;
                options.ResponseMode = OpenIdConnectResponseMode.Query;
                options.ResponseType = OpenIdConnectResponseType.Code;
                openIdOptionAction(options);
            });
            return builder;
        }

        public static AuthenticationBuilder AddCasdoorWebApi(this AuthenticationBuilder builder,
            CasdoorOptions casdoorOptions,
            Action<OpenIdConnectOptions> openIdOptionAction)
        {
            throw new NotImplementedException("WebApi is not supported yet.");
        }
    }
}
