using System;
using System.Data;
using System.Data.SqlClient;

namespace uçakSistemi
{
    /// <summary>
    /// Dinamik fiyatlandırma sistemi
    /// Doluluk oranı, uçuşa kalan gün, koltuk tipi, sezon ve indirim koduna göre fiyat hesaplar
    /// </summary>
    public static class DinamikFiyatlandirma
    {
        /// <summary>
        /// Uçuş için dinamik fiyat hesaplar
        /// </summary>
        public static decimal FiyatHesapla(int ucusId, string koltukTipi = "Ekonomi", string indirimKodu = null)
        {
            try
            {
                // Uçuş bilgilerini al
                string query = @"SELECT Fiyat, BosKoltukSayisi, KalkisTarihi, 
                               (SELECT Kapasite FROM Ucaklar WHERE UcakKodu = Ucuslar.UcakKodu) AS Kapasite
                               FROM Ucuslar 
                               WHERE UcusId = @UcusId";

                SqlParameter[] parameters = {
                    new SqlParameter("@UcusId", ucusId)
                };

                DataTable dt = VeritabaniHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                    throw new Exception("Uçuş bulunamadı!");

                DataRow row = dt.Rows[0];
                decimal baseFiyat = Convert.ToDecimal(row["Fiyat"]);
                int bosKoltuk = Convert.ToInt32(row["BosKoltukSayisi"]);
                int kapasite = row["Kapasite"] != DBNull.Value ? Convert.ToInt32(row["Kapasite"]) : 200;
                DateTime kalkisTarihi = Convert.ToDateTime(row["KalkisTarihi"]);

                // 1. Doluluk Oranı Katsayısı
                decimal dolulukKatsayisi = DolulukKatsayisiHesapla(bosKoltuk, kapasite);

                // 2. Tarih Katsayısı (uçuşa kalan gün)
                decimal tarihKatsayisi = TarihKatsayisiHesapla(kalkisTarihi);

                // 3. Koltuk Tipi Katsayısı
                decimal koltukTipiKatsayisi = KoltukTipiKatsayisiHesapla(koltukTipi);

                // 4. Sezon Katsayısı
                decimal sezonKatsayisi = SezonKatsayisiHesapla(kalkisTarihi);

                // Toplam fiyat hesapla
                decimal toplamFiyat = baseFiyat * dolulukKatsayisi * tarihKatsayisi * koltukTipiKatsayisi * sezonKatsayisi;

                // 5. İndirim Kodu Uygula
                if (!string.IsNullOrWhiteSpace(indirimKodu))
                {
                    decimal indirimOrani = IndirimKoduKontrol(indirimKodu);
                    if (indirimOrani > 0)
                    {
                        toplamFiyat = toplamFiyat * (1 - indirimOrani / 100);
                    }
                }

                // Minimum fiyat kontrolü (base fiyatın %50'sinden az olmasın)
                decimal minFiyat = baseFiyat * 0.5m;
                if (toplamFiyat < minFiyat)
                    toplamFiyat = minFiyat;

                return Math.Round(toplamFiyat, 2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Fiyat hesaplama hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Doluluk oranına göre katsayı hesaplar
        /// Uçak doluysa fiyat artar, boşsa azalır
        /// </summary>
        private static decimal DolulukKatsayisiHesapla(int bosKoltuk, int kapasite)
        {
            if (kapasite == 0) return 1.0m;

            decimal dolulukOrani = (decimal)(kapasite - bosKoltuk) / kapasite * 100;

            if (dolulukOrani >= 90) return 1.5m;      // %90+ dolu -> %50 zam
            else if (dolulukOrani >= 80) return 1.35m; // %80-90 dolu -> %35 zam
            else if (dolulukOrani >= 70) return 1.25m; // %70-80 dolu -> %25 zam
            else if (dolulukOrani >= 60) return 1.15m; // %60-70 dolu -> %15 zam
            else if (dolulukOrani >= 50) return 1.05m; // %50-60 dolu -> %5 zam
            else if (dolulukOrani >= 30) return 1.0m;  // %30-50 dolu -> Normal fiyat
            else if (dolulukOrani >= 20) return 0.95m; // %20-30 dolu -> %5 indirim
            else return 0.85m;                         // %20- dolu -> %15 indirim
        }

        /// <summary>
        /// Uçuşa kalan güne göre katsayı hesaplar
        /// Erken rezervasyon = ucuz, son dakika = pahalı
        /// </summary>
        private static decimal TarihKatsayisiHesapla(DateTime kalkisTarihi)
        {
            TimeSpan kalanSure = kalkisTarihi - DateTime.Now;
            int kalanGun = (int)kalanSure.TotalDays;

            if (kalanGun < 0) return 1.0m;              // Geçmiş uçuş
            else if (kalanGun <= 1) return 1.8m;        // 1 gün kala -> %80 zam (son dakika)
            else if (kalanGun <= 3) return 1.5m;        // 3 gün kala -> %50 zam
            else if (kalanGun <= 7) return 1.3m;        // 1 hafta kala -> %30 zam
            else if (kalanGun <= 14) return 1.1m;       // 2 hafta kala -> %10 zam
            else if (kalanGun <= 30) return 1.0m;       // 1 ay kala -> Normal fiyat
            else if (kalanGun <= 60) return 0.9m;       // 2 ay kala -> %10 indirim (erken rezervasyon)
            else return 0.8m;                           // 2+ ay kala -> %20 indirim
        }

        /// <summary>
        /// Koltuk tipine göre katsayı hesaplar
        /// </summary>
        private static decimal KoltukTipiKatsayisiHesapla(string koltukTipi)
        {
            switch (koltukTipi?.ToLower())
            {
                case "ekonomi":
                    return 1.0m;        // Normal fiyat
                case "business":
                case "iş sınıfı":
                    return 2.5m;        // %150 zam
                case "first class":
                case "birinci sınıf":
                    return 4.0m;        // %300 zam
                default:
                    return 1.0m;
            }
        }

        /// <summary>
        /// Sezona göre katsayı hesaplar
        /// </summary>
        private static decimal SezonKatsayisiHesapla(DateTime kalkisTarihi)
        {
            int ay = kalkisTarihi.Month;
            int gun = kalkisTarihi.Day;

            // Yaz sezonu (Haziran-Ağustos)
            if (ay >= 6 && ay <= 8)
                return 1.3m;    // %30 zam

            // Yılbaşı ve Ramazan Bayramı dönemleri
            if ((ay == 12 && gun >= 20) || (ay == 1 && gun <= 10))
                return 1.4m;    // %40 zam

            // Kurban Bayramı (yaklaşık Temmuz)
            if (ay == 7 && gun >= 15 && gun <= 25)
                return 1.35m;   // %35 zam

            // Yarıyıl tatili (Şubat)
            if (ay == 2)
                return 1.2m;    // %20 zam

            // Diğer aylar
            return 1.0m;        // Normal fiyat
        }

        /// <summary>
        /// İndirim kodunu kontrol eder ve indirim oranını döner
        /// </summary>
        private static decimal IndirimKoduKontrol(string indirimKodu)
        {
            try
            {
                string query = @"SELECT IndirimOrani 
                               FROM IndirimKodlari 
                               WHERE Kod = @Kod 
                               AND AktifMi = 1 
                               AND GETDATE() BETWEEN GecerlilikBaslangic AND GecerlilikBitis";

                SqlParameter[] parameters = {
                    new SqlParameter("@Kod", indirimKodu)
                };

                object result = VeritabaniHelper.ExecuteScalar(query, parameters);

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToDecimal(result);
                }

                return 0;
            }
            catch
            {
                return 0; // Hata durumunda indirim uygulanmaz
            }
        }

        /// <summary>
        /// Fiyat detaylarını açıklayan rapor döner
        /// </summary>
        public static string FiyatDetayRaporu(int ucusId, string koltukTipi = "Ekonomi", string indirimKodu = null)
        {
            try
            {
                // Uçuş bilgilerini al
                string query = @"SELECT Fiyat, BosKoltukSayisi, KalkisTarihi, 
                               (SELECT Kapasite FROM Ucaklar WHERE UcakKodu = Ucuslar.UcakKodu) AS Kapasite
                               FROM Ucuslar 
                               WHERE UcusId = @UcusId";

                SqlParameter[] parameters = {
                    new SqlParameter("@UcusId", ucusId)
                };

                DataTable dt = VeritabaniHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                    return "Uçuş bulunamadı!";

                DataRow row = dt.Rows[0];
                decimal baseFiyat = Convert.ToDecimal(row["Fiyat"]);
                int bosKoltuk = Convert.ToInt32(row["BosKoltukSayisi"]);
                int kapasite = row["Kapasite"] != DBNull.Value ? Convert.ToInt32(row["Kapasite"]) : 200;
                DateTime kalkisTarihi = Convert.ToDateTime(row["KalkisTarihi"]);

                decimal dolulukKatsayisi = DolulukKatsayisiHesapla(bosKoltuk, kapasite);
                decimal tarihKatsayisi = TarihKatsayisiHesapla(kalkisTarihi);
                decimal koltukTipiKatsayisi = KoltukTipiKatsayisiHesapla(koltukTipi);
                decimal sezonKatsayisi = SezonKatsayisiHesapla(kalkisTarihi);

                decimal toplamFiyat = baseFiyat * dolulukKatsayisi * tarihKatsayisi * koltukTipiKatsayisi * sezonKatsayisi;

                decimal indirimOrani = 0;
                if (!string.IsNullOrWhiteSpace(indirimKodu))
                {
                    indirimOrani = IndirimKoduKontrol(indirimKodu);
                    if (indirimOrani > 0)
                    {
                        toplamFiyat = toplamFiyat * (1 - indirimOrani / 100);
                    }
                }

                int dolulukYuzde = (int)((kapasite - bosKoltuk) * 100.0 / kapasite);
                int kalanGun = (int)(kalkisTarihi - DateTime.Now).TotalDays;

                string rapor = $@"
=== FİYAT DETAY RAPORU ===

Temel Fiyat: {baseFiyat:C2}

KATSAYILAR:
• Doluluk Oranı: %{dolulukYuzde} ({kapasite - bosKoltuk}/{kapasite}) → x{dolulukKatsayisi:F2}
• Kalan Gün: {kalanGun} gün → x{tarihKatsayisi:F2}
• Koltuk Tipi: {koltukTipi} → x{koltukTipiKatsayisi:F2}
• Sezon: {kalkisTarihi:MMMM} → x{sezonKatsayisi:F2}

Ara Toplam: {baseFiyat * dolulukKatsayisi * tarihKatsayisi * koltukTipiKatsayisi * sezonKatsayisi:C2}
";

                if (indirimOrani > 0)
                {
                    rapor += $"İndirim Kodu ({indirimKodu}): %{indirimOrani} indirim\n";
                }

                rapor += $"\nSONUÇ: {toplamFiyat:C2}";

                return rapor;
            }
            catch (Exception ex)
            {
                return $"Rapor oluşturma hatası: {ex.Message}";
            }
        }
    }
}