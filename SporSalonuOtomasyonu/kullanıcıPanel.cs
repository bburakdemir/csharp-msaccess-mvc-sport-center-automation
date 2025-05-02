using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SporSalonuOtomasyonu
{
    public partial class kullanıcıPanel : Form
    {
        private KullaniciManager _kullaniciManager;
        public kullanıcıPanel()
        {
            InitializeComponent();
            _kullaniciManager = new KullaniciManager();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tarih = textBox1.Text;
            int tutar = int.Parse(textBox2.Text);
            int kullanıcııd = int.Parse(textBox3.Text);

            try
            {
                
                _kullaniciManager.OdemeKayit(tarih, tutar, kullanıcııd);
                

               
                MessageBox.Show("Ödeme başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kullanıcıPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
