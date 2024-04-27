﻿// Copyright 2022 The Casdoor Authors. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Casdoor.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            return applicationType switch
            {
                CasdoorDefaults.WebAppApplicationType => builder.AddCasdoorWebApp(o => { }),
                CasdoorDefaults.WebApiApplicationType => builder.AddCasdoorWebApi(o => { }),
                _ => throw new ArgumentOutOfRangeException(nameof(applicationType))
            };
        }

        public static AuthenticationBuilder AddIdentity(this AuthenticationBuilder builder, string applicationType, Action<CasdoorOptions> optionAction)
        {
            builder.Services.AddScoped<UserManager<CasdoorUser>>();
            builder.Services.AddScoped<IUserStore<CasdoorUser>, CasdoorIdentityUserStore>();
            builder.Services.AddScoped<CasdoorClient>();
            return builder;
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
                    options.CallbackPath = casdoorOptions.CallbackPath;
                    options.ClientId = casdoorOptions.ClientId;
                    options.ClientSecret = casdoorOptions.ClientSecret;
                    options.RequireHttpsMetadata = casdoorOptions.RequireHttpsMetadata;
                    options.ResponseMode = OpenIdConnectResponseMode.Query;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                });
            builder.AddOpenIdConnect(CasdoorDefaults.AuthenticationScheme, CasdoorDefaults.AuthenticationScheme, openIdOptionAction);
            return builder;
        }

        public static AuthenticationBuilder AddCasdoorWebApi(this AuthenticationBuilder builder, Action<JwtBearerOptions> jwtBearerOptionAction)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme).Configure<CasdoorOptions>(
                (options, casdoorOptions) =>
                {
                    options.Authority = casdoorOptions.Protocols.Authority;
                    options.Audience = casdoorOptions.Protocols.Audience;
                    options.RequireHttpsMetadata = casdoorOptions.RequireHttpsMetadata;
                }
            );
            builder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, jwtBearerOptionAction);
            return builder;
        }
    }
}
