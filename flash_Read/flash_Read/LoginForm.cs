using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Identity.Client;

namespace flash_Read
{
    internal class LoginForm : Form
    {
        private const string ClientId = "d48e98f4-34a3-4cb0-a132-b29561204757";
        private static string[] Scopes = { "user.read" };

        public LoginForm()
        {
            InitializeComponent();
            this.Load += LoginForm_Load;
        }

        private async void LoginForm_Load(object sender, EventArgs e)
        {
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(ClientId)
                        .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                        .WithRedirectUri("http://localhost:3000/auth/microsoft/callback")
                        .Build();

            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();

            AuthenticationResult result;
            try
            {
                result = await app.AcquireTokenSilent(Scopes, firstAccount)
                                .ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                Console.WriteLine(ex.Message);
                result = await app.AcquireTokenInteractive(Scopes)
                                .WithAccount(firstAccount)
                                .ExecuteAsync();
            }

            // Do something with the authentication result
            // For example, you can display the user's name:
            MessageBox.Show($"Welcome, {result.Account.Username}");
            Form1 mainScreen = new Form1();
            mainScreen.Show();
            Console.WriteLine(123);
            this.Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LoginForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "LoginForm";
            this.ResumeLayout(false);
        }
    }
}