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
    // TODO: what are `createdTime` and `description` for?
    public virtual Task<CasdoorResponse?> UploadResourceAsync(
        string user, string tag, string parent, string fullFilePath,
        Stream fileStream, string createdTime = "", string description = "", CancellationToken cancellationToken = default)
    {
        var queryMap = new QueryMapBuilder()
            .Add("owner", _options.OrganizationName)
            .Add("user", user)
            .Add("application", _options.ApplicationName)
            .Add("tag", tag)
            .Add("parent", parent)
            .Add("fullFilePath", fullFilePath)
            .GetMap();
        string url = _options.GetActionUrl("upload-resource", queryMap);
        return _httpClient.PostFileAsync(url, new StreamContent(fileStream), cancellationToken: cancellationToken);
    }

    public virtual Task<CasdoorResponse?> DeleteResourceAsync(string name, CancellationToken cancellationToken = default)
    {
        CasdoorUserResource resource = new() {Owner = _options.OrganizationName, Name = name};
        return PostAsJsonAsync("delete-resource", resource, cancellationToken);
    }
}
