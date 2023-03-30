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

/// <summary>
///     CasdoorUser has the same definition as https://github.com/casdoor/casdoor/blob/master/object/user.go#L24
/// </summary>
public class CasdoorUser
{
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("createdTime")]
    public string? CreatedTime { get; set; }

    [JsonPropertyName("updatedTime")]
    public string? UpdatedTime { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("passwordSalt")]
    public string? PasswordSalt { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    [JsonPropertyName("permanentAvatar")]
    public string? PermanentAvatar { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [JsonPropertyName("address")]
    public IEnumerable<string>? Address { get; set; }

    [JsonPropertyName("affiliation")]
    public string? Affiliation { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("idCardType")]
    public string? IdCardType { get; set; }

    [JsonPropertyName("idCard")]
    public string? IdCard { get; set; }

    [JsonPropertyName("homePage")]
    public string? Homepage { get; set; }

    [JsonPropertyName("bio")]
    public string? Bio { get; set; }

    [JsonPropertyName("tag")]
    public string? Tag { get; set; }

    [JsonPropertyName("region")]
    public string? Region { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    [JsonPropertyName("birthday")]
    public string? Birthday { get; set; }

    [JsonPropertyName("education")]
    public string? Education { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("karma")]
    public int Karma { get; set; }

    [JsonPropertyName("ranking")]
    public int Ranking { get; set; }

    [JsonPropertyName("isDefaultAvatar")]
    public bool IsDefaultAvatar { get; set; }

    [JsonPropertyName("isOnline")]
    public bool IsOnline { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonPropertyName("isGlobalAdmin")]
    public bool IsGlobalAdmin { get; set; }

    [JsonPropertyName("isForbidden")]
    public bool IsForbidden { get; set; }

    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }

    [JsonPropertyName("signupApplication")]
    public string? SignupApplication { get; set; }

    [JsonPropertyName("hash")]
    public string? Hash { get; set; }

    [JsonPropertyName("preHash")]
    public string? PreHash { get; set; }

    [JsonPropertyName("createdIp")]
    public string? CreatedIp { get; set; }

    [JsonPropertyName("lastSigninTime")]
    public string? LastSigninTime { get; set; }

    [JsonPropertyName("lastSigninIp")]
    public string? LastSigninIp { get; set; }

    [JsonPropertyName("github")]
    public string? Github { get; set; }

    [JsonPropertyName("google")]
    public string? Google { get; set; }

    [JsonPropertyName("qq")]
    public string? QQ { get; set; }

    [JsonPropertyName("wechat")]
    public string? WeChat { get; set; }

    [JsonPropertyName("facebook")]
    public string? Facebook { get; set; }

    [JsonPropertyName("dingtalk")]
    public string? DingTalk { get; set; }

    [JsonPropertyName("weibo")]
    public string? Weibo { get; set; }

    [JsonPropertyName("gitee")]
    public string? Gitee { get; set; }

    [JsonPropertyName("linkedin")]
    public string? LinkedIn { get; set; }

    [JsonPropertyName("wecom")]
    public string? Wecom { get; set; }

    [JsonPropertyName("lark")]
    public string? Lark { get; set; }

    [JsonPropertyName("gitlab")]
    public string? Gitlab { get; set; }

    [JsonPropertyName("adfs")]
    public string? Adfs { get; set; }

    [JsonPropertyName("baidu")]
    public string? Baidu { get; set; }

    [JsonPropertyName("alipay")]
    public string? Alipay { get; set; }

    [JsonPropertyName("casdoor")]
    public string? Casdoor { get; set; }

    [JsonPropertyName("infoflow")]
    public string? Infoflow { get; set; }

    [JsonPropertyName("apple")]
    public string? Apple { get; set; }

    [JsonPropertyName("azuread")]
    public string? AzureAD { get; set; }

    [JsonPropertyName("slack")]
    public string? Slack { get; set; }

    [JsonPropertyName("steam")]
    public string? Steam { get; set; }

    [JsonPropertyName("bilibili")]
    public string? Bilibili { get; set; }

    [JsonPropertyName("okta")]
    public string? Okta { get; set; }

    [JsonPropertyName("douyin")]
    public string? Douyin { get; set; }

    [JsonPropertyName("custom")]
    public string? Custom { get; set; }

    [JsonPropertyName("ldap")]
    public string? Ldap { get; set; }

    [JsonPropertyName("properties")]
    public IDictionary<string, string>? Properties { get; set; }

    [JsonPropertyName("roles")]
    public IEnumerable<CasdoorRole>? Roles { get; set; }

    [JsonPropertyName("permissions")]
    public IEnumerable<CasdoorPermission>? Permissions { get; set; }
}
