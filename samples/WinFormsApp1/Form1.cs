using Casdoor.Client;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public readonly CasdoorClient _casdoorClient;
        public readonly CasdoorOptions _options;

        public Form1()
        {
            InitializeComponent();
            var httpClient = new HttpClient();

            var options = new CasdoorOptions
            {
                // Require: Basic options
                Endpoint = "https://door.casdoor.com",
                OrganizationName = "build-in",
                ApplicationName = "app-build-in",
                ApplicationType = "native", // webapp, webapi or native
                ClientId = "541738959670d221d59d",
                ClientSecret = "66863369a64a5863827cf949bab70ed560ba24bf",

                // Optional: The callback path that the client will be redirected to
                // after the user has authenticated. default is "/casdoor/signin-callback"
                CallbackPath = "/callback",
                // Optional: Whether require https for casdoor endpoint
                RequireHttpsMetadata = true,
                // Optional: The scopes that the client is requesting.
                Scope = "openid profile email"

                // More options can be found at README.md
                // https://github.com/casdoor/casdoor-dotnet-sdk/blob/master/README.md
            };
            _options = options;

            _casdoorClient = new CasdoorClient(httpClient, _options);
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Auto fetching OpenIdConnectConfiguration...");
            var configuration = await _options.GetOpenIdConnectConfigurationAsync();

            MessageBox.Show("Get tokens by username and password...");
            var token = await _casdoorClient.RequestPasswordTokenAsync("admin", "123");
        }
    }
}
