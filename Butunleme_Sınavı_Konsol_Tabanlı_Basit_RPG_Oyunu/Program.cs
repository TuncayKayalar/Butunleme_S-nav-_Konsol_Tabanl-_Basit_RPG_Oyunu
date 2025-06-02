using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Butunleme_Sınavı_Konsol_Tabanlı_Basit_RPG_Oyunu
{


    public class Karakter
    {
        public string Isim;
        public int Can;
        public int MaxCan;
        public int Guc;
        public int Mana;
        public int MaxMana;

        public static int ToplamVurus = 0;

        public Karakter(string isim, int can, int guc, int mana)
        {
            Isim = isim;
            Can = can;
            MaxCan = can;
            Guc = guc;
            Mana = mana;
            MaxMana = mana;
        }

        public virtual int Vur()
        {
            ToplamVurus++;
            var r = new Random();
            return r.Next(5, Guc + 1);
        }

        public void HasarVer(int miktar)
        {
            Can -= miktar;
            if (Can < 0)
                Can = 0;

            Console.WriteLine($"{Isim} {miktar} hasar aldı! Kalan can: {Can}");
        }

        public void ManaDoldur()
        {
            int eklenecek = MaxMana / 3;
            Mana += eklenecek;
            if (Mana > MaxMana) Mana = MaxMana;

            Console.WriteLine($"{Isim} mana yeniledi. Yeni mana: {Mana}");
        }

        public bool YasiyorMu()
        {
            return Can > 0;
        }

        public void DurumuYazdir()
        {
            Console.WriteLine($"\n>>> {Isim} <<<");
            Console.WriteLine($"Can: {Can}/{MaxCan}");
            Console.WriteLine($"Mana: {Mana}/{MaxMana}");
            Console.WriteLine($"Güç: {Guc}\n");
        }
    }

    public interface IOzelHareket
    {
        bool BüyüSaldirisi(Karakter hedef);
    }

    public class Oyuncu : Karakter, IOzelHareket
    {
        public int XP;
        public int Seviye;
        public List<string> Log;

        public Oyuncu(string isim) : base(isim, 100, 15, 30)
        {
            XP = 0;
            Seviye = 1;
            Log = new List<string>();
        }

        public override int Vur()
        {
            int hasar = base.Vur();
            string mesaj = $"{Isim} normal saldırı yaptı ({hasar} hasar)";
            Console.WriteLine(mesaj);
            Log.Add(mesaj);
            return hasar;
        }

        public bool BüyüSaldirisi(Karakter hedef)
        {
            if (Mana < 20)
            {
                Console.WriteLine("Yetersiz mana! (Gerekli: 20)");
                return false;
            }

            Mana -= 20;
            var r = new Random();
            int hasar = r.Next(Guc * 2, Guc * 3 + 1);
            Console.WriteLine($"{Isim} özel saldırı yaptı! ({hasar} hasar)");
            hedef.HasarVer(hasar);
            Log.Add($"{Isim} büyü ile {hasar} hasar verdi.");
            ToplamVurus++;
            return true;
        }

        public void TecrubeKazan(int miktar)
        {
            XP += miktar;
            Console.WriteLine($"{miktar} XP kazandınız. Toplam: {XP}");

            if (XP >= Seviye * 50)
                SeviyeYukselt();
        }

        private void SeviyeYukselt()
        {
            Seviye++;
            XP = 0;
            MaxCan += 15;
            Can = MaxCan;
            Guc += 3;
            MaxMana += 5;
            Mana = MaxMana;

            Console.WriteLine($"Yeni seviye: {Seviye}! Karakteriniz güçlendi.");
        }

        public void LogYazdir()
        {
            Console.WriteLine(">>> Son 3 saldırınız:");
            int basla = Math.Max(0, Log.Count - 3);
            for (int i = basla; i < Log.Count; i++)
                Console.WriteLine("- " + Log[i]);
        }
    }

    public class Dusman : Karakter
    {
        public int OdulXP;

        public Dusman(string isim, int can, int guc, int mana, int odulXP)
            : base(isim, can, guc, mana)
        {
            OdulXP = odulXP;
        }

        public override int Vur()
        {
            int hasar = base.Vur();
            Console.WriteLine($"{Isim} size vurdu! ({hasar} hasar)");
            return hasar;
        }

        public void HamleYap(Karakter hedef)
        {
            Random r = new Random();
            int sec = r.Next(1, 4);

            if (sec < 3)
            {
                int hasar = Vur();
                hedef.HasarVer(hasar);
            }
            else
            {
                ManaDoldur();
            }
        }
    }

    public class Zombi : Dusman
    {
        public Zombi() : base("Zombi", 50, 10, 10, 15) { }
    }

    public class Goblin : Dusman
    {
        public Goblin() : base("Goblin", 40, 12, 15, 20) { }
    }

    public class Ejderha : Dusman
    {
        public Ejderha() : base("Ejderha", 80, 20, 35, 50) { }
    }

    public class Oyun
    {
        Oyuncu Oyuncu;
        Dusman AktifDusman;
        int DusmanSayisi = 0;

        public void Baslat()
        {
            Console.WriteLine("*** RPG ARENASI ***");
            Console.Write("İsminizi girin: ");
            string isim = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isim)) isim = "Savaşçı";

            Oyuncu = new Oyuncu(isim);
            Console.WriteLine($"Hoş geldin {isim}!");

            YeniDusman();
            while (Oyuncu.YasiyorMu())
                Tur();

            OyunSonu();
        }

       public void YeniDusman()
        {
            Random r = new Random();
            if (DusmanSayisi != 0 && DusmanSayisi % 3 == 0)
            {
                AktifDusman = new Ejderha();
                Console.WriteLine("\n!!! BOSS GELİYOR: Ejderha !!!");
            }
            else
            {
                if (r.Next(2) == 0)
                {
                    AktifDusman = new Zombi();
                }
                else
                {
                    AktifDusman = new Goblin();
                }
            }

            Console.WriteLine($"Yeni düşman: {AktifDusman.Isim} (XP: {AktifDusman.OdulXP})\n");
        }

       public void Tur()
        {
            Oyuncu.DurumuYazdir();
            AktifDusman.DurumuYazdir();

            Console.WriteLine("1) Saldır");
            Console.WriteLine("2) Büyü Saldırısı");
            Console.WriteLine("3) Mana Yenile");
            Console.WriteLine("4) Saldırı Geçmişi");
            Console.WriteLine("5) Durum");
            Console.Write("Seçim: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    int hasar = Oyuncu.Vur();
                    AktifDusman.HasarVer(hasar);
                    break;
                case "2":
                    Oyuncu.BüyüSaldirisi(AktifDusman);
                    break;
                case "3":
                    Oyuncu.ManaDoldur();
                    break;
                case "4":
                    Oyuncu.LogYazdir();
                    break;
                case "5":
                    BilgiGoster();
                    break;
                default:
                    Console.WriteLine("Geçersiz giriş!");
                    return;
            }

            if (!AktifDusman.YasiyorMu())
            {
                Console.WriteLine($"{AktifDusman.Isim} yenildi!");
                Oyuncu.TecrubeKazan(AktifDusman.OdulXP);
                DusmanSayisi++;
                Console.WriteLine("Devam için Enter...");
                Console.ReadLine();
                YeniDusman();
                return;
            }

            Console.WriteLine("--- Düşman hamlesi ---");
            AktifDusman.HamleYap(Oyuncu);
            Console.WriteLine("Devam için Enter...");
            Console.ReadLine();
        }

        public void BilgiGoster()
        {
            Console.WriteLine("\n--- OYUNCU BİLGİLERİ ---");
            Console.WriteLine($"Seviye: {Oyuncu.Seviye}");
            Console.WriteLine($"XP: {Oyuncu.XP}");
            Console.WriteLine($"Can: {Oyuncu.Can}/{Oyuncu.MaxCan}");
            Console.WriteLine($"Mana: {Oyuncu.Mana}/{Oyuncu.MaxMana}");
            Console.WriteLine($"Yenilen düşman: {DusmanSayisi}");
            Console.WriteLine($"Toplam saldırı: {Karakter.ToplamVurus}\n");
        }

        public void OyunSonu()
        {
            Console.Clear();
            Console.WriteLine(">>> OYUN SONA ERDİ <<<");
            Console.WriteLine("Öldünüz!");

            Console.WriteLine($"\nToplam Skor: {(DusmanSayisi * 10) + (Oyuncu.Seviye * 5)}");
            Console.Write("Yeniden oynamak ister misiniz? (E/H): ");
            string cevap = Console.ReadLine();
            if (cevap?.ToUpper() == "E")
            {
                new Oyun().Baslat();
            }
            else
            {
                Console.WriteLine("Teşekkürler!");
            }
        }

        public static void Main(string[] args)
        {
            new Oyun().Baslat();
            Console.WriteLine("Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
