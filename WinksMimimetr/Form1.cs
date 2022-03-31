using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinksMimimetr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form evaluating = new Evaluating();
            evaluating.Left = this.Left;
            evaluating.Top = this.Top;
            evaluating.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Array buf ;
            SQLiteConnection con=new SQLiteConnection("Data Source=winks.db;Vetsion=3;");
            con.Open();


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                buf = ms.ToArray();


                //записываем массив байт в таблицу (в поле с типом массив байт)

                SQLiteCommand cmd = new SQLiteCommand("insert into winks(image,id) values(@photo,@id)", con);
                cmd.Parameters.Add("@photo", DbType.Binary, 8000).Value = buf;
                cmd.Parameters.Add("@id", DbType.Int32).Value = 6;
                cmd.ExecuteNonQuery();
                this.Close();
            }
            con.Close();*/
            

        }
    }
}
