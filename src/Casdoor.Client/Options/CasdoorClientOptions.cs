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

using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Casdoor.Client;

/// <summary>
///     CasdoorClientOptions is the core configuration.
///     The first step to use this SDK is to config an instance of CasdoorClientOptions.
/// </summary>
public class CasdoorClientOptions
{
    public string Endpoint { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string JwtPublicKey { get; set; } = string.Empty;
    public string OrganizationName { get; set; } = string.Empty;
    public string ApplicationName { get; set; } = string.Empty;
    public TokenValidationParameters? TokenValidationParameters { get; set; } = new TokenValidationParameters
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
    };

    public string ApiPath { get; set; } = "/api";
    public string LoginAuthorizePath { get; set; } = "/api/login/oauth/authorize";
    public string SignupAuthorizePath { get; set; } = "api/signup/oauth/authorize";

    public string TokenPath { get; set; } = "/api/login/oauth/access_token";
}
