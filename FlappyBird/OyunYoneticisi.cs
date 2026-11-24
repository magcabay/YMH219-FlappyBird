namespace FlappyBird
{
    // Oyunun durumunu (skor, hız, kuş, engeller) tutan ve güncelleyen sınıf
    public class OyunYoneticisi
    {
        public Kus Kus { get; private set; }
        public List<Engel> Engeller { get; private set; } = new List<Engel>();

        public int Skor { get; private set; }
        public int EnYuksekSkor { get; private set; }
        public float Hiz { get; private set; }
        public int Seviye { get; private set; }
        public bool OyunBitti { get; private set; }
        public bool Basladi { get; private set; }

        public int Genislik { get; }
        public int Yukseklik { get; }   // zemin hariç oyun alanı
        public int ZeminYuksekligi { get; } = 60;

        private const float BaslangicHizi = 4f;
        private const int EngelSayisi = 3;
        private const int EngelAraligi = 230;
        private const int BoslukBoyu = 150;

        private readonly Random rnd = new Random();
        private readonly string skorDosyasi = "enyuksekskor.txt";

        public OyunYoneticisi(int genislik, int toplamYukseklik)
        {
            Genislik = genislik;
            Yukseklik = toplamYukseklik - ZeminYuksekligi;
            EnYuksekSkor = EnYuksekSkoruOku();
            YeniOyun();
        }

        public void YeniOyun()
        {
            Kus = new Kus(90, Yukseklik / 2);
            Skor = 0;
            Seviye = 1;
            Hiz = BaslangicHizi;
            OyunBitti = false;
            Basladi = false;

            Engeller.Clear();
            for (int i = 0; i < EngelSayisi; i++)
            {
                float x = Genislik + 100 + i * EngelAraligi;
                Engeller.Add(new Engel(x, RastgeleBoslukY(), BoslukBoyu, Yukseklik));
            }
        }

        public void Zipla()
        {
            if (OyunBitti) return;
            Basladi = true;
            Kus.Zipla();
        }

        // Timer her çalıştığında formdan çağrılıyor
        public void Guncelle()
        {
            if (OyunBitti || !Basladi) return;

            Kus.Guncelle();

            foreach (Engel engel in Engeller)
            {
                engel.Hareket(Hiz);

                // engel kuşun arkasında kaldıysa skor
                if (!engel.Gecildi && engel.X + engel.Genislik < Kus.X)
                {
                    engel.Gecildi = true;
                    SkorArtir();
                }

                if (engel.CarpistiMi(Kus.Sinirlar))
                {
                    OyunuBitir();
                    return;
                }
            }

            // ekrandan çıkan engeli en sağdakinin arkasına taşı
            foreach (Engel engel in Engeller)
            {
                if (engel.EkrandanCiktiMi())
                {
                    float enSag = Engeller.Max(e => e.X);
                    engel.YenidenKonumlandir(enSag + EngelAraligi, RastgeleBoslukY());
                }
            }

            // zemine çarpma
            if (Kus.Y + Kus.Yukseklik >= Yukseklik)
            {
                OyunuBitir();
            }
        }

        private void SkorArtir()
        {
            Skor++;
            // her 5 puanda bir seviye atla, hız artsın
            if (Skor % 5 == 0)
            {
                Seviye++;
                Hiz += 0.7f;
            }
        }

        private void OyunuBitir()
        {
            OyunBitti = true;
            if (Skor > EnYuksekSkor)
            {
                EnYuksekSkor = Skor;
                EnYuksekSkoruKaydet();
            }
        }

        private int RastgeleBoslukY()
        {
            return rnd.Next(60, Yukseklik - BoslukBoyu - 60);
        }

        private int EnYuksekSkoruOku()
        {
            try
            {
                if (File.Exists(skorDosyasi))
                {
                    string yazi = File.ReadAllText(skorDosyasi);
                    if (int.TryParse(yazi.Trim(), out int skor))
                        return skor;
                }
            }
            catch (IOException)
            {
                // dosya okunamazsa 0'dan başla
            }
            return 0;
        }

        private void EnYuksekSkoruKaydet()
        {
            try
            {
                File.WriteAllText(skorDosyasi, EnYuksekSkor.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("En yüksek skor kaydedilemedi: " + ex.Message, "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
