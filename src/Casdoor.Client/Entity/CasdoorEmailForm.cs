using System.Text.Json.Serialization;

namespace Casdoor.Client.Entity;

public class CasdoorEmailForm
{
    [JsonPropertyName("title")] public string Title { get; set; }
    [JsonPropertyName("content")] public string Content { get; set; }
    [JsonPropertyName("sender")] public string Sender { get; set; }
    [JsonPropertyName("receivers")] public string[] Receivers { get; set; }

    public CasdoorEmailForm(string title, string content, string sender, string[] receivers)
    {
        Title = title;
        Content = content;
        Sender = sender;
        Receivers = receivers;
    }
}
