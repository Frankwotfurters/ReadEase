using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using static System.Drawing.Text.PrivateFontCollection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RichTextBoxExtensions;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace readEase
{

    public partial class Form2 : Form
    {
        int counter = 0;
        
        public string extractedTexts { get; set; }
        public string timerValue { get; set; }
        public Color bgColor { get; set; }
        public Color textColor { get; set; }
        public Color boldColor { get; set; }

        bool running = true;
        

        public static Timer timer = new Timer();

        public Form2()
        {
            this.BackColor = ColorTranslator.FromHtml("#FF2D2D30");
            InitializeComponent();
   
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            button3.Visible = !running;
            button4.Visible = !running;
            button2.Visible = false;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.BackColor = bgColor;
            richTextBox1.ForeColor = textColor;
            this.BackColor = bgColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            running = false;
            timer.Stop();

        }

        public void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            
            button1.Visible = false;
            button2.Visible = true;
            SendKeys.Send("{TAB}");
            int result;
            int.TryParse(timerValue, out result);

            if (running == true)
            {
                timer.Interval = (60000 / result); // in ms
                timer.Tick += new EventHandler(timer_Tick);
                timer.Start();
            }
            
            
            
            
            

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            //Create your private font collection object.
            PrivateFontCollection pfc = new PrivateFontCollection();

            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = ReadEase.Properties.Resources.ClintonBold.Length;

            // create a buffer to read in to
            byte[] fontdata = ReadEase.Properties.Resources.ClintonBold;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);

            string[] visualWords = extractedTexts.Split(' ');
            List<string> wordList = new List<string>(visualWords);
            
            if (running == true)
            {
                richTextBox1.Rtf = "";
                this.FormBorderStyle = FormBorderStyle.None;
                if (counter >= wordList.Count && running == true)
                {
                    counter = 0;
                }
                
                string currentWord = wordList[counter].Trim();

                if (string.IsNullOrEmpty(currentWord) && running == true)
                {
                    counter++;
                    return;
                }


                int middleIndex = currentWord.Length / 2;
                string firstHalf = "\\cf2\\fs58 " + currentWord.Substring(0, middleIndex);
                string secondHalf = "\\cf2 " + currentWord.Substring(middleIndex + 1, currentWord.Length - middleIndex - 1);
                string boldedMiddleLetterWord = firstHalf + "\\b\\fs58\\cf1 " + currentWord[middleIndex] + "\\b0\\fs54\\cf1" + secondHalf;
                richTextBox1.Rtf = $@"{{\rtf1\ansi{{\colortbl;\red{boldColor.R}\green{boldColor.G}\blue{boldColor.B};\red{textColor.R}\green{textColor.G}\blue{textColor.B};}} " + boldedMiddleLetterWord + "}\rtf1";                
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font(pfc.Families[0], 48, FontStyle.Bold);
            }


            counter++;
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            string[] visualWords = extractedTexts.Split(' ');
            List<string> wordList = new List<string>(visualWords);
            if (!(counter <= 0))
            {
                counter--;
                string currentWord = wordList[counter].Trim();

               // if (!(counter - 1 < 0))
              //  {
               //     currentWord = wordList[counter - 1].Trim();
              //  }
                
                if (string.IsNullOrEmpty(currentWord) && running == true)
                {
                    counter--;
                    return;
                }
                int middleIndex = currentWord.Length / 2;
                string firstHalf = "\\cf2\\fs58 " + currentWord.Substring(0, middleIndex);
                string secondHalf = "\\cf2 " + currentWord.Substring(middleIndex + 1, currentWord.Length - middleIndex - 1);
                string boldedMiddleLetterWord = firstHalf + "\\b\\fs58\\cf1 " + currentWord[middleIndex] + "\\b0\\fs54\\cf1" + secondHalf;
                richTextBox1.Rtf = @"{\rtf1\ansi{\colortbl;\red204\green84\blue64;\red255\green251\blue245;} " + boldedMiddleLetterWord + "}\rtf1";
                Console.WriteLine(counter);

            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string[] visualWords = extractedTexts.Split();
            List<string> wordList = new List<string>(visualWords);
            if (counter < wordList.Count - 1)
            {
                counter++;
                

                string currentWord = wordList[counter].Trim();

                Console.WriteLine(currentWord);
                if (string.IsNullOrEmpty(currentWord) && running == true)
                {
                    counter++;
                    return;
                }
                int middleIndex = currentWord.Length / 2;
                string firstHalf = "\\cf2\\fs58 " + currentWord.Substring(0, middleIndex);
                string secondHalf = "\\cf2 " + currentWord.Substring(middleIndex + 1, currentWord.Length - middleIndex - 1);
                string boldedMiddleLetterWord = firstHalf + "\\b\\fs58\\cf1 " + currentWord[middleIndex] + "\\b0\\fs54\\cf1" + secondHalf;
                richTextBox1.Rtf = @"{\rtf1\ansi{\colortbl;\red204\green84\blue64;\red255\green251\blue245;} " + boldedMiddleLetterWord;
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.ForeColor = textColor;
            richTextBox1.BackColor = bgColor;
            ;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            //Do nothing
        }
        private void richTextBox1_MouseClick(object sender, EventArgs e)
        {
            //Do nothing
        }
        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Controls.Find("button2", true).First().Focus();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            running = !running;
            timer.Enabled = running;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            button3.Visible = !running;
            button4.Visible = !running;
            

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            timer.Stop();
        }

        
    }
}
public class Form2 : Form
{
    public Form2()
    {
       
    }

    
}
