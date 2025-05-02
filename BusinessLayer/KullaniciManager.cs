using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;


namespace BusinessLayer
{
    public class KullaniciManager
    {
        private KullaniciDal _kullaniciDal = new KullaniciDal();

        public Kullanici ValidateUser(string kullaniciAdi, string sifre)
        {
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
                return null;

            return _kullaniciDal.GetUser(kullaniciAdi, sifre);
        }


        public void KayitUser(string adSoyad, string telefon, string kayitTarihi)
        {
            if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(kayitTarihi))
            {
                // Parametrelerin eksik olduğu durumda bir exception veya başka bir işlem yapılabilir.
                throw new ArgumentException("Tüm alanlar doldurulmalıdır.");
            }

            try
            {
                // Kullanıcı kaydını veritabanına ekleme işlemi
                _kullaniciDal.AddKullanici(adSoyad, telefon, kayitTarihi);
            }
            catch (Exception ex)
            {
                // Hata oluştuğunda bir mesaj yazdırabilir veya loglayabilirsiniz.
                Console.WriteLine("Hata oluştu: " + ex.Message);
                throw new Exception("Kullanıcı kaydedilemedi.");
            }
        }

        public void Kayit(string kullaniciAdi, string sifre, string mail, string yetki)
        {
            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(yetki))
            {
                throw new ArgumentException("tüm alanlar doldurulmalıdır");

            }
            try
            {
                _kullaniciDal.Add(kullaniciAdi, sifre, mail, yetki);
            }
            catch (Exception ex)
            {
                Console.WriteLine("hata oluştu: " + ex.Message);
                throw new Exception("kullanıcı kaydedilemedi");
            }
        }
        public void Sil(string kullaniciAdi)
        {
            if (string.IsNullOrEmpty(kullaniciAdi))
            {
                throw new ArgumentException("Lütfen silinecek kullanıcının adını giriniz.");
            }
            try
            {
                _kullaniciDal.Del(kullaniciAdi);
            }
            catch (Exception ex)
            {
                Console.WriteLine("hata oluştu: " + ex.Message);
                throw new Exception("kullanıcı silinemedi");
            }

        }
        public void Sil1(string telefon)
        {
            if (string.IsNullOrEmpty(telefon))
            {
                throw new ArgumentException("Lütfen silinecek kullanıcının telefon numarasını giriniz.");
            }
            try
            {
                _kullaniciDal.Del1(telefon);
            }
            catch (Exception ex)
            {
                Console.WriteLine("hata oluştu: " + ex.Message);
                throw new Exception("kullanıcı silinemedi");
            }
        }

        public void OdemeKayit(string tarih, int tutar, int kullanıcııd)
        {
            if (string.IsNullOrEmpty(tarih))
            {
                throw new ArgumentException("tüm alanlar doldurulmalıdır");

            }
            try
            {
                _kullaniciDal.odemeAdd(tarih, tutar, kullanıcııd);
            }
            catch (Exception ex)
            {
                Console.WriteLine("hata oluştu: " + ex.Message);
                throw new Exception("kullanıcı kaydedilemedi");
            }
        }


    }
}


