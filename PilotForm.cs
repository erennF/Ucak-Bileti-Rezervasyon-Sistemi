// uçakSistemi\PilotForm.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace uçakSistemi
{
    public partial class PilotForm : Form
    {
        private List<Ucus> _ucuslar = new List<Ucus>();
        private Dictionary<Ucus, string> _iptalNedenleri = new Dictionary<Ucus, string>();
        private Dictionary<string, List<Ucus>> _pilotUcusAtamasi = new Dictionary<string, List<Ucus>>();
        private Pilot _currentPilot;

        public PilotForm()
        {
            InitializeComponent();
           
            btnBilgilerimiGuncelle.Click += btnBilgileriGuncelle_Click; // Bilgilerimi Güncelle
            btnKisiselBilgiler.Click += btnBilgileriGoruntule_Click; // Kişisel Bilgiler

            SeedSampleData();
        }

        private void PilotForm_Load(object sender, EventArgs e)
        {
            // Tag ile Pilot ya da Kullanıcı gelebilir
            if (this.Tag is Pilot p) _currentPilot = p;
            else if (this.Tag is Kullanıcı k)
            {
                _currentPilot = new Pilot("PIL" + k.Id, 0, "Kaptan")
                {
                    Ad = k.Ad,
                    Soyad = k.Soyad,
                    Eposta = k.Eposta,
                    TelefonNumarasi = k.TelefonNumarasi,
                    Şifre = k.Şifre,
                    Role = k.Role,
                    KayitTarihi = k.KayitTarihi
                };
            }
            else
            {
                _currentPilot = new Pilot("PIL999", 0, "Kaptan") { Ad = "Örnek", Soyad = "Pilot", Eposta = "pilot@ornek.com" };
            }
        }

        private void SeedSampleData()
        {
            var u1 = new Ucus("İstanbul", DateTime.Now.AddDays(-20).AddHours(9), "Ankara", DateTime.Now.AddDays(-20).AddHours(10), TimeSpan.FromHours(1), "A1", "08:30", 9, 200m, "ÖrnekAir");
            var u2 = new Ucus("İstanbul", DateTime.Now.AddDays(2).AddHours(14), "İzmir", DateTime.Now.AddDays(2).AddHours(15), TimeSpan.FromHours(1), "B3", "13:30", 13, 300m, "ÖrnekAir");
            var u3 = new Ucus("Ankara", DateTime.Now.AddDays(4).AddHours(12), "İstanbul", DateTime.Now.AddDays(4).AddHours(13), TimeSpan.FromHours(1), "C2", "11:30", 11, 260m, "ÖrnekAir");
            var u4 = new Ucus("İstanbul", DateTime.Now.AddDays(6).AddHours(10), "Adana", DateTime.Now.AddDays(6).AddHours(11), TimeSpan.FromHours(1), "D4", "09:30", 15, 350m, "ÖrnekAir");

            _ucuslar.AddRange(new[] { u1, u2, u3, u4 });

            // admin iptali örneği
            _iptalNedenleri[u3] = "Hava koşulları nedeniyle iptal edildi (admin).";

            // pilot atamaları (pilot lisans numarası -> uçuşlar)
            _pilotUcusAtamasi["PIL999"] = new List<Ucus> { u1, u2 };
            _pilotUcusAtamasi["PIL1"] = new List<Ucus> { u3, u4 };
        }

        // Uçuşlarımı Görüntüle
        private void btnUcusGoruntule_Click(object sender, EventArgs e)
        {
            var key = _currentPilot?.LisansNumarasi ?? "PIL999";
            var assigned = _pilotUcusAtamasi.ContainsKey(key) ? _pilotUcusAtamasi[key] : new List<Ucus>();

            using (var f = new Form())
            {
                f.Text = "Uçuşlarım";
                f.Width = 900;
                f.Height = 520;

                var rbAll = new RadioButton { Text = "Tüm Uçuşlar", Left = 10, Top = 10, Checked = true };
                var rbNextWeek = new RadioButton { Text = "Önümüzdeki 1 Hafta", Left = 140, Top = 10 };

                var lblTotal = new Label { Left = 320, Top = 12, Width = 250, Text = "" };
                var lblNextWeekCount = new Label { Left = 600, Top = 12, Width = 250, Text = "" };

                var lv = new ListView { Left = 10, Top = 40, Width = 860, Height = 420, View = View.Details, FullRowSelect = true };
                lv.Columns.Add("Kalkış"); lv.Columns.Add("Varış"); lv.Columns.Add("Kalkış Tarihi"); lv.Columns.Add("Varış Tarihi");
                lv.Columns.Add("Uçak Kodu"); lv.Columns.Add("İptal Nedeni");

                Action refresh = () =>
                {
                    lv.Items.Clear();
                    var now = DateTime.Now;
                    var weekEnd = now.AddDays(7);

                    var totalCount = assigned.Count;
                    var nextWeekCount = assigned.Count(u => u.KalkısTarihi >= now && u.KalkısTarihi <= weekEnd);

                    IEnumerable<Ucus> list = assigned;
                    if (rbNextWeek.Checked) list = list.Where(u => u.KalkısTarihi >= now && u.KalkısTarihi <= weekEnd);

                    foreach (var uc in list.OrderBy(u => u.KalkısTarihi))
                    {
                        var neden = _iptalNedenleri.ContainsKey(uc) ? _iptalNedenleri[uc] : "";
                        var item = new ListViewItem(new[]
                        {
                            uc.KalkisNoktasi,
                            uc.VarisNoktasi,
                            uc.KalkısTarihi.ToString(),
                            uc.VarisTarihi.ToString(),
                            uc.UcakKodu.ToString(),
                            neden
                        });
                        item.Tag = uc;
                        lv.Items.Add(item);
                    }

                    lblTotal.Text = $"Toplam Atanan Uçuş: {totalCount}";
                    lblNextWeekCount.Text = $"Önümüzdeki 1 Hafta: {nextWeekCount}";
                };

                rbAll.CheckedChanged += (s, ea) => refresh();
                rbNextWeek.CheckedChanged += (s, ea) => refresh();

                refresh();

                var lblInfo = new Label { Left = 10, Top = 470, Width = 860, Text = "Not: Uçuş iptalleri yalnızca admin tarafından yapılır; iptal nedenleri listede görünür." };

                f.Controls.AddRange(new Control[] { rbAll, rbNextWeek, lblTotal, lblNextWeekCount, lv, lblInfo });
                f.ShowDialog();
            }
        }

        // Bilgilerimi Güncelle (ad değiştirilemez)
        private void btnBilgileriGuncelle_Click(object sender, EventArgs e)
        {
            using (var f = new Form())
            {
                f.Text = "Bilgilerimi Güncelle";
                f.Width = 400;
                f.Height = 320;

                var lblAd = new Label { Text = "Ad:", Left = 10, Top = 10 };
                var tbAd = new TextBox { Left = 120, Top = 8, Width = 200, Text = _currentPilot?.Ad ?? "", Enabled = false }; // Ad değiştirilemez
                var lblSoyad = new Label { Text = "Soyad:", Left = 10, Top = 50 };
                var tbSoyad = new TextBox { Left = 120, Top = 48, Width = 200, Text = _currentPilot?.Soyad ?? "" };
                var lblEposta = new Label { Text = "E-posta:", Left = 10, Top = 90 };
                var tbEposta = new TextBox { Left = 120, Top = 88, Width = 200, Text = _currentPilot?.Eposta ?? "" };
                var lblTel = new Label { Text = "Telefon:", Left = 10, Top = 130 };
                var tbTel = new TextBox { Left = 120, Top = 128, Width = 200, Text = _currentPilot?.TelefonNumarasi ?? "" };

                var btnSave = new Button { Text = "Kaydet", Left = 120, Top = 170, Width = 100 };
                btnSave.Click += (s, ea) =>
                {
                    if (_currentPilot != null)
                    {
                        _currentPilot.Soyad = tbSoyad.Text;
                        _currentPilot.Eposta = tbEposta.Text;
                        _currentPilot.TelefonNumarasi = tbTel.Text;
                        MessageBox.Show("Bilgiler güncellendi.");
                        f.Close();
                    }
                };

                f.Controls.AddRange(new Control[] { lblAd, tbAd, lblSoyad, tbSoyad, lblEposta, tbEposta, lblTel, tbTel, btnSave });
                f.ShowDialog();
            }
        }

        // Kişisel Bilgiler (görüntüleme, değiştirilemez)
        private void btnBilgileriGoruntule_Click(object sender, EventArgs e)
        {
            using (var f = new Form())
            {
                f.Text = "Kişisel Bilgiler (Görüntüleme)";
                f.Width = 400;
                f.Height = 300;

                var y = 10;
                void AddLabel(string title, string value)
                {
                    var lblTitle = new Label { Left = 10, Top = y, Width = 120, Text = title };
                    var lblValue = new TextBox { Left = 140, Top = y - 3, Width = 220, Text = value, ReadOnly = true };
                    f.Controls.Add(lblTitle);
                    f.Controls.Add(lblValue);
                    y += 30;
                }

                AddLabel("Ad:", _currentPilot?.Ad ?? "");
                AddLabel("Soyad:", _currentPilot?.Soyad ?? "");
                AddLabel("E-posta:", _currentPilot?.Eposta ?? "");
                AddLabel("Telefon:", _currentPilot?.TelefonNumarasi ?? "");
                AddLabel("Lisans No:", _currentPilot?.LisansNumarasi ?? "");
                AddLabel("Rütbe:", _currentPilot?.Rutbe ?? "");

                f.ShowDialog();
            }
        }
    }
}
