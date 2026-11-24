namespace FlappyBird
{
    public partial class Form1 : Form
    {
        private OyunYoneticisi oyun;

        private readonly Font skorFont = new Font("Segoe UI", 28, FontStyle.Bold);
        private readonly Font bilgiFont = new Font("Segoe UI", 11, FontStyle.Regular);
        private readonly Font baslikFont = new Font("Segoe UI", 30, FontStyle.Bold);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            oyun = new OyunYoneticisi(ClientSize.Width, ClientSize.Height);
            oyunTimer.Start();
        }

        private void oyunTimer_Tick(object sender, EventArgs e)
        {
            oyun.Guncelle();
            Invalidate(); // ekranı yeniden çiz
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                oyun.Zipla();
                // butona space gitmesin
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.R || e.KeyCode == Keys.Enter)
            {
                if (oyun.OyunBitti)
                    YenidenBaslat();
                e.SuppressKeyPress = true;
            }
        }

        private void btnYeniOyun_Click(object sender, EventArgs e)
        {
            YenidenBaslat();
        }

        private void YenidenBaslat()
        {
            oyun.YeniOyun();
            // odak butonda kalmasın, klavye forma gelsin
            ActiveControl = null;
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (oyun == null) return;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Engel engel in oyun.Engeller)
                engel.Ciz(g);

            // zemin
            g.FillRectangle(Brushes.SandyBrown, 0, oyun.Yukseklik, ClientSize.Width, oyun.ZeminYuksekligi);
            g.FillRectangle(Brushes.OliveDrab, 0, oyun.Yukseklik, ClientSize.Width, 10);

            oyun.Kus.Ciz(g);

            // skor ve bilgiler
            g.DrawString(oyun.Skor.ToString(), skorFont, Brushes.White, 20, 10);
            string bilgi = $"Seviye: {oyun.Seviye}   Hız: {oyun.Hiz:0.0}   En Yüksek: {oyun.EnYuksekSkor}";
            g.DrawString(bilgi, bilgiFont, Brushes.Black, 10, oyun.Yukseklik + 25);

            if (!oyun.Basladi && !oyun.OyunBitti)
            {
                OrtayaYaz(g, "Başlamak için SPACE", 260);
            }

            if (oyun.OyunBitti)
            {
                using (var yariSeffaf = new SolidBrush(Color.FromArgb(140, Color.Black)))
                    g.FillRectangle(yariSeffaf, 0, 0, ClientSize.Width, ClientSize.Height);

                SizeF boyut = g.MeasureString("GAME OVER", baslikFont);
                g.DrawString("GAME OVER", baslikFont, Brushes.OrangeRed, (ClientSize.Width - boyut.Width) / 2, 220);
                OrtayaYaz(g, $"Skor: {oyun.Skor}    En Yüksek: {oyun.EnYuksekSkor}", 290);
                OrtayaYaz(g, "Yeniden başlamak için R / Enter veya 'Yeni Oyun'", 320);
            }
        }

        private void OrtayaYaz(Graphics g, string yazi, float y)
        {
            SizeF boyut = g.MeasureString(yazi, bilgiFont);
            g.DrawString(yazi, bilgiFont, Brushes.White, (ClientSize.Width - boyut.Width) / 2, y);
        }
    }
}
