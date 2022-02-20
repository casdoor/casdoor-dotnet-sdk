namespace Casdoor.Client.Entity
{
    [Serializable]
    public class CasdoorEmailForm
    {
        public string title { get; set; }

        public string content { get; set; }

        public string sender { get; set; }
        public string[] receivers { get; set; }
    }
}
