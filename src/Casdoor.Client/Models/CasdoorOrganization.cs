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

using System.Text.Json.Serialization;

namespace Casdoor.Client;

public class CasdoorOrganization
{
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("createdTime")]
    public string? CreatedTime { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("websiteUrl")]
    public string? WebsiteUrl { get; set; }

    [JsonPropertyName("favicon")]
    public string? Favicon { get; set; }

    [JsonPropertyName("passwordType")]
    public string? PasswordType { get; set; }

    [JsonPropertyName("passwordSalt")]
    public string? PasswordSalt { get; set; }

    [JsonPropertyName("phonePrefix")]
    public string? PhonePrefix { get; set; }

    [JsonPropertyName("defaultAvatar")]
    public string? DefaultAvatar { get; set; }

    [JsonPropertyName("masterPassword")]
    public string? MasterPassword { get; set; }

    [JsonPropertyName("enableSoftDeletion")]
    public bool? EnableSoftDeletion { get; set; }
}
