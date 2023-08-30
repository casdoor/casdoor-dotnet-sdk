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

using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using Casdoor.Client;

namespace Casdoor.AspNetCore.Authentication
{
    public class CasdoorIdentityUserStore : IUserStore<CasdoorUser>
    {
        private readonly CasdoorClient _casdoorClient;
        public CasdoorIdentityUserStore(CasdoorClient casdoorClient)
        {
            _casdoorClient = casdoorClient;
        }
        /// <summary>
        /// Creates the specified user in the user store.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            var response = await _casdoorClient.AddUserAsync(user, cancellationToken);
            return CasdoorResponse2IdentityResult(response);
        }
        /// <summary>
        /// Deletes the specified user from the user store.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            if (user.Name == null)
            {
                throw new ArgumentException($"{nameof(user)}.{nameof(user.Name)} should not be null");
            }

            var response = await _casdoorClient.DeleteUserAsync(user.Name, cancellationToken);
            return CasdoorResponse2IdentityResult(response);
        }
        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CasdoorUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _casdoorClient.GetUserByIdAsync(userId, cancellationToken: cancellationToken);
        }
        /// <summary>
        /// Finds and returns a user, if any, who has the specified normalized user name.
        /// </summary>
        /// <param name="normalizedUserName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CasdoorUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _casdoorClient.GetUserAsync(normalizedUserName, cancellationToken: cancellationToken);
        }
        /// <summary>
        /// Gets the normalized user name for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetNormalizedUserNameAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            return new Task<string>(()=>user.Name);
        }
        /// <summary>
        /// Gets the user identifier for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserIdAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            return new Task<string>(()=>user.Id);
        }
        /// <summary>
        /// Gets the user name for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> GetUserNameAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            return new Task<string>(()=>user.Name);
        }
        /// <summary>
        /// Sets the given normalized name for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="normalizedName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SetNormalizedUserNameAsync(CasdoorUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Name = normalizedName;
            await _casdoorClient.UpdateUserAsync(user, new[] {"name"}, cancellationToken);
        }
        /// <summary>
        /// Sets the given userName for the specified user.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SetUserNameAsync(CasdoorUser user, string userName, CancellationToken cancellationToken)
        {
            user.Name = userName;
            await _casdoorClient.UpdateUserAsync(user, new[] {"name"}, cancellationToken);
        }
        /// <summary>
        /// Updates the specified user in the user store.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateAsync(CasdoorUser user, CancellationToken cancellationToken)
        {
            var response = await _casdoorClient.UpdateUserAsync(user, CasdoorConstants.AllUserProperties, cancellationToken);
            return CasdoorResponse2IdentityResult(response);
        }
        public void Dispose()
        {

        }
        private IdentityResult CasdoorResponse2IdentityResult(CasdoorResponse response)
        {
            return response.Status?.Equals(CasdoorConstants.DefaultCasdoorSuccessStatus) == true ?
                IdentityResult.Success : IdentityResult.Failed(new IdentityError { Description = response.Msg ?? string.Empty });
        }
    }
}
