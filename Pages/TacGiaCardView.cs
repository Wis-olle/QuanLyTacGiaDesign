using System.Drawing.Drawing2D;
using System.Drawing.Text;
using AntdUI;
using QuanLyTacGia.Models;
using QuanLyTacGia.Ui;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Pages;

/// <summary>
/// Chế độ xem dạng THẺ của P2 (toggle bảng ↔ thẻ ở toolbar). Tham khảo Dribbble: "Modern Data Table Variants for SaaS Dashboard UI"
/// (27574900) và "Organization Access Control – Team Members List" (21103831): avatar + tên + email trên một khối, nút hành động icon.
/// Lưới FlowLayoutPanel cuộn dọc, mỗi thẻ là <see cref="AuthorCard"/> tự vẽ (nhẹ, 48 thẻ không tốn control con). Kích thước × K (DPI).
/// </summary>
public sealed class TacGiaCardView : FlowLayoutPanel
{
    public Action<TacGia>? View, EditRequested, DeleteRequested;
    readonly AntdUI.Label empty = new()
    {
        Text = "Không tìm thấy tác giả", Font = AppTheme.Body, ForeColor = AppTheme.TextMuted,
        Size = new Size(320, 24), BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter, Margin = new Padding(8, 48, 8, 8),   // px 96: WinForms tự nhân
    };

    public TacGiaCardView()
    {
        AutoScroll = true; WrapContents = true; FlowDirection = FlowDirection.LeftToRight;
        BackColor = Color.White; Padding = new Padding(16, 4, 16, 8);   // px 96 (đặt lại trong Designer của trang)
    }

    public void SetData(IReadOnlyList<TacGia> list)
    {
        SuspendLayout();
        foreach (var c in Controls.Cast<Control>().ToList()) { Controls.Remove(c); if (c != empty) c.Dispose(); }
        if (list.Count == 0) Controls.Add(empty);
        int idx = 0;
        foreach (var t in list)
        {
            var card = new AuthorCard(t) { Margin = new Padding(S(8) - AuthorCard.Inset) };   // lề 4 + vùng bóng 4 trong control = 8 như cũ
            card.Appear(Math.Min(idx++, 14) * 30);   // 15 thẻ đầu xuất hiện lệch nhịp 30 ms, phần sau hiện cùng lúc
            card.Clicked += () => View?.Invoke(t);
            card.DoubleClicked += () => EditRequested?.Invoke(t);
            card.Action += id => { if (id == "view") View?.Invoke(t); else if (id == "edit") EditRequested?.Invoke(t); else DeleteRequested?.Invoke(t); };
            Controls.Add(card);
        }
        ResumeLayout();
        if (VerticalScroll.Visible) VerticalScroll.Value = 0;
        lastW = 0;
        Relayout();
    }

    int lastW;

    /// <summary>Số cột = phần rộng còn lại / (236 + 16 lề); thẻ giãn đều để lấp kín hàng (1920: 6 cột ~250, 1536: 4 cột ~288, 1024: 2 cột ~336).</summary>
    void Relayout()
    {
        int avail = ClientSize.Width - Padding.Horizontal, gap = S(16), minW = AuthorCard.MinW;
        if (avail <= 0) return;
        int cols = Math.Max(1, (avail + gap) / (minW + gap));
        int w = Math.Max(minW, (avail - gap * cols) / cols);
        if (w == lastW) return;
        lastW = w;
        SuspendLayout();
        foreach (var c in Controls.OfType<AuthorCard>()) c.Width = w + 2 * AuthorCard.Inset;   // w là bề rộng thẻ nhìn thấy
        ResumeLayout();
    }

    protected override void OnResize(EventArgs e) { base.OnResize(e); Relayout(); }
}

/// <summary>
/// Thẻ tác giả (rộng ≥ 236 × cao 192, × K) tự vẽ: avatar 56 tròn · tên · "mã · ngày sinh" · pill giới tính · tag "Mới" (thêm ≤ 30 ngày) ·
/// điện thoại, email có icon · 3 nút icon Xem / Sửa / Xoá ở đáy. Click thẻ → xem, double click → sửa.
/// Hover (240 ms, một nhịp — theo Farhad Ahmed "Smooth Product Cards Hover" 27252843 và George "Card hover" 25183999): thẻ NHẤC lên 3 px kèm bóng mờ,
/// avatar phóng 6 %, nền SurfaceHover + viền Primary. Control rộng hơn thẻ <see cref="Inset"/> mỗi bên để có chỗ vẽ bóng / nhấc mà không bị cắt.
/// </summary>
public sealed class AuthorCard : Control
{
    public event Action? Clicked, DoubleClicked;
    public event Action<string>? Action;

