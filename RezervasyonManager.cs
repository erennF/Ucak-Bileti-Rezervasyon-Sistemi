using System;
using System.Data;
using System.Data.SqlClient;

namespace uçakSistemi
{
    public static class RezervasyonManager
    {
        /// <summary>
        /// Yeni rezervasyon oluşturur
        /// </summary>
        public static bool RezervasyonOlustur(int ucusId, int kullaniciId, string koltukNo,
                                             string koltukTipi = "Ekonomi", string indirimKodu = null)
        {
            try
            {
                // Boş koltuk kontrolü
                string checkQuery = "SELECT BosKoltukSayisi FROM Ucuslar WHERE UcusId = @UcusId";
                SqlParameter[] checkParams = {
                    new SqlParameter("@UcusId", ucusId)
                };

                object result = VeritabaniHelper.ExecuteScalar(checkQuery, checkParams);
                if (result == null || Convert.ToInt32(result) <= 0)
                {
                    throw new Exception("Bu uçuşta boş koltuk kalmamıştır!");
                }

                // Koltuk numarası kullanımda mı kontrol et
                string checkKoltukQuery = @"SELECT COUNT(*) FROM Rezervasyonlar 
                                          WHERE UcusId = @UcusId 
                                          AND KoltukNumarasi = @KoltukNo 
                                          AND RezervasyonDurumu = 'Aktif'";
                SqlParameter[] checkKoltukParams = {
                    new SqlParameter("@UcusId", ucusId),
                    new SqlParameter("@KoltukNo", koltukNo)
                };

                int koltukKullaniliyor = Convert.ToInt32(VeritabaniHelper.ExecuteScalar(checkKoltukQuery, checkKoltukParams));
                if (koltukKullaniliyor > 0)
                {
                    throw new Exception($"Koltuk numarası {koltukNo} bu uçuşta zaten rezerve edilmiş!");
                }

                // Fiyat hesapla
                decimal fiyat = DinamikFiyatlandirma.FiyatHesapla(ucusId, koltukTipi, indirimKodu);

                // Bilet numarası oluştur
                string biletNo = "BLT" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(1000, 9999);

                // Rezervasyon ekle
                string insertQuery = @"INSERT INTO Rezervasyonlar 
                                     (UcusId, KullaniciId, BiletNumarasi, KoltukNumarasi, 
                                      KoltukTipi, RezervasyonDurumu, Fiyat, IndirimKodu)
                                     VALUES 
                                     (@UcusId, @KullaniciId, @BiletNo, @KoltukNo, 
                                      @KoltukTipi, 'Aktif', @Fiyat, @IndirimKodu)";

                SqlParameter[] insertParams = {
                    new SqlParameter("@UcusId", ucusId),
                    new SqlParameter("@KullaniciId", kullaniciId),
                    new SqlParameter("@BiletNo", biletNo),
                    new SqlParameter("@KoltukNo", koltukNo),
                    new SqlParameter("@KoltukTipi", koltukTipi),
                    new SqlParameter("@Fiyat", fiyat),
                    new SqlParameter("@IndirimKodu", (object)indirimKodu ?? DBNull.Value)
                };

                int insertResult = VeritabaniHelper.ExecuteNonQuery(insertQuery, insertParams);

                if (insertResult > 0)
                {
                    // Boş koltuk sayısını azalt
                    string updateQuery = "UPDATE Ucuslar SET BosKoltukSayisi = BosKoltukSayisi - 1 WHERE UcusId = @UcusId";
                    SqlParameter[] updateParams = {
                        new SqlParameter("@UcusId", ucusId)
                    };
                    VeritabaniHelper.ExecuteNonQuery(updateQuery, updateParams);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon oluşturma hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Rezervasyon iptal eder
        /// </summary>
        public static bool RezervasyonIptal(int rezervasyonId)
        {
            try
            {
                // Rezervasyon bilgilerini al
                string getQuery = "SELECT UcusId, RezervasyonDurumu FROM Rezervasyonlar WHERE RezervasyonId = @Id";
                SqlParameter[] getParams = {
                    new SqlParameter("@Id", rezervasyonId)
                };

                DataTable dt = VeritabaniHelper.ExecuteQuery(getQuery, getParams);
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("Rezervasyon bulunamadı!");
                }

                string durum = dt.Rows[0]["RezervasyonDurumu"].ToString();
                if (durum == "İptal")
                {
                    throw new Exception("Bu rezervasyon zaten iptal edilmiş!");
                }

                int ucusId = Convert.ToInt32(dt.Rows[0]["UcusId"]);

                // Rezervasyonu iptal et
                string updateQuery = "UPDATE Rezervasyonlar SET RezervasyonDurumu = 'İptal' WHERE RezervasyonId = @Id";
                SqlParameter[] updateParams = {
                    new SqlParameter("@Id", rezervasyonId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(updateQuery, updateParams);

                if (result > 0)
                {
                    // Boş koltuk sayısını artır
                    string updateUcusQuery = "UPDATE Ucuslar SET BosKoltukSayisi = BosKoltukSayisi + 1 WHERE UcusId = @UcusId";
                    SqlParameter[] updateUcusParams = {
                        new SqlParameter("@UcusId", ucusId)
                    };
                    VeritabaniHelper.ExecuteNonQuery(updateUcusQuery, updateUcusParams);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon iptal hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kullanıcının rezervasyonlarını listeler
        /// </summary>
        public static DataTable KullaniciRezervasyonlari(int kullaniciId)
        {
            try
            {
                string query = @"SELECT r.RezervasyonId, r.BiletNumarasi, r.KoltukNumarasi, r.KoltukTipi,
                               r.RezervasyonTarihi, r.RezervasyonDurumu, r.Fiyat, r.IndirimKodu,
                               u.KalkisNoktasi, u.VarisNoktasi, u.KalkisTarihi, u.VarisTarihi,
                               u.HavayoluSirketi, u.KapiNumarasi
                               FROM Rezervasyonlar r
                               INNER JOIN Ucuslar u ON r.UcusId = u.UcusId
                               WHERE r.KullaniciId = @KullaniciId
                               ORDER BY r.RezervasyonTarihi DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@KullaniciId", kullaniciId)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tüm rezervasyonları listeler (Admin için)
        /// </summary>
        public static DataTable TumRezervasyonlar()
        {
            try
            {
                string query = @"SELECT r.RezervasyonId, r.BiletNumarasi, r.KoltukNumarasi, 
                               r.RezervasyonTarihi, r.RezervasyonDurumu, r.Fiyat,
                               k.Ad + ' ' + k.Soyad AS YolcuAdi,
                               u.KalkisNoktasi, u.VarisNoktasi, u.KalkisTarihi,
                               u.HavayoluSirketi
                               FROM Rezervasyonlar r
                               INNER JOIN Kullanicilar k ON r.KullaniciId = k.Id
                               INNER JOIN Ucuslar u ON r.UcusId = u.UcusId
                               ORDER BY r.RezervasyonTarihi DESC";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Belirli bir uçuşun rezervasyonlarını listeler
        /// </summary>
        public static DataTable UcusRezervasyonlari(int ucusId)
        {
            try
            {
                string query = @"SELECT r.RezervasyonId, r.BiletNumarasi, r.KoltukNumarasi, r.KoltukTipi,
                               r.RezervasyonTarihi, r.RezervasyonDurumu, r.Fiyat,
                               k.Ad, k.Soyad, k.Eposta, k.TelefonNumarasi
                               FROM Rezervasyonlar r
                               INNER JOIN Kullanicilar k ON r.KullaniciId = k.Id
                               WHERE r.UcusId = @UcusId
                               ORDER BY r.KoltukNumarasi";

                SqlParameter[] parameters = {
                    new SqlParameter("@UcusId", ucusId)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçuş rezervasyon listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Rezervasyon detaylarını getirir
        /// </summary>
        public static DataTable RezervasyonDetay(int rezervasyonId)
        {
            try
            {
                string query = @"SELECT r.*, 
                               k.Ad, k.Soyad, k.Eposta, k.TelefonNumarasi,
                               u.KalkisNoktasi, u.VarisNoktasi, u.KalkisTarihi, u.VarisTarihi,
                               u.HavayoluSirketi, u.KapiNumarasi, u.KapiKapanisSaati,
                               uc.Model AS UcakModel, uc.Plaka
                               FROM Rezervasyonlar r
                               INNER JOIN Kullanicilar k ON r.KullaniciId = k.Id
                               INNER JOIN Ucuslar u ON r.UcusId = u.UcusId
                               LEFT JOIN Ucaklar uc ON u.UcakKodu = uc.UcakKodu
                               WHERE r.RezervasyonId = @Id";

                SqlParameter[] parameters = {
                    new SqlParameter("@Id", rezervasyonId)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Rezervasyon detay hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Rezervasyon istatistikleri (Admin için)
        /// </summary>
        public static DataTable RezervasyonIstatistikleri()
        {
            try
            {
                string query = @"SELECT 
                               COUNT(*) AS ToplamRezervasyonSayisi,
                               COUNT(CASE WHEN RezervasyonDurumu = 'Aktif' THEN 1 END) AS AktifRezervasyonSayisi,
                               COUNT(CASE WHEN RezervasyonDurumu = 'İptal' THEN 1 END) AS IptalRezervasyonSayisi,
                               SUM(CASE WHEN RezervasyonDurumu = 'Aktif' THEN Fiyat ELSE 0 END) AS ToplamGelir,
                               AVG(CASE WHEN RezervasyonDurumu = 'Aktif' THEN Fiyat ELSE NULL END) AS OrtalamaFiyat
                               FROM Rezervasyonlar";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"İstatistik hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Günlük rezervasyon raporu
        /// </summary>
        public static DataTable GunlukRaporlar(DateTime tarih)
        {
            try
            {
                string query = @"SELECT u.KalkisNoktasi, u.VarisNoktasi, u.HavayoluSirketi,
                               COUNT(r.RezervasyonId) AS RezervasyonSayisi,
                               SUM(r.Fiyat) AS ToplamGelir,
                               u.Kapasite - u.BosKoltukSayisi AS DoluKoltuk,
                               u.BosKoltukSayisi
                               FROM Ucuslar u
                               LEFT JOIN Rezervasyonlar r ON u.UcusId = r.UcusId AND r.RezervasyonDurumu = 'Aktif'
                               WHERE CAST(u.KalkisTarihi AS DATE) = CAST(@Tarih AS DATE)
                               GROUP BY u.KalkisNoktasi, u.VarisNoktasi, u.HavayoluSirketi, 
                                        u.Kapasite, u.BosKoltukSayisi
                               ORDER BY ToplamGelir DESC";

                SqlParameter[] parameters = {
                    new SqlParameter("@Tarih", tarih)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Günlük rapor hatası: {ex.Message}", ex);
            }
        }
    }
}