using System;
using uçakSistemi;

namespace uçakSistemi
{
    public class Rezervasyon
    {
        public Ucus Ucus { get; private set; }
        public Yolcu Yolcu { get; private set; }
        public string BiletNumarasi { get; private set; }
        public DateTime RezervasyonTarihi { get; private set; }
        public string KoltukNumarasi { get; private set; }

        public Rezervasyon(Ucus ucus, Yolcu yolcu, string biletNumarasi, DateTime rezervasyonTarihi, string koltukNumarasi)
        {
            Ucus = ucus ?? throw new ArgumentNullException(nameof(ucus));
            Yolcu = yolcu ?? throw new ArgumentNullException(nameof(yolcu));
            BiletNumarasi = biletNumarasi;
            RezervasyonTarihi = rezervasyonTarihi;
            KoltukNumarasi = koltukNumarasi;
        }

        public void RezervasyonBilgileriniGoster()
        {
            Ucus.UçuşBilgileriniGoster();
            Yolcu.YolcuBilgileriniGoster();
            Console.WriteLine($"Bilet Numarası: {BiletNumarasi}");
            Console.WriteLine($"Rezervasyon Tarihi: {RezervasyonTarihi}");
            Console.WriteLine($"Koltuk Numarası: {KoltukNumarasi}");
        }

        public void KoltukNumarasiGuncelle(string yeniKoltukNumarasi)
        {
            KoltukNumarasi = yeniKoltukNumarasi;
        }

        public void RezervasyonTarihiGuncelle(DateTime yeniRezervasyonTarihi)
        {
            RezervasyonTarihi = yeniRezervasyonTarihi;
        }

        public void BiletNumarasiGuncelle(string yeniBiletNumarasi)
        {
            BiletNumarasi = yeniBiletNumarasi;
        }

        public void YolcuBilgileriGuncelle(string yeniYolcuAdi, string yeniYolcuSoyadi)
        {
            Yolcu.AdSoyadGuncelle(yeniYolcuAdi, yeniYolcuSoyadi);
        }
    }
}