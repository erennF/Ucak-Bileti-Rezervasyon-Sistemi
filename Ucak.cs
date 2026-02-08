using System;
//Uçak sınıfı, bir uçağın temel özelliklerini ve işlevlerini temsil eder.
// Uçak, model, plaka, kapasite, havayolu şirketi ve uçak kodu gibi bilgileri içerir.
public class Ucak
{
    public string Model { get; set; }
    public string Plaka { get; set; }
    public int Kapasite { get; set; }
    public string HavayoluSirketi { get; set; }
    public int UcakKodu { get; set; }

    public Ucak(string model, string plaka, int kapasite, string havayoluSirketi, int ucakKodu)
    {
        Model = model;
        Plaka = plaka;
        Kapasite = kapasite;
        HavayoluSirketi = havayoluSirketi;
        UcakKodu = ucakKodu;
    }

    public void UcakBilgileriniGoster()
    {
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Plaka: {Plaka}");
        Console.WriteLine($"Kapasite: {Kapasite}");
        Console.WriteLine($"Havayolu Şirketi: {HavayoluSirketi}");
        Console.WriteLine($"Uçak Kodu: {UcakKodu}");
    }

    public void KapasiteGuncelle(int yeniKapasite)
    {
        Kapasite = yeniKapasite;
    }

    public void HavayoluSirketiGuncelle(string yeniHavayoluSirketi)
    {
        HavayoluSirketi = yeniHavayoluSirketi;
    }

    public void DolulukDurumuGoster(int doluKoltukSayisi)
    {
        Console.WriteLine($"Uçağın Doluluk Durumu: {doluKoltukSayisi}/{Kapasite}");
    }

}
