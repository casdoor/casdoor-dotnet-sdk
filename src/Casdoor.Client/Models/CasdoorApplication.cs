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

public class CasdoorProviderItem
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("canSignUp")]
    public bool? CanSignUp { get; set; }
    
    [JsonPropertyName("canSignIn")]
    public bool? CanSignIn { get; set; }
    
    [JsonPropertyName("canUnlink")]
    public bool? CanUnlink { get; set; }

    [JsonPropertyName("prompted")]
    public bool? Prompted { get; set; }
    
    [JsonPropertyName("alertType")]
    public string? AlertType { get; set; }
    
    [JsonPropertyName("provider")]
    public CasdoorProvider? Provider { get; set; }
}

public class CasdoorSignupItem
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("visible")]
    public bool? Visible { get; set; }

    [JsonPropertyName("required")]
    public bool? Required { get; set; }

    [JsonPropertyName("prompted")]
    public bool? Prompted { get; set; }

    [JsonPropertyName("rule")]
    public string? Rule { get; set; }
}

public class CasdoorApplication
{
    [JsonPropertyName("owner")]
    public string? Owner { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("createdTime")]
    public string? CreatedTime { get; set; }

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }

    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    [JsonPropertyName("homepageUrl")]
    public string? HomepageUrl { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("organization")]
    public string? Organization { get; set; }

    [JsonPropertyName("cert")]
    public string? Cert { get; set; }

    [JsonPropertyName("enablePassword")]
    public bool? EnablePassword { get; set; }

    [JsonPropertyName("enableSignUp")]
    public bool? EnableSignUp { get; set; }

    [JsonPropertyName("enableSigninSession")]
    public bool? EnableSigninSession { get; set; }

    [JsonPropertyName("enableCodeSignin")]
    public bool? EnableCodeSignin { get; set; }

    [JsonPropertyName("enableAutoSignin")]
    public bool? EnableAutoSignin { get; set; }

    [JsonPropertyName("enableSamlCompress")]
    public bool? EnableSamlCompress { get; set; }

    [JsonPropertyName("enableWebAuth")]
    public bool? EnableWebAuth { get; set; }

    [JsonPropertyName("providers")]
    public CasdoorProviderItem[]? Providers { get; set; }
    
    [JsonPropertyName("signupItems")]
    public CasdoorSignupItem[]? SignupItems { get; set; }

    [JsonPropertyName("grantTypes")] 
    public string[]? GrantTypes { get; set; }

    [JsonPropertyName("organizationObj")]
    public CasdoorOrganization? OrganizationObj { get; set; }
    
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    [JsonPropertyName("clientSecret")]
    public string? ClientSecret { get; set; }

    [JsonPropertyName("redirectUris")]
    public string[]? RedirectUris { get; set; }

    [JsonPropertyName("tokenFormat")]
    public string? TokenFormat { get; set; }

    [JsonPropertyName("expireInHours")]
    public int? ExpireInHours { get; set; }

    [JsonPropertyName("refreshExpireInHours")]
    public int? RefreshExpireInHours { get; set; }

    [JsonPropertyName("signupUrl")]
    public string? SignupUrl { get; set; }

    [JsonPropertyName("signinUrl")]
    public string? SigninUrl { get; set; }

    [JsonPropertyName("forgetUrl")]
    public string? ForgetUrl { get; set; }

    [JsonPropertyName("affiliationUrl")]
    public string? AffiliationUrl { get; set; }

    [JsonPropertyName("termsOfUse")]
    public string? TermsOfUse { get; set; }

    [JsonPropertyName("signupHtml")]
    public string? SignupHtml { get; set; }

    [JsonPropertyName("signinHtml")]
    public string? SigninHtml { get; set; }

    [JsonPropertyName("formCss")]
    public string? FormCss { get; set; }
    
    [JsonPropertyName("formOffset")]
    public int? FormOffset { get; set; }

    [JsonPropertyName("formBackgroundUrl")]
    public string? FormBackgroundUrl { get; set; }
}