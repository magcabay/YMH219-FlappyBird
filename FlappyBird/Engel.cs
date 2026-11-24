namespace FlappyBird
{
    // Üst ve alt borudan oluşan bir engel çifti
    public class Engel
    {
        public float X { get; private set; }
        public int Genislik { get; } = 60;

        // kuşun geçeceği boşluğun üst noktası ve boyu
        public int BoslukY { get; private set; }
        public int BoslukBoyu { get; private set; }

        // skor bir kere artsın diye
        public bool Gecildi { get; set; }

        private readonly int oyunYuksekligi;

        public Engel(float x, int boslukY, int boslukBoyu, int oyunYuksekligi)
        {
            X = x;
            BoslukY = boslukY;
            BoslukBoyu = boslukBoyu;
            this.oyunYuksekligi = oyunYuksekligi;
        }

        public void Hareket(float hiz)
        {
            X -= hiz;
        }

        public bool EkrandanCiktiMi()
        {
            return X + Genislik < 0;
        }

        // ekrandan çıkan engeli sağa geri al, yeni boşluk ver
        public void YenidenKonumlandir(float yeniX, int yeniBoslukY)
        {
            X = yeniX;
            BoslukY = yeniBoslukY;
            Gecildi = false;
        }

        public RectangleF UstBoru
        {
            get { return new RectangleF(X, 0, Genislik, BoslukY); }
        }

        public RectangleF AltBoru
        {
            get
            {
                float altY = BoslukY + BoslukBoyu;
                return new RectangleF(X, altY, Genislik, oyunYuksekligi - altY);
            }
        }

        public bool CarpistiMi(RectangleF kus)
        {
            return kus.IntersectsWith(UstBoru) || kus.IntersectsWith(AltBoru);
        }

        public void Ciz(Graphics g)
        {
            BoruCiz(g, UstBoru);
            BoruCiz(g, AltBoru);
        }

        private void BoruCiz(Graphics g, RectangleF r)
        {
            g.FillRectangle(Brushes.ForestGreen, r);
            g.DrawRectangle(Pens.DarkGreen, r.X, r.Y, r.Width, r.Height);
        }
    }
}
