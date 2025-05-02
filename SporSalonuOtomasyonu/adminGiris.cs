using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using System.Data.OleDb;

namespace SporSalonuOtomasyonu
{
    public partial class adminGiris : Form
    {
        public adminGiris()
        {
            InitializeComponent();
        }

        private void adminGiris_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_MouseClick(object sender, MouseEventArgs e)
        {
            sifreSıfırlama sifreSıfırlama = new sifreSıfırlama();
            sifreSıfırlama.Show();  
            this.Hide();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            
                // Kullanıcı adı ve şifre girişlerini al
                string KullaniciAdi = textBox1.Text;
                string Sifre = textBox2.Text;

                // Girişlerin boş olup olmadığını kontrol et
                if (string.IsNullOrEmpty(KullaniciAdi) || string.IsNullOrEmpty(Sifre))
                {
                    MessageBox.Show("Lütfen kullanıcı adı ve şifreyi girin.");
                    return;
                }
                
                // Kullanıcı doğrulama işlemi
                KullaniciManager kullaniciManager = new KullaniciManager();
                Kullanici kullanici = kullaniciManager.ValidateUser(KullaniciAdi, Sifre);

                // Kullanıcı doğrulama sonucunu kontrol et
                if (kullanici == null)
                {
                    MessageBox.Show("Geçersiz kullanıcı adı veya şifre.");
                    return;
                }

                // Kullanıcı admin mi kontrol et    
                if (kullanici.KullaniciAdi == "admin" && kullanici.Sifre == "admin123")
                {
                    MessageBox.Show("Giriş başarılı! Admin paneline yönlendiriliyorsunuz.");
                    AdminPanelForm adminPanel = new AdminPanelForm();
                    adminPanel.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show($"Giriş başarılı! Hoş geldiniz, {kullanici.KullaniciAdi}.");
                    AdminPanelForm kullaniciPanel = new AdminPanelForm();
                    kullaniciPanel.Show();
                    this.Hide();
                }
            

        }
    }
}
