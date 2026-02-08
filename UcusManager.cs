using System;
using System.Data;
using System.Data.SqlClient;

namespace uçakSistemi
{
    public static class UcusManager
    {
        /// <summary>
        /// Uçuş arama - Kalkış, varış ve tarih filtreli
        /// </summary>
        public static DataTable UcusAra(string kalkis, string varis, DateTime? tarih = null)
        {
            try
            {
                string query = @"SELECT u.UcusId, u.KalkisNoktasi, u.VarisNoktasi, 
                               u.KalkisTarihi, u.VarisTarihi, u.UcusSuresi,
                               u.KapiNumarasi, u.KapiKapanisSaati, u.BinisSaati,
                               u.UcakKodu, u.Fiyat, u.HavayoluSirketi, u.BosKoltukSayisi,
                               uc.Model AS UcakModel, uc.Kapasite
                               FROM Ucuslar u
                               LEFT JOIN Ucaklar uc ON u.UcakKodu = uc.UcakKodu
                               WHERE u.AktifMi = 1 
                               AND u.KalkisNoktasi LIKE @Kalkis 
                               AND u.VarisNoktasi LIKE @Varis";

                if (tarih.HasValue)
                {
                    query += " AND CAST(u.KalkisTarihi AS DATE) = CAST(@Tarih AS DATE)";
                }

                query += " ORDER BY u.KalkisTarihi";

                SqlParameter[] parameters;

                if (tarih.HasValue)
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@Kalkis", "%" + kalkis + "%"),
                        new SqlParameter("@Varis", "%" + varis + "%"),
                        new SqlParameter("@Tarih", tarih.Value)
                    };
                }
                else
                {
                    parameters = new SqlParameter[] {
                        new SqlParameter("@Kalkis", "%" + kalkis + "%"),
                        new SqlParameter("@Varis", "%" + varis + "%")
                    };
                }

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş arama hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tüm uçuşları listeler
        /// </summary>
        public static DataTable TumUcuslariListele()
        {
            try
            {
                string query = @"SELECT u.UcusId, u.KalkisNoktasi, u.VarisNoktasi, 
                               u.KalkisTarihi, u.VarisTarihi, u.Fiyat, u.HavayoluSirketi,
                               u.BosKoltukSayisi, uc.Kapasite,
                               CASE WHEN u.AktifMi = 1 THEN 'Aktif' ELSE 'İptal' END AS Durum
                               FROM Ucuslar u
                               LEFT JOIN Ucaklar uc ON u.UcakKodu = uc.UcakKodu
                               ORDER BY u.KalkisTarihi DESC";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Yeni uçuş ekler
        /// </summary>
        public static bool UcusEkle(string kalkis, string varis, DateTime kalkisTarihi, DateTime varisTarihi,
                                   string ucusSuresi, string kapiNo, string kapiKapanisSaati, int binisSaati,
                                   int ucakKodu, decimal fiyat, string havayoluSirketi, int bosKoltuk)
        {
            try
            {
                string query = @"INSERT INTO Ucuslar 
                               (KalkisNoktasi, VarisNoktasi, KalkisTarihi, VarisTarihi, UcusSuresi,
                                KapiNumarasi, KapiKapanisSaati, BinisSaati, UcakKodu, Fiyat, 
                                HavayoluSirketi, BosKoltukSayisi, AktifMi)
                               VALUES 
                               (@Kalkis, @Varis, @KalkisTarihi, @VarisTarihi, @UcusSuresi,
                                @KapiNo, @KapiKapanisSaati, @BinisSaati, @UcakKodu, @Fiyat,
                                @HavayoluSirketi, @BosKoltuk, 1)";

                SqlParameter[] parameters = {
                    new SqlParameter("@Kalkis", kalkis),
                    new SqlParameter("@Varis", varis),
                    new SqlParameter("@KalkisTarihi", kalkisTarihi),
                    new SqlParameter("@VarisTarihi", varisTarihi),
                    new SqlParameter("@UcusSuresi", ucusSuresi),
                    new SqlParameter("@KapiNo", kapiNo),
                    new SqlParameter("@KapiKapanisSaati", kapiKapanisSaati),
                    new SqlParameter("@BinisSaati", binisSaati),
                    new SqlParameter("@UcakKodu", ucakKodu),
                    new SqlParameter("@Fiyat", fiyat),
                    new SqlParameter("@HavayoluSirketi", havayoluSirketi),
                    new SqlParameter("@BosKoltuk", bosKoltuk)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş ekleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçuş bilgilerini günceller
        /// </summary>
        public static bool UcusGuncelle(int ucusId, string kalkis, string varis, DateTime kalkisTarihi,
                                       DateTime varisTarihi, decimal fiyat, string kapiNo, int bosKoltuk)
        {
            try
            {
                string query = @"UPDATE Ucuslar 
                               SET KalkisNoktasi = @Kalkis, 
                                   VarisNoktasi = @Varis, 
                                   KalkisTarihi = @KalkisTarihi,
                                   VarisTarihi = @VarisTarihi,
                                   Fiyat = @Fiyat,
                                   KapiNumarasi = @KapiNo,
                                   BosKoltukSayisi = @BosKoltuk
                               WHERE UcusId = @UcusId";

                SqlParameter[] parameters = {
                    new SqlParameter("@Kalkis", kalkis),
                    new SqlParameter("@Varis", varis),
                    new SqlParameter("@KalkisTarihi", kalkisTarihi),
                    new SqlParameter("@VarisTarihi", varisTarihi),
                    new SqlParameter("@Fiyat", fiyat),
                    new SqlParameter("@KapiNo", kapiNo),
                    new SqlParameter("@BosKoltuk", bosKoltuk),
                    new SqlParameter("@UcusId", ucusId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş güncelleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçuş iptal eder (soft delete)
        /// </summary>
        public static bool UcusIptal(int ucusId)
        {
            try
            {
                string query = "UPDATE Ucuslar SET AktifMi = 0 WHERE UcusId = @UcusId";
                SqlParameter[] parameters = {
                    new SqlParameter("@UcusId", ucusId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş iptal hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçuş detaylarını getirir
        /// </summary>
        public static DataTable UcusDetay(int ucusId)
        {
            try
            {
                string query = @"SELECT u.*, uc.Model, uc.Plaka, uc.Kapasite
                               FROM Ucuslar u
                               LEFT JOIN Ucaklar uc ON u.UcakKodu = uc.UcakKodu
                               WHERE u.UcusId = @UcusId";

                SqlParameter[] parameters = {
                    new SqlParameter("@UcusId", ucusId)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş detay hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Boş koltuk sayısını günceller
        /// </summary>
        public static bool BosKoltukGuncelle(int ucusId, int yeniSayi)
        {
            try
            {
                string query = "UPDATE Ucuslar SET BosKoltukSayisi = @YeniSayi WHERE UcusId = @UcusId";
                SqlParameter[] parameters = {
                    new SqlParameter("@YeniSayi", yeniSayi),
                    new SqlParameter("@UcusId", ucusId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Boş koltuk güncelleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Belirli bir uçağın uçuşlarını listeler
        /// </summary>
        public static DataTable UcagaGoreUcuslar(int ucakKodu)
        {
            try
            {
                string query = @"SELECT UcusId, KalkisNoktasi, VarisNoktasi, KalkisTarihi, VarisTarihi,
                               Fiyat, BosKoltukSayisi
                               FROM Ucuslar
                               WHERE UcakKodu = @UcakKodu AND AktifMi = 1
                               ORDER BY KalkisTarihi";

                SqlParameter[] parameters = {
                    new SqlParameter("@UcakKodu", ucakKodu)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçağa göre uçuş listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Popüler rotaları listeler (en çok rezervasyon yapılan)
        /// </summary>
        public static DataTable PopulerRotalar(int top = 10)
        {
            try
            {
                string query = $@"SELECT TOP {top} u.KalkisNoktasi, u.VarisNoktasi, 
                                COUNT(r.RezervasyonId) AS RezervasyonSayisi,
                                AVG(u.Fiyat) AS OrtalamFiyat
                                FROM Ucuslar u
                                LEFT JOIN Rezervasyonlar r ON u.UcusId = r.UcusId
                                WHERE u.AktifMi = 1
                                GROUP BY u.KalkisNoktasi, u.VarisNoktasi
                                ORDER BY RezervasyonSayisi DESC";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Popüler rota listeleme hatası: {ex.Message}", ex);
            }
        }
    }
}