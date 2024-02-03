﻿// Copyright 2024 The Casdoor Authors. All Rights Reserved.
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

public interface ICasdoorPricingClient
{
    public Task<CasdoorResponse?> AddPricingAsync(CasdoorPricing pricing, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> UpdatePricingAsync(CasdoorPricing pricing, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> DeletePricingAsync(CasdoorPricing pricing, CancellationToken cancellationToken = default);
    public Task<CasdoorPricing?> GetPricingAsync(string name, CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorPricing>?> GetPricingsAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorPricing>?> GetPaginationPricingsAsync(int pageSize, int p,
        List<KeyValuePair<string, string?>>? queryMap, CancellationToken cancellationToken = default);
}
