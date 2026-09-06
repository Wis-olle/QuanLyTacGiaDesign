using System.Drawing.Drawing2D;
using System.Drawing.Text;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Biểu đồ tự vẽ GDI+ cho P1 (thay AntdUI.Chart vì không chỉnh được kiểu). Phong cách lấy từ Dribbble:
///  • Radiant Studio "KPI Analytics Widget – Dashboard UI Charts" (26726793): area chart spline, tô gradient tím mờ dần, chấm dữ liệu, tooltip tối.
///  • Farhad Ahmed "Analytics Dashboard Charts UI" (27661843): donut dày có số tổng ở tâm + chú giải bên phải; thanh ngang bo tròn, lưới dọc gạch đứt.
///  • Mẫu 27274985: lưới gạch đứt mảnh, chấm tròn trên đường, chú giải chấm màu.
/// Chung: lưới `Border` gạch đứt 4/4, chữ trục `Small` màu `TextMuted`, animation 700 ms (Motion.Slow).
/// Hover (theo Seative "Employee Growth Chart Hover" 27670663, Keitoto "Crypto Spending Dashboard Live" 27362053 / "Transactions Page Live" 27365702):
/// phần tử được trỏ NỔI (hoverT), phần còn lại MỜ (dimT), tooltip nền TextPrimary trượt theo điểm; rời chuột thì mờ dần rồi mới bỏ.
/// Mọi kích thước px nhân K (DPI).
/// </summary>
public abstract class ChartBase : Control
{
    protected float progress = 1f;   // 0 → 1 khi animation
    protected int hover = -1;
    protected float hoverT;           // 0 → 1 khi phần tử vừa được trỏ nổi dần lên (đặt lại mỗi lần chuyển phần tử)
    protected float dimT;             // 0 → 1 khi có phần tử được trỏ, giữ 1 khi chuyển giữa các phần tử, → 0 khi rời: mờ phần còn lại + alpha tooltip
    bool leaving;
    Action? cancel, hoverAnim, dimAnim;

