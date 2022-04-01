using System;
using Casdoor.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var options = new CasdoorOptions();
            optionAction?.Invoke(options);
            return builder.AddCasdoor(options.ApplicationType, optionAction);
        }

        public static AuthenticationBuilder AddCasdoor(this AuthenticationBuilder builder, string applicationType, Action<CasdoorOptions> optionAction)
        {
            builder.Services.AddCasdoorClient(optionAction);
            return applicationType switch {
                CasdoorDefaults.WebAppApplicationType => builder.AddCasdoorWebApp(o => {}),
                CasdoorDefaults.WebApiApplicationType => builder.AddCasdoorWebApi(o => {}),
                _ => throw new ArgumentOutOfRangeException(nameof(applicationType))
            };
        }

        public static AuthenticationBuilder AddCasdoorWebApp(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> openIdOptionAction)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CasdoorDefaults.AuthenticationScheme;
            });
            builder.Services.AddOptions<OpenIdConnectOptions>(CasdoorDefaults.AuthenticationScheme).Configure<CasdoorOptions>(
                (options, casdoorOptions) =>
                {
                    options.Authority = casdoorOptions.Protocols.Authority;
                    options.CallbackPath = casdoorOptions.CallBackPath;
                    options.ClientId = casdoorOptions.ClientId;
                    options.ClientSecret = casdoorOptions.ClientSecret;
                    options.RequireHttpsMetadata = casdoorOptions.RequireHttpsMetadata;
                    options.ResponseMode = OpenIdConnectResponseMode.Query;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                });
            builder.AddOpenIdConnect(CasdoorDefaults.AuthenticationScheme, CasdoorDefaults.AuthenticationScheme, openIdOptionAction);
            return builder;
        }

        public static AuthenticationBuilder AddCasdoorWebApi(this AuthenticationBuilder builder, Action<OpenIdConnectOptions> openIdOptionAction)
        {
            throw new NotImplementedException("WebApi is not supported yet.");
        }
    }
}
