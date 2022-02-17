using System.Text.Json.Serialization;

namespace Casdoor.Client.Entity;

public class CasdoorUserResource
{
    [JsonPropertyName("owner")] public string? Owner { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }

    public CasdoorUserResource(string owner, string name)
    {
        Owner = owner;
        Name = name;
    }
}
