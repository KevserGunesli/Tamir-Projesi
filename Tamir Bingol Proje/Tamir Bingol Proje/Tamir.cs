using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tamir_Bingol_Proje
{
    internal class Tamir
    {
        public static Is[] DagitimUnitesi = new Is[25];
        public static Is Isler = new Is();
        public static TamirBirimi[] TamirBirimleri;// uzunluk belli değil
        public static DateTime ServisSaati = new DateTime(1, 1, 1, 8, 0, 0); // Servisin Başlangıç saati

        //servis saatinde bttiği zaman kalan iş paketleri için oluşturulan listeler 
        public static List<Is> BitenİsPaketleri = new List<Is>();
        public static List<Is> TamirdeKalanİsPaketleri=new List<Is>();
        public static List<Is> DagitimUnitesindekiİsPaketleri = new List<Is>();

        // Arıza türleri ve sürelerini içeren dictionary'yi buraya taşıdık
        public static Dictionary<int, int> arizaTurleriVeSureleri = new Dictionary<int, int>()
        {
            {1, 10},    // Arıza Türü 1 için süre 10 dakika
            {2, 15},    // Arıza Türü 2 için süre 15 dakika
            {3, 20},    // ve böyle devam eder...
            {4, 12},
            {5, 14},
            {6, 30},
            {7, 26},
            {8, 24},
            {9, 22},
            {10, 12}
        };

        public class Is
        {
            public string PaketID;
            public string ArizaTurleri;
            public string TamirTurleri;
            public int TamirSuresi;
            public float GelisSaatiHour; // Saat bilgisini tutacak alan
            public float GelisSaatiMinute; // Dakika bilgisini tutacak alan
            public string CikisSaati;
            public Is? Next;
        }

        public class TamirBirimi
        {
            public string TamirBirimID;
            public int CalisanSayisi;
            public int Kapasite;
            public Is[] TamirdeBulunanİsler;// gelen kapasiteye göre

            public TamirBirimi(string tamirBirimID, int calisanSayisi, int kapasite)
            {
                TamirBirimID = tamirBirimID;
                CalisanSayisi = calisanSayisi;
                Kapasite = kapasite;
                TamirdeBulunanİsler = new Is[kapasite];
            }
        }

        
        public static int BekleyenİslerinSayisi(Is[] dagitimUnitesi)
        {
            int sayac = 0;
            foreach (var isNesnesi in dagitimUnitesi)
            {
                if (isNesnesi != null)
                {
                    sayac++;
                }
            }
            return sayac;
        }

        public static int BekleyenİslerinTamirSuresi(Is[] dagitimUnitesi)
        {
            int toplamsure = 0;
            foreach (var isNesnesi in dagitimUnitesi)
            {
                if (isNesnesi != null)
                {
                    toplamsure+=isNesnesi.TamirSuresi;
                }
            }
            return toplamsure;
        }

        public static bool DagitilmaDurumu()
        {
            int issayisi = BekleyenİslerinSayisi(DagitimUnitesi);
            if (issayisi >= 15)
                return true;
            else
            {
                if (BekleyenİslerinTamirSuresi(DagitimUnitesi) >= 300)
                    return true;
                else
                    return false;
            }
        }

        public static void IsPaketiOku()
        {
            const int MaxLineLength = 100;
            // Dosya adının yanında dosyanın tam yolunu belirttik
            string[] lines = File.ReadAllLines("IsPaketi.txt");
            Is isoku = Isler;

            //Console.WriteLine("PaketID\tArizaTurleri\tTamirTurleri\tTamirSuresi\tGelisSaati\tCikisSaati");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(',');
                isoku.PaketID = tokens[0];
                isoku.ArizaTurleri = tokens[1];
                isoku.TamirTurleri = tokens[2];
                //isoku.TamirSuresi = int.Parse(tokens[3]);
                string[] gelisSaatiParts = tokens[4].Split('.'); // Geliş saatiyi saat ve dakika olarak ayır
                isoku.GelisSaatiHour = float.Parse(gelisSaatiParts[0]); // Saat bilgisini float'a çevir
                isoku.GelisSaatiMinute = float.Parse(gelisSaatiParts[1]); // Dakika bilgisini float'a çevir
                isoku.CikisSaati = tokens[5];

                TamirTuruBelirle(ref isoku);
                isoku.TamirSuresi = TamirSuresiHesapla(isoku.ArizaTurleri);

                //Console.WriteLine($"{isoku.PaketID}\t{isoku.ArizaTurleri}\t\t{isoku.TamirTurleri}\t\t{isoku.TamirSuresi}\t\t{isoku.GelisSaatiHour}.{isoku.GelisSaatiMinute}\t\t{isoku.CikisSaati}");

                if (i < lines.Length - 1)
                {
                    isoku.Next = new Is();
                    isoku = isoku.Next;
                }
            }
            isoku.Next = null;
        }

        public static void TamirBirimiOku()
        {
            const int MaxLineLength = 100;
            // Dosya adının yanında dosyanın tam yolunu belirttik
            string[] lines = File.ReadAllLines("TamirBirimleri.txt");

            TamirBirimleri = new TamirBirimi[lines.Length];// Tamir Birimi saısı için

            //Console.WriteLine("TamirBirimID\tCalisanSayisi\tKapasite");

            int i = 0;
            foreach (string line in lines)
            {
                string[] tokens = line.Split(',');
                string tamirBirimID = tokens[0];
                int calisanSayisi = int.Parse(tokens[1]);
                int kapasite = int.Parse(tokens[2]);

                //Console.WriteLine($"{tamirBirimID}\t\t{calisanSayisi}\t\t{kapasite}");
                TamirBirimleri[i] = new TamirBirimi(tamirBirimID, calisanSayisi, kapasite);
                i++;
            }
        }

        public static void TamirTuruBelirle(ref Is isPaket)
        {
            string[] arizaTurleri = isPaket.ArizaTurleri.Split('*');
            StringBuilder tamirTuru = new StringBuilder();

            foreach (var ariza in arizaTurleri)
            {
                int arizaTuru = int.Parse(ariza);
                if (arizaTuru >= 1 && arizaTuru <= 3)
                {
                    tamirTuru.Append("1");
                }
                else if (arizaTuru >= 4 && arizaTuru <= 5)
                {
                    tamirTuru.Append("2");
                }
                else if (arizaTuru >= 6 && arizaTuru <= 8)
                {
                    tamirTuru.Append("3");
                }
                else if (arizaTuru >= 9 && arizaTuru <= 10)
                {
                    tamirTuru.Append("4");
                }
            }
            isPaket.TamirTurleri = tamirTuru.ToString();
        }

        public static int TamirSuresiHesapla(string ARİZATurleri)
        {
            int tamirSuresi = 0;

            foreach (char arızaTuru in ARİZATurleri)
            {
                if (arızaTuru != '*')
                {
                    int arizaTuru = int.Parse(arızaTuru.ToString());
                    if (arizaTurleriVeSureleri.ContainsKey(arizaTuru))
                    {
                        tamirSuresi += arizaTurleriVeSureleri[arizaTuru];
                    }
                    else
                    {
                        Console.WriteLine($"Hata: Tanımsız tamir türü {arızaTuru}");
                    }
                }
            }
            return tamirSuresi;
        }

        public static void IsPaketleriniSiralaVeIDVer()
        {
            Is current = Isler; // Başlangıç düğümü

            // İlk düğüm boş değilse geliş saatine göre sıralama yap
            if (current != null && current.Next != null)
            {
                // İş paketlerini geliş saatine göre sırala
                while (current != null)
                {
                    Is next = current.Next; // Bir sonraki düğümü al
                    while (next != null)
                    {
                        // Geliş saatine göre sıralama yap
                        if (next.GelisSaatiHour < current.GelisSaatiHour || (next.GelisSaatiHour == current.GelisSaatiHour && next.GelisSaatiMinute < current.GelisSaatiMinute))
                        {
                            // Düğümlerin yerlerini değiştir
                            SwapIsPaketleri(current, next);
                        }
                        next = next.Next;
                    }
                    current = current.Next;
                }
            }

            // Her iş paketine sırasıyla bir ID numarası ata
            int id = 1;
            current = Isler; // Başlangıç düğümüne geri dön
            while (current != null)
            {
                current.PaketID = id.ToString(); // ID numarasını string olarak ata
                id++;
                current = current.Next;
            }
        }

        // İki iş paketinin yerini değiştirir
        private static void SwapIsPaketleri(Is first, Is second)
        {
            // İş paketlerinin verilerini değiştir
            string tempPaketID = first.PaketID;
            string tempArizaTurleri = first.ArizaTurleri;
            string tempTamirTurleri = first.TamirTurleri;
            int tempTamirSuresi = first.TamirSuresi;
            float tempGelisSaatiHour = first.GelisSaatiHour;
            float tempGelisSaatiMinute = first.GelisSaatiMinute;
            string tempCikisSaati = first.CikisSaati;

            first.PaketID = second.PaketID;
            first.ArizaTurleri = second.ArizaTurleri;
            first.TamirTurleri = second.TamirTurleri;
            first.TamirSuresi = second.TamirSuresi;
            first.GelisSaatiHour = second.GelisSaatiHour;
            first.GelisSaatiMinute= second.GelisSaatiMinute;
            first.CikisSaati = second.CikisSaati;

            second.PaketID = tempPaketID;
            second.ArizaTurleri = tempArizaTurleri;
            second.TamirTurleri = tempTamirTurleri;
            second.TamirSuresi = tempTamirSuresi;
            second.GelisSaatiHour = tempGelisSaatiHour;
            second.GelisSaatiMinute= tempGelisSaatiMinute;
            second.CikisSaati = tempCikisSaati;
        }

        private static bool TamirBirimleriDoluMu()
        {
            foreach(TamirBirimi tamir in TamirBirimleri)
            {
                if (TamirBirimiDoluMu(tamir))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool DagitimUnitesiBosMu()
        {
            // Dağıtım ünitesinin herhangi bir iş içerip içermediğini kontrol et
            foreach (var isNesnesi in DagitimUnitesi)
            {
                if (isNesnesi != null)
                {
                    // Eğer en az bir iş varsa, dağıtım ünitesi boş değil demektir
                    return false;
                }
            }
            // Eğer hiçbir iş yoksa, dağıtım ünitesi boştur
            return true;
        }

        public static void IsleriBirimlereDagit()
        {
            while (!TamirBirimleriDoluMu() && !DagitimUnitesiBosMu())
            {
                Is ilkIs = DagitimUnitesi.FirstOrDefault(isNesnesi => isNesnesi != null);

                if (ilkIs != null)
                {
                    // İşin arıza türüne göre ilgili tamir birimini bul
                    string[] arizaTurleri = ilkIs.ArizaTurleri.Split('*');
                    foreach (var ariza in arizaTurleri)
                    {
                        int arizaTuru = int.Parse(ariza);
                        string tamirBirimID = ArizaTuruBazindaTamirBirimiBelirle(arizaTuru);

                        // İlgili tamir birimine işi gönder
                        if (tamirBirimID != null)
                        {
                            // İlgili tamir birimini bul
                            TamirBirimi ilgiliBirim = TamirBirimleri.FirstOrDefault(birim => birim.TamirBirimID == tamirBirimID);

                            // İlgili tamir biriminin dolu olup olmadığını kontrol et
                            if (ilgiliBirim != null && !TamirBirimiDoluMu(ilgiliBirim))
                            {
                                // İş paketini ilgili tamir birimine gönder
                                for (int i = 0; i < ilgiliBirim.Kapasite; i++)
                                {
                                    if (ilgiliBirim.TamirdeBulunanİsler[i] == null)
                                    {
                                        // İş paketini ilgili pozisyona yerleştir
                                        ilgiliBirim.TamirdeBulunanİsler[i] = ilkIs;

                                        // Dağıtım ünitesindeki işleri kaydır
                                        KaydirIsleri();

                                        // İşin dağıtıldığına dair mesaj yazdır
                                       // Console.WriteLine($"Paket ID: {ilkIs.PaketID} {ilkIs.ArizaTurleri} arızası için {tamirBirimID} tamir birimine gönderildi.");

                                        //return; // İşlem tamamlandı, fonksiyondan çık
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string ArizaTuruBazindaTamirBirimiBelirle(int arizaTuru)
        {
            // Arıza türüne göre ilgili tamir birimini belirle
            if (arizaTuru >= 1 && arizaTuru <= 3)
            {
                return "T01";
            }
            else if (arizaTuru >= 4 && arizaTuru <= 5)
            {
                return "T02";
            }
            else if(arizaTuru >= 6 && arizaTuru <= 8)
            {
                return "T03";
            }
            else
            {
                return "T04";
            }
        }

        // İlgili tamir biriminin dolu olup olmadığını kontrol et
        public static bool TamirBirimiDoluMu(TamirBirimi birim)
        {
            return birim.TamirdeBulunanİsler.All(isNesnesi => isNesnesi != null);
        }

        public static void KaydirIsleri()
        {
            for (int i = 0; i < DagitimUnitesi.Length - 1; i++)
            {
                DagitimUnitesi[i] = DagitimUnitesi[i + 1];
            }
            DagitimUnitesi[DagitimUnitesi.Length - 1] = null;
        }

        //public static void TamirBirimineIsGonder(Is isNesnesi)
        //{
        //    // İşin tamir süresini ve ilgili tamir biriminin çalışan sayısını dikkate alarak işin tamir edilme süresini hesapla
        //    int tamirEdilmeSuresi = isNesnesi.TamirSuresi / TamirBirimleri[0].CalisanSayisi;

        //    // İlgili tamir birimine işi gönder
        //    foreach (var birim in TamirBirimleri)
        //    {
        //        if (birim.TamirdeBulunanİsler.All(x => x != null))
        //        {
        //            // Tamir birimindeki bütün iş paketleri dolu ise bir sonraki birime geç
        //            continue;
        //        }

        //        // İş paketini ilk boş pozisyona yerleştir
        //        for (int i = 0; i < birim.Kapasite; i++)
        //        {
        //            if (birim.TamirdeBulunanİsler[i] == null)
        //            {
        //                // İş paketini ilgili pozisyona yerleştir
        //                birim.TamirdeBulunanİsler[i] = isNesnesi;

        //                // İşin tamir edilme süresini hesapla ve işin bittiği saati belirle
        //                DateTime tamirBaslangicZamani = (DateTime)ServisSaati;
        //                DateTime tamirBitisZamani = tamirBaslangicZamani.AddMinutes(tamirEdilmeSuresi);
        //                isNesnesi.CikisSaati = $"{tamirBitisZamani.Hour}:{tamirBitisZamani.Minute}";
        //                ServisSaati= tamirBitisZamani;

        //                // İşin tamir edildiğine dair mesaj yazdır
        //                Console.WriteLine($"Paket ID: {isNesnesi.PaketID}, Tamir Birimi: {birim.TamirBirimID}, Tamir Başlangıç Saati: {tamirBaslangicZamani.Hour}:{tamirBaslangicZamani.Minute}, Tamir Bitiş Saati: {tamirBitisZamani.Hour}:{tamirBitisZamani.Minute}");

        //                // İş tamir birimine gönderildiğinde işlemleri sonlandır
        //                return;
        //            }
        //        }
        //    }
        //}

        public static void DagitimUnitesineİsGonder()
        {
            if (Isler != null)
            {
                Is a = Isler;
                Isler = Isler.Next;
                for (int i = 0; i < DagitimUnitesi.Length ; i++)
                {
                    if (DagitimUnitesi[i] == null)
                    {
                        DagitimUnitesi[i] = a;

                        //Burada Servis saati dağıtım ünitesine gelen iş paketinin saati olacaktır.
                        ServisSaati = new DateTime(1, 1, 1, (int)a.GelisSaatiHour, (int)a.GelisSaatiMinute, 0);
                        break;
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Bütün işler tamir edildi");
                Environment.Exit(0);
            }
        }

        public static void DagitimUnitesineİsGonder(Is gelenis)
        {
            for (int i = 0; i < DagitimUnitesi.Length; i++)
            {
                if (DagitimUnitesi[i] == null)
                {
                    DagitimUnitesi[i] = gelenis;
                    break;
                }
            }
        }

        public static void TamireBasla()
        {
            // Servis saati 19:00'dan önceyse ve bütün tamir birimlerindeki işler tamamlanmadıysa devam et
            while (ServisSaati.Hour < 19 && !TumIslerTamirEdildi())
            {
                foreach (TamirBirimi tamirBirimi in TamirBirimleri)
                {
                    // Tamir birimindeki işleri tek tek kontrol et
                    for (int i = 0; i < tamirBirimi.TamirdeBulunanİsler.Length; i++)
                    {
                        if (tamirBirimi.TamirdeBulunanİsler[i] != null)
                        {
                            // İşin tamir süresini al
                            int tamirSuresi = tamirBirimi.TamirdeBulunanİsler[i].TamirSuresi/tamirBirimi.CalisanSayisi;

                            // İşin tamir edilme süresini hesapla ve servis saati üzerine ekle
                            ServisSaati = ServisSaati.AddMinutes(tamirSuresi);

                            // İşin tamir edildiğine dair mesaj yazdır
                           // Console.WriteLine($"Paket ID: {tamirBirimi.TamirdeBulunanİsler[i].PaketID} {tamirBirimi.TamirdeBulunanİsler[i].ArizaTurleri} arızası için {tamirBirimi.TamirBirimID} tamir biriminde tamir edildi.");
                            MessageBox.Show($"Paket ID: {tamirBirimi.TamirdeBulunanİsler[i].PaketID} {tamirBirimi.TamirdeBulunanİsler[i].ArizaTurleri} arızası için {tamirBirimi.TamirBirimID} tamir biriminde tamir edildi.");
                            // İşin arıza türünü ve tamir biriminden çıkar
                            string[] arizaTurleri = tamirBirimi.TamirdeBulunanİsler[i].ArizaTurleri.Split('*');
                            foreach (var ariza in arizaTurleri)
                            {
                                int arizaTuru = int.Parse(ariza);
                                tamirBirimi.TamirdeBulunanİsler[i].ArizaTurleri = tamirBirimi.TamirdeBulunanİsler[i].ArizaTurleri.Replace(ariza + "*", "");
                            }


                            // Biten iş paketlerini BitenİsPaketleri listesine ekle
                            BitenİsPaketleri.Add(tamirBirimi.TamirdeBulunanİsler[i]);

                            // İşin tamir edildiği pozisyonu temizle
                            tamirBirimi.TamirdeBulunanİsler[i] = null;



                            // İşin tamir edilip edilmediğini kontrol et
                            if (!TumIslerTamirEdildi())
                            {
                                // Servis saati kontrolü yap ve 19:00'dan sonraysa döngüden çık
                                if (ServisSaati.Hour >= 19)
                                {
                                    KalanİsleriBul();
                                    Console.WriteLine("Servis saati 19:00'ı geçti, işlemler sonlandırılıyor.");
                                    return;
                                }

                                // Dağıtım ünitesine iş gönderme işlemi
                                if (DagitimUnitesiBosMu())
                                {
                                    // Dağıtım ünitesi boşsa işi dağıtım ünitesine ekle
                                    DagitimUnitesineİsGonder(tamirBirimi.TamirdeBulunanİsler[i]);
                                }
                                else
                                {
                                    // Dağıtım ünitesi doluysa işlemleri sonlandır
                                    //Console.WriteLine("Dağıtım ünitesi dolu, işlemler sonlandırılıyor.");
                                    return;
                                }
                            }
                            else
                            {
                                // Tüm işler tamir edildiyse işlemleri sonlandır
                                //Console.WriteLine("Tüm işler tamir edildi, işlemler sonlandırılıyor.");
                                return;
                            }
                        }
                    }
                }
            }
        }
        
        // Tüm işlerin tamir edilip edilmediğini kontrol eder
        private static bool TumIslerTamirEdildi()
        {
            foreach (TamirBirimi birim in TamirBirimleri)
            {
                if (!TamirBirimiDoluMu(birim))
                {
                    // Eğer birimde hala iş varsa tamir edilmemiş işler var demektir
                    return false;
                }
            }
            // Tüm birimlerde iş yoksa tüm işler tamir edilmiş demektir
            return true;
        }

        public static void KalanİsleriBul()
        {
            //Tamir birimlerinde kalan iş paketlerini al
            foreach(TamirBirimi tamirBirimi in TamirBirimleri)
            {
                foreach(Is a in tamirBirimi.TamirdeBulunanİsler)
                {
                    if(a!= null)
                    {
                        TamirdeKalanİsPaketleri.Add(a);
                    }
                }
            }
            // dağıtım ünitesinde kalan iş paketlerini al
            for (int i = 0;i<DagitimUnitesi.Length;i++)
            {
                if (DagitimUnitesi[i] != null)
                {
                    DagitimUnitesindekiİsPaketleri.Add(DagitimUnitesi[i]);
                }
            }
        }

    }
}
