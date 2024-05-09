using System.Windows.Forms;
using Tamir_Bingol_Proje;
using static Tamir_Bingol_Proje.Tamir;

namespace Tamir_Bingol_Proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTamir_Click(object sender, EventArgs e)
        {
            Tamir.TamirBirimiOku();
            //TamirBirimleriniGoster();
            btnTamir.Enabled = false;
            btnIsPaketi.Enabled = true;

        }

        private void btnIsPaketi_Click(object sender, EventArgs e)
        {
            Tamir.IsPaketiOku();
            btnIsPaketi.Enabled = false;
            btnServis.Enabled = true;
        }

        private void btnServis_Click(object sender, EventArgs e)
        {

            Tamir.IsPaketleriniSiralaVeIDVer();

            // �� paketlerini richTextBox'a yazd�r
            richtxtbox.AppendText("�� Paketleri:\n");
            Tamir.Is isPaket = Tamir.Isler;
            while (isPaket != null)
            {
                richtxtbox.AppendText($"Paket ID: {isPaket.PaketID}, Ar�za T�rleri: {isPaket.ArizaTurleri}, Tamir T�rleri: {isPaket.TamirTurleri}, Tamir S�resi: {isPaket.TamirSuresi}, Geli� Saati: {isPaket.GelisSaatiHour}.{isPaket.GelisSaatiMinute}, ��k�� Saati: {isPaket.CikisSaati}\n");
                isPaket = isPaket.Next;
            }

            // Tamir birimlerini richTextBox'a yazd�r
            richtxtbox.AppendText("\nTamir Birimleri:\n");
            foreach (var birim in Tamir.TamirBirimleri)
            {
                richtxtbox.AppendText($"Birim ID: {birim.TamirBirimID}, �al��an Say�s�: {birim.CalisanSayisi}, Kapasite: {birim.Kapasite}\n");
            }

            // �� paketlerini ve tamir birimlerini g�ster
            TamirBirimleriniGoster();

            // Servis ba�lat�ld���nda i�leri da��t�m �nitesine ve tamir birimlerine da��t


            while (Tamir.ServisSaati< new DateTime(1, 1, 1, 19, 0, 0))
            {
                if (Tamir.DagitilmaDurumu())
                {
                   Tamir.IsleriBirimlereDagit();
                   Tamir.TamireBasla();
                }
                else
                {
                    Tamir.DagitimUnitesine�sGonder();
                }
            }
            Kalan�sleriBul();
            MessageBox.Show("Servis Saati Dolmu�tur.");

            Kalan�slerFormu kalan�slerFormu = new Kalan�slerFormu();
            kalan�slerFormu.ShowDialog();

            

            btnServis.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnIsPaketi.Enabled=false;
            btnServis.Enabled=false;
        }

        public void TamirBirimleriniGoster()
        {
            int x = 10; // Ba�lang�� X konumu
            int y = 10; // Ba�lang�� Y konumu
            int padding = 5; // Kontroller aras� bo�luk
            int birimlerPerRow = 2; // Her sat�rda g�sterilecek tamir birimi say�s�

            for (int i = 0; i < TamirBirimleri.Length; i++)
            {
                // Tamir birimi i�in panel olu�tur
                Panel panelTamirBirimi = new Panel
                {
                    Location = new Point(x + (i % birimlerPerRow) * (200 + padding), y + (i / birimlerPerRow) * (200 + padding)),
                    Size = new Size(200, 250),
                    BorderStyle = BorderStyle.FixedSingle
                };
                this.Controls.Add(panelTamirBirimi);

                TamirBirimi birim = TamirBirimleri[i];

                // Tamir birimi ba�l��� olu�tur
                Label lblBirimBaslik = new Label
                {
                    Text = $"{birim.TamirBirimID} Nolu Tamir Birimi",
                    Location = new Point(padding, padding),
                    Size = new Size(panelTamirBirimi.Width - 2 * padding, 20),
                    Font = new Font("Arial", 12, FontStyle.Bold)
                };
                panelTamirBirimi.Controls.Add(lblBirimBaslik);

                // �� paketleri i�in panel olu�tur
                Panel panelIsPaketleri = new Panel
                {
                    Location = new Point(padding, lblBirimBaslik.Bottom + padding),
                    Size = new Size(panelTamirBirimi.Width - 2 * padding, panelTamirBirimi.Height - lblBirimBaslik.Bottom - 3 * padding),
                    BorderStyle = BorderStyle.None // �� paketleri aras�nda s�n�r olmas�n
                };
                panelTamirBirimi.Controls.Add(panelIsPaketleri);

                // �� paketlerini listele
                for (int j = 0; j < birim.Kapasite; j++)
                {
                    Button btnIs = new Button
                    {
                        Size = new Size(panelIsPaketleri.Width - 2 * padding, 20),
                        Location = new Point(padding, padding + j * (20 + padding)),
                        Font = new Font("Arial", 8)
                    };

                    Label lblIs = new Label
                    {
                        Size = new Size(panelIsPaketleri.Width - 2 * padding, 20),
                        Location = new Point(padding, btnIs.Bottom + padding),
                        Font = new Font("Arial", 8)
                    };

                    if (birim.TamirdeBulunan�sler[j] != null)
                    {
                        Is isNesnesi = birim.TamirdeBulunan�sler[j];
                        btnIs.Text = $"�� {j + 1}";
                        lblIs.Text = $"Paket ID: {isNesnesi.PaketID}, Ar�za T�rleri: {isNesnesi.ArizaTurleri}";

                        // �� paketine t�kland���nda detaylar� g�ster
                        btnIs.Tag = isNesnesi;
                        btnIs.Click += (sender, e) =>
                        {
                            Is clickedIs = (Is)((Button)sender).Tag;
                            MessageBox.Show($"Paket ID: {clickedIs.PaketID}\nAr�za T�rleri: {clickedIs.ArizaTurleri}\nTamir T�rleri: {clickedIs.TamirTurleri}\nTamir S�resi: {clickedIs.TamirSuresi}\nGeli� Saati: {clickedIs.GelisSaatiHour}:{clickedIs.GelisSaatiMinute}\n��k�� Saati: {clickedIs.CikisSaati}");
                        };
                    }
                    else
                    {
                        btnIs.Text = "Bo�";
                        lblIs.Text = "�� Yok";
                        btnIs.Enabled = false; // Bo� i� paketleri t�klanamaz
                    }

                    panelIsPaketleri.Controls.Add(btnIs);
                    panelIsPaketleri.Controls.Add(lblIs);
                }
            }
        }





    }
}
