// Copyright 2023 The Casdoor Authors. All Rights Reserved.
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

public class CasdoorLdap
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("createdTime")]
    public string? CreatedTime { get; set; }

    [JsonPropertyName("serverName")]
    public string? ServerName { get; set; }

    [JsonPropertyName("host")]
    public string? Host { get; set; }

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("enableSsl")]
    public bool? EnableSsl { get; set; }

    [JsonPropertyName("username")]
    public string? Username { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("baseDn")]
    public string? BaseDn { get; set; }

    [JsonPropertyName("filter")]
    public string? Filter { get; set; }

    [JsonPropertyName("filterFields")]
    public string[]? FilterFields { get; set; }

    [JsonPropertyName("autoSync")]
    public int AutoSync { get; set; }

    [JsonPropertyName("lastSync")]
    public string? LastSync { get; set; }
}

public class CasdoorLdapUsers
{
    [JsonPropertyName("existUuids")]
    public IEnumerable<string>? ExistUuids { get; set; }

    [JsonPropertyName("users")]
    public IEnumerable<CasdoorLdapUser>? Users { get; set; }
}

public class CasdoorLdapUser
{
    [JsonPropertyName("uidNumber")]
    public string? UidNumber { get; set; }

    [JsonPropertyName("uid")]
    public string? Uid { get; set; }

    [JsonPropertyName("cn")]
    public string? Cn { get; set; }

    [JsonPropertyName("gidNumber")]
    public string? GidNumber { get; set; }

    [JsonPropertyName("uuid")]
    public string? Uuid { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("mail")]
    public string? Mail { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("emailAddress")]
    public string? EmailAddress { get; set; }

    [JsonPropertyName("telephoneNumber")]
    public string? TelephoneNumber { get; set; }

    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }

    [JsonPropertyName("mobileTelephoneNumber")]
    public string? MobileTelephoneNumber { get; set; }

    [JsonPropertyName("registeredAddress")]
    public string? RegisteredAddress { get; set; }

    [JsonPropertyName("postalAddress")]
    public string? PostalAddress { get; set; }

    [JsonPropertyName("groupId")]
    public string? GroupId { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }
}
