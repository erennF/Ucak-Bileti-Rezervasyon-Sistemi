using System;
using System.Data;
// Kullanıcı sınıfı, uçak sistemi için temel kullanıcı bilgilerini ve işlevlerini içerir.
// Tüm kullanıcı türleri (yolcu, pilot, hostes vb.) bu sınıftan türetilmiştir.
public class Kullanıcı
{
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string Eposta { get; set; }
    public string TelefonNumarasi { get; set; }
    public string Şifre { get; set; }
    public DateTime KayitTarihi { get; set; }
    public bool AktifMi { get; set; }
    public string Role { get; set; }

    public Kullanıcı(string ad = "", string soyad = "", string eposta = "", string telefonNumarasi = "", string sifre = "", string role = "")
    {
        Ad = ad;
        Soyad = soyad;
        Eposta = eposta;
        TelefonNumarasi = telefonNumarasi;
        Şifre = sifre;
        KayitTarihi = DateTime.Now;
        AktifMi = true;
        Role = role;
    }

    public void BilgileriGoster()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Ad: {Ad}");
        Console.WriteLine($"Soyad: {Soyad}");
        Console.WriteLine($"Eposta: {Eposta}");
        Console.WriteLine($"Telefon Numarası: {TelefonNumarasi}");
        Console.WriteLine($"Kayıt Tarihi: {KayitTarihi}");
        Console.WriteLine($"Aktif Mi: {AktifMi}");
        Console.WriteLine($"Role: {Role}");
    }

    public void BilgileriGuncelle(string ad, string soyad, string eposta, string telefonNumarasi, string şifre)
    {
        Ad = ad;
        Soyad = soyad;
        Eposta = eposta;
        this.TelefonNumarasi = telefonNumarasi;
        Şifre = şifre;
    }

    public void HesapKapat()
    {
        AktifMi = false;
    }

    public bool GirisYap(string eposta, string şifre)
    {
        if (string.IsNullOrEmpty(Eposta) || string.IsNullOrEmpty(Şifre))
            return false;
        return string.Equals(Eposta, eposta, StringComparison.OrdinalIgnoreCase) && string.Equals(Şifre, şifre, StringComparison.Ordinal);
    }
    public bool SifreDogrula(string sifre)
    {
        if (string.IsNullOrEmpty(Şifre)) return false;
        return string.Equals(Şifre, sifre, StringComparison.Ordinal);
    }
    public bool SifreDegistir(string eskiSifre, string yeniSifre)
    {
        if (!SifreDogrula(eskiSifre)) return false;
        if (string.IsNullOrWhiteSpace(yeniSifre) || yeniSifre.Length < 6)
            throw new ArgumentException("Yeni şifre en az 6 karakter olmalıdır.", nameof(yeniSifre));
        Şifre = yeniSifre;
        return true;
    }
}