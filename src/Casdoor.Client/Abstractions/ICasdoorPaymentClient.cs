// Copyright 2024 The Casdoor Authors. All Rights Reserved.
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

public interface ICasdoorPaymentClient
{
    public Task<CasdoorResponse?> AddPaymentAsync(CasdoorPayment payment, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> UpdatePaymentAsync(CasdoorPayment payment, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> NotifyPaymentAsync(CasdoorPayment payment, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> InvoicePaymentAsync(CasdoorPayment payment, CancellationToken cancellationToken = default);
    public Task<CasdoorResponse?> DeletePaymentAsync(CasdoorPayment payment, CancellationToken cancellationToken = default);
    public Task<CasdoorPayment?> GetPaymentAsync(string name, CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorPayment>?> GetUserPaymentsAsync(string userName,CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorPayment>?> GetPaymentsAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<CasdoorPayment>?> GetPaginationPaymentsAsync(int pageSize, int p,
        List<KeyValuePair<string, string?>>? queryMap, CancellationToken cancellationToken = default);
}
