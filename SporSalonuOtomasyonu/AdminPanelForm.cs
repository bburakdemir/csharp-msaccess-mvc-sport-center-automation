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
using EntityLayer;
using BusinessLayer;
using System.Security.Policy;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iText.Forms;


namespace SporSalonuOtomasyonu
{
    public partial class AdminPanelForm : Form
    {

        private KullaniciManager _kullaniciManager;
        public AdminPanelForm()
        {
            InitializeComponent();
            _kullaniciManager = new KullaniciManager();  // Yeni nesne oluşturuluyor
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string adSoyad = textBox1.Text;  // Ad ve Soyad textbox'ı
            string telefon = textBox2.Text;  // Telefon textbox'ı
            string kayitTarihi = textBox4.Text;  // Kayıt tarihi dateTimePicker'dan
            
            string kullaniciAdi = textBox3.Text;
            string sifre = textBox5.Text;
            string mail = textBox6.Text;
            string yetki = textBox7.Text;



            try
            {
                // Kullanıcıyı veritabanına kaydetmek için KullaniciManager'dan metodu çağır
                _kullaniciManager.KayitUser(adSoyad, telefon, kayitTarihi);
                _kullaniciManager.Kayit(kullaniciAdi, sifre, mail, yetki);

                // Başarılı işlem sonrası kullanıcıya mesaj göster
                MessageBox.Show("Üye başarıyla kaydedildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox3.Text;
            string telefon = textBox2.Text;

            try
            {
                // Kullanıcıyı veritabanına kaydetmek için KullaniciManager'dan metodu çağır
                _kullaniciManager.Sil(kullaniciAdi);
                _kullaniciManager.Sil1(telefon);

                // Başarılı işlem sonrası kullanıcıya mesaj göster
                MessageBox.Show("Üye başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya mesaj göster
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Access veritabanı bağlantı yolu
            
            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ASUS\source\repos\SporSalonuOtomasyonu\SporSalonuOtomasyonu\bin\Debug\sporSalonu.accdb";
            string query = "SELECT * FROM Kayitlar";

            DataTable dataTable = new DataTable();

            // Access veritabanından veri çekme
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Veritabanına bağlanırken hata oluştu: {ex.Message}");
                    return;
                }
            }


            // PDF oluşturma
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Kayitlar.pdf");

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    Document pdfDoc = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Başlık ekle
                    Paragraph title = new Paragraph("Kayitlar Tablosu\n\n", iTextSharp.text.FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD));
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    // Tablo oluştur
                    PdfPTable pdfTable = new PdfPTable(dataTable.Columns.Count);
                    pdfTable.WidthPercentage = 100;

                    // Tablo başlıklarını ekle
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, iTextSharp.text.FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD)));
                        cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        pdfTable.AddCell(cell);
                    }

                    // Tablo verilerini ekle
                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            pdfTable.AddCell(new Phrase(item.ToString(), iTextSharp.text.FontFactory.GetFont("Arial", 10)));
                        }
                    }

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                   MessageBox.Show("PDF başarıyla oluşturuldu! Masaüstünüzde Kayitlar.pdf olarak kaydedildi.");
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PDF oluşturulurken bir hata oluştu: {ex.Message}");
            }

        }
    }
}
