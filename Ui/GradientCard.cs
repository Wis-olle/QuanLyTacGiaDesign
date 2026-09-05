using System.Drawing.Drawing2D;
using AntdUI;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Nền vẽ GDI+ cho thẻ:
///  • <see cref="MakeStat"/>: 4 thẻ số liệu làm theo đúng 4 thẻ của mẫu Dribbble 27274985 (đo trên ảnh gốc 5584×4620):
///    nền màu đặc (không gradient), khối sáng / khối tối CÙNG TÔNG, nét 3 px cùng tông, ô icon pastel trên tấm lót trắng xếp chồng lệch góc.
///  • <see cref="Make"/>: banner gradient + 2 vòng tròn sáng (drawer).
/// Toạ độ hoạ tiết tính theo tỉ lệ w/h nên thẻ co giãn vẫn giữ bố cục. Hover sáng dần 8 %.
/// </summary>
public static class GradientCard
{
    public enum Deco { Circles, Rings, Contours, Waves, Blob }

    /// <summary>Bộ màu một thẻ: nền đặc, tông sáng (đĩa / nét), tông tối (khối), màu pastel của ô icon phía trước.</summary>
    public readonly record struct Tone(Color Base, Color Light, Color Dark, Color Tile);

    /// <summary>Lề trái chữ trên thẻ (20 px × K).</summary>
    public static int Pad => AppTheme.S(20);
    /// <summary>Thẻ hẹp (&lt; 230): ô icon 26 px dồn vào góc để tiêu đề không bị cắt; thẻ &lt; 165 thì ẩn hẳn. (px × K)</summary>
    public static bool Narrow(int w) => w < AppTheme.S(230);
    public static bool ShowIcon(int w) => w >= AppTheme.S(165);
    /// <summary>Ô icon = 0,19 × cao, đỉnh 0,18 h (mẫu), tối thiểu 30; thẻ hẹp: 26 px cách mép 12.</summary>
    public static Rectangle IconRect(int w, int h)
    {
        if (Narrow(w)) { int n = AppTheme.S(26), m = AppTheme.S(12); return new Rectangle(w - m - n, m, n, n); }
        int s = Math.Max(AppTheme.S(30), (int)(h * 0.19f));
        return new Rectangle(w - Pad - s, (int)(h * 0.18f), s, s);
    }

