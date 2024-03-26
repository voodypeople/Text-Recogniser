using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using System.Windows;
using System.IO;

namespace Text_Recogniser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //panel1.AutoScroll= true;
            //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            textBox1.ReadOnly= true;
            comboBox1.Items.Add("RUS");
            comboBox1.Items.Add("ENG");
            comboBox1.Items.Add("RUS + ENG");
            comboBox1.SelectedItem = "RUS";
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {

                pictureBox1.Image = new Bitmap(open.FileName);

                textBox1.Text = open.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Сначала выберите файл!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.SelectedItem == "RUS")
            {
                richTextBox1.Clear();
                var ocrengine = new TesseractEngine(@".\tessdata", "rus", EngineMode.Default);
                var img = Pix.LoadFromFile(textBox1.Text);
                var res = ocrengine.Process(img);
                richTextBox1.Text = res.GetText();
                if (res.GetText() == " \n" || string.IsNullOrEmpty(res.GetText()))
                {
                    MessageBox.Show("Ошибка!", "Текст не был обнаружен!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            if (comboBox1.SelectedItem == "ENG")
            {
                richTextBox1.Clear();
                var ocrengine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default);
                var img = Pix.LoadFromFile(textBox1.Text);
                var res = ocrengine.Process(img);
                richTextBox1.Text = res.GetText();
                if (res.GetText() == " \n" || string.IsNullOrEmpty(res.GetText()))
                {
                    MessageBox.Show("Ошибка!", "Текст не был обнаружен!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (comboBox1.SelectedItem == "RUS + ENG")
            {
                richTextBox1.Clear();
                var ocrengine = new TesseractEngine(@".\tessdata", "rus+eng", EngineMode.Default);
                var img = Pix.LoadFromFile(textBox1.Text);
                var res = ocrengine.Process(img);
                richTextBox1.Text = res.GetText();
                if (res.GetText() == " \n" || string.IsNullOrEmpty(res.GetText()))
                {
                    MessageBox.Show("Ошибка!", "Текст не был обнаружен!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            

            
            
            
            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension= true;
            dialog.DefaultExt = ".txt"; // Default file extension
            dialog.Filter = "Text documents (.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(dialog.FileName);
                label1.Text = "File succesfully saved!";
            }
            else
                label1.Text = "File was not saved";
        }
    }
}
