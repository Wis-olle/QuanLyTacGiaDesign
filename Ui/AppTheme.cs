namespace QuanLyTacGia.Ui;

/// <summary>
/// Token màu, chữ và HỆ SỐ DPI theo PHONG_CACH_THIET_KE.md.
/// Mẫu: Dribbble "User Management Dashboard System" (Gaurav Singh Kushwaha, shot 27274985).
///
/// BẢN KÉO THẢ: bố cục nằm trong *.Designer.cs (px 96 DPI) và WinForms AutoScaleMode.Dpi tự nhân theo DPI màn.
/// <see cref="K"/> (DPI/96, MainForm đặt lúc mở và khi đổi DPI) chỉ dùng cho phần VẼ TAY (biểu đồ, thẻ tác giả, hoạ tiết, HeaderView)
/// và tính toán lúc chạy (Relayout, ngưỡng ẩn cột) qua <see cref="S"/> / <see cref="SF"/>; font vẽ tay theo point × <see cref="FontK"/>.
/// Thuộc tính px của AntdUI (Radius, Gap, RowHeight, Modal.Width…) KHÔNG nhân K vì AntdUI đã nhân Config.Dpi (được đặt bằng K).
/// </summary>
public static class AppTheme
{
    // ---------- DPI ----------
    /// <summary>Hệ số phóng hiện tại (DPI màn / 96). 1 = 100 %, 1.25 = 125 %…</summary>
    public static float K { get; private set; } = 1f;
    public static int S(int px) => (int)Math.Round(px * K);
    public static float SF(float px) => px * K;
    public static Point P(int x, int y) => new(S(x), S(y));
    public static Size Sz(int w, int h) => new(S(w), S(h));
    public static Padding Pd(int all) => new(S(all));
    public static Padding Pd(int l, int t, int r, int b) => new(S(l), S(t), S(r), S(b));

    /// <summary>
    /// Hệ số cho FONT (point). GDI+ đổi point → pixel theo DPI của Graphics: nếu Graphics đã báo DPI màn (graphicsDpi = 96·K) thì font
    /// không cần nhân thêm (FontK = 1); nếu Graphics vẫn báo DPI hệ thống khác DPI màn thì FontK = K·96/DPI hệ thống. Tính từ DPI thật nên đúng cả hai trường hợp.
    /// </summary>
    public static float FontK { get; private set; } = 1f;

    /// <summary>Đặt hệ số DPI mới: dựng lại font vẽ tay, báo AntdUI (Config.Dpi) và toast offset. MainForm gọi trước InitializeComponent và trong OnDpiChanged.</summary>
    public static void SetScale(float k, float graphicsDpi = 96f)
    {
        K = Math.Clamp(k, 0.5f, 4f);
        FontK = Math.Clamp(K * 96f / Math.Max(48f, graphicsDpi), 0.5f, 4f);
        Body = Pt(9.75f); BodySemi = PtSemi(9.75f);
        Small = Pt(9f); SmallSemi = PtSemi(9f);
        Subtitle = PtSemi(12f); Title = PtSemi(16f); Display = PtSemi(22f);
        CardTitle = Pt(14f); CardTitleSm = Pt(11f); CardNumber = PtSemi(26f); CardNumberSm = PtSemi(23f);
        Wordmark = BrandFont(15f);
        AntdUI.Config.Font = Body;
        AntdUI.Config.SetDpi(K);
        AntdUI.Config.NoticeWindowOffsetXY = S(64);   // toast không đè nút thu / phóng / đóng (thanh tiêu đề 56)
    }

    // ---------- Nền và bề mặt ----------
    public static readonly Color Canvas          = Hex("#F3F5F9");
    public static readonly Color Surface         = Color.White;
    public static readonly Color SurfaceHover    = Hex("#F6F8FB");
    public static readonly Color SurfaceSelected = Hex("#EAF3FC");
    public static readonly Color SurfacePressed  = Hex("#E9ECF3");
    public static readonly Color Border          = Hex("#E3E7EE");
    public static readonly Color BorderSubtle    = Hex("#EEF0F4");
    public static readonly Color Divider         = Hex("#F2F4F5");

    // ---------- Chữ và icon ----------
    public static readonly Color TextPrimary     = Hex("#0D1426");
    public static readonly Color TextSecondary   = Hex("#4A5064");
    public static readonly Color TextMuted       = Hex("#6F7689");   // 4.5:1 trên trắng
    public static readonly Color TextDisabled    = Hex("#9AA0B2");
    public static readonly Color Icon            = Hex("#414650");
    public static readonly Color IconMuted       = Hex("#8A90A2");
    public static readonly Color IconFaint       = Hex("#C5CBD8");

