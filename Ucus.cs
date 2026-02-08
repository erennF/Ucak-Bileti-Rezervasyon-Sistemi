using System;
//Uçuş sınıfı, bir uçuşun temel özelliklerini ve işlevlerini temsil eder.
// Uçuş, kalkış noktası, varış noktası, uçuş süresi, kapı numarası, biniş saati, uçak kodu, fiyat ve havayolu şirketi gibi bilgileri içerir.
public class Ucus
{
    public string KalkisNoktasi { get; set; }
    public DateTime KalkısTarihi { get; set; }
    public string VarisNoktasi { get; set; }
    public DateTime VarisTarihi { get; set; }
    public TimeSpan UcusSuresi { get; set; }
    public string KapiNumarasi { get; set; }
    public string KapiKapanisSaati { get; set; }
    public int BinisSaati { get; set; }
    public int UcakKodu { get; set; }
    public decimal Fiyat { get; set; }
    public string HavayoluSirketi { get; set; }

    public Ucus(string kalkisNoktasi, DateTime kalkisTarihi, string varisNoktasi, DateTime varisTarihi, TimeSpan ucusSuresi, string kapiNumarasi, string kapiKapanisSaati, int binisSaati, decimal fiyat, string havayoluSirketi)
    {
        KalkisNoktasi = kalkisNoktasi;
        KalkısTarihi = kalkisTarihi;
        VarisNoktasi = varisNoktasi;
        VarisTarihi = varisTarihi;
        UcusSuresi = ucusSuresi;
        KapiNumarasi = kapiNumarasi;
        KapiKapanisSaati = kapiKapanisSaati;
        BinisSaati = binisSaati;
        Fiyat = fiyat;
        HavayoluSirketi = havayoluSirketi;
    }
    public void UçuşBilgileriniGoster()
    {
        Console.WriteLine($"Kalkış Noktası: {KalkisNoktasi}");
        Console.WriteLine($"Kalkış Tarihi: {KalkısTarihi}");
        Console.WriteLine($"Varış Noktası: {VarisNoktasi}");
        Console.WriteLine($"Varış Tarihi: {VarisTarihi}");
        Console.WriteLine($"Uçuş Süresi: {UcusSuresi}");
        Console.WriteLine($"Kapı Numarası: {KapiNumarasi}");
        Console.WriteLine($"Kapı Kapanış Saati: {KapiKapanisSaati}");
        Console.WriteLine($"Biniş Saati: {BinisSaati}");
        Console.WriteLine($"Fiyat: {Fiyat}");
        Console.WriteLine($"Havayolu Şirketi: {HavayoluSirketi}");
    }
    public void FiyatGuncelle(decimal yeniFiyat)
    {
        Fiyat = yeniFiyat;
    }
    public void UcusSuresiGuncelle(TimeSpan yeniUcusSuresi)
    {
        UcusSuresi = yeniUcusSuresi;
    }
    public void KapiNumarasiGuncelle(string yeniKapiNumarasi)
    {
        KapiNumarasi = yeniKapiNumarasi;
    }
    public void BinisSaatiGuncelle(int yeniBinisSaati)
    {
        BinisSaati = yeniBinisSaati;
    }
    public void KapiKapanisSaatiGuncelle(string yeniKapiKapanisSaati)
    {
        KapiKapanisSaati = yeniKapiKapanisSaati;
    }
    public void KalkisNoktasiGuncelle(string yeniKalkisNoktasi)
    {
        KalkisNoktasi = yeniKalkisNoktasi;
    }
    public void VarisNoktasiGuncelle(string yeniVarisNoktasi)
    {
        VarisNoktasi = yeniVarisNoktasi;
    }
    public void KalkisTarihiGuncelle(DateTime yeniKalkisTarihi)
    {
        KalkısTarihi = yeniKalkisTarihi;
    }
    public void VarisTarihiGuncelle(DateTime yeniVarisTarihi)
    {
        VarisTarihi = yeniVarisTarihi;
    }
}