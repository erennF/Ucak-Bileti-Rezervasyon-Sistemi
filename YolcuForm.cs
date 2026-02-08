using System;
using System.Data;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class YolcuForm : Form
    {
        private Kullanıcı _aktifKullanici;

        public YolcuForm(Kullanıcı kullanici)
        {
            InitializeComponent();
            _aktifKullanici = kullanici;
            this.Text = $"Yolcu Paneli - {kullanici.Ad} {kullanici.Soyad}";
            this.Size = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Controls.Clear();

            Label lblBaslik = new Label
            {
                Text = $"Hoş Geldiniz {_aktifKullanici.Ad} {_aktifKullanici.Soyad}",
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(740, 40),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            Button btnUcusAra = new Button
            {
                Text = "Uçuş Ara ve Rezervasyon Yap",
                Location = new System.Drawing.Point(250, 100),
                Size = new System.Drawing.Size(280, 60),
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            btnUcusAra.Click += BtnUcusAra_Click;

            Button btnRezervasyonlarim = new Button
            {
                Text = "Rezervasyonlarım",
                Location = new System.Drawing.Point(250, 180),
                Size = new System.Drawing.Size(280, 60),
                Font = new System.Drawing.Font("Arial", 12)
            };
            btnRezervasyonlarim.Click += BtnRezervasyonlarim_Click;

            Button btnBilgileriGuncelle = new Button
            {
                Text = "Bilgilerimi Güncelle",
                Location = new System.Drawing.Point(250, 260),
                Size = new System.Drawing.Size(280, 60),
                Font = new System.Drawing.Font("Arial", 12)
            };
            btnBilgileriGuncelle.Click += BtnBilgileriGuncelle_Click;

            Button btnIndirimler = new Button
            {
                Text = "İndirim Kodları",
                Location = new System.Drawing.Point(250, 340),
                Size = new System.Drawing.Size(280, 60),
                Font = new System.Drawing.Font("Arial", 12)
            };
            btnIndirimler.Click += BtnIndirimler_Click;

            Button btnCikis = new Button
            {
                Text = "Çıkış",
                Location = new System.Drawing.Point(250, 420),
                Size = new System.Drawing.Size(280, 60),
                Font = new System.Drawing.Font("Arial", 12),
                BackColor = System.Drawing.Color.IndianRed
            };
            btnCikis.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] {
                lblBaslik, btnUcusAra, btnRezervasyonlarim,
                btnBilgileriGuncelle, btnIndirimler, btnCikis
            });
        }

        private void BtnUcusAra_Click(object sender, EventArgs e)
        {
            Form aramaForm = new Form
            {
                Text = "Uçuş Ara",
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterParent
            };

            Label lblKalkis = new Label { Text = "Kalkış:", Left = 20, Top = 20, Width = 100 };
            TextBox txtKalkis = new TextBox { Left = 130, Top = 20, Width = 200 };

            Label lblVaris = new Label { Text = "Varış:", Left = 20, Top = 60, Width = 100 };
            TextBox txtVaris = new TextBox { Left = 130, Top = 60, Width = 200 };

            Label lblTarih = new Label { Text = "Tarih:", Left = 20, Top = 100, Width = 100 };
            DateTimePicker dtpTarih = new DateTimePicker
            {
                Left = 130,
                Top = 100,
                Width = 200,
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Now
            };
            CheckBox chkTarih = new CheckBox { Text = "Tarih filtresi", Left = 340, Top = 100, Width = 150 };

            Button btnAra = new Button { Text = "Ara", Left = 130, Top = 140, Width = 100, Height = 35 };

            DataGridView dgvSonuclar = new DataGridView
            {
                Left = 20,
                Top = 190,
                Width = 840,
                Height = 350,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false
            };

            Button btnRezervasyonYap = new Button
            {
                Text = "Seçili Uçuş için Rezervasyon Yap",
                Left = 300,
                Top = 560,
                Width = 280,
                Height = 50,
                Enabled = false
            };

            btnAra.Click += (s, ev) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtKalkis.Text) || string.IsNullOrWhiteSpace(txtVaris.Text))
                    {
                        MessageBox.Show("Kalkış ve varış noktalarını girin!", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DateTime? tarih = chkTarih.Checked ? (DateTime?)dtpTarih.Value : null;
                    DataTable sonuclar = UcusManager.UcusAra(txtKalkis.Text, txtVaris.Text, tarih);

                    if (sonuclar.Rows.Count == 0)
                    {
                        MessageBox.Show("Arama kriterlerinize uygun uçuş bulunamadı!", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvSonuclar.DataSource = null;
                        btnRezervasyonYap.Enabled = false;
                        return;
                    }

                    sonuclar.Columns.Add("GuncelFiyat", typeof(decimal));
                    foreach (DataRow row in sonuclar.Rows)
                    {
                        int ucusId = Convert.ToInt32(row["UcusId"]);
                        decimal fiyat = DinamikFiyatlandirma.FiyatHesapla(ucusId, "Ekonomi");
                        row["GuncelFiyat"] = fiyat;
                    }

                    dgvSonuclar.DataSource = sonuclar;

                    if (dgvSonuclar.Columns.Contains("UcusId"))
                        dgvSonuclar.Columns["UcusId"].HeaderText = "Uçuş No";
                    if (dgvSonuclar.Columns.Contains("GuncelFiyat"))
                    {
                        dgvSonuclar.Columns["GuncelFiyat"].HeaderText = "Fiyat (₺)";
                        dgvSonuclar.Columns["GuncelFiyat"].DefaultCellStyle.Format = "C2";
                    }

                    btnRezervasyonYap.Enabled = true;
                    MessageBox.Show($"{sonuclar.Rows.Count} uçuş bulundu!", "Başarılı",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnRezervasyonYap.Click += (s, ev) =>
            {
                if (dgvSonuclar.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen bir uçuş seçin!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int ucusId = Convert.ToInt32(dgvSonuclar.SelectedRows[0].Cells["UcusId"].Value);
                RezervasyonFormuAc(ucusId);
            };

            aramaForm.Controls.AddRange(new Control[] {
                lblKalkis, txtKalkis, lblVaris, txtVaris, lblTarih, dtpTarih, chkTarih,
                btnAra, dgvSonuclar, btnRezervasyonYap
            });

            aramaForm.ShowDialog();
        }

        private void RezervasyonFormuAc(int ucusId)
        {
            try
            {
                DataTable ucusDetay = UcusManager.UcusDetay(ucusId);
                if (ucusDetay.Rows.Count == 0)
                {
                    MessageBox.Show("Uçuş bilgileri alınamadı!", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow ucus = ucusDetay.Rows[0];

                Form rezervasyonForm = new Form
                {
                    Text = "Rezervasyon Yap",
                    Width = 600,
                    Height = 550,
                    StartPosition = FormStartPosition.CenterParent
                };

                int y = 20;

                Label lblBilgi = new Label
                {
                    Text = $"Uçuş: {ucus["KalkisNoktasi"]} → {ucus["VarisNoktasi"]}\n" +
                           $"Tarih: {Convert.ToDateTime(ucus["KalkisTarihi"]):dd.MM.yyyy HH:mm}\n" +
                           $"Havayolu: {ucus["HavayoluSirketi"]}\n" +
                           $"Boş Koltuk: {ucus["BosKoltukSayisi"]}",
                    Left = 20,
                    Top = y,
                    Width = 540,
                    Height = 80,
                    Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
                };
                y += 90;

                Label lblKoltuk = new Label { Text = "Koltuk No:", Left = 20, Top = y, Width = 120 };
                TextBox txtKoltuk = new TextBox { Left = 150, Top = y, Width = 100 };
                y += 40;

                Label lblKoltukTipi = new Label { Text = "Koltuk Tipi:", Left = 20, Top = y, Width = 120 };
                ComboBox cmbKoltukTipi = new ComboBox
                {
                    Left = 150,
                    Top = y,
                    Width = 150,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbKoltukTipi.Items.AddRange(new string[] { "Ekonomi", "Business", "FirstClass" });
                cmbKoltukTipi.SelectedIndex = 0;
                y += 40;

                Label lblIndirim = new Label { Text = "İndirim Kodu:", Left = 20, Top = y, Width = 120 };
                TextBox txtIndirim = new TextBox { Left = 150, Top = y, Width = 150 };
                y += 40;

                Label lblFiyat = new Label
                {
                    Text = "Fiyat: Hesaplanıyor...",
                    Left = 20,
                    Top = y,
                    Width = 540,
                    Height = 60,
                    Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                    ForeColor = System.Drawing.Color.Green
                };
                y += 70;

                EventHandler fiyatHesapla = (s, ev) =>
                {
                    try
                    {
                        string koltukTipi = cmbKoltukTipi.SelectedItem?.ToString() ?? "Ekonomi";
                        string indirimKodu = string.IsNullOrWhiteSpace(txtIndirim.Text) ? null : txtIndirim.Text.Trim();
                        decimal fiyat = DinamikFiyatlandirma.FiyatHesapla(ucusId, koltukTipi, indirimKodu);
                        lblFiyat.Text = $"Ödenecek Tutar: {fiyat:C2}";
                    }
                    catch
                    {
                        lblFiyat.Text = "Fiyat hesaplanamadı!";
                    }
                };

                cmbKoltukTipi.SelectedIndexChanged += fiyatHesapla;
                txtIndirim.TextChanged += fiyatHesapla;
                fiyatHesapla(null, null);

                Button btnOnayla = new Button
                {
                    Text = "Rezervasyonu Onayla",
                    Left = 180,
                    Top = y,
                    Width = 220,
                    Height = 50,
                    Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold),
                    BackColor = System.Drawing.Color.LightGreen
                };

                btnOnayla.Click += (s, ev) =>
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(txtKoltuk.Text))
                        {
                            MessageBox.Show("Koltuk numarası girin!", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string koltukTipi = cmbKoltukTipi.SelectedItem.ToString();
                        string indirimKodu = string.IsNullOrWhiteSpace(txtIndirim.Text) ? null : txtIndirim.Text.Trim();

                        bool sonuc = RezervasyonManager.RezervasyonOlustur(
                            ucusId,
                            _aktifKullanici.Id,
                            txtKoltuk.Text.Trim(),
                            koltukTipi,
                            indirimKodu
                        );

                        if (sonuc)
                        {
                            MessageBox.Show("✓ Rezervasyon başarıyla oluşturuldu!", "Başarılı",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            rezervasyonForm.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                rezervasyonForm.Controls.AddRange(new Control[] {
                    lblBilgi, lblKoltuk, txtKoltuk, lblKoltukTipi, cmbKoltukTipi,
                    lblIndirim, txtIndirim, lblFiyat, btnOnayla
                });

                rezervasyonForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRezervasyonlarim_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable rezervasyonlar = RezervasyonManager.KullaniciRezervasyonlari(_aktifKullanici.Id);

                Form listForm = new Form
                {
                    Text = "Rezervasyonlarım",
                    Width = 1000,
                    Height = 600,
                    StartPosition = FormStartPosition.CenterParent
                };

                DataGridView dgv = new DataGridView
                {
                    DataSource = rezervasyonlar,
                    Dock = DockStyle.Top,
                    Height = 450,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                Button btnIptal = new Button
                {
                    Text = "Seçili Rezervasyonu İptal Et",
                    Left = 350,
                    Top = 470,
                    Width = 280,
                    Height = 50
                };

                btnIptal.Click += (s, ev) =>
                {
                    if (dgv.SelectedRows.Count > 0)
                    {
                        int rezervasyonId = Convert.ToInt32(dgv.SelectedRows[0].Cells["RezervasyonId"].Value);
                        string durum = dgv.SelectedRows[0].Cells["RezervasyonDurumu"].Value.ToString();

                        if (durum == "İptal")
                        {
                            MessageBox.Show("Bu rezervasyon zaten iptal edilmiş!", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        DialogResult cevap = MessageBox.Show("Rezervasyonu iptal etmek istediğinizden emin misiniz?",
                            "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (cevap == DialogResult.Yes)
                        {
                            bool sonuc = RezervasyonManager.RezervasyonIptal(rezervasyonId);
                            if (sonuc)
                            {
                                MessageBox.Show("Rezervasyon iptal edildi!", "Başarılı",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dgv.DataSource = RezervasyonManager.KullaniciRezervasyonlari(_aktifKullanici.Id);
                            }
                        }
                    }
                };

                listForm.Controls.Add(dgv);
                listForm.Controls.Add(btnIptal);
                listForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bilgi güncelleme formu henüz eklenmedi.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnIndirimler_Click(object sender, EventArgs e)
        {
            try
            {
                string query = @"SELECT Kod AS [İndirim Kodu], IndirimOrani AS [İndirim (%)], 
                               GecerlilikBaslangic AS [Başlangıç], GecerlilikBitis AS [Bitiş]
                               FROM IndirimKodlari 
                               WHERE AktifMi = 1 
                               AND GETDATE() BETWEEN GecerlilikBaslangic AND GecerlilikBitis";

                DataTable indirimler = VeritabaniHelper.ExecuteQuery(query);

                Form indirimForm = new Form
                {
                    Text = "Kullanılabilir İndirim Kodları",
                    Width = 700,
                    Height = 500,
                    StartPosition = FormStartPosition.CenterParent
                };

                DataGridView dgv = new DataGridView
                {
                    DataSource = indirimler,
                    Dock = DockStyle.Fill,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false
                };

                indirimForm.Controls.Add(dgv);
                indirimForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