    static readonly Dictionary<string, Bitmap> icons = new();
    static readonly (string Id, string Svg)[] Btns = [("view", "EyeOutlined"), ("edit", "EditOutlined"), ("del", "DeleteOutlined")];
    public static int MinW => S(236);   // bề rộng THẺ nhìn thấy (control = thẻ + 2 × Inset)
    public static int H => S(192);
    public static int Inset => S(4);    // vùng trống quanh thẻ trong control: chỗ cho bóng và cú nhấc 3 px
    static int Pad => S(16);
    static int Btn => S(28);
    int W => Width - 2 * Inset;   // bề rộng thẻ; TacGiaCardView đặt Width để lấp đầy hàng

    readonly TacGia t;
    bool hover;
    int hoverBtn = -1;
    float appear = 1f, hoverT;
    Action? appearAnim, hoverAnim;

    public AuthorCard(TacGia t)
    {
        this.t = t;
        Size = new Size(MinW + 2 * Inset, H + 2 * Inset);
        SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
        BackColor = Color.White;
        Cursor = Cursors.Hand;
    }

    /// <summary>Xuất hiện: trượt lên 14 px + hiện dần trong 320 ms sau delay ms.</summary>
    public void Appear(int delay)
    {
        appearAnim?.Invoke();
        appear = 0f; Invalidate();
        appearAnim = Motion.Delay(delay, () => appearAnim = Motion.Run(320, p => { appear = p; Invalidate(); }));
    }

