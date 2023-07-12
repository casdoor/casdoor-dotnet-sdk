﻿// Copyright 2022 The Casdoor Authors. All Rights Reserved.
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

public interface ICasdoorUserClient
{
    public Task<IEnumerable<CasdoorUser>?> GetUsersAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorUser>?> GetSortedUsersAsync(string sorter, int limit, CancellationToken cancellationToken = default);
    public Task<CasdoorUser?> GetUserAsync(string name, CancellationToken cancellationToken = default);
    public Task<CasdoorUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> AddUserAsync(CasdoorUser user, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> UpdateUserAsync(CasdoorUser user, IEnumerable<string> propertyNames, CancellationToken cancellationToken = default );
    public Task<CasdoorResponse?> DeleteUserAsync(string name, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> CheckUserPasswordAsync(string name, CancellationToken cancellationToken = default);
}
