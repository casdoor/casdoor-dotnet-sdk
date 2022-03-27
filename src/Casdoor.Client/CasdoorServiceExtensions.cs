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

using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Casdoor.Client;

public static class CasdoorServiceExtensions
{
    public static IServiceCollection AddCasdoorClient(this IServiceCollection services)
        => services.AddCasdoorClient(o => { });

    public static IServiceCollection AddCasdoorClient(this IServiceCollection services, Action<CasdoorOptions> optionAction)
    {
        CasdoorOptions clientOptions = new();
        optionAction(clientOptions);
        services.Configure(optionAction);
        services.Configure<TokenClientOptions>(options =>
        {
            options.Address = clientOptions.Endpoint;
            options.ClientId = clientOptions.ClientId;
            options.ClientSecret = clientOptions.ClientSecret;
        });
        services.TryAddTransient(p => p.GetRequiredService<IOptions<TokenClientOptions>>().Value);
        services.TryAddTransient(p => p.GetRequiredService<IOptions<CasdoorOptions>>().Value);
        services.AddHttpClient<TokenClient>();
        services.AddHttpClient<CasdoorApiClient>();
        services.TryAddSingleton<JsonWebTokenHandler>();
        services.TryAddScoped<ICasdoorTokenClient, CasdoorTokenClient>();
        services.TryAddScoped<ICasdoorUserClient, CasdoorUserClient>();
        return services;
    }
}
