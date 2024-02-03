// Copyright 2024 The Casdoor Authors. All Rights Reserved.
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

using System.Text.Json.Serialization;

namespace Casdoor.Client;

public class CasdoorAccount
{
    public CasdoorUser? cassdoorUser { get; set; }
    public CasdoorOrganization? casdoorOrganization {get; set;}

    public CasdoorAccount(CasdoorUser? cassdoorUser, CasdoorOrganization? casdoorOrganization)
    {
        this.cassdoorUser = cassdoorUser;
        this.casdoorOrganization = casdoorOrganization;
    }
}

public class CasdoorResetEmailOrPhoneForm
{
    [JsonPropertyName("dest")]
    public string? Dest { get; set;}

    [JsonPropertyName("type")]
    public string? Type { get; set;}

    [JsonPropertyName("code")]
    public string? Code { get; set;}
}

public class CasdoorLaravelResponse
{
    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("email_verified_at")]
    public string? EmailVerifiedAt { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }
}

public class CasdoorUserInfo
{
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("aud")]
    public string? Aud { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("email_verified")]
    public string? EmailVerified { get; set; }

    [JsonPropertyName("groups")]
    public List<string>? Groups { get; set; }

    [JsonPropertyName("iss")]
    public string? Iss { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("picture")]
    public string? Picture { get; set; }

    [JsonPropertyName("preferred_username")]
    public string? PreferredUsername { get; set; }

    [JsonPropertyName("sub")]
    public string? Sub { get; set; }
}
