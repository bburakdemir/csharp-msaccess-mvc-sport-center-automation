using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SporSalonuOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            adminGiris adminGiris = new adminGiris();
            adminGiris.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kullanıcıPanel kullanıcıPaneli = new kullanıcıPanel();
            kullanıcıPaneli.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
