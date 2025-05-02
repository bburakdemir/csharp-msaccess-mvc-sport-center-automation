using EntityLayer;
using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Policy;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace DataAccessLayer
{
    public class KullaniciDal
    {
        // Veritabanı bağlantı dizesi


        private string _connectionString;
        public KullaniciDal()
        {
            // Uygulamanın ana dizinini almak
            string directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            // Veritabanı dosyasının tam yolu
            string dbPath = Path.Combine(directory, "sporSalonu.accdb");

            // Bağlantı dizesini oluşturuyoruz
            _connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath}";
        }

        /// <summary>
        /// Kullanıcı adı ve şifreye göre kullanıcıyı döner.
        /// </summary>
        /// <param name="kullaniciAdi">Kullanıcı adı</param>
        /// <param name="sifre">Şifre</param>
        /// <returns>Kullanıcı nesnesi ya da null</returns>

        public void AddKullanici(string adSoyad, string telefon, string kayitTarihi)
        {
            OleDbConnection connection = new OleDbConnection(_connectionString);

            try
            {
                connection.Open();
                string query = "INSERT INTO Kayitlar (AdSoyad, Telefon, KayitTarihi) VALUES (?, ?, ?)";
                
                OleDbCommand command = new OleDbCommand(query, connection);
                
                
               
                
                command.Parameters.AddWithValue("?", adSoyad);
                command.Parameters.AddWithValue("?", telefon);
                command.Parameters.AddWithValue("?", kayitTarihi);

                command.ExecuteNonQuery();

                

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void Add(string kullaniciAdi,string sifre, string mail, string yetki)
        {
            OleDbConnection connection = new OleDbConnection(_connectionString);
            try
            {
                connection.Open();
                string query2 = "INSERT INTO Kullanicilar (KullaniciAdi, Sifre, Email, Yetki) VALUES(?,?,?,?)";

                OleDbCommand command2 = new OleDbCommand(query2, connection);

                command2.Parameters.AddWithValue ("?", kullaniciAdi);
                command2.Parameters.AddWithValue("?", sifre);
                command2.Parameters.AddWithValue("?", mail);
                command2.Parameters.AddWithValue("?", yetki);

                command2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            
        }

        public void Del(string kullaniciAdi)
        {
            OleDbConnection connection = new OleDbConnection(_connectionString);
            try
            {
                connection.Open();
                string query3 = "DELETE FROM Kullanicilar WHERE KullaniciAdi=?";
                OleDbCommand command3= new OleDbCommand(query3, connection);
                command3.Parameters.AddWithValue("?", kullaniciAdi);

                command3.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void Del1(string telefon)
        {
            OleDbConnection connection = new OleDbConnection(_connectionString);
            try
            {
                connection.Open();
                string query4 = "DELETE FROM Kayitlar WHERE Telefon=?";
                OleDbCommand command4 = new OleDbCommand(query4, connection);
                command4.Parameters.AddWithValue("?", telefon);

                command4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public Kullanici GetUser(string kullaniciAdi, string sifre)
        {
            // Veritabanı bağlantısı için OleDbConnection nesnesi oluştur
            OleDbConnection connection = new OleDbConnection(_connectionString);

            try
            {
                // Bağlantıyı aç
                connection.Open();


                
                // Sorgu tanımlama
                string query = "SELECT * FROM Kullanicilar WHERE KullaniciAdi = ? AND Sifre = ?";

                // OleDbCommand nesnesi oluştur
                OleDbCommand command = new OleDbCommand(query, connection);

                // Parametreleri ekle
                command.Parameters.AddWithValue("?", kullaniciAdi);
                command.Parameters.AddWithValue("?", sifre);

                // Sorguyu çalıştır ve sonuçları oku
                OleDbDataReader reader = command.ExecuteReader();

                // Eğer sonuç varsa kullanıcı nesnesi oluştur ve döndür
                if (reader.Read())
                {
                    return new Kullanici
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("ID")),
                        KullaniciAdi = reader.GetString(reader.GetOrdinal("KullaniciAdi")),
                        Sifre = reader.GetString(reader.GetOrdinal("Sifre")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Yetki = reader.GetString(reader.GetOrdinal("Yetki"))
                    };
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }
            finally
            {
                // Bağlantıyı kapat
                if (connection.State == System.Data.ConnectionState.Open)
                {       
                    connection.Close();
                }
            }
            // Hiçbir sonuç bulunamazsa null döndür
            return null;
        }

        public void odemeAdd(string tarih, int tutar, int kullanıcııd)
        {
            OleDbConnection connection = new OleDbConnection(_connectionString);
            try
            {
                connection.Open();
                string query2 = "INSERT INTO Odeme (KullaniciID, Tutar, OdemeTarihi) VALUES(?,?,?)";

                OleDbCommand command2 = new OleDbCommand(query2, connection);

                command2.Parameters.AddWithValue("?", kullanıcııd);
                command2.Parameters.AddWithValue("?", tutar);
                command2.Parameters.AddWithValue("?", tarih);
               

                command2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("bir hata oluştu: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }



    }
}

