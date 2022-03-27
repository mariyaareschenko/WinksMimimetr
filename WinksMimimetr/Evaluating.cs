using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WinksMimimetr
{
    public partial class Evaluating : Form
    {
        public Evaluating()
        {
            InitializeComponent();
            choosenWinks = null;
        }
        Form1 currentForm;
        
        static Dictionary<Image, int> winks;
        static List<Image> images;
        public Evaluating(Form1 form)
        {
            InitializeComponent();
            currentForm =form;
            choosenWinks = null;
        }
        static int  currentWinks1;
        static int currentWinks2;
        static int? choosenWinks;

        private Image GetImage(int id)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=winks.db;Version=3;");         
            SQLiteCommand cmd = new SQLiteCommand($"select image from winks where id={id}", con);
            con.Open();
            SQLiteDataReader dr;
            Image img=null;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!dr.IsDBNull(0))
                {
                    byte[] im = (byte[])dr["image"];
                    MemoryStream ms = new MemoryStream(im);
                    img = Image.FromStream(ms);                    
                }
                else
                {
                    img = null;
                }
            }
            con.Close();
            return img;
        }
        private void Evaluating_Load(object sender, EventArgs e)
        {
            int count=6;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            images = new List<Image>();
            winks = new Dictionary<Image, int>();//value-кол-во голосов
            for (int i = 0; i < count; i++)
            {
                images.Add(GetImage(i+1));
            }
            for (int i = 0; i <count ; i++)
            {
                winks.Add(images[i],0);
            }
            
            ChooseRandomImg();
        }
        private void ChooseRandomImg()
        {
            Random random = new Random();
            currentWinks1 = random.Next(images.Count);
            currentWinks2 = random.Next(images.Count);
            while (currentWinks2 == currentWinks1)
            {
                currentWinks1 = random.Next(images.Count);
                currentWinks2 = random.Next(images.Count);
            }
            pictureBox1.Image = GetImage(currentWinks1);
            pictureBox2.Image = GetImage(currentWinks2);
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (choosenWinks == null)
            {
                MessageBox.Show("Вы не выбрали Винкс!");
            }
            else
            {
                winks[images[choosenWinks.Value]]++;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            choosenWinks = currentWinks1;
            panel1.BackColor = Color.Pink;
            panel2.BackColor = DefaultBackColor;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            choosenWinks = currentWinks2;
            panel2.BackColor = Color.Pink;
            panel1.BackColor = DefaultBackColor;
        }
    }
}
