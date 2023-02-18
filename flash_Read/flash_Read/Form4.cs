using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace flash_Read
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        public static String CreateLicenseKeyFile(string licenseKey)
        {
            // Get the path to the application data folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Create a folder for the license key file
            string licenseFolder = System.IO.Path.Combine(appDataPath, "ReadEase", "Licenses");
            Directory.CreateDirectory(licenseFolder);

            // Create the license key file in the license folder
            string licenseFilePath = System.IO.Path.Combine(licenseFolder, "LicenseKey.txt");
            using (StreamWriter writer = new StreamWriter(licenseFilePath))
            {
                writer.WriteLine(licenseKey);
                return licenseFilePath;
            }
        }

        public static String GetLicenseKey()
        {
            // Get the path to the application data folder
            String appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Get the path to the license key file
            String licenseFilePath = Path.Combine(appDataPath, "ReadEase", "Licenses", "LicenseKey.txt");

            // Check if the license key file exists
            if (!File.Exists(licenseFilePath))
            {
                throw new FileNotFoundException("License key file not found.");
            }

            // Read the license key from the file
            using (StreamReader reader = new StreamReader(licenseFilePath))
            {
                String licenseKey = reader.ReadLine();
                return licenseKey;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            Boolean validKey = false;

            // User input
            String keyInput = textBox1.Text;

            // Verify against our database
            if (keyInput == "good") // CHANGE THIS TO ACTUAL VERIFICATION
            {
                validKey = true;
            }

            if (validKey)
            {
                // Close this window and allow user to access the app
                this.Close();
            }
            else
            {
                // Wrong key
                label2.Visible = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            String keyInput = textBox1.Text;
            String filePath = CreateLicenseKeyFile(keyInput);
            Console.WriteLine(filePath);
        }
    }
}
