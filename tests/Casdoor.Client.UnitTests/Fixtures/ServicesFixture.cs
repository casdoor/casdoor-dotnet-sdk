// Copyright 2022 The Casdoor Authors. All Rights Reserved.
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

using Casdoor.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Casdoor.Client.UnitTests.Fixtures;

public class ServicesFixture
{
    public ServicesFixture()
    {
        ServiceProvider = new ServiceCollection()
            .AddCasdoorClient(options =>
            {
                options.Endpoint = "http://localhost:8000";
                options.OrganizationName = "app-built-in";
                options.ApplicationName = "app-built-in";
                options.ClientId = "e47ada02bd296997a4e8";
                options.ClientSecret = "760b303f6bbdce805ba8f2b765b71e07306b8134";
                options.ApplicationType = "webapp";
            }).BuildServiceProvider();
    }

    public IServiceProvider ServiceProvider { get; set; }
}