    protected ChartBase()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
        Font = AppTheme.Small;
    }

    /// <summary>Vẽ dần từ 0 → 1 trong 700 ms, bắt đầu sau delay ms (lệch nhịp với các biểu đồ khác).</summary>
    public void Animate(int delay = 0)
    {
        cancel?.Invoke();
        progress = 0f; Invalidate();
        cancel = Motion.Delay(delay, () => cancel = Motion.Run(Motion.Slow, p => { progress = p; Invalidate(); }));
    }
    /// <summary>Hiện thẳng trạng thái cuối (làm mới dữ liệu).</summary>
    public void ShowFinal() { cancel?.Invoke(); progress = 1f; Invalidate(); }
    /// <summary>Về trạng thái đầu (chưa vẽ) để trang chụp ảnh rồi trượt vào; sau đó gọi <see cref="Animate"/>.</summary>
    public void Prepare() { cancel?.Invoke(); progress = 0f; Invalidate(); }

    // Lớp tĩnh (lưới, nhãn trục) vẽ một lần vào bitmap theo kích thước + khoá dữ liệu; mỗi khung animation chỉ vẽ phần động lên trên.
    Bitmap? layer; (Size size, string key) layerKey;
    protected Bitmap StaticLayer(string key, Action<Graphics> paint)
    {
        if (layer != null && layerKey == (Size, key)) return layer;
        layer?.Dispose();
        layer = new Bitmap(Math.Max(1, Width), Math.Max(1, Height));
        using (var g = Graphics.FromImage(layer)) { Setup(g); g.Clear(BackColor); paint(g); }
        layerKey = (Size, key);
        return layer;
    }
    protected void DropLayer() { layer?.Dispose(); layer = null; }

    protected override void OnMouseLeave(EventArgs e) { SetHover(-1); base.OnMouseLeave(e); }
    protected void SetHover(int i)
    {
        if (hover == i && !leaving) return;
        int prev = hover;
        hoverAnim?.Invoke();
        if (i >= 0)
        {
            leaving = false;
            hover = i;
            hoverT = 0f;
            hoverAnim = Motion.Run(Motion.Fast, p => { hoverT = p; Invalidate(); });
            if (dimT < 1f) { dimAnim?.Invoke(); float from = dimT; dimAnim = Motion.Run(Motion.Fast, p => { dimT = from + (1f - from) * p; Invalidate(); }); }
            OnHoverChanged(prev, i);
        }
        else
        {
            // rời chuột: giữ chỉ số cũ để phần nổi và tooltip mờ dần (160 ms) rồi mới bỏ
            leaving = true;
            dimAnim?.Invoke();
            float h0 = hoverT, d0 = dimT;
            hoverAnim = Motion.Run(Motion.Fast, p => { hoverT = h0 * (1f - p); dimT = d0 * (1f - p); Invalidate(); },
                () => { if (leaving) { hover = -1; leaving = false; Invalidate(); } });
        }
    }
    /// <summary>Gọi khi chuyển sang phần tử now (prev = -1 nếu vừa vào). Biểu đồ dùng để trượt tooltip.</summary>
    protected virtual void OnHoverChanged(int prev, int now) { }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { cancel?.Invoke(); hoverAnim?.Invoke(); dimAnim?.Invoke(); DropLayer(); }
        base.Dispose(disposing);
    }

    protected static void Setup(Graphics g)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
    }

    protected static Pen Dashed(Color c) => new(c, 1f) { DashStyle = DashStyle.Custom, DashPattern = [4f, 4f] };

    /// <summary>Trục "đẹp": bước 1·2·5·10… sao cho tối đa 5 khoảng (5 → 0..5 bước 1; 10 → bước 2; 48 → 50 bước 10).</summary>
    protected static (int Max, int Step) Nice(int max)
    {
        max = Math.Max(max, 1);
        foreach (var step in new[] { 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000 })
            if ((int)Math.Ceiling(max / (double)step) <= 5) return ((int)Math.Ceiling(max / (double)step) * step, step);
        return (max, Math.Max(1, max / 5));
    }

    protected static GraphicsPath RoundRect(RectangleF r, float rad)
    {
        rad = Math.Min(rad, Math.Min(r.Width, r.Height) / 2f);
        var p = new GraphicsPath();
        if (rad <= 0.5f) { p.AddRectangle(r); return p; }
        float d = rad * 2;
        p.AddArc(r.X, r.Y, d, d, 180, 90);
        p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
        p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure();
        return p;
    }

    /// <summary>Tooltip nền tối bo 8, chữ trắng Small Semibold, phía trên điểm neo, kẹp trong bounds; alpha 0 → 1 kèm trượt lên 6 px khi xuất hiện.</summary>
    protected static void Tooltip(Graphics g, string text, PointF anchor, Rectangle bounds, float alpha = 1f)
    {
        var size = g.MeasureString(text, AppTheme.SmallSemi);
        float w = size.Width + S(18), h = size.Height + S(10);
        float x = Math.Clamp(anchor.X - w / 2, bounds.Left, bounds.Right - w);
        float y = anchor.Y - h - S(12) + S(6) * (1f - alpha);
        if (y < bounds.Top) y = anchor.Y + S(12);
        using var path = RoundRect(new RectangleF(x, y, w, h), S(8));
        using var back = new SolidBrush(Color.FromArgb((int)(240 * alpha), AppTheme.TextPrimary));
        g.FillPath(back, path);
        using var fore = new SolidBrush(Color.FromArgb((int)(255 * alpha), Color.White));
        g.DrawString(text, AppTheme.SmallSemi, fore, x + S(9), y + S(5));
    }

    protected static void Text(Graphics g, string s, Font f, Color c, float x, float y, ContentAlignment align = ContentAlignment.MiddleLeft)
    {
        var size = g.MeasureString(s, f);
        float dx = align is ContentAlignment.MiddleRight or ContentAlignment.TopRight or ContentAlignment.BottomRight ? -size.Width
                 : align is ContentAlignment.MiddleCenter or ContentAlignment.TopCenter or ContentAlignment.BottomCenter ? -size.Width / 2 : 0;
        float dy = align is ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight ? -size.Height / 2
                 : align is ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight ? -size.Height : 0;
        using var br = new SolidBrush(c);
        g.DrawString(s, f, br, x + dx, y + dy);
    }
}

/// <summary>Area chart spline: gradient mờ dần, chấm trắng viền màu, hover có đường dóng đứt + tooltip.</summary>
public sealed class AreaChart : ChartBase
{
    string[] labels = [];
    int[] values = [];
    string unit = "";
    Color line = AppTheme.CardViolet.Base;
    float gx = float.NaN, gy;   // vị trí đường dóng / tooltip, trượt mượt giữa các điểm khi đổi hover
    Action? glide;
    static int PadL => S(40); static int PadR => S(16); static int PadT => S(16); static int PadB => S(30);

