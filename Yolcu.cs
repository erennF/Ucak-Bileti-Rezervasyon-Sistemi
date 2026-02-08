using System;
//Yolcu sınıfı, bir yolcunun temel özelliklerini ve işlevlerini temsil eder.
// Yolcu, bilet numarası, koltuk numarası, uçuş noktası ve varış noktası gibi bilgileri içerir.
// Yolcu sınıfı, Kullanıcı sınıfından türetilmiştir.
public class Yolcu : Kullanıcı
{
    public string BiletNumarasi { get; set; }
    public string KoltukNumarasi { get; set; }
    public string UcusNoktasi { get; set; }
    public string VarisNoktasi { get; set; }

    public Yolcu(string biletNumarasi, string koltukNumarasi, string ucusNoktasi, string varisNoktasi)
    {
        BiletNumarasi = biletNumarasi;
        KoltukNumarasi = koltukNumarasi;
        UcusNoktasi = ucusNoktasi;
        VarisNoktasi = varisNoktasi;
    }

    public void YolcuBilgileriniGoster()
    {
        BilgileriGoster();
        Console.WriteLine($"Bilet Numarası: {BiletNumarasi}");
        Console.WriteLine($"Koltuk Numarası: {KoltukNumarasi}");
        Console.WriteLine($"Uçuş Noktası: {UcusNoktasi}");
        Console.WriteLine($"Varış Noktası: {VarisNoktasi}");
    }

    // Güvenli bilgi getirme metodları
    public (string ad, string soyad) GetAdSoyad()
    {
        return (Ad, Soyad);
    }

    public (string eposta, string telefon) GetIletisimBilgileri()
    {
        return (Eposta, TelefonNumarasi);
    }

    public void KoltukNumarasiGuncelle(string yeniKoltukNumarasi)
    {
        KoltukNumarasi = yeniKoltukNumarasi;
    }

    public void UcusNoktasiGuncelle(string yeniUcusNoktasi)
    {
        UcusNoktasi = yeniUcusNoktasi;
    }

    public void VarisNoktasiGuncelle(string yeniVarisNoktasi)
    {
        VarisNoktasi = yeniVarisNoktasi;
    }
    public void AdSoyadGuncelle(string yeniAd, string yeniSoyad)
    {
        Ad = yeniAd;
        Soyad = yeniSoyad;
    }
    public void EpostaSifreGuncelle(string yeniEposta, string yeniŞifre)
    {
        Eposta = yeniEposta;
        Şifre = yeniŞifre;
    }
}