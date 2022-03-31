using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinksMimimetr
{
    public partial class Rating : Form
    {
        

        public Rating()
        {
            InitializeComponent();
        }

        private void Rating_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form form = Application.OpenForms[0];
            form.StartPosition = FormStartPosition.Manual;
            form.Left = this.Left;
            form.Top = this.Top;
            form.Show();
        }
    }
}
