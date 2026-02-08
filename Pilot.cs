using System;
//Pilot sınıfı, Kullanıcı sınıfından türetilmiştir ve pilotlara özgü özellikler ve yöntemler içerir.
public class Pilot : Kullanıcı
{
    public string LisansNumarasi { get; set; }
    public int UcusSaati { get; set; }
    public string Rutbe { get; set; }

    public Pilot(string lisansNumarasi, int ucusSaati, string rutbe)
    {
        LisansNumarasi = lisansNumarasi;
        UcusSaati = ucusSaati;
        Rutbe = rutbe;
    }

    public void PilotBilgileriniGoster()
    {
        BilgileriGoster();
        Console.WriteLine($"Lisans Numarası: {LisansNumarasi}");
        Console.WriteLine($"Uçuş Saati: {UcusSaati}");
        Console.WriteLine($"Rütbe: {Rutbe}");

    }

    public void UcusSaatiEkle(int saat)
    {
        UcusSaati += saat;
    }

    public void RutbeGuncelle(string yeniRutbe)
    {
        Rutbe = yeniRutbe;
    }
}