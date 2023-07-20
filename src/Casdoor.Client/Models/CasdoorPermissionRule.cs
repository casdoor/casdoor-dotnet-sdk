using System.Text.Json.Serialization;

namespace Casdoor.Client;

public class CasdoorPermissionRule
{
    [JsonPropertyName("ptype")]
    public string? Ptype { get; set; }

    [JsonPropertyName("v0")]
    public string? V0 { get; set; }

    [JsonPropertyName("v1")]
    public string? V1 { get; set; }

    [JsonPropertyName("v2")]
    public string? V2 { get; set; }

    [JsonPropertyName("v3")]
    public string? V3 { get; set; }

    [JsonPropertyName("v4")]
    public string? V4 { get; set; }

    [JsonPropertyName("v5")]
    public string? V5 { get; set; }

    [JsonPropertyName("id")]
    public string? Id { get; set; }
}


