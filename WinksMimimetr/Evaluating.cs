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
        static int firstInd;
        static int secondInd;
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
            firstInd = 5;
            secondInd = 6;///////////////////////////////////
            Choose(firstInd,secondInd);
        }
        private void Choose(int firstInd,int secondIndex)
        {
           
           currentWinks1 = firstInd;
           currentWinks2 = secondIndex;
           
           pictureBox1.Image = GetImage(currentWinks1);
           pictureBox2.Image = GetImage(currentWinks2);

            
        }
        private void nextButton_Click(object sender, EventArgs e)
        {
            if (choosenWinks == null)
            {
                MessageBox.Show("Вы не выбрали фею!");
            }
            else
            {
                winks[images[choosenWinks.Value-1]]++;
                choosenWinks = null;
                panel1.BackColor = BackColor;
                panel2.BackColor = BackColor;
                if(secondInd<7 && firstInd < 6)
                {
                    if (secondInd < 6)
                    {
                        secondInd++;
                    }
                    else
                    {
                        firstInd++;
                        secondInd = firstInd + 1;
                    }
                    
                    Choose(firstInd, secondInd);
                }
                if(secondInd==7 && firstInd == 6)
                {
                    Rating rating = new Rating();
                    rating.Left = this.Left;
                    rating.Top = this.Top;
                    rating.Show();
                    winks = winks.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                    var firstWinks = winks.ElementAt(5).Key;
                    var secondWinks = winks.ElementAt(4).Key;
                    var thirdWinks = winks.ElementAt(3).Key;
                    rating.pictureBox1.Image = firstWinks;
                    rating.pictureBox2.Image = secondWinks;
                    rating.pictureBox3.Image = thirdWinks;
                    Close();
                }
                
               
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            choosenWinks = currentWinks1;
            panel1.BackColor = Color.DeepPink;
            panel2.BackColor = BackColor;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            choosenWinks = currentWinks2;
            panel2.BackColor = Color.DeepPink;
            panel1.BackColor = BackColor;
        }

        //private void Evaluating_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    Form form1 = Application.OpenForms[0];
        //    form1.StartPosition = FormStartPosition.Manual;
        //    form1.Left = this.Left;
        //    form1.Top = this.Top;
        //    form1.Show();
        //}
    }
}