    public void SetData(string[] labels, int[] values, string unit, Color? color = null)
    {
        this.labels = labels; this.values = values; this.unit = unit;
        if (color is { } c) line = c;
        DropLayer();
        Invalidate();
    }

    Rectangle Plot => new(PadL, PadT, Math.Max(10, Width - PadL - PadR), Math.Max(10, Height - PadT - PadB));

    PointF[] Points(Rectangle plot, int yMax)
    {
        int n = values.Length;
        var pts = new PointF[n];
        for (int i = 0; i < n; i++)
        {
            float x = n == 1 ? plot.Left + plot.Width / 2f : plot.Left + plot.Width * i / (float)(n - 1);
            float y = plot.Bottom - plot.Height * (values[i] / (float)yMax) * progress;
            pts[i] = new PointF(x, y);
        }
        return pts;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics; Setup(g);
        if (values.Length == 0) { g.Clear(BackColor); return; }
        var plot = Plot;
        var (yMax, step) = Nice(values.Max());
        var pts = Points(plot, yMax);

        // lớp tĩnh: lưới ngang gạch đứt + nhãn Y + nhãn X (không đổi theo progress / hover)
        g.DrawImageUnscaled(StaticLayer($"{yMax}|{string.Join(",", labels)}", sg =>
        {
            using var grid = Dashed(AppTheme.Border);
            for (int v = 0; v <= yMax; v += step)
            {
                float y = plot.Bottom - plot.Height * v / (float)yMax;
                sg.DrawLine(grid, plot.Left, y, plot.Right, y);
                Text(sg, v.ToString(), AppTheme.Small, AppTheme.TextMuted, plot.Left - S(10), y, ContentAlignment.MiddleRight);
            }
            int every = plot.Width < labels.Length * S(34) ? 2 : 1;   // nhãn X: bỏ cách nếu hẹp
            for (int i = 0; i < labels.Length; i++)
                if (i % every == 0 || i == labels.Length - 1)
                    Text(sg, labels[i], AppTheme.Small, AppTheme.TextMuted, pts[i].X, plot.Bottom + S(8), ContentAlignment.TopCenter);
        }), 0, 0);
        var state = g.Save();
        g.SetClip(new Rectangle(plot.Left - S(4), plot.Top - S(8), plot.Width + S(8), plot.Height + S(9)));   // spline không trồi xuống dưới trục
        if (pts.Length >= 2)
        {
            using var area = new GraphicsPath();
            area.AddCurve(pts, 0.4f);
            area.AddLine(pts[^1], new PointF(pts[^1].X, plot.Bottom));
            area.AddLine(new PointF(pts[^1].X, plot.Bottom), new PointF(pts[0].X, plot.Bottom));
            area.CloseFigure();
            using var fill = new LinearGradientBrush(new Rectangle(plot.Left, plot.Top - S(8), plot.Width, plot.Height + S(9)), Color.FromArgb(105, line), Color.FromArgb(0, line), LinearGradientMode.Vertical);
            g.FillPath(fill, area);
            using var curve = new GraphicsPath();
            curve.AddCurve(pts, 0.4f);
            using var pen = new Pen(line, SF(2.5f)) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round };
            g.DrawPath(pen, curve);
        }
        g.Restore(state);

        // đường dóng hover: từ điểm xuống trục (mẫu Seative), tại vị trí trượt gx
        if (hover >= 0 && hover < pts.Length && !float.IsNaN(gx))
        {
            using var guide = Dashed(Color.FromArgb((int)(140 * dimT), line));
            g.DrawLine(guide, gx, gy, gx, plot.Bottom);
        }

        // chấm dữ liệu: chấm được trỏ to dần, các chấm khác mờ còn 60 %
        using (var dot = new SolidBrush(Color.White))
            for (int i = 0; i < pts.Length; i++)
            {
                float r = SF(4f) + SF(2f) * (i == hover ? hoverT : 0f);
                using var ring = new Pen(Color.FromArgb(i == hover ? 255 : (int)(255 - 100 * dimT), line), SF(2f));
                g.FillEllipse(dot, pts[i].X - r, pts[i].Y - r, r * 2, r * 2);
                g.DrawEllipse(ring, pts[i].X - r, pts[i].Y - r, r * 2, r * 2);
            }

