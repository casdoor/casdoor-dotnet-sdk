using System.Text.Json.Serialization;

namespace Casdoor.Client.Entity;

public class CasdoorSmsForm
{
    [JsonPropertyName("organizationId")] public string OrganizationId { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("receivers")] public string[] Receivers { get; set; }

    public CasdoorSmsForm(string organizationId, string content, string[] receivers)
    {
        OrganizationId = organizationId;
        Content = content;
        Receivers = receivers;
    }
}
