using System.Text.Json.Serialization;

namespace Casdoor.Client;

public class CasdoorThemeData
{
    [JsonPropertyName("themeType")]
    public string? ThemeType { get; set; }

    [JsonPropertyName("colorPrimary")]
    public string? ColorPrimary { get; set; }

    [JsonPropertyName("borderRadius")]
    public int? BorderRadius { get; set; }

    [JsonPropertyName("isCompact")]
    public bool? IsCompact { get; set; }

    [JsonPropertyName("isEnabled")]
    public bool? IsEnabled { get; set; }
}
