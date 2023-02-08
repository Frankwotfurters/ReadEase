using System;
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
    public partial class Form2 : Form
    {
        int counter = 0;
        
        public string extractedTexts { get; set; }
        public string timerValue { get; set; }

        bool running = true;
        

        public static Timer timer = new Timer();

        public Form2()
        {
            this.BackColor = ColorTranslator.FromHtml("#FF2D2D30");

            InitializeComponent();
            Button closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Click += CloseButton_Click;
            this.Controls.Add(closeButton);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            button3.Visible = !running;
            button4.Visible = !running;
            button2.Visible = false;
            richTextBox1.BackColor = ColorTranslator.FromHtml("#FF2D2D30");
            richTextBox1.BorderStyle = BorderStyle.None;
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.Close();
            running = false;
        }

       

        public void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;
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
            
        
            string[] visualWords = extractedTexts.Split(' ');
            List<string> wordList = new List<string>(visualWords);
            
            if (running == true)
            {
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
                richTextBox1.Rtf = @"{\rtf1\ansi{\colortbl;\red249\green148\blue23;\red255\green251\blue245;} " + boldedMiddleLetterWord + "}\rtf1";
                //Console.WriteLine(boldedMiddleLetterWord);
                Console.WriteLine(counter);
                counter++;
            }
            
            
           
            



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
                richTextBox1.Rtf = @"{\rtf1\ansi{\colortbl;\red249\green148\blue23;\red255\green251\blue245;} " + boldedMiddleLetterWord + "}\rtf1";
                Console.WriteLine(counter);

            }
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string[] visualWords = extractedTexts.Split(' ');
            List<string> wordList = new List<string>(visualWords);
            if (counter < wordList.Count - 1)
            {
                counter++;
                

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
                richTextBox1.Rtf = @"{\rtf1\ansi{\colortbl;\red249\green148\blue23;\red255\green251\blue245;} " + boldedMiddleLetterWord;
                Console.WriteLine(counter);
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = ColorTranslator.FromHtml("#FF2D2D30");

            ;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            running = !running;
            timer.Enabled = running;
            button3.Visible = !running;
            button4.Visible = !running;
        }
    }
}
public class Form2 : Form
{
    public Form2()
    {
       
    }

    
}
