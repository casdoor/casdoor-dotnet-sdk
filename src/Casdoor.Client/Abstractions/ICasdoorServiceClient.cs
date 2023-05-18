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

public interface ICasdoorServiceClient
{
    public Task<CasdoorResponse?> SendSmsAsync(string content, params string[] receivers);
    public Task<CasdoorResponse?> SendSmsAsync(string content, CancellationToken cancellationToken , params string[] receivers);

    public Task<CasdoorResponse?> SendEmailAsync(string title, string content, string sender,
        string[] receivers, CancellationToken cancellationToken = default);
}
