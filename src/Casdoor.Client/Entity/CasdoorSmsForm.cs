using Newtonsoft.Json;

namespace Casdoor.Client.Entity;

public class CasdoorSmsForm
{
    [JsonProperty("organizationId")] public string OrganizationId { get; set; }
    [JsonProperty("content")] public string Content { get; set; }
    [JsonProperty("receivers")] public string[] Receivers { get; set; }

    public CasdoorSmsForm(string organizationId, string content, string[] receivers)
    {
        OrganizationId = organizationId;
        Content = content;
        Receivers = receivers;
    }
}
