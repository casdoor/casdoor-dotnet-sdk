using System.Text.Json.Serialization;

namespace Casdoor.Client.Util;

public class CasdoorResponse
{
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("msg")] public string? Msg { get; set; }
    [JsonPropertyName("data")] public object? Data { get; set; }
    [JsonPropertyName("data2")] public object? Data2 { get; set; }
}
