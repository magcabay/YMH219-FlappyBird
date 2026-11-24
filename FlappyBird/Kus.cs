namespace FlappyBird
{
    // Oyuncunun kontrol ettiği kuş sınıfı
    public class Kus
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Genislik { get; } = 34;
        public int Yukseklik { get; } = 26;

        // dikey hız (+ aşağı, - yukarı)
        public float DikeyHiz { get; private set; }

        private const float YerCekimi = 0.55f;
        private const float ZiplamaGucu = -8.5f;
        private const float MaksDusmeHizi = 11f;

        public Kus(float x, float y)
        {
            X = x;
            Y = y;
            DikeyHiz = 0;
        }

        public void Zipla()
        {
            DikeyHiz = ZiplamaGucu;
        }

        // her timer tick'inde çağrılır
        public void Guncelle()
        {
            DikeyHiz += YerCekimi;
            if (DikeyHiz > MaksDusmeHizi)
                DikeyHiz = MaksDusmeHizi;

            Y += DikeyHiz;

            // ekranın üstünden çıkmasın
            if (Y < 0)
            {
                Y = 0;
                DikeyHiz = 0;
            }
        }

        public RectangleF Sinirlar
        {
            get { return new RectangleF(X, Y, Genislik, Yukseklik); }
        }

        public void Ciz(Graphics g)
        {
            // gövde
            g.FillEllipse(Brushes.Gold, X, Y, Genislik, Yukseklik);
            g.DrawEllipse(Pens.DarkOrange, X, Y, Genislik, Yukseklik);
            // göz
            g.FillEllipse(Brushes.White, X + 20, Y + 4, 10, 10);
            g.FillEllipse(Brushes.Black, X + 25, Y + 7, 4, 4);
            // gaga
            PointF[] gaga =
            {
                new PointF(X + Genislik - 2, Y + 12),
                new PointF(X + Genislik + 8, Y + 15),
                new PointF(X + Genislik - 2, Y + 19)
            };
            g.FillPolygon(Brushes.OrangeRed, gaga);
        }
    }
}
