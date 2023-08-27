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

using System.Text.Json;
using System.Text.Json.Nodes;

namespace Casdoor.Client;

public static class CasdoorResponseExtension
{
    internal static T? DeserializeData<T>(this CasdoorResponse? response)
    {
        if (response?.Status != "ok")
        {
            throw new CasdoorApiException(response?.Msg);
        }
        if (response.Data is JsonElement element)
        {
            return element.Deserialize<T>();
        }

        return default;
    }

    internal static void DeserializeFromJson<T>(this CasdoorResponse<T> response, string json)
    {
        var parsedJson = JsonNode.Parse(json);
        response.Status = parsedJson!["status"]!.ToString();
        response.Msg = parsedJson["msg"]!.ToString();

        if (response.Status != "ok")
        {
            return;
        }

        response.Sub = parsedJson["sub"]!.ToString();
        response.Name = parsedJson["name"]!.ToString();

        try
        {
            response.Data = parsedJson["data"].Deserialize<IEnumerable<T>>();
        }
        catch
        {
            var data = new List<T>();

            foreach (var jsonElement in parsedJson["data"].Deserialize<IEnumerable<JsonElement>>()!)
            {
                data.AddRange(jsonElement.Deserialize<IEnumerable<T>>()!);
            }

            response.Data = data;
        }

        response.Data2 = parsedJson["data2"]?.ToString();
    }
}
