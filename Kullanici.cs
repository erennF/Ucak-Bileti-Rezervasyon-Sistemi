using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class KullaniciForm : Form
    {
        private Kullanıcı _aktifKullanici;

        public KullaniciForm(Kullanıcı kullanici)
        {
            InitializeComponent();
            _aktifKullanici = kullanici;
            this.Text = $"Admin Paneli - {kullanici.Ad} {kullanici.Soyad}";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeAdminUI();
        }

        private void InitializeAdminUI()
        {
            this.Controls.Clear();

            // Üst panel - Hoşgeldiniz mesajı
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(52, 152, 219)
            };

            Label lblHosgeldin = new Label
            {
                Text = $"Admin Paneli - Hoş Geldiniz {_aktifKullanici.Ad} {_aktifKullanici.Soyad}",
                ForeColor = Color.White,
                Font = new Font("Arial", 16, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            topPanel.Controls.Add(lblHosgeldin);

            // Sol panel - Menü butonları
            Panel leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 250,
                BackColor = Color.FromArgb(44, 62, 80)
            };

            int y = 20;
            Button btnUcusIslemleri = CreateMenuButton("Uçuş İşlemleri", y);
            btnUcusIslemleri.Click += BtnUcusIslemleri_Click;
            y += 60;

            Button btnRezervasyonlar = CreateMenuButton("Rezervasyonlar", y);
            btnRezervasyonlar.Click += BtnRezervasyonlar_Click;
            y += 60;

            Button btnPilotHostes = CreateMenuButton("Pilot/Hostes İşlemleri", y);
            btnPilotHostes.Click += BtnPilotHostes_Click;
            y += 60;

            Button btnKullanicilar = CreateMenuButton("Kullanıcı Yönetimi", y);
            btnKullanicilar.Click += BtnKullanicilar_Click;
            y += 60;

            Button btnRaporlar = CreateMenuButton("Raporlar", y);
            btnRaporlar.Click += BtnRaporlar_Click;
            y += 60;

            Button btnVeritabani = CreateMenuButton("Veritabanı Yönetimi", y);
            btnVeritabani.Click += BtnVeritabani_Click;
            y += 60;

            Button btnCikis = CreateMenuButton("Çıkış", y);
            btnCikis.BackColor = Color.IndianRed;
            btnCikis.Click += (s, e) => this.Close();

            leftPanel.Controls.AddRange(new Control[] {
                btnUcusIslemleri, btnRezervasyonlar, btnPilotHostes,
                btnKullanicilar, btnRaporlar, btnVeritabani, btnCikis
            });

            // Orta panel - İçerik alanı
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(20)
            };

            Label lblIcerik = new Label
            {
                Text = "Sol menüden bir işlem seçin",
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.Gray
            };

            contentPanel.Controls.Add(lblIcerik);

            this.Controls.Add(contentPanel);
            this.Controls.Add(leftPanel);
            this.Controls.Add(topPanel);
        }

        private Button CreateMenuButton(string text, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(230, 50),
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(52, 73, 94),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
        }

        private void BtnUcusIslemleri_Click(object sender, EventArgs e)
        {
            try
            {
                Form ucusForm = new Form
                {
                    Text = "Uçuş İşlemleri",
                    Width = 1100,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };

                DataGridView dgv = new DataGridView
                {
                    Dock = DockStyle.Top,
                    Height = 450,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                Panel buttonPanel = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 150
                };

                Button btnYenile = new Button { Text = "Yenile", Left = 20, Top = 20, Width = 120, Height = 40 };
                Button btnEkle = new Button { Text = "Yeni Uçuş Ekle", Left = 160, Top = 20, Width = 150, Height = 40, BackColor = Color.LightGreen };
                Button btnGuncelle = new Button { Text = "Güncelle", Left = 330, Top = 20, Width = 120, Height = 40, BackColor = Color.LightBlue };
                Button btnIptal = new Button { Text = "İptal Et", Left = 470, Top = 20, Width = 120, Height = 40, BackColor = Color.IndianRed };
                Button btnDetay = new Button { Text = "Detay Gör", Left = 610, Top = 20, Width = 120, Height = 40 };

                btnYenile.Click += (s, ev) => dgv.DataSource = UcusManager.TumUcuslariListele();
                btnEkle.Click += (s, ev) => UcusEkleGuncelle(null, dgv);
                btnGuncelle.Click += (s, ev) => {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        int ucusId = Convert.ToInt32(dgv.SelectedRows[0].Cells["UcusId"].Value);
                        UcusEkleGuncelle(ucusId, dgv);
                    }
                    else
                        MessageBox.Show("Lütfen bir uçuş seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };
                btnIptal.Click += (s, ev) => {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        int ucusId = Convert.ToInt32(dgv.SelectedRows[0].Cells["UcusId"].Value);
                        DialogResult cevap = MessageBox.Show("Uçuşu iptal etmek istediğinizden emin misiniz?",
                            "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (cevap == DialogResult.Yes && UcusManager.UcusIptal(ucusId))
                        {
                            MessageBox.Show("Uçuş iptal edildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv.DataSource = UcusManager.TumUcuslariListele();
                        }
                    }
                };
                btnDetay.Click += (s, ev) => {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        int ucusId = Convert.ToInt32(dgv.SelectedRows[0].Cells["UcusId"].Value);
                        UcusDetayGoster(ucusId);
                    }
                };

                buttonPanel.Controls.AddRange(new Control[] { btnYenile, btnEkle, btnGuncelle, btnIptal, btnDetay });

                dgv.DataSource = UcusManager.TumUcuslariListele();

                ucusForm.Controls.Add(dgv);
                ucusForm.Controls.Add(buttonPanel);
                ucusForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UcusEkleGuncelle(int? ucusId, DataGridView dgv)
        {
            Form form = new Form
            {
                Text = ucusId.HasValue ? "Uçuş Güncelle" : "Yeni Uçuş Ekle",
                Width = 500,
                Height = 650,
                StartPosition = FormStartPosition.CenterParent
            };

            int y = 20;
            Label lblKalkis = new Label { Text = "Kalkış:", Left = 20, Top = y, Width = 120 };
            TextBox txtKalkis = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblVaris = new Label { Text = "Varış:", Left = 20, Top = y, Width = 120 };
            TextBox txtVaris = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblKalkisTarih = new Label { Text = "Kalkış Tarihi:", Left = 20, Top = y, Width = 120 };
            DateTimePicker dtpKalkis = new DateTimePicker { Left = 150, Top = y, Width = 300, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
            y += 40;

            Label lblVarisTarih = new Label { Text = "Varış Tarihi:", Left = 20, Top = y, Width = 120 };
            DateTimePicker dtpVaris = new DateTimePicker { Left = 150, Top = y, Width = 300, Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm" };
            y += 40;

            Label lblFiyat = new Label { Text = "Fiyat:", Left = 20, Top = y, Width = 120 };
            NumericUpDown numFiyat = new NumericUpDown { Left = 150, Top = y, Width = 300, Maximum = 10000, Minimum = 100, Value = 500 };
            y += 40;

            Label lblKapiNo = new Label { Text = "Kapı No:", Left = 20, Top = y, Width = 120 };
            TextBox txtKapiNo = new TextBox { Left = 150, Top = y, Width = 300 };
            y += 40;

            Label lblBosKoltuk = new Label { Text = "Boş Koltuk:", Left = 20, Top = y, Width = 120 };
            NumericUpDown numBosKoltuk = new NumericUpDown { Left = 150, Top = y, Width = 300, Maximum = 500, Minimum = 0, Value = 180 };
            y += 60;

            Button btnKaydet = new Button { Text = "Kaydet", Left = 150, Top = y, Width = 140, Height = 50, BackColor = Color.LightGreen };
            Button btnIptal = new Button { Text = "İptal", Left = 310, Top = y, Width = 140, Height = 50, BackColor = Color.LightCoral };

            btnKaydet.Click += (s, e) => {
                try
                {
                    if (ucusId.HasValue)
                    {
                        bool sonuc = UcusManager.UcusGuncelle(ucusId.Value, txtKalkis.Text, txtVaris.Text,
                            dtpKalkis.Value, dtpVaris.Value, numFiyat.Value, txtKapiNo.Text, (int)numBosKoltuk.Value);
                        if (sonuc)
                        {
                            MessageBox.Show("Uçuş güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv.DataSource = UcusManager.TumUcuslariListele();
                            form.Close();
                        }
                    }
                    else
                    {
                        bool sonuc = UcusManager.UcusEkle(txtKalkis.Text, txtVaris.Text, dtpKalkis.Value, dtpVaris.Value,
                            "01:00", txtKapiNo.Text, "08:30", 9, 1, numFiyat.Value, "Türk Hava Yolları", (int)numBosKoltuk.Value);
                        if (sonuc)
                        {
                            MessageBox.Show("Uçuş eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgv.DataSource = UcusManager.TumUcuslariListele();
                            form.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnIptal.Click += (s, e) => form.Close();

            form.Controls.AddRange(new Control[] {
                lblKalkis, txtKalkis, lblVaris, txtVaris, lblKalkisTarih, dtpKalkis,
                lblVarisTarih, dtpVaris, lblFiyat, numFiyat, lblKapiNo, txtKapiNo,
                lblBosKoltuk, numBosKoltuk, btnKaydet, btnIptal
            });

            if (ucusId.HasValue)
            {
                DataTable dt = UcusManager.UcusDetay(ucusId.Value);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtKalkis.Text = row["KalkisNoktasi"].ToString();
                    txtVaris.Text = row["VarisNoktasi"].ToString();
                    dtpKalkis.Value = Convert.ToDateTime(row["KalkisTarihi"]);
                    dtpVaris.Value = Convert.ToDateTime(row["VarisTarihi"]);
                    numFiyat.Value = Convert.ToDecimal(row["Fiyat"]);
                    txtKapiNo.Text = row["KapiNumarasi"].ToString();
                    numBosKoltuk.Value = Convert.ToInt32(row["BosKoltukSayisi"]);
                }
            }

            form.ShowDialog();
        }

        private void UcusDetayGoster(int ucusId)
        {
            try
            {
                DataTable rezervasyonlar = RezervasyonManager.UcusRezervasyonlari(ucusId);

                Form detayForm = new Form
                {
                    Text = $"Uçuş Detayları - ID: {ucusId}",
                    Width = 900,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };

                Label lblBaslik = new Label
                {
                    Text = $"Toplam Rezervasyon: {rezervasyonlar.Rows.Count}",
                    Dock = DockStyle.Top,
                    Height = 40,
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                DataGridView dgv = new DataGridView
                {
                    DataSource = rezervasyonlar,
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                detayForm.Controls.Add(dgv);
                detayForm.Controls.Add(lblBaslik);
                detayForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRezervasyonlar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable rezervasyonlar = RezervasyonManager.TumRezervasyonlar();

                Form rezForm = new Form
                {
                    Text = "Tüm Rezervasyonlar",
                    Width = 1100,
                    Height = 650,
                    StartPosition = FormStartPosition.CenterParent
                };

                DataGridView dgv = new DataGridView
                {
                    DataSource = rezervasyonlar,
                    Dock = DockStyle.Top,
                    Height = 500,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                Button btnDetay = new Button
                {
                    Text = "Seçili Rezervasyon Detayı",
                    Left = 400,
                    Top = 520,
                    Width = 250,
                    Height = 50
                };

                btnDetay.Click += (s, ev) => {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        int rezId = Convert.ToInt32(dgv.SelectedRows[0].Cells["RezervasyonId"].Value);
                        RezervasyonDetayGoster(rezId);
                    }
                };

                rezForm.Controls.Add(dgv);
                rezForm.Controls.Add(btnDetay);
                rezForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RezervasyonDetayGoster(int rezervasyonId)
        {
            try
            {
                DataTable detay = RezervasyonManager.RezervasyonDetay(rezervasyonId);

                if (detay.Rows.Count == 0)
                {
                    MessageBox.Show("Rezervasyon bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = detay.Rows[0];
                string mesaj = $@"
=== REZERVASYON DETAYI ===

Bilet No: {row["BiletNumarasi"]}
Yolcu: {row["Ad"]} {row["Soyad"]}
E-posta: {row["Eposta"]}
Telefon: {row["TelefonNumarasi"]}

Uçuş Bilgileri:
{row["KalkisNoktasi"]} → {row["VarisNoktasi"]}
Kalkış: {Convert.ToDateTime(row["KalkisTarihi"]):dd.MM.yyyy HH:mm}
Varış: {Convert.ToDateTime(row["VarisTarihi"]):dd.MM.yyyy HH:mm}

Koltuk: {row["KoltukNumarasi"]} ({row["KoltukTipi"]})
Fiyat: {Convert.ToDecimal(row["Fiyat"]):C2}
Durum: {row["RezervasyonDurumu"]}

Havayolu: {row["HavayoluSirketi"]}
Kapı: {row["KapiNumarasi"]}
                ";

                MessageBox.Show(mesaj, "Rezervasyon Detayı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPilotHostes_Click(object sender, EventArgs e)
        {
            try
            {
                PilotHostesIslemleriForm form = new PilotHostesIslemleriForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKullanicilar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable kullanicilar = KullaniciManager.KullanicilariListele();

                Form kullaniciForm = new Form
                {
                    Text = "Kullanıcı Yönetimi",
                    Width = 1000,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };

                DataGridView dgv = new DataGridView
                {
                    DataSource = kullanicilar,
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                kullaniciForm.Controls.Add(dgv);
                kullaniciForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRaporlar_Click(object sender, EventArgs e)
        {
            try
            {
                Form raporForm = new Form
                {
                    Text = "Raporlar ve İstatistikler",
                    Width = 900,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };

                TabControl tabControl = new TabControl { Dock = DockStyle.Fill };

                // İstatistikler Sekmesi
                TabPage tabIstatistik = new TabPage("Rezervasyon İstatistikleri");
                DataGridView dgvIstatistik = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };
                dgvIstatistik.DataSource = RezervasyonManager.RezervasyonIstatistikleri();
                tabIstatistik.Controls.Add(dgvIstatistik);

                // Popüler Rotalar Sekmesi
                TabPage tabPopuler = new TabPage("Popüler Rotalar");
                DataGridView dgvPopuler = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };
                dgvPopuler.DataSource = UcusManager.PopulerRotalar(20);
                tabPopuler.Controls.Add(dgvPopuler);

                // Günlük Rapor Sekmesi
                TabPage tabGunluk = new TabPage("Günlük Rapor");
                DateTimePicker dtpRapor = new DateTimePicker
                {
                    Dock = DockStyle.Top,
                    Format = DateTimePickerFormat.Short
                };
                DataGridView dgvGunluk = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };
                dtpRapor.ValueChanged += (s, ev) => {
                    dgvGunluk.DataSource = RezervasyonManager.GunlukRaporlar(dtpRapor.Value);
                };
                dgvGunluk.DataSource = RezervasyonManager.GunlukRaporlar(DateTime.Now);
                tabGunluk.Controls.Add(dgvGunluk);
                tabGunluk.Controls.Add(dtpRapor);

                tabControl.TabPages.AddRange(new TabPage[] { tabIstatistik, tabPopuler, tabGunluk });

                raporForm.Controls.Add(tabControl);
                raporForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVeritabani_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                "Veritabanını yeniden oluşturmak istediğinizden emin misiniz?\n\n" +
                "Bu işlem mevcut verileri silecek ve örnek verilerle yeni bir veritabanı oluşturacaktır.",
                "Dikkat!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (cevap == DialogResult.Yes)
            {
                try
                {
                    VeritabaniHelper.InitializeDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}