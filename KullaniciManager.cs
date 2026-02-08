using System;
using System.Data;
using System.Data.SqlClient;

namespace uçakSistemi
{
    public static class KullaniciManager
    {
        /// <summary>
        /// Yeni kullanıcı kaydı oluşturur
        /// </summary>
        public static bool KayitOl(string ad, string soyad, string eposta, string telefon, string sifre, string role)
        {
            try
            {
                // E-posta kontrolü
                string checkQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE Eposta = @Eposta";
                SqlParameter[] checkParams = {
                    new SqlParameter("@Eposta", eposta)
                };

                int count = Convert.ToInt32(VeritabaniHelper.ExecuteScalar(checkQuery, checkParams));

                if (count > 0)
                {
                    throw new Exception("Bu e-posta adresi zaten kayıtlı!");
                }

                // Kullanıcı ekle
                string insertQuery = @"INSERT INTO Kullanicilar (Ad, Soyad, Eposta, TelefonNumarasi, Sifre, Role) 
                                      VALUES (@Ad, @Soyad, @Eposta, @Telefon, @Sifre, @Role)";

                SqlParameter[] insertParams = {
                    new SqlParameter("@Ad", ad),
                    new SqlParameter("@Soyad", soyad),
                    new SqlParameter("@Eposta", eposta),
                    new SqlParameter("@Telefon", telefon),
                    new SqlParameter("@Sifre", sifre),
                    new SqlParameter("@Role", role)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(insertQuery, insertParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Kayıt hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        public static Kullanıcı GirisYap(string eposta, string sifre)
        {
            try
            {
                string query = @"SELECT Id, Ad, Soyad, Eposta, TelefonNumarasi, Sifre, Role, KayitTarihi, AktifMi 
                               FROM Kullanicilar 
                               WHERE Eposta = @Eposta AND Sifre = @Sifre AND AktifMi = 1";

                SqlParameter[] parameters = {
                    new SqlParameter("@Eposta", eposta),
                    new SqlParameter("@Sifre", sifre)
                };

                DataTable dt = VeritabaniHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count == 0)
                    return null;

                DataRow row = dt.Rows[0];
                return new Kullanıcı
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Ad = row["Ad"].ToString(),
                    Soyad = row["Soyad"].ToString(),
                    Eposta = row["Eposta"].ToString(),
                    TelefonNumarasi = row["TelefonNumarasi"].ToString(),
                    Şifre = row["Sifre"].ToString(),
                    Role = row["Role"].ToString(),
                    KayitTarihi = Convert.ToDateTime(row["KayitTarihi"]),
                    AktifMi = Convert.ToBoolean(row["AktifMi"])
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Giriş hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Şifremi unuttum - E-posta ile şifreyi getirir
        /// </summary>
        public static string SifremiUnuttum(string eposta)
        {
            try
            {
                string query = "SELECT Sifre FROM Kullanicilar WHERE Eposta = @Eposta AND AktifMi = 1";
                SqlParameter[] parameters = {
                    new SqlParameter("@Eposta", eposta)
                };

                object result = VeritabaniHelper.ExecuteScalar(query, parameters);

                if (result == null)
                    throw new Exception("Bu e-posta adresi kayıtlı değil!");

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Hata: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tüm kullanıcıları listeler (Admin için)
        /// </summary>
        public static DataTable KullanicilariListele()
        {
            try
            {
                string query = @"SELECT Id, Ad, Soyad, Eposta, TelefonNumarasi, Role, KayitTarihi, 
                               CASE WHEN AktifMi = 1 THEN 'Aktif' ELSE 'Pasif' END AS Durum
                               FROM Kullanicilar 
                               ORDER BY KayitTarihi DESC";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Listeleme hatası: {ex.Message}", ex);
            }
        }

        // ============ PİLOT İŞLEMLERİ ============

        /// <summary>
        /// Yeni pilot ekler
        /// </summary>
        public static bool PilotEkle(string ad, string soyad, string eposta, string telefon, string sifre, string lisansNo, string rutbe)
        {
            try
            {
                // Önce kullanıcı olarak ekle
                bool kullaniciEklendi = KayitOl(ad, soyad, eposta, telefon, sifre, "Pilot");

                if (!kullaniciEklendi)
                    return false;

                // Kullanıcı ID'sini al
                string getUserIdQuery = "SELECT Id FROM Kullanicilar WHERE Eposta = @Eposta";
                SqlParameter[] getUserIdParams = {
                    new SqlParameter("@Eposta", eposta)
                };
                int kullaniciId = Convert.ToInt32(VeritabaniHelper.ExecuteScalar(getUserIdQuery, getUserIdParams));

                // Pilot tablosuna ekle
                string insertPilotQuery = @"INSERT INTO Pilotlar (KullaniciId, LisansNumarasi, UcusSaati, Rutbe) 
                                           VALUES (@KullaniciId, @LisansNo, 0, @Rutbe)";

                SqlParameter[] insertPilotParams = {
                    new SqlParameter("@KullaniciId", kullaniciId),
                    new SqlParameter("@LisansNo", lisansNo),
                    new SqlParameter("@Rutbe", rutbe)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(insertPilotQuery, insertPilotParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Pilot ekleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Pilotları listeler
        /// </summary>
        public static DataTable PilotlariListele()
        {
            try
            {
                string query = @"SELECT k.Id, k.Ad, k.Soyad, k.Eposta, k.TelefonNumarasi, 
                               p.LisansNumarasi, p.UcusSaati, p.Rutbe
                               FROM Kullanicilar k
                               INNER JOIN Pilotlar p ON k.Id = p.KullaniciId
                               WHERE k.Role = 'Pilot' AND k.AktifMi = 1
                               ORDER BY k.Ad, k.Soyad";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Pilot listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Pilot bilgilerini günceller
        /// </summary>
        public static bool PilotGuncelle(int kullaniciId, string soyad, string eposta, string telefon, string rutbe)
        {
            try
            {
                // Kullanıcı bilgilerini güncelle
                string updateKullaniciQuery = @"UPDATE Kullanicilar 
                                               SET Soyad = @Soyad, Eposta = @Eposta, TelefonNumarasi = @Telefon
                                               WHERE Id = @Id";

                SqlParameter[] updateKullaniciParams = {
                    new SqlParameter("@Soyad", soyad),
                    new SqlParameter("@Eposta", eposta),
                    new SqlParameter("@Telefon", telefon),
                    new SqlParameter("@Id", kullaniciId)
                };

                VeritabaniHelper.ExecuteNonQuery(updateKullaniciQuery, updateKullaniciParams);

                // Pilot bilgilerini güncelle
                string updatePilotQuery = @"UPDATE Pilotlar 
                                           SET Rutbe = @Rutbe 
                                           WHERE KullaniciId = @KullaniciId";

                SqlParameter[] updatePilotParams = {
                    new SqlParameter("@Rutbe", rutbe),
                    new SqlParameter("@KullaniciId", kullaniciId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(updatePilotQuery, updatePilotParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Pilot güncelleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Pilot siler (soft delete)
        /// </summary>
        public static bool PilotSil(int kullaniciId)
        {
            try
            {
                string query = "UPDATE Kullanicilar SET AktifMi = 0 WHERE Id = @Id";
                SqlParameter[] parameters = {
                    new SqlParameter("@Id", kullaniciId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Pilot silme hatası: {ex.Message}", ex);
            }
        }

        // ============ HOSTES İŞLEMLERİ ============

        /// <summary>
        /// Yeni hostes ekler
        /// </summary>
        public static bool HostesEkle(string ad, string soyad, string eposta, string telefon, string sifre, string hostesNo, string dilBecerileri)
        {
            try
            {
                // Önce kullanıcı olarak ekle
                bool kullaniciEklendi = KayitOl(ad, soyad, eposta, telefon, sifre, "Hostes");

                if (!kullaniciEklendi)
                    return false;

                // Kullanıcı ID'sini al
                string getUserIdQuery = "SELECT Id FROM Kullanicilar WHERE Eposta = @Eposta";
                SqlParameter[] getUserIdParams = {
                    new SqlParameter("@Eposta", eposta)
                };
                int kullaniciId = Convert.ToInt32(VeritabaniHelper.ExecuteScalar(getUserIdQuery, getUserIdParams));

                // Hostes tablosuna ekle
                string insertHostesQuery = @"INSERT INTO Hostesler (KullaniciId, HostesNumarasi, UcusSayisi, DilBecerileri) 
                                            VALUES (@KullaniciId, @HostesNo, 0, @DilBecerileri)";

                SqlParameter[] insertHostesParams = {
                    new SqlParameter("@KullaniciId", kullaniciId),
                    new SqlParameter("@HostesNo", hostesNo),
                    new SqlParameter("@DilBecerileri", dilBecerileri)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(insertHostesQuery, insertHostesParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Hostes ekleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Hostesleri listeler
        /// </summary>
        public static DataTable HostesleriListele()
        {
            try
            {
                string query = @"SELECT k.Id, k.Ad, k.Soyad, k.Eposta, k.TelefonNumarasi, 
                               h.HostesNumarasi, h.UcusSayisi, h.DilBecerileri
                               FROM Kullanicilar k
                               INNER JOIN Hostesler h ON k.Id = h.KullaniciId
                               WHERE k.Role = 'Hostes' AND k.AktifMi = 1
                               ORDER BY k.Ad, k.Soyad";

                return VeritabaniHelper.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                throw new Exception($"Hostes listeleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Hostes bilgilerini günceller
        /// </summary>
        public static bool HostesGuncelle(int kullaniciId, string ad, string soyad, string eposta, string telefon, string dilBecerileri)
        {
            try
            {
                // Kullanıcı bilgilerini güncelle
                string updateKullaniciQuery = @"UPDATE Kullanicilar 
                                               SET Ad = @Ad, Soyad = @Soyad, Eposta = @Eposta, TelefonNumarasi = @Telefon
                                               WHERE Id = @Id";

                SqlParameter[] updateKullaniciParams = {
                    new SqlParameter("@Ad", ad),
                    new SqlParameter("@Soyad", soyad),
                    new SqlParameter("@Eposta", eposta),
                    new SqlParameter("@Telefon", telefon),
                    new SqlParameter("@Id", kullaniciId)
                };

                VeritabaniHelper.ExecuteNonQuery(updateKullaniciQuery, updateKullaniciParams);

                // Hostes bilgilerini güncelle
                string updateHostesQuery = @"UPDATE Hostesler 
                                            SET DilBecerileri = @DilBecerileri 
                                            WHERE KullaniciId = @KullaniciId";

                SqlParameter[] updateHostesParams = {
                    new SqlParameter("@DilBecerileri", dilBecerileri),
                    new SqlParameter("@KullaniciId", kullaniciId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(updateHostesQuery, updateHostesParams);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Hostes güncelleme hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Hostes siler (soft delete)
        /// </summary>
        public static bool HostesSil(int kullaniciId)
        {
            try
            {
                string query = "UPDATE Kullanicilar SET AktifMi = 0 WHERE Id = @Id";
                SqlParameter[] parameters = {
                    new SqlParameter("@Id", kullaniciId)
                };

                int result = VeritabaniHelper.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Hostes silme hatası: {ex.Message}", ex);
            }
        }
    }
}