    /// <param name="hoverT">0 → 1 khi rê chuột (một nhịp 240 ms): vòng nở, khối tối trôi, đồi nhô, khối cong lấn, tấm lót icon xoay thêm. Không lặp.</param>
    public static Bitmap MakeStat(int w, int h, Tone t, Deco deco, float wash = 0f, float hoverT = 0f)
    {
        w = Math.Max(1, w); h = Math.Max(1, h);
        float hv = hoverT;
        var bmp = new Bitmap(w, h);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(t.Base);
        using var light = new SolidBrush(t.Light);
        using var dark = new SolidBrush(t.Dark);
        // nét 3 px: tím / lá / dương ~80 % tông sáng (đỉnh nét mẫu #7F66C9 / #8EC96C / #77A6DF); cam nhạt hơn (#F68A5B)
        using var pen = new Pen(Color.FromArgb(deco == Deco.Rings ? 140 : 200, t.Light), AppTheme.SF(3f));
        switch (deco)
        {
            case Deco.Rings:
                // thẻ cam: đĩa sáng lớn sau tiêu đề + số (tâm 0,245h; 0,27h · r 0,52h) và 3 vòng mảnh tâm sát mép phải (r 0,37 / 0,55 / 0,71 h)
                Disc(g, light, h * (0.245f + 0.03f * hv), h * (0.27f - 0.03f * hv), h * 0.52f * (1f + 0.06f * hv));
                for (int i = 0; i < 3; i++)
                {
                    float r = h * new[] { 0.37f, 0.55f, 0.71f }[i] * (1f + hv * (0.06f + 0.02f * i));   // vòng ngoài nở nhiều hơn
                    Ring(g, pen, w - h * 0.04f, h * 0.42f, r);
                }
                break;
            case Deco.Contours:
                // thẻ tím: khối tròn tối góc dưới phải (tâm 0,87w; 0,82h · r 0,50h → đỉnh 0,32h vừa lộ dưới ô icon, thoát đáy 0,66w) + 3 cung mảnh tâm ngoài góc trên trái
                Disc(g, dark, w * 0.87f - w * 0.02f * hv, h * (0.82f - 0.05f * hv), h * 0.50f * (1f + 0.05f * hv));   // khối tối trôi lên trái
                for (int i = 0; i < 3; i++)
                {
                    float r = h * new[] { 0.34f, 0.48f, 0.60f }[i] * (1f + hv * (0.06f + 0.02f * i));
                    Ring(g, pen, -h * 0.05f, h * 0.2f, r);
                }
                break;
            case Deco.Waves:
                // thẻ xanh lá: "đồi" tối phủ góc dưới phải (từ 0,46w đáy lên 0,36h mép phải) + 3 đường đồng mức lượn ở góc trên trái
                using (var p = new GraphicsPath())
                {
                    float rise = h * 0.06f * hv;   // đồi nhô lên
                    p.AddBezier(w * 0.46f, h + 2, w * 0.75f, h * 0.98f, w * 0.76f, h * 0.36f - rise, w + 2, h * 0.36f - rise);
                    p.AddLine(w + 2, h * 0.36f - rise, w + 2, h + 2);
                    p.CloseFigure();
                    g.FillPath(dark, p);
                }
                for (int i = 0; i < 3; i++) Contour(g, pen, -h * 0.02f, -h * 0.15f, h * new[] { 0.35f, 0.50f, 0.65f }[i] * (1f + 0.07f * hv), h * 0.02f * (1f + 2f * hv), hv * 0.6f);   // đường viền dịch pha nhẹ
                break;
            case Deco.Blob:
                // thẻ xanh dương: khối tối phủ phần phải, ranh giới cong chữ S (0,47w trên → 0,70w dưới) + 3 sóng dọc bên trái
                using (var p = new GraphicsPath())
                {
                    float sway = -w * 0.025f * hv;   // khối cong lấn sang trái
                    p.AddBezier(w * 0.47f + sway, -2, w * 0.56f + sway, h * 0.25f, w * 0.86f + sway, h * 0.60f, w * 0.70f + sway, h + 2);
                    p.AddLine(w * 0.70f + sway, h + 2, w + 2, h + 2);
                    p.AddLine(w + 2, h + 2, w + 2, -2);
                    p.CloseFigure();
                    g.FillPath(dark, p);
                }
                Waves(g, pen, h, h * 0.08f, 3, h * 0.10f, h * 0.045f * (1f + 0.4f * hv), hv * 0.8f);   // sóng dịch pha nhẹ
                break;
        }

        // tấm lót TRẮNG ĐẶC xoay -10°, lệch lên trái 17 % / 15 % (mẫu: 2 tấm thẻ xếp chồng; ô trước là Avatar màu pastel t.Tile đặt đúng IconRect)
        if (ShowIcon(w))
        {
            var r = IconRect(w, h);
            var st = g.Save();
            g.TranslateTransform(r.X + r.Width / 2f - r.Width * (0.17f + 0.08f * hv), r.Y + r.Height / 2f - r.Height * (0.15f + 0.10f * hv));
            g.RotateTransform(-10f - 8f * hv);   // tấm lót xoè ra thêm khi hover
            using var back = new SolidBrush(Color.White);
            using var rr = RoundRect(new RectangleF(-r.Width / 2f, -r.Height / 2f, r.Width, r.Height), Math.Max(AppTheme.S(6), r.Width / 5));
            g.FillPath(back, rr);
            g.Restore(st);
        }

        Wash(g, w, h, wash);
        return bmp;
    }

