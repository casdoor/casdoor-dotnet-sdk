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

using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Casdoor.Client;

public static class CasdoorUserExtension
{
    static CasdoorUserExtension()
    {
        var properties = typeof(CasdoorUser).GetProperties();
        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (attribute is null)
            {
                continue;
            }
            s_propertyInfos[attribute.Name] = property;
        }
    }

    private static readonly IDictionary<string, PropertyInfo> s_propertyInfos = new Dictionary<string, PropertyInfo>();

    internal static void SetClaim(this CasdoorUser user, Claim claim)
    {
        if (s_propertyInfos.TryGetValue(claim.Type, out var propertyInfo) is false)
        {
            user.Properties ??= new Dictionary<string, string>();
            user.Properties[claim.Type] = claim.Value;
            return;
        }
        propertyInfo.SetValue(user, claim.Value);
    }
}
