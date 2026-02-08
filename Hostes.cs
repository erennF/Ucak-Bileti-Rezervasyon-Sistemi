using System;
using uçakSistemi;
// Hostes sınıfı, Kullanıcı sınıfından türetilmiştir ve hosteslere özgü özellikler ve yöntemler içerir.
public class Hostes : Kullanıcı
{
    public string HostesNumarasi { get; set; }
    public int UcusSayisi { get; set; }
    public string DilBecerileri { get; set; }

    public Hostes(string hostesNumarasi, int ucusSayisi, string dilBecerileri)
    {
        HostesNumarasi = hostesNumarasi;
        UcusSayisi = ucusSayisi;
        DilBecerileri = dilBecerileri;
    }

    public void HostesBilgileriniGoster()
    {
        BilgileriGoster();
        Console.WriteLine($"Hostes Numarası: {HostesNumarasi}");
        Console.WriteLine($"Uçuş Sayısı: {UcusSayisi}");
        Console.WriteLine($"Dil Becerileri: {DilBecerileri}");
    }

    public void UcusSayisiEkle(int sayi)
    {
        UcusSayisi += sayi;
    }

    public void DilBecerileriGuncelle(string yeniDiller)
    {
        DilBecerileri = yeniDiller;
    }
}