    /// <summary>Banner gradient (drawer): 2 vòng tròn sáng.</summary>
    public static Bitmap Make(int w, int h, Color a, Color b, float wash = 0f)
    {
        w = Math.Max(1, w); h = Math.Max(1, h);
        var bmp = new Bitmap(w, h);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var br = new LinearGradientBrush(new Rectangle(0, 0, w, h), a, b, 20f))
            g.FillRectangle(br, 0, 0, w, h);
        using var light = new SolidBrush(Color.FromArgb(26, 255, 255, 255));
        float d1 = h * 1.5f, d2 = h * 1.05f;
        g.FillEllipse(light, w - d1 * 0.55f, -d1 * 0.45f, d1, d1);
        g.FillEllipse(light, w - d1 * 0.55f - d2 * 0.9f, h - d2 * 0.4f, d2, d2);
        Wash(g, w, h, wash);
        return bmp;
    }

    static void Disc(Graphics g, Brush b, float cx, float cy, float r) => g.FillEllipse(b, cx - r, cy - r, r * 2, r * 2);
    static void Ring(Graphics g, Pen p, float cx, float cy, float r) => g.DrawEllipse(p, cx - r, cy - r, r * 2, r * 2);

    /// <summary>Cung tròn lượn nhẹ (đường đồng mức) quanh tâm, chỉ vẽ phần từ -10° đến 100° (góc trên trái thẻ).</summary>
    static void Contour(Graphics g, Pen p, float cx, float cy, float r, float amp, float phase = 0f)
    {
        var pts = new List<PointF>();
        for (int a = -10; a <= 100; a += 4)
        {
            float th = a * MathF.PI / 180f, rr = r + amp * MathF.Sin(a * 0.11f + phase);
            pts.Add(new PointF(cx + rr * MathF.Cos(th), cy + rr * MathF.Sin(th)));
        }
        g.DrawCurve(p, pts.ToArray(), 0.5f);
    }

    static void Waves(Graphics g, Pen pen, int h, float x0, int count, float gap, float amp, float phase = 0f)
    {
        for (int k = 0; k < count; k++)
        {
            var pts = new List<PointF>();
            float x = x0 + k * gap;
            for (int y = -4; y <= h + 4; y += 4)
                pts.Add(new PointF(x + amp * MathF.Sin((y + k * 18) / (float)h * MathF.PI * 2.2f + phase), y));
            g.DrawCurve(pen, pts.ToArray(), 0.5f);
        }
    }

    static void Wash(Graphics g, int w, int h, float wash)
    {
        if (wash <= 0f) return;
        using var white = new SolidBrush(Color.FromArgb((int)(255 * wash), 255, 255, 255));
        g.FillRectangle(white, 0, 0, w, h);
    }

    static GraphicsPath RoundRect(RectangleF r, float d)
    {
        var p = new GraphicsPath();
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }

    /// <summary>Thẻ số liệu phẳng theo mẫu. Gọi SAU khi đã thêm control con để hover bắt được trên cả thẻ.
    /// Trả về hàm Reveal(delayMs): thẻ phủ trắng rồi hiện dần trong 480 ms sau delay (xuất hiện lệch nhịp).</summary>
    public static Action<int> ApplyStat(AntdUI.Panel card, Tone tone, Deco deco, Action<float>? onHover = null) =>
        Bind(card, tone.Base, (wash, hv) => MakeStat(card.Width, card.Height, tone, deco, wash, hv), hover: true, onHover);

    /// <summary>Banner gradient (drawer).</summary>
    public static void Apply(AntdUI.Panel card, (Color A, Color B) grad, bool hover = false) =>
        Bind(card, grad.A, (wash, _) => Make(card.Width, card.Height, grad.A, grad.B, wash), hover, null);

    /// <summary>Hover: hoverT 0 → 1 trong 240 ms ease-out (hoạ tiết nở / trôi, nền sáng 8 %), rời chuột trôi về. Một nhịp, không lặp.</summary>
    static Action<int> Bind(AntdUI.Panel card, Color back, Func<float, float, Bitmap> draw, bool hover, Action<float>? onHover)
    {
        card.Back = back;
        card.BackgroundImageLayout = TFit.Fill;
        float wash = 0f, hoverT = 0f;
        Action? anim = null, hoverAnim = null;
        void Redraw()
        {
            if (card.Width <= 0 || card.Height <= 0 || card.IsDisposed) return;
            var old = card.BackgroundImage;
            card.BackgroundImage = draw(wash, hoverT);
            old?.Dispose();
        }
        void HoverTo(float target)
        {
            hoverAnim?.Invoke();
            float from = hoverT;
            hoverAnim = Motion.Run(Motion.Base, p =>
            {
                hoverT = from + (target - from) * p;
                wash = 0.08f * hoverT;
                onHover?.Invoke(hoverT);
                Redraw();
            });
        }
        void To(float target, int ms)
        {
            anim?.Invoke();
            float from = wash;
            anim = Motion.Run(ms, p => { wash = from + (target - from) * p; Redraw(); });
        }
        card.Resize += (_, _) => Redraw();
        Redraw();

        if (hover)
        {
            bool inside = false;
            foreach (Control c in Enumerable.Repeat<Control>(card, 1).Concat(card.Controls.Cast<Control>()))
            {
                c.MouseEnter += (_, _) =>
                {
                    if (inside) return;
                    inside = true;
                    HoverTo(1f);
                };
                c.MouseLeave += (_, _) =>
                {
                    if (card.ClientRectangle.Contains(card.PointToClient(Cursor.Position))) return;
                    inside = false; HoverTo(0f);
                };
            }
            card.Disposed += (_, _) => { hoverAnim?.Invoke(); anim?.Invoke(); };
        }
        return delay => { anim?.Invoke(); wash = 1f; Redraw(); anim = Motion.Delay(delay, () => To(0f, 480)); };
    }
}
