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
using System.Collections;
using System.Text.RegularExpressions;

namespace _9._29_revised_word_counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        StreamReader inputfile;
        string textfile;
        int frequency;

        private void loadTextFileButton_Click(object sender, EventArgs e)
        {// Read Text File.
            listView1.Items.Clear();
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inputfile = File.OpenText(openFileDialog.FileName);
                    while (!inputfile.EndOfStream)
                    {
                        textfile = inputfile.ReadToEnd();
                        LoadFile();

                    }
                    inputfile.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        private void LoadFile()
        {
            string phrase = textfile;
            phrase = textfile.ToLower();
            Regex regex = new Regex("[^a-z A-Z 0-9 ' ,]");
            phrase = regex.Replace(phrase, " ");
            phrase = phrase.Replace(",", "");
            String[] words = phrase.Split(' ', '.', ',', '!', '?', '\n', '\r', '\t', '\v');
            //will seperate words such as "thats" and "that's" as 2 seperate words, otherwise wed get a few instances of "s" etc as its own word

            for (int i = 0; i < words.Length; i++)
            {
                frequency = 1;
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (words[i].Equals(words[j]))
                    {
                        words[j] = "0";
                        frequency++;
                    }
                }
                if (frequency > 0 && words[i] != "0" && words[i] != "")
                {
                    String[] rowItem = new string[] { words[i], frequency.ToString() };
                    
                    listView1.Items.Add(new ListViewItem(rowItem));
                }
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.Columns.Add("Word:", 120 );
            listView1.Columns.Add("Frequency:", 100);
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Sorting = SortOrder.Ascending;
        }
    }
}
