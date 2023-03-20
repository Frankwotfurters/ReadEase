using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Identity.Client;



namespace flash_Read
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.BackColor = ColorTranslator.FromHtml("#FF2D2D30");
            InitializeComponent();
        }

        static string filePath;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            label1.ForeColor = Color.White; label1.Font = new Font("Arial", 12, FontStyle.Bold);
            //label2.ForeColor = Color.White; label2.Font = new Font("Arial", 12, FontStyle.Bold);
            label3.ForeColor = Color.White; label3.Font = new Font("Arial", 12, FontStyle.Bold);
            label4.ForeColor = Color.White; label4.Font = new Font("Arial", 12, FontStyle.Bold);
            //label5.ForeColor = Color.White; label5.BackColor = ColorTranslator.FromHtml("#FF2D2D30"); label5.Font = new Font("Arial", 20, FontStyle.Bold);
            label6.ForeColor = Color.White; label6.Font = new Font("Arial", 12, FontStyle.Bold);
            

        }

        public void button1_Click(object sender, EventArgs e)
        {

            //if (!(filePath == null || !string.IsNullOrEmpty(extractedText)))
           // {
            //    MessageBox.Show("Please select a file");

            //}
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                // Execute statement
                MessageBox.Show("Please select a file or paste text into textbox");
            }
            else
            {
                Form2 secondWindow = new Form2();
                secondWindow.extractedTexts = textBox2.Text; //to send variable to other class

                // check if WPM input is valid
                int results;
                int.TryParse(textBox1.Text, out results);
                if (results > 0 && results < 5001)
                {
                    // if valid:
                    secondWindow.timerValue = textBox1.Text; //to send variable to other class

                    // Show the second window
                    secondWindow.Show();
                }
                else
                {
                    // error
                    MessageBox.Show("Please enter a number between 1-5000");
                }

            }

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the initial directory
            openFileDialog.InitialDirectory = @"C:\";

            // Set the filter for the file types to display
            openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";

            // Set the default filter index
            openFileDialog.FilterIndex = 1;

            // Set the RestoreDirectory property to true
            openFileDialog.RestoreDirectory = true;

            // Show the OpenFileDialog and store the result in a DialogResult variable
            DialogResult result = openFileDialog.ShowDialog();

            // If the result is OK, get the selected file path
            if (result == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                // Do something with the file path, such as display it in a label
                label1.Text = filePath;
                textBox2.Text = "";
            }
            //MessageBox.Show(label1.Text);
            using (PdfReader reader = new PdfReader(label1.Text))
            {
                
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    textBox2.Text = PdfTextExtractor.GetTextFromPage(reader, i);
                }

                //MessageBox.Show(extractedText);
            }

            
            
        }
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                // Check if the file is a PDF and process it
                if (System.IO.Path.GetExtension(file) == ".pdf")
                {
                    // Do something with the PDF file
                    using (PdfReader reader = new PdfReader(file))
                    {

                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            textBox2.Text = PdfTextExtractor.GetTextFromPage(reader, i);
                        }

                        //MessageBox.Show(extractedText);
                    }
                }
                else
                {
                    MessageBox.Show("Only PDF files are accepted!");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 thirdWindow = new Form3();
            thirdWindow.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 licenseWindow = new Form4();
            licenseWindow.Show();
        }
    }
}
