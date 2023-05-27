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

namespace Casdoor.Client;

public partial class CasdoorClient
{
    public virtual Task<CasdoorResponse?> SendSmsAsync(string content, IEnumerable<string> receivers, CancellationToken cancellationToken = default)
    {
        CasdoorSmsForm form = new()
        {
            OrganizationId = string.Concat("admin/", _options.OrganizationName),
            Content = content,
            Receivers = receivers as string[] ?? receivers.ToArray(),
        };
        string url = _options.GetActionUrl("send-sms");
        return PostAsJsonAsync(url, form, cancellationToken);
    }

    public virtual Task<CasdoorResponse?> SendEmailAsync(string title, string content, string sender,
        IEnumerable<string> receivers, CancellationToken cancellationToken = default)
    {
        CasdoorEmailForm form = new()
        {
            Title = title,
            Content = content,
            Receivers = receivers as string[] ?? receivers.ToArray(),
            Sender = sender
        };
        string url = _options.GetActionUrl("send-email");
        return PostAsJsonAsync(url, form, cancellationToken);
    }
}
