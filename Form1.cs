using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Şifre gizle
            if (txtSifre != null)
            {
                txtSifre.PasswordChar = '●';
            }

            // Bağlantı testi
            try
            {
                if (VeritabaniHelper.TestConnection())
                {
                    this.Text = "Uçak Bileti Rezervasyon Sistemi - Bağlantı Başarılı ✓";
                }
                else
                {
                    MessageBox.Show("Veritabanı bağlantısı kurulamadı!\n\nLütfen şunları kontrol edin:\n• LocalDB yüklü mü?\n• UcakSistemiDB veritabanı oluşturuldu mu?\n• Connection string doğru mu?",
                        "Bağlantı Hatası",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnKayitOl_Click(object sender, EventArgs e)
        {
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(txtAd.Text))
                {
                    MessageBox.Show("Lütfen adınızı girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAd.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSoyAd.Text))
                {
                    MessageBox.Show("Lütfen soyadınızı girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoyAd.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEposta.Text))
                {
                    MessageBox.Show("Lütfen e-posta adresinizi girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEposta.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelefonNo.Text))
                {
                    MessageBox.Show("Lütfen telefon numaranızı girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefonNo.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                {
                    MessageBox.Show("Lütfen şifrenizi girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                    return;
                }

                // E-posta format kontrolü
                if (!txtEposta.Text.Contains("@") || !txtEposta.Text.Contains("."))
                {
                    MessageBox.Show("Geçerli bir e-posta adresi girin!\n\nÖrnek: isim@email.com", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEposta.Focus();
                    return;
                }

                // Şifre uzunluk kontrolü
                if (txtSifre.Text.Length < 6)
                {
                    MessageBox.Show("Şifre en az 6 karakter olmalıdır!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                    return;
                }

                // Telefon kontrolü
                if (txtTelefonNo.Text.Length < 10)
                {
                    MessageBox.Show("Telefon numarası en az 10 karakter olmalıdır!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTelefonNo.Focus();
                    return;
                }

                // Kayıt oluştur
                bool sonuc = KullaniciManager.KayitOl(
                    txtAd.Text.Trim(),
                    txtSoyAd.Text.Trim(),
                    txtEposta.Text.Trim().ToLower(),
                    txtTelefonNo.Text.Trim(),
                    txtSifre.Text,
                    "Yolcu"
                );

                if (sonuc)
                {
                    MessageBox.Show(
                        "✓ KAYIT BAŞARILI!\n\n" +
                        "───────────────────────\n" +
                        "Bilgileriniz:\n" +
                        $"• Ad Soyad: {txtAd.Text} {txtSoyAd.Text}\n" +
                        $"• E-posta: {txtEposta.Text}\n" +
                        $"• Telefon: {txtTelefonNo.Text}\n" +
                        $"• Şifre: {txtSifre.Text}\n" +
                        "───────────────────────\n\n" +
                        "Şimdi giriş yapabilirsiniz!",
                        "Kayıt Başarılı",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Sadece ad, soyad, telefon temizle (giriş için eposta ve şifre kalsın)
                    txtAd.Clear();
                    txtSoyAd.Clear();
                    txtTelefonNo.Clear();
                    txtAd.Focus();
                }
                else
                {
                    MessageBox.Show("Kayıt işlemi başarısız oldu!\n\nLütfen tekrar deneyin.",
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "KAYIT HATASI!\n\n" +
                    "───────────────────────\n" +
                    $"Hata: {ex.Message}\n\n" +
                    (ex.InnerException != null ? $"Detay: {ex.InnerException.Message}" : ""),
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnGirisYap_Click1(object sender, EventArgs e)
        {

            string baglantiAdresi = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UcakSistemiDB;Integrated Security=True;Encrypt=True";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                try
                {
                    baglanti.Open();
                    MessageBox.Show("Bağlantı Başarılı!");
                }
                catch (Exception ex)
                {
                    // Bağlantı neden başarısız olduysa Windows bize tam sebebini söylesin
                    MessageBox.Show("Detaylı Hata: " + ex.Message);
                }
            }
            try
            {
                // Boş alan kontrolü
                if (string.IsNullOrWhiteSpace(txtEposta.Text))
                {
                    MessageBox.Show("Lütfen e-posta adresinizi girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEposta.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSifre.Text))
                {
                    MessageBox.Show("Lütfen şifrenizi girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSifre.Focus();
                    return;
                }

                // Giriş yap
                Kullanıcı kullanici = KullaniciManager.GirisYap(
                    txtEposta.Text.Trim().ToLower(),
                    txtSifre.Text
                );

                if (kullanici == null)
                {
                    MessageBox.Show(
                        "❌ GİRİŞ BAŞARISIZ!\n\n" +
                        "E-posta veya şifre hatalı!\n\n" +
                        "Kontrol edin:\n" +
                        "• E-posta adresi doğru mu?\n" +
                        "• Şifre doğru mu?\n" +
                        "• Kayıt oldunuz mu?\n\n" +
                        "Şifrenizi unuttuysanız 'Şifremi Unuttum' butonunu kullanın.",
                        "Giriş Başarısız",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    txtSifre.Clear();
                    txtSifre.Focus();
                    return;
                }

                // Başarılı giriş mesajı
                MessageBox.Show(
                    "✓ HOŞ GELDİNİZ!\n\n" +
                    "───────────────────────\n" +
                    $"Ad Soyad: {kullanici.Ad} {kullanici.Soyad}\n" +
                    $"Rol: {kullanici.Role}\n" +
                    $"E-posta: {kullanici.Eposta}\n" +
                    $"Kayıt Tarihi: {kullanici.KayitTarihi:dd.MM.yyyy}\n" +
                    "───────────────────────",
                    "Giriş Başarılı",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Formu gizle
                this.Hide();

                // ROL'E GÖRE FORM AÇ
                try
                {
                    if (kullanici.Role.ToLower() == "yolcu")
                    {
                        YolcuForm yolcuForm = new YolcuForm(kullanici);
                        yolcuForm.ShowDialog();
                    }
                    else if (kullanici.Role.ToLower() == "admin" || kullanici.Role.ToLower() == "kullanici")
                    {
                        KullaniciForm adminForm = new KullaniciForm(kullanici);
                        adminForm.ShowDialog();
                    }
                    else if (kullanici.Role.ToLower() == "pilot")
                    {
                        MessageBox.Show("Pilot formu henüz eklenmedi.\n\nŞimdilik sadece Yolcu ve Admin formları çalışıyor.",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else if (kullanici.Role.ToLower() == "hostes")
                    {
                        MessageBox.Show("Hostes formu henüz eklenmedi.\n\nŞimdilik sadece Yolcu ve Admin formları çalışıyor.",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Bilinmeyen rol: {kullanici.Role}\n\nDiğer formlar henüz eklenmedi.",
                            "Bilgi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Form açma hatası:\n\n{ex.Message}",
                        "Hata",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

                // Formu tekrar göster
                this.Show();

                // Şifreyi temizle (güvenlik için)
                txtSifre.Clear();
                txtEposta.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "GİRİŞ HATASI!\n\n" +
                    "───────────────────────\n" +
                    $"Hata: {ex.Message}\n\n" +
                    (ex.InnerException != null ? $"Detay: {ex.InnerException.Message}" : ""),
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Show();
            }
        }

        private void btnSifremiUnuttum_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEposta.Text))
                {
                    MessageBox.Show("Lütfen e-posta adresinizi girin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEposta.Focus();
                    return;
                }

                string sifre = KullaniciManager.SifremiUnuttum(txtEposta.Text.Trim().ToLower());

                MessageBox.Show(
                    "✓ ŞİFRE HATIRLATMA\n\n" +
                    "───────────────────────\n" +
                    $"E-posta: {txtEposta.Text}\n" +
                    $"Şifreniz: {sifre}\n" +
                    "───────────────────────\n\n" +
                    "ℹ Not: Gerçek uygulamada bu bilgi\ne-posta ile gönderilir.",
                    "Şifre Hatırlatma",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Şifreyi otomatik doldur (kullanıcı kolaylığı için)
                txtSifre.Text = sifre;
                txtSifre.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "HATA!\n\n" +
                    "───────────────────────\n" +
                    $"{ex.Message}\n" +
                    "───────────────────────\n\n" +
                    "Bu e-posta adresi kayıtlı değil olabilir.\n" +
                    "Lütfen önce kayıt olun!",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Designer'dan gelen event handler'lar için boş metodlar
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Boş - Designer için gerekli
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Boş - Designer için gerekli
        }

        // Form kapatma olayı
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                "Uygulamadan çıkmak istediğinizden emin misiniz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (cevap == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

       
    }
}