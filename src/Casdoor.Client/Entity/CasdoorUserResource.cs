using Newtonsoft.Json;

namespace Casdoor.Client.Entity;

/// <summary>
/// /UserResource.cs -> /Entity/CasdoorUserResource.cs
/// using Newtonsoft.Json to make it serializable
/// </summary>
public class CasdoorUserResource
{
    [JsonProperty("owner")] public string? Owner { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }

    public CasdoorUserResource(string owner, string name)
    {
        Owner = owner;
        Name = name;
    }
}