        if (hover >= 0 && hover < pts.Length && !float.IsNaN(gx))
            Tooltip(g, $"{labels[hover]} · {values[hover]} {unit}", new PointF(gx, gy), ClientRectangle, dimT);
    }

    protected override void OnHoverChanged(int prev, int now)
    {
        var pts = Points(Plot, Nice(values.Max()).Max);
        if (now < 0 || now >= pts.Length) return;
        var target = pts[now];
        glide?.Invoke();
        if (prev < 0 || float.IsNaN(gx)) { gx = target.X; gy = target.Y; return; }   // vừa vào: đặt thẳng
        float x0 = gx, y0 = gy;
        glide = Motion.Run(Motion.Fast, p => { gx = x0 + (target.X - x0) * p; gy = y0 + (target.Y - y0) * p; Invalidate(); });
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (values.Length == 0) return;
        var plot = Plot;
        if (e.Y < plot.Top - S(8) || e.Y > plot.Bottom + S(8)) { SetHover(-1); return; }
        // điểm gần nhất theo X trên toàn bề rộng đồ thị (tooltip luôn đi theo chuột như mẫu)
        var pts = Points(plot, Nice(values.Max()).Max);
        int best = -1; float bestD = float.MaxValue;
        for (int i = 0; i < pts.Length; i++) { float d = Math.Abs(pts[i].X - e.X); if (d < bestD) { bestD = d; best = i; } }
        SetHover(best);
    }
}

/// <summary>Donut dày: số tổng ở tâm, chú giải bên phải (chấm màu · nhãn · số · %), khe 3° giữa các phần. Hover: phần được trỏ TÁCH ra 4 px theo hướng tâm (mẫu Keitoto), phần khác mờ còn 70 %, dòng chú giải tương ứng có nền; không tooltip.</summary>
public sealed class DonutChart : ChartBase
{
    (string Label, int Value, Color Color)[] items = [];
    string center = "", centerSub = "";

    public void SetData((string Label, int Value, Color Color)[] items, string center, string centerSub)
    {
        this.items = items; this.center = center; this.centerSub = centerSub;
        Invalidate();
    }

    bool Side => Width >= S(260);

    (RectangleF ring, float thick) Geometry()
    {
        bool side = Side;
        float d = side ? Math.Min(Height - S(12), Math.Min(S(176), Width * 0.5f)) : Math.Min(Height - S(96), Width - S(24));
        d = Math.Max(S(60), d);
        float thick = d * 0.17f;
        float x = side ? S(8) : (Width - d) / 2f, y = side ? (Height - d) / 2f : S(8);
        return (new RectangleF(x + thick / 2, y + thick / 2, d - thick, d - thick), thick);
    }

