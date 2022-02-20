using Newtonsoft.Json;

namespace Casdoor.Client.Util;

public class CasdoorResponse
{
    [JsonProperty("status")] public string? Status { get; set; }
    [JsonProperty("msg")] public string? Msg { get; set; }
    [JsonProperty("data")] public object? Data { get; set; }
    [JsonProperty("data2")] public object? Data2 { get; set; }
}
