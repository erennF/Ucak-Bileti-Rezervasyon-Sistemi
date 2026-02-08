using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace uçakSistemi
{
    public static class VeritabaniHelper
    {
        // LocalDB connection string
        private static readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\UcakSistemiDB.mdf;Integrated Security=True;Connect Timeout=30";

        /// <summary>
        /// Veritabanı bağlantısını test eder
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// SELECT sorguları için kullanılır - DataTable döner
        /// </summary>
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Sorgu çalıştırma hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// INSERT, UPDATE, DELETE sorguları için kullanılır - Etkilenen satır sayısını döner
        /// </summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Sorgu çalıştırma hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tek bir değer döndüren sorgular için (COUNT, MAX, vb.)
        /// </summary>
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Sorgu çalıştırma hatası: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Veritabanı ve tabloları oluşturur (ilk kurulum için)
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                // Veritabanı yoksa oluştur
                CreateDatabaseIfNotExists();

                // Tabloları oluştur
                CreateTables();

                // Örnek verileri ekle
                InsertSampleData();

                MessageBox.Show("Veritabanı başarıyla oluşturuldu!", "Başarılı",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı oluşturma hatası:\n{ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDatabaseIfNotExists()
        {
            // LocalDB kullandığımız için veritabanı dosyası otomatik oluşturulur
            // Bu metod isteğe bağlı kontroller için kullanılabilir
        }

        private static void CreateTables()
        {
            string createTables = @"
            -- Kullanıcılar Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Kullanicilar')
            BEGIN
                CREATE TABLE Kullanicilar (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Ad NVARCHAR(50) NOT NULL,
                    Soyad NVARCHAR(50) NOT NULL,
                    Eposta NVARCHAR(100) NOT NULL UNIQUE,
                    TelefonNumarasi NVARCHAR(20),
                    Sifre NVARCHAR(100) NOT NULL,
                    Role NVARCHAR(20) NOT NULL DEFAULT 'Yolcu',
                    KayitTarihi DATETIME NOT NULL DEFAULT GETDATE(),
                    AktifMi BIT NOT NULL DEFAULT 1
                );
            END

            -- Pilotlar Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Pilotlar')
            BEGIN
                CREATE TABLE Pilotlar (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    KullaniciId INT NOT NULL,
                    LisansNumarasi NVARCHAR(50) NOT NULL UNIQUE,
                    UcusSaati INT DEFAULT 0,
                    Rutbe NVARCHAR(50),
                    FOREIGN KEY (KullaniciId) REFERENCES Kullanicilar(Id)
                );
            END

            -- Hostesler Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Hostesler')
            BEGIN
                CREATE TABLE Hostesler (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    KullaniciId INT NOT NULL,
                    HostesNumarasi NVARCHAR(50) NOT NULL UNIQUE,
                    UcusSayisi INT DEFAULT 0,
                    DilBecerileri NVARCHAR(200),
                    FOREIGN KEY (KullaniciId) REFERENCES Kullanicilar(Id)
                );
            END

            -- Uçaklar Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ucaklar')
            BEGIN
                CREATE TABLE Ucaklar (
                    UcakKodu INT PRIMARY KEY IDENTITY(1,1),
                    Model NVARCHAR(100) NOT NULL,
                    Plaka NVARCHAR(20) NOT NULL UNIQUE,
                    Kapasite INT NOT NULL,
                    HavayoluSirketi NVARCHAR(100),
                    AktifMi BIT DEFAULT 1
                );
            END

            -- Uçuşlar Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ucuslar')
            BEGIN
                CREATE TABLE Ucuslar (
                    UcusId INT PRIMARY KEY IDENTITY(1,1),
                    KalkisNoktasi NVARCHAR(100) NOT NULL,
                    VarisNoktasi NVARCHAR(100) NOT NULL,
                    KalkisTarihi DATETIME NOT NULL,
                    VarisTarihi DATETIME NOT NULL,
                    UcusSuresi TIME,
                    KapiNumarasi NVARCHAR(10),
                    KapiKapanisSaati NVARCHAR(10),
                    BinisSaati INT,
                    UcakKodu INT,
                    Fiyat DECIMAL(10,2) NOT NULL,
                    HavayoluSirketi NVARCHAR(100),
                    BosKoltukSayisi INT,
                    AktifMi BIT DEFAULT 1,
                    FOREIGN KEY (UcakKodu) REFERENCES Ucaklar(UcakKodu)
                );
            END

            -- Rezervasyonlar Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Rezervasyonlar')
            BEGIN
                CREATE TABLE Rezervasyonlar (
                    RezervasyonId INT PRIMARY KEY IDENTITY(1,1),
                    UcusId INT NOT NULL,
                    KullaniciId INT NOT NULL,
                    BiletNumarasi NVARCHAR(50) UNIQUE,
                    KoltukNumarasi NVARCHAR(10),
                    KoltukTipi NVARCHAR(20) DEFAULT 'Ekonomi',
                    RezervasyonTarihi DATETIME NOT NULL DEFAULT GETDATE(),
                    RezervasyonDurumu NVARCHAR(20) DEFAULT 'Aktif',
                    Fiyat DECIMAL(10,2),
                    IndirimKodu NVARCHAR(20),
                    FOREIGN KEY (UcusId) REFERENCES Ucuslar(UcusId),
                    FOREIGN KEY (KullaniciId) REFERENCES Kullanicilar(Id)
                );
            END

            -- İndirim Kodları Tablosu
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'IndirimKodlari')
            BEGIN
                CREATE TABLE IndirimKodlari (
                    Id INT PRIMARY KEY IDENTITY(1,1),
                    Kod NVARCHAR(20) NOT NULL UNIQUE,
                    IndirimOrani DECIMAL(5,2) NOT NULL,
                    GecerlilikBaslangic DATETIME NOT NULL,
                    GecerlilikBitis DATETIME NOT NULL,
                    AktifMi BIT DEFAULT 1
                );
            END
            ";

            ExecuteNonQuery(createTables);
        }

        private static void InsertSampleData()
        {
            // Admin kullanıcı ekle
            string checkAdmin = "SELECT COUNT(*) FROM Kullanicilar WHERE Role = 'Admin'";
            int adminCount = Convert.ToInt32(ExecuteScalar(checkAdmin));

            if (adminCount == 0)
            {
                string insertAdmin = @"INSERT INTO Kullanicilar (Ad, Soyad, Eposta, TelefonNumarasi, Sifre, Role) 
                                      VALUES ('Admin', 'User', 'admin@ucak.com', '5551234567', 'admin123', 'Admin')";
                ExecuteNonQuery(insertAdmin);
            }

            // Örnek uçaklar ekle
            string checkPlanes = "SELECT COUNT(*) FROM Ucaklar";
            int planeCount = Convert.ToInt32(ExecuteScalar(checkPlanes));

            if (planeCount == 0)
            {
                string insertPlanes = @"
                INSERT INTO Ucaklar (Model, Plaka, Kapasite, HavayoluSirketi) VALUES
                ('Boeing 737', 'TC-AAA', 189, 'Türk Hava Yolları'),
                ('Airbus A320', 'TC-BBB', 180, 'Pegasus Hava Yolları'),
                ('Boeing 777', 'TC-CCC', 396, 'Türk Hava Yolları'),
                ('Airbus A330', 'TC-DDD', 277, 'Anadolu Jet');
                ";
                ExecuteNonQuery(insertPlanes);
            }

            // Örnek uçuşlar ekle
            string checkFlights = "SELECT COUNT(*) FROM Ucuslar";
            int flightCount = Convert.ToInt32(ExecuteScalar(checkFlights));

            if (flightCount == 0)
            {
                string insertFlights = @"
                INSERT INTO Ucuslar (KalkisNoktasi, VarisNoktasi, KalkisTarihi, VarisTarihi, UcusSuresi, KapiNumarasi, KapiKapanisSaati, BinisSaati, UcakKodu, Fiyat, HavayoluSirketi, BosKoltukSayisi) VALUES
                ('İstanbul', 'Ankara', DATEADD(day, 1, GETDATE()), DATEADD(day, 1, DATEADD(hour, 1, GETDATE())), '01:00', 'A12', '08:30', 9, 1, 500.00, 'Türk Hava Yolları', 189),
                ('İstanbul', 'İzmir', DATEADD(day, 2, GETDATE()), DATEADD(day, 2, DATEADD(hour, 1, GETDATE())), '01:10', 'B5', '14:00', 15, 2, 450.00, 'Pegasus Hava Yolları', 180),
                ('Ankara', 'Antalya', DATEADD(day, 3, GETDATE()), DATEADD(day, 3, DATEADD(hour, 1, GETDATE())), '01:20', 'C8', '10:30', 11, 3, 600.00, 'Türk Hava Yolları', 396),
                ('İzmir', 'İstanbul', DATEADD(day, 4, GETDATE()), DATEADD(day, 4, DATEADD(hour, 1, GETDATE())), '01:10', 'D3', '16:00', 17, 4, 470.00, 'Anadolu Jet', 277),
                ('Antalya', 'İstanbul', DATEADD(day, 5, GETDATE()), DATEADD(day, 5, DATEADD(hour, 1, GETDATE())), '01:15', 'E7', '12:30', 13, 1, 550.00, 'Türk Hava Yolları', 189);
                ";
                ExecuteNonQuery(insertFlights);
            }

            // Örnek indirim kodları ekle
            string checkCoupons = "SELECT COUNT(*) FROM IndirimKodlari";
            int couponCount = Convert.ToInt32(ExecuteScalar(checkCoupons));

            if (couponCount == 0)
            {
                string insertCoupons = @"
                INSERT INTO IndirimKodlari (Kod, IndirimOrani, GecerlilikBaslangic, GecerlilikBitis) VALUES
                ('YILBASI2025', 20, GETDATE(), DATEADD(month, 1, GETDATE())),
                ('OGRENCI15', 15, GETDATE(), DATEADD(year, 1, GETDATE())),
                ('ERKEN10', 10, GETDATE(), DATEADD(month, 3, GETDATE()));
                ";
                ExecuteNonQuery(insertCoupons);
            }
        }
    }
}