    (float lx, float ly, float rowH) Legend(RectangleF ring, float thick, PointF c)
    {
        float rowH = S(44);
        float lx = Side ? ring.Right + thick / 2 + S(24) : S(16);
        float ly = Side ? c.Y - rowH * items.Length / 2f : ring.Bottom + thick / 2 + S(20);
        return (lx, ly, rowH);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics; Setup(g);
        g.Clear(BackColor);
        int total = items.Sum(i => i.Value);
        if (items.Length == 0 || total == 0) return;
        var (ring, thick) = Geometry();
        const float gap = 3f;
        float start = -90f, sweepAll = 360f * progress;
        for (int i = 0; i < items.Length && sweepAll > 0; i++)
        {
            float sweep = 360f * items[i].Value / total;
            float drawn = Math.Min(sweep, sweepAll);
            sweepAll -= sweep;
            float s = drawn - (items.Length > 1 ? gap : 0);
            if (s > 0.5f)
            {
                var rr = ring;
                if (i == hover && hoverT > 0f)
                {
                    float mid = (start + sweep / 2) * MathF.PI / 180f, off = S(4) * hoverT;   // tách ra theo hướng tâm phần
                    rr.Offset(off * MathF.Cos(mid), off * MathF.Sin(mid));
                }
                using var pen = new Pen(Color.FromArgb(i == hover ? 255 : (int)(255 - 76 * dimT), items[i].Color), thick);
                g.DrawArc(pen, rr, start + gap / 2, s);
            }
            start += sweep;
        }

        // số tổng ở tâm
        var c = new PointF(ring.X + ring.Width / 2, ring.Y + ring.Height / 2);
        Text(g, center, AppTheme.Display, AppTheme.TextPrimary, c.X, c.Y - S(6), ContentAlignment.MiddleCenter);
        Text(g, centerSub, AppTheme.Small, AppTheme.TextMuted, c.X, c.Y + S(18), ContentAlignment.MiddleCenter);

        // chú giải
        var (lx, ly, rowH) = Legend(ring, thick, c);
        float lw = Width - lx - S(8);
        for (int i = 0; i < items.Length; i++)
        {
            float y = ly + i * rowH + rowH / 2;
            if (i == hover && hoverT > 0f)
            {
                using var rowBg = RoundRect(new RectangleF(lx - S(8), y - rowH / 2 + S(3), lw + S(16), rowH - S(6)), S(8));
                using var rb = new SolidBrush(Color.FromArgb((int)(255 * hoverT), AppTheme.SurfaceHover));
                g.FillPath(rb, rowBg);
            }
            using (var path = RoundRect(new RectangleF(lx, y - S(6), S(12), S(12)), S(4)))
            using (var br = new SolidBrush(items[i].Color)) g.FillPath(br, path);
            Text(g, items[i].Label, i == hover ? AppTheme.BodySemi : AppTheme.Body, AppTheme.TextSecondary, lx + S(22), y - S(9));
            Text(g, $"{items[i].Value * 100.0 / total:0}%", AppTheme.Small, AppTheme.TextMuted, lx + S(22), y + S(10));
            Text(g, items[i].Value.ToString(), AppTheme.BodySemi, AppTheme.TextPrimary, lx + lw, y, ContentAlignment.MiddleRight);
        }

        // không tooltip: chú giải bên phải đã có nhãn, số và % (người dùng bỏ 05/09/2026)
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int total = items.Sum(i => i.Value);
        if (total == 0) return;
        var (ring, thick) = Geometry();
        var c = new PointF(ring.X + ring.Width / 2, ring.Y + ring.Height / 2);
        float dx = e.X - c.X, dy = e.Y - c.Y, dist = MathF.Sqrt(dx * dx + dy * dy), r = ring.Width / 2;
        int idx = -1;
        if (dist >= r - thick / 2 - S(4) && dist <= r + thick / 2 + S(4))
        {
            float ang = (MathF.Atan2(dy, dx) * 180f / MathF.PI + 90f + 360f) % 360f;   // 0 ở đỉnh, xuôi chiều kim đồng hồ
            float acc = 0;
            for (int i = 0; i < items.Length; i++) { acc += 360f * items[i].Value / total; if (ang < acc) { idx = i; break; } }
        }
        else
        {
            // hover lên dòng chú giải
            var (lx, ly, rowH) = Legend(ring, thick, c);
            if (e.X >= lx) { int k = (int)((e.Y - ly) / rowH); if (k >= 0 && k < items.Length && e.Y >= ly) idx = k; }
        }
        SetHover(idx);
    }
}

/// <summary>
/// Thanh ngang bo tròn, gradient, giá trị ở đầu thanh; lưới dọc gạch đứt. Hover (không tooltip): gradient thanh được trỏ ĐỔI SẮC từ xanh dương
/// (CardBlue → ChartBlueLight) sang cặp màu nút chính (Primary → Accent) trong 160 ms, một VỆT SÁNG chạy qua thanh một lần (400 ms), các thanh khác mờ còn 55 %.
/// Tham khảo Dribbble: Keitoto "Karvio Animated Bar Chart" 27340882 (gradient lấp đầy khi hover), Ankit Baghel "Motion UI Analytics Chart" 27072221 (gradient chạy dọc thanh),
/// Dreamten "Chart Hoverstate" 22945384 (thanh được trỏ đổi màu đậm).
/// </summary>
public sealed class HBarChart : ChartBase
{
    string[] labels = [];
    int[] values = [];
    string unit = "";
    Color a = AppTheme.CardBlue.Base, b = AppTheme.ChartBlueLight;
    float shineT = 1f;   // 0 → 1: vệt sáng chạy từ trái sang phải thanh được trỏ, một lần mỗi khi trỏ sang thanh khác
    Action? shine;
    static int LabelW => S(46); static int PadR => S(40); static int PadT => S(6); static int PadB => S(22);

    public void SetData(string[] labels, int[] values, string unit, Color? from = null, Color? to = null)
    {
        this.labels = labels; this.values = values; this.unit = unit;
        if (from is { } f) a = f;
        if (to is { } t) b = t;
        DropLayer();
        Invalidate();
    }

