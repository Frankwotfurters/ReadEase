﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Drawing;

namespace readEase
{
    internal class LoginForm : Form
    {
        private const string ClientId = "d48e98f4-34a3-4cb0-a132-b29561204757";
        private Button button1;
        private Label label1;
        private static string[] Scopes = { "user.read" };

        public LoginForm()
        {
            this.BackColor = ColorTranslator.FromHtml("#FF2D2D30");
            InitializeComponent();
            this.Load += LoginForm_Load;
        }
        private async void promptLogin()
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
                MessageBox.Show($"Welcome, {result.Account.Username}");
                Form1 mainScreen = new Form1();
                mainScreen.Show();
                this.Close();

                // Get the path to the application data folder
                String appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Get the path to the properties file
                String propertiesFilePath = Path.Combine(appDataPath, "ReadEase", "properties.json");

                UserProperties properties;

                // Check if the properties file exists
                if (File.Exists(propertiesFilePath))
                {
                    // Retrieve existing properties
                    properties = JsonConvert.DeserializeObject<UserProperties>(File.ReadAllText(propertiesFilePath));
                }
                else
                {
                    properties = new UserProperties();
                }

                properties.user_token = result.Account.Username;
                File.WriteAllText(propertiesFilePath, JsonConvert.SerializeObject(properties));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\nPlease try logging in again.");
            }
        }
        private async void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                // First check if user is already logged in
                // Get the path to the application data folder
                String appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Get the path to the properties file
                String propertiesFilePath = Path.Combine(appDataPath, "ReadEase", "properties.json");

                // Check if the properties file exists
                if (!File.Exists(propertiesFilePath))
                {
                    throw new FileNotFoundException("Properties file not found.");
                }
                // Retrieve token from properties file
                UserProperties properties = JsonConvert.DeserializeObject<UserProperties>(File.ReadAllText(propertiesFilePath));

                // Check if user_token is valid
                if (properties.user_token != null) // TODO: CHANGE THIS
                {
                    // If valid
                    Form1 mainScreen = new Form1();
                    mainScreen.Show();
                    this.Close();
                }
                else
                {
                    // If not valid: prompt login
                    promptLogin();
                }
            }
            catch (FileNotFoundException)
            {
                // Properties file not found
                promptLogin();
            }
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Image = global::ReadEase.Properties.Resources.MSFT;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(37, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(248, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Continue with Microsoft Account";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(52, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Welcome to ReadEase!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // LoginForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(319, 333);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            promptLogin();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}