    // ---------- Màu nhấn ----------
    public static readonly Color Primary         = Hex("#166BB8");   // nút chính, tab, link; chữ trắng 5.0:1
    public static readonly Color PrimarySoft     = Hex("#E3F1FA");
    public static readonly Color NavActive       = Hex("#3197D2");   // ô icon mục nav đang chọn (đo từ mẫu)
    public static readonly Color Accent          = Hex("#5047E0");   // logo, nút hành động đang chọn (đo từ mẫu)
    public static readonly Color AccentSoft      = Hex("#EEEDFD");
    public static readonly Color InfoSoft        = Hex("#D3E5FF");   // badge "Quản trị viên" (đo từ mẫu)
    public static readonly Color InfoText        = Hex("#1D5AEA");
    public static readonly Color SuccessSoft     = Hex("#CEFFC9");   // pill trạng thái (đo từ mẫu)
    public static readonly Color SuccessText     = Hex("#15803D");   // 5.0:1 trên trắng
    public static readonly Color Success         = Hex("#22C55E");
    public static readonly Color PinkSoft        = Hex("#FCE7F3");
    public static readonly Color PinkText        = Hex("#BE185D");
    public static readonly Color Danger          = Hex("#DC2626");
    public static readonly Color DangerSoft      = Hex("#FEF2F2");

    // ---------- Gradient (uiGradients, MIT) ----------
    public static readonly (Color A, Color B) GradViolet = (Hex("#5B3FB8"), Hex("#7A5CD9")); // tím mẫu #6647BF
    public static readonly (Color A, Color B) GradBlue   = (Hex("#3F7FD0"), Hex("#5A9BE6")); // xanh dương mẫu #4B8CDA
    public static readonly (Color A, Color B) GradPrimary = (Hex("#166BB8"), Hex("#5047E0")); // nút primary: Primary → Accent

    // 4 thẻ số liệu — màu đo trực tiếp trên ảnh gốc mẫu 27274985: nền đặc / tông sáng (đĩa, nét) / tông tối (khối) / ô icon pastel
    public static readonly GradientCard.Tone CardOrange = new(Hex("#F27945"), Hex("#FF9060"), Hex("#DD6A3A"), Hex("#FFD5C2"));
    public static readonly GradientCard.Tone CardViolet = new(Hex("#6647BF"), Hex("#8A6FD6"), Hex("#5431B3"), Hex("#DCE6FF"));
    public static readonly GradientCard.Tone CardGreen  = new(Hex("#70BA43"), Hex("#8CCB62"), Hex("#5DA836"), Hex("#DCE6FF"));
    public static readonly GradientCard.Tone CardBlue   = new(Hex("#4D8CD6"), Hex("#6AA3E3"), Hex("#457EC3"), Hex("#DCE6FF"));
    public static readonly Color CardIcon = Hex("#6E63DE");   // icon tím trên ô pastel, đo từ mẫu
    public static readonly Color ChartPink = Hex("#F0669B");  // phần Nữ trong donut (PinkText quá tối cho vòng dày)
    public static readonly Color ChartBlueLight = Hex("#8FC1F5");   // đuôi gradient thanh ngang (đầu = CardBlue.Base)
    public static readonly Color ChartGrid = Hex("#EEF1F6");

    /// <summary>Chuỗi gradient cho AntdUI BackExtend / FillExtend: "góc, #màu1, #màu2".</summary>
    public static string Extend((Color A, Color B) g, int angle = 135) =>
        $"{angle}, #{g.A.R:X2}{g.A.G:X2}{g.A.B:X2}, #{g.B.R:X2}{g.B.G:X2}{g.B.B:X2}";

    // Bo góc là thuộc tính AntdUI (đã nhân Config.Dpi) → không nhân K
    public const int RadiusCard = 16;
    public const int RadiusControl = 8;

    // ---------- Chữ (point × FontK; AntdUI tự suy font tiêu đề từ Font.Size theo point nên KHÔNG dùng đơn vị pixel) ----------
    public const string FontFamily = "Segoe UI";
    public const string FontFamilySemibold = "Segoe UI Semibold";

    public static Font Body      = Pt(9.75f);
    public static Font BodySemi  = PtSemi(9.75f);
    public static Font Small     = Pt(9f);
    public static Font SmallSemi = PtSemi(9f);
    public static Font Subtitle  = PtSemi(12f);
    public static Font Title     = PtSemi(16f);
    public static Font Display   = PtSemi(22f);
    // thẻ số liệu (mẫu: tiêu đề thường ~0,10h, số đậm ~0,23h, chữ phụ semibold ~0,08h)
    public static Font CardTitle    = Pt(14f);      // thẻ cao ≥ 160 (mẫu: cap tiêu đề 0,075 h; số : tiêu đề = 1,8)
    public static Font CardTitleSm  = Pt(11f);      // thẻ cao 132–159
    public static Font CardNumber   = PtSemi(26f);  // mẫu: nét vừa, không Bold
    public static Font CardNumberSm = PtSemi(23f);