    Rectangle Plot => new(LabelW + S(12), PadT, Math.Max(10, Width - LabelW - S(12) - PadR), Math.Max(10, Height - PadT - PadB));

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics; Setup(g);
        if (values.Length == 0) { g.Clear(BackColor); return; }
        var plot = Plot;
        var (max, step) = Nice(values.Max());
        float rowH = plot.Height / (float)values.Length;
        float barH = Math.Min(S(16), rowH * 0.6f);

        // lớp tĩnh: lưới dọc gạch đứt + số trục
        g.DrawImageUnscaled(StaticLayer(max.ToString(), sg =>
        {
            using var grid = Dashed(AppTheme.Border);
            for (int v = 0; v <= max; v += step)
            {
                float x = plot.Left + plot.Width * v / (float)max;
                sg.DrawLine(grid, x, plot.Top, x, plot.Bottom);
                Text(sg, v.ToString(), AppTheme.Small, AppTheme.TextMuted, x, plot.Bottom + S(6), ContentAlignment.TopCenter);
            }
        }), 0, 0);

        for (int i = 0; i < values.Length; i++)
        {
            float cy = plot.Top + rowH * i + rowH / 2;
            Text(g, labels[i], AppTheme.Small, Motion.Lerp(AppTheme.TextSecondary, AppTheme.TextPrimary, i == hover ? hoverT : 0f), plot.Left - S(12), cy, ContentAlignment.MiddleRight);
            float w = plot.Width * (values[i] / (float)max) * progress;
            var rect = new RectangleF(plot.Left, cy - barH / 2, Math.Max(w, S(4)), barH);
            using var path = RoundRect(rect, barH / 2);
            if (values[i] == 0) { using var stub = new SolidBrush(AppTheme.Border); g.FillPath(stub, path); continue; }
            int alpha = i == hover ? 255 : (int)(255 - 115 * dimT);   // thanh không được trỏ mờ còn 55 %
            // thanh được trỏ: gradient đổi sắc xanh dương → Primary/Accent theo hoverT (rời chuột thì trôi về)
            Color ca = i == hover ? Motion.Lerp(a, AppTheme.Primary, hoverT) : a, cb = i == hover ? Motion.Lerp(b, AppTheme.Accent, hoverT) : b;
            using (var grad = new LinearGradientBrush(new RectangleF(rect.X - 1, rect.Y, Math.Max(rect.Width, 2) + 2, rect.Height), Color.FromArgb(alpha, ca), Color.FromArgb(alpha, cb), LinearGradientMode.Horizontal))
                g.FillPath(grad, path);
            if (i == hover && shineT < 1f && rect.Width > S(8))
            {
                // vệt sáng rộng 25 % thanh (≥ 24 px) trượt từ ngoài mép trái ra khỏi mép phải, kẹp trong thanh
                float band = Math.Max(S(24), rect.Width * 0.25f), sx = rect.X - band + (rect.Width + band) * shineT;
                var st = g.Save(); g.SetClip(path);
                using var sweep = new LinearGradientBrush(new RectangleF(sx - 1, rect.Y, band + 2, rect.Height), Color.Transparent, Color.Transparent, LinearGradientMode.Horizontal)
                {
                    InterpolationColors = new ColorBlend
                    {
                        Colors = [Color.FromArgb(0, Color.White), Color.FromArgb((int)(130 * hoverT), Color.White), Color.FromArgb(0, Color.White)],
                        Positions = [0f, 0.5f, 1f],
                    },
                };
                g.FillRectangle(sweep, sx, rect.Y, band, rect.Height);
                g.Restore(st);
            }
            if (progress >= 1f)
                Text(g, values[i].ToString(), AppTheme.SmallSemi, i == hover ? AppTheme.TextPrimary : Motion.Lerp(AppTheme.TextPrimary, AppTheme.TextMuted, dimT), rect.Right + S(8), cy);
        }

        // không tooltip: nhãn thập niên và số ở đầu thanh đã đủ (người dùng bỏ 05/09/2026)
    }

    protected override void OnHoverChanged(int prev, int now)
    {
        shine?.Invoke();
        shineT = 0f;
        shine = Motion.Run(400, p => { shineT = p; Invalidate(); });   // một nhịp, không lặp
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) shine?.Invoke();
        base.Dispose(disposing);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (values.Length == 0) return;
        var plot = Plot;
        int idx = (int)((e.Y - plot.Top) / (plot.Height / (float)values.Length));
        SetHover(e.Y >= plot.Top && idx >= 0 && idx < values.Length ? idx : -1);
    }
}
