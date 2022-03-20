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

public class Organization
{
    public string? Owner { get; set; }
    public string? Name { get; set; }
    public string? CreatedTime { get; set; }
    public string? DisplayName { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? Favicon { get; set; }
    public string? PasswordType { get; set; }
    public string? PasswordSalt { get; set; }
    public string? PhonePrefix { get; set; }
}
