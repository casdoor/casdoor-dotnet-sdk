namespace Casdoor.Client.Entity;

public class CasdoorEmailForm
{
    private string Title { get; set; }
    private string Content { get; set; }
    private string Sender { get; set; }
    private string[] Receivers { get; set; }

    public CasdoorEmailForm(string title, string content, string sender, string[] receivers)
    {
        Title = title;
        Content = content;
        Sender = sender;
        Receivers = receivers;
    }
}
