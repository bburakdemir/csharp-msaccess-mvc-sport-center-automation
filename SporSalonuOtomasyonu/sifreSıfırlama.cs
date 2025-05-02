using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using BusinessLayer;
using System.Data.OleDb;

namespace SporSalonuOtomasyonu
{
    public partial class sifreSıfırlama : Form
    {
        private KullaniciManager _kullaniciManager;
        public sifreSıfırlama()
        {
            InitializeComponent();
            _kullaniciManager = new KullaniciManager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

           
            string email = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Lütfen bir e-posta adresi giriniz.");
                return;
            }

            // 1. Veritabanından Şifreyi Al
            string password = GetPasswordFromDatabase(email);
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Bu e-posta adresiyle eşleşen bir kullanıcı bulunamadı.");
                return;
            }

            // 2. E-Posta Gönder
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("sportcenterautomation@gmail.com"); // Gönderici e-posta adresi
                mail.To.Add(email); // Alıcı e-posta adresi
                mail.Subject = "Şifre Hatırlatma";
                mail.Body = $"Merhaba,\n\nŞifreniz: {password}\n\nİyi günler dileriz.";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com"); // SMTP sunucusu
                smtp.Port = 587; // SMTP portu (gMail için 587)
                smtp.Credentials = new NetworkCredential("sportcenterautomation@gmail.com", "jrrz saog kwtb puzs"); // Gönderici kimlik bilgileri
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("Şifre başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"E-posta gönderme hatası: {ex.Message}");
            }
        }
        private string GetPasswordFromDatabase(string email)
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ASUS\source\repos\SporSalonuOtomasyonu\SporSalonuOtomasyonu\bin\Debug\sporSalonu.accdb";
            string password = "";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Sifre FROM Kullanicilar WHERE Email = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            password = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı hatası: {ex.Message}");
                }
            }

            return password;
        }

    }
}