    void HoverTo(float target)
    {
        hoverAnim?.Invoke();
        float from = hoverT;
        hoverAnim = Motion.Run(Motion.Base, p => { hoverT = from + (target - from) * p; Invalidate(); });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) { appearAnim?.Invoke(); hoverAnim?.Invoke(); }
        base.Dispose(disposing);
    }

    static Bitmap Icon(string name, Color c)
    {
        string k = name + c.ToArgb() + "@" + S(14);
        if (!icons.TryGetValue(k, out var b)) icons[k] = b = SvgExtend.SvgToBmp(SvgDb.Custom[name], S(14), S(14), c)!;
        return b;
    }

    Rectangle BtnRect(int i) => new(W - Pad - (3 - i) * Btn - (2 - i) * S(4), H - S(14) - Btn, Btn, Btn);

    static GraphicsPath RoundRect(RectangleF r, float rad)
    {
        var p = new GraphicsPath(); float d = rad * 2;
        p.AddArc(r.X, r.Y, d, d, 180, 90); p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
        p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90); p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
        p.CloseFigure(); return p;
    }

    static void Txt(Graphics g, string s, Font f, Color c, float x, float y, float w)
    {
        using var br = new SolidBrush(c);
        using var sf = new StringFormat(StringFormatFlags.NoWrap) { Trimming = StringTrimming.EllipsisCharacter };
        g.DrawString(s, f, br, new RectangleF(x, y, w, f.Height + S(4)), sf);
    }

    static float Pill(Graphics g, string s, Color back, Color fore, float x, float y, bool rightAlign = false)
    {
        var size = g.MeasureString(s, AppTheme.SmallSemi);
        float w = size.Width + S(14), h = S(22);
        if (rightAlign) x -= w;
        using var path = RoundRect(new RectangleF(x, y, w, h), h / 2);
        using var b = new SolidBrush(back); g.FillPath(b, path);
        using var f = new SolidBrush(fore); g.DrawString(s, AppTheme.SmallSemi, f, x + S(7), y + (h - size.Height) / 2 + 1);
        return w;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (appear >= 1f) { PaintCard(e.Graphics); return; }
        // đang xuất hiện: vẽ thẻ vào bitmap rồi chép với alpha = appear và trượt lên (1 - appear) × 14 px
        e.Graphics.Clear(Parent?.BackColor ?? Color.White);
        using var tmp = new Bitmap(Math.Max(1, Width), Math.Max(1, Height));
        using (var tg = Graphics.FromImage(tmp)) PaintCard(tg);
        using var attr = new System.Drawing.Imaging.ImageAttributes();
        attr.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix { Matrix33 = appear });
        int dy = (int)((1f - appear) * S(14));
        e.Graphics.DrawImage(tmp, new Rectangle(0, dy, Width, Height), 0, 0, Width, Height, GraphicsUnit.Pixel, attr);
    }

    void PaintCard(Graphics g)
    {
        g.SmoothingMode = SmoothingMode.AntiAlias; g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit; g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.Clear(Parent?.BackColor ?? Color.White);
        // nhấc lên 3 px khi hover + bóng mờ 3 lớp phía dưới (vẽ trong vùng Inset nên không bị cắt)
        float lift = S(3) * hoverT;
        if (hoverT > 0f)
            for (int k = 0; k < 3; k++)
            {
                using var sh = RoundRect(new RectangleF(Inset - k, Inset - lift + S(2) + k * 1.5f, W + 2 * k, H), S(12) + k);
                using var sb = new SolidBrush(Color.FromArgb((int)((10 - 3 * k) * hoverT), AppTheme.TextPrimary));
                g.FillPath(sb, sh);
            }
        g.TranslateTransform(Inset, Inset - lift);
        using (var path = RoundRect(new RectangleF(0.5f, 0.5f, W - 1, H - 1), S(12)))
        {
            using var bg = new SolidBrush(Motion.Lerp(Color.White, AppTheme.SurfaceHover, hoverT)); g.FillPath(bg, path);
            using var pen = new Pen(Motion.Lerp(AppTheme.Border, AppTheme.Primary, hoverT), 1f); g.DrawPath(pen, path);
        }

        // avatar tròn, phóng 6 % khi hover (tâm giữ nguyên)
        int av = S(56), x2 = S(84);
        var img = t.Avatar ?? AvatarFactory.Get(t.HoTen, 112);
        float avz = av * (1f + 0.06f * hoverT), ao = (av - avz) / 2f;
        var avRect = new RectangleF(Pad + ao, Pad + ao, avz, avz);
        var st = g.Save();
        using (var clip = new GraphicsPath()) { clip.AddEllipse(avRect); g.SetClip(clip); g.DrawImage(img, avRect); }
        g.Restore(st);

        bool isNew = (DateTime.Today - t.NgayTao).TotalDays <= 30;
        float nameW = W - x2 - Pad - (isNew ? S(46) : 0);
        Txt(g, t.HoTen, AppTheme.BodySemi, AppTheme.TextPrimary, x2, S(16), nameW);
        Txt(g, $"{t.MaTacGia} · {t.NgaySinh:dd/MM/yyyy}", AppTheme.Small, AppTheme.TextMuted, x2, S(38), W - x2 - Pad);
        bool nu = t.GioiTinh == "Nữ";
        Pill(g, nu ? "Nữ" : "Nam", nu ? AppTheme.PinkSoft : AppTheme.PrimarySoft, nu ? AppTheme.PinkText : AppTheme.Primary, x2, S(60));
        if (isNew) Pill(g, "Mới", AppTheme.SuccessSoft, AppTheme.SuccessText, W - Pad, S(14), rightAlign: true);

        using (var d = new Pen(AppTheme.Divider)) g.DrawLine(d, Pad, S(92), W - Pad, S(92));
        int tx = Pad + S(22), tw = W - Pad * 2 - S(22);
        g.DrawImage(Icon("PhoneOutlined", AppTheme.IconMuted), Pad, S(105));
        Txt(g, t.DienThoai, AppTheme.Body, AppTheme.TextSecondary, tx, S(102), tw);
        g.DrawImage(Icon("MailOutlined", AppTheme.IconMuted), Pad, S(128));
        Txt(g, t.Email, AppTheme.Body, AppTheme.TextSecondary, tx, S(125), tw);

        for (int i = 0; i < Btns.Length; i++)
        {
            var r = BtnRect(i);
            bool danger = i == 2;
            if (i == hoverBtn)
            {
                using var p = RoundRect(r, S(6));
                using var b = new SolidBrush(danger ? AppTheme.DangerSoft : AppTheme.SurfacePressed);
                g.FillPath(b, p);
            }
            g.DrawImage(Icon(Btns[i].Svg, danger ? AppTheme.Danger : AppTheme.TextSecondary), r.X + S(7), r.Y + S(7));
        }
    }

    protected override void OnMouseEnter(EventArgs e) { hover = true; HoverTo(1f); base.OnMouseEnter(e); }
    protected override void OnMouseLeave(EventArgs e) { hover = false; hoverBtn = -1; HoverTo(0f); base.OnMouseLeave(e); }
    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        int b = -1;
        var p = new Point(e.X - Inset, e.Y - Inset);   // toạ độ trong thẻ (bỏ vùng Inset)
        for (int i = 0; i < Btns.Length; i++) if (BtnRect(i).Contains(p)) b = i;
        if (b != hoverBtn) { hoverBtn = b; Invalidate(); }
    }
    protected override void OnMouseClick(MouseEventArgs e)
    {
        base.OnMouseClick(e);
        if (e.Button != MouseButtons.Left) return;
        if (hoverBtn >= 0) Action?.Invoke(Btns[hoverBtn].Id); else Clicked?.Invoke();
    }
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
        base.OnMouseDoubleClick(e);
        if (e.Button == MouseButtons.Left && hoverBtn < 0) DoubleClicked?.Invoke();
    }
}
