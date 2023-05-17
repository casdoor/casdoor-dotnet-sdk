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

namespace Casdoor
{
    public static class CasdoorConstants
    {
        public static readonly string DefaultCasdoorSuccessStatus = "ok";
        public static readonly string[] AllUserProperties = new string[]
        {
            "owner", "name", "createdTime", "updateTime", "id", "type",
            "password", "passwordSalt", "displayName", "avatar", "permanentAvatar",
            "email", "phone", "location", "address", "affiliation", "title",
            "idCardType", "homePage", "bio", "tag", "region", "language", "gender",
            "birthday", "education", "score", "ranking", "isDefaultAvatar", "isOnline",
            "isAdmin", "isGlobalAdmin", "isForbidden", "isDeleted", "signupApplication",
            "hash", "preHash", "createdIp", "lastSigninTime", "lastSigninIp", "github",
            "google", "qq", "wechat", "facebook", "dingtalk", "weibo", "gitee", "linkedin",
            "wecom", "lark", "gitlab", "ldap", "properties"
        };

        public static readonly string DefaultCasdoorOwner = "admin";
    }
}