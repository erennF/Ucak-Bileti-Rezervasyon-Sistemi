using System;
using System.Data;
using System.Data.SqlClient;

namespace uçakSistemi
{
    public static class UcakManager
    {
        /// <summary>
        /// Tüm uçakları listeler
        /// </summary>
        public static DataTable TumUcaklariListele()
        {
            try
            {
                string query = @"SELECT UcakKodu, Model, Plaka, Kapasite, HavayoluSirketi,
                               CASE WHEN AktifMi = 1 THEN 'Aktif' ELSE 'Pasif' END AS Durum
                               FROM Ucaklar
                               ORDER BY Model";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçak listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Yeni uçak ekler
        /// </summary>
        public static bool UcakEkle(string model, string plaka, int kapasite, string havayoluSirketi)
        {
            try
            {
                // Plaka kontrolü
                string checkQuery = "SELECT COUNT(*) FROM Ucaklar WHERE Plaka = @Plaka";
                SqlParameter[] checkParams = {
                    new SqlParameter("@Plaka", plaka)
                };

                int count = Convert.ToInt32(VeritabaniHelper.ExecuteScalar(checkQuery, checkParams));

                if (count > 0)
                {
                    throw new Exception("Bu plaka zaten kayıtlı!");
                }

                // Uçak ekle
                string insertQuery = @"INSERT INTO Ucaklar (Model, Plaka, Kapasite, HavayoluSirketi, AktifMi) 
                                      VALUES (@Model, @Plaka, @Kapasite, @HavayoluSirketi, 1)";

                SqlParameter[] insertParams = {
                    new SqlParameter("@Model", model),
                    new SqlParameter("@Plaka", plaka),
                    new SqlParameter("@Kapasite", kapasite),
                    new SqlParameter("@HavayoluSirketi", havayoluSirketi)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(insertQuery, insertParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçak ekleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçak bilgilerini günceller
        /// </summary>
        public static bool UcakGuncelle(int ucakKodu, string model, string plaka, int kapasite, string havayoluSirketi)
        {
            try
            {
                string query = @"UPDATE Ucaklar 
                               SET Model = @Model, 
                                   Plaka = @Plaka, 
                                   Kapasite = @Kapasite,
                                   HavayoluSirketi = @HavayoluSirketi
                               WHERE UcakKodu = @UcakKodu";

                SqlParameter[] parameters = {
                    new SqlParameter("@Model", model),
                    new SqlParameter("@Plaka", plaka),
                    new SqlParameter("@Kapasite", kapasite),
                    new SqlParameter("@HavayoluSirketi", havayoluSirketi),
                    new SqlParameter("@UcakKodu", ucakKodu)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçak güncelleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçak siler (soft delete)
        /// </summary>
        public static bool UcakSil(int ucakKodu)
        {
            try
            {
                string query = "UPDATE Ucaklar SET AktifMi = 0 WHERE UcakKodu = @UcakKodu";
                SqlParameter[] parameters = {
                    new SqlParameter("@UcakKodu", ucakKodu)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçak silme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Uçak detaylarını getirir
        /// </summary>
        public static DataTable UcakDetay(int ucakKodu)
        {
            try
            {
                string query = "SELECT * FROM Ucaklar WHERE UcakKodu = @UcakKodu";
                SqlParameter[] parameters = {
                    new SqlParameter("@UcakKodu", ucakKodu)
                };

                return VeritabaniHelper.ExecuteQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Uçak detay hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Aktif uçakları listeler
        /// </summary>
        public static DataTable AktifUcaklar()
        {
            try
            {
                string query = @"SELECT UcakKodu, Model, Plaka, Kapasite, HavayoluSirketi
                               FROM Ucaklar
                               WHERE AktifMi = 1
                               ORDER BY Model";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Aktif uçak listeleme hatası: {ex.Message}", ex);
            }
        }
    }
}