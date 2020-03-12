using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;


namespace KomutTanıma
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void KomutlariOlustur()
        {
            string[] komutlar = new string[] 
            { 
                "Yes","No","Open İmage","Open Video","Thank You","Search","Run Notepad","Run Paint","Open Google","Open Translate","Open Facebook","What is your name","What time is it","Ney's today","Date","I love you"
            };





            Choices insChoices = new Choices(komutlar);
            GrammarBuilder insGrammarBuilder = new GrammarBuilder(insChoices);
            Grammar insGrammar = new Grammar(insGrammarBuilder);
            sre.LoadGrammar(insGrammar);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KomutlariOlustur();
            sre.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(sre_SpeechDetected);
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(sre_RecognizeCompleted);
            sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(sre_SpeechRecognitionRejected);
            Thread t = new Thread(delegate() { sre.SetInputToDefaultAudioDevice(); sre.RecognizeAsync(RecognizeMode.Single); });
            t.Start();

        }

        void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            lblDurum.Text = "Komut algılanamadı.";
        }

        void sre_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            sre.RecognizeAsync();
            progressBar1.Style = ProgressBarStyle.Blocks;
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "Yes")
            {
                
                listBox11 .Items .Add ("Evet Komutu Algılandı");
            }
            else if (e.Result.Text == "No")
            {
                listBox11.Items.Add("Hayır Komutu Algılandı");
            }
            else if (e.Result.Text == "Open İmage")
            {
                listBox11.Items.Add("Resmi Aç Komutu Algılandı");
            }
            else if (e.Result.Text == "Open Video")
            {
                listBox11.Items.Add("Video'yu Aç Komutu Algılandı");
            }
            else if (e.Result.Text == "Thank You")
            {
                listBox11.Items.Add("Teşekkürler Komutu Algılandı");
            }
            else if (e.Result.Text == "Search")
            {
                listBox11.Items.Add("Arama Komutu Algılandı");
            }
            
            if (e.Result.Text == "Run Notepad")
            {
                Process.Start("notepad.exe");
                listBox1.Items.Add("Run Notepad");
            }
            else if (e.Result.Text == "Run Paint")
            {
                Process.Start("mspaint.exe");
                listBox1.Items.Add("Run Paint");
            }
            else if (e.Result.Text == "Open Google")
            {
                string target = "http://www.google.com";
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch
                    (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
                listBox1.Items.Add("Open Google");
            }
            else if (e.Result.Text == "Open Translate")
            {
                string target = "http://translate.google.com.tr/";
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch
                    (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
                listBox1.Items.Add("Open Translate");
            }
            else if (e.Result.Text == "Open Facebook")
            {
                string target = "http://www.facebook.com";
                try
                {
                    System.Diagnostics.Process.Start(target);
                }
                catch
                    (System.ComponentModel.Win32Exception noBrowser)
                {
                    if (noBrowser.ErrorCode == -2147467259)
                        MessageBox.Show(noBrowser.Message);
                }
                catch (System.Exception other)
                {
                    MessageBox.Show(other.Message);
                }
                listBox1.Items.Add("Open Facebook");

            }
            else if (e.Result.Text == "What is your name")
            {
                MessageBox.Show("İsmimden sanane");
                listBox1.Items.Add("What is your name");
            }
            //Zamanlar
            else if (e.Result.Text == "What time is it")
            {
                string Saat = DateTime.Now.Hour + " : " + DateTime.Now.Minute + " : " + DateTime.Now.Second;
                MessageBox.Show("Time : " +Saat);
                listBox1.Items.Add("What time is it");
            }
            else if (e.Result.Text == "Ney's today")
            {
                string Gün = "Today is " + DateTime.Now.DayOfWeek;
                MessageBox.Show(Gün);
                listBox1.Items.Add("Ney's today");
            }
            else if (e.Result.Text == "Date")
            {
                string Ay = "Date is " + DateTime.Now.Day.ToString() + ":" + DateTime.Now.Month + ":" + DateTime.Now.Year;
                MessageBox.Show(Ay);
                listBox1.Items.Add("Date");
            }

            else if (e.Result.Text == "I love you")
            {
                MessageBox.Show("I got you");
                listBox1.Items.Add("I love you");
            }
            lblDurum.Text = "Komut Algılandı. Suan algılama pasif.";
        }


        void sre_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            lblDurum.Text = "Komut algılanıyor...";
            progressBar1.Style = ProgressBarStyle.Marquee;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }



       

    }
}