    /// <summary>
    /// Wordmark thanh tiêu đề: **Plus Jakarta Sans Bold** (Google Fonts, OFL, hỗ trợ tiếng Việt) — font geometric phổ biến nhất cho
    /// wordmark dashboard trên Dribbble, gần nhất với chữ logo của mẫu 27274985. File đóng gói ở Assets/fonts, nạp bằng PrivateFontCollection
    /// (GDI+ nên AntdUI vẽ được). Thiếu file thì Segoe UI Bold. (Bahnschrift đã thử: lỗi dấu tiếng Việt trên máy người dùng — bỏ.)
    /// </summary>
    public static Font Wordmark = BrandFont(15f);
    static System.Drawing.Text.PrivateFontCollection? brandFonts;
    public static Font BrandFont(float pt)
    {
        try
        {
            if (brandFonts == null)
            {
                var path = Path.Combine(AppContext.BaseDirectory, "Assets", "fonts", "PlusJakartaSans-Bold.ttf");
                if (File.Exists(path)) { brandFonts = new(); brandFonts.AddFontFile(path); }
            }
            if (brandFonts is { Families.Length: > 0 }) return new Font(brandFonts.Families[0], pt * FontK, FontStyle.Bold);
        }
        catch { }
        return new Font(FontFamily, pt * FontK, FontStyle.Bold);
    }

    /// <summary>Font thường, cỡ pt (ở 96 DPI) nhân FontK, đơn vị point.</summary>
    public static Font Pt(float pt) => new(FontFamily, pt * FontK);
    public static Font PtSemi(float pt)
    {
        try { return new Font(FontFamilySemibold, pt * FontK); }
        catch { return new Font(FontFamily, pt * FontK, FontStyle.Bold); }
    }
    /// <summary>Tên cũ: Semibold theo pt.</summary>
    public static Font Semibold(float pt) => PtSemi(pt);

    public static Color Hex(string hex) => ColorTranslator.FromHtml(hex);

    /// <summary>Gọi 1 lần trong Program.cs trước Application.Run.</summary>
    public static void Apply()
    {
        AntdUI.Config.Font = Body;
        AntdUI.Config.TextRenderingHighQuality = true;
        AntdUI.Config.ShowInWindow = true;
        AntdUI.Config.NoticeWindowOffsetXY = S(64);
        AntdUI.Style.SetPrimary(Primary);
        AntdUI.Style.SetError(Danger);
        AntdUI.Style.SetSuccess(Success);
        AntdUI.Style.Set(AntdUI.Colour.TextBase, TextPrimary);
        AntdUI.Style.Set(AntdUI.Colour.Text, TextPrimary);
        AntdUI.Style.Set(AntdUI.Colour.TextSecondary, TextSecondary);
        AntdUI.Style.Set(AntdUI.Colour.TextTertiary, TextMuted);
        AntdUI.Style.Set(AntdUI.Colour.TextQuaternary, IconFaint);
        AntdUI.Style.Set(AntdUI.Colour.BorderColor, Border);
        AntdUI.Style.Set(AntdUI.Colour.BorderSecondary, BorderSubtle);
        AntdUI.Style.Set(AntdUI.Colour.Split, Divider);
        AntdUI.Style.Set(AntdUI.Colour.BgLayout, Canvas);
        AntdUI.Style.Set(AntdUI.Colour.BgBase, Surface);
        AntdUI.Style.Set(AntdUI.Colour.BgContainer, Surface);
        AntdUI.Style.Set(AntdUI.Colour.Fill, Canvas);
        AntdUI.Style.Set(AntdUI.Colour.FillSecondary, Canvas);
        AntdUI.Style.Set(AntdUI.Colour.FillTertiary, Canvas);
        AntdUI.Style.Set(AntdUI.Colour.FillQuaternary, SurfaceHover);
        AntdUI.Style.Set(AntdUI.Colour.HoverBg, SurfaceHover);
        AntdUI.Style.Set(AntdUI.Colour.DefaultBorder, Border);
        AntdUI.Style.Set(AntdUI.Colour.DefaultColor, TextPrimary);
    }

    /// <summary>Avatar người dùng đăng nhập (DiceBear "notionists", CC0). Rơi về avatar chữ cái nếu thiếu file.</summary>
    public static Image UserAvatar(string name)
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Assets", "avatar-quangminh.png");
            if (File.Exists(path)) using (var fs = File.OpenRead(path)) return Image.FromStream(fs);
        }
        catch { }
        return AvatarFactory.Get(name, 160);
    }
}
