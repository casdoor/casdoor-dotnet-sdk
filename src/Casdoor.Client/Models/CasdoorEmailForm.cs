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

public class CasdoorEmailForm
{
    public CasdoorEmailForm(string title, string content, string sender, string[] receivers)
    {
        Title = title;
        Content = content;
        Sender = sender;
        Receivers = receivers;
    }

    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("sender")] public string Sender { get; set; }
    [JsonPropertyName("receivers")] public string[] Receivers { get; set; }
}
