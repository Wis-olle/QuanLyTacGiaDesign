using System.ComponentModel;
using AntdUI;
using QuanLyTacGia.Data;
using QuanLyTacGia.Ui;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Pages
{
    /// <summary>
    /// P1 Tổng quan: thẻ chào + 4 thẻ số liệu (đếm số) + area chart theo tháng + donut giới tính + thanh ngang thập niên + gần nhất.
    /// Bố cục tĩnh nằm trong DashboardPage.Designer.cs (px 96 DPI, WinForms tự nhân theo DPI). File này chỉ giữ: dữ liệu, hoạ tiết thẻ,
    /// vị trí chữ trong thẻ theo chiều cao (Resize), chia chiều cao hai hàng biểu đồ (Relayout), motion.
    /// </summary>
    public partial class DashboardPage : UserControl, IPageEntrance
    {
        readonly AntdUI.Panel[] cards;
        readonly AntdUI.Label[] titles, numbers, subs;
        readonly Avatar[] iconBoxes;
        readonly GradientCard.Reveal[] reveal = new GradientCard.Reveal[4];
        readonly Action?[] countAnim = new Action?[4];
        int[] values = new int[4];
        Action<ListPreset> view = _ => { };
        Action onAdd = () => { };

        public DashboardPage()
        {
            InitializeComponent();
            DoubleBuffered = true;
            cards = [card1, card2, card3, card4];
            titles = [lblTitle1, lblTitle2, lblTitle3, lblTitle4];
            numbers = [lblNumber1, lblNumber2, lblNumber3, lblNumber4];
            subs = [lblSub1, lblSub2, lblSub3, lblSub4];
            iconBoxes = [iconBox1, iconBox2, iconBox3, iconBox4];
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime) return;   // đang mở trong Designer: chỉ hiện bố cục

            btnAdd.Click += (_, _) => onAdd();
            btnAll.Click += (_, _) => view(ListPreset.All);
            // nút neo phải đặt theo Resize (không dùng Anchor Right: WinForms tính sai khoảng neo khi DPI ≠ 100 %)
            welcome.Resize += (_, _) => btnAdd.Location = new Point(welcome.Width - S(24) - btnAdd.Width, S(34));
            recentCard.Resize += (_, _) => btnAll.Location = new Point(recentCard.Width - S(20) - btnAll.Width, S(17));
            SetupCards();
            SetupRecent();
            Resize += (_, _) => Relayout();
            Relayout();
        }

        public DashboardPage(string userName, string userRole, Image userAvatar, Action<ListPreset> view, Action onAdd) : this()
        {
            this.view = view;
            this.onAdd = onAdd;
            avatarUser.Image = userAvatar;
            lblHello.Text = $"Xin chào, {userName}";
            tagRole.Text = userRole;
        }

        /// <summary>4 thẻ số liệu theo mẫu 27274985: hoạ tiết nền (GradientCard), chữ đặt theo % chiều cao, ô icon xếp chồng, bấm mở P2 đúng bộ lọc.</summary>
        void SetupCards()
        {
            string[] icons = [Phosphor.Users, Phosphor.GenderMale, Phosphor.GenderFemale, Phosphor.CalendarPlus];
            GradientCard.Deco[] decos = [GradientCard.Deco.Rings, GradientCard.Deco.Contours, GradientCard.Deco.Waves, GradientCard.Deco.Blob];
            GradientCard.Tone[] tones = [AppTheme.CardOrange, AppTheme.CardViolet, AppTheme.CardGreen, AppTheme.CardBlue];
            for (int i = 0; i < 4; i++)
            {
                var card = cards[i]; var title = titles[i]; var num = numbers[i]; var sub = subs[i]; var iconBox = iconBoxes[i]; string svg = icons[i];
                int lastBox = 0;
                var iconRect = Rectangle.Empty;
                card.Resize += (_, _) =>
                {
                    int w = card.Width, h = card.Height;
                    // vị trí đo từ mẫu: tâm tiêu đề 0,22h · tâm số 0,56h · tâm chữ phụ 0,80h
                    bool narrow = GradientCard.Narrow(w);
                    bool tall = h >= S(160);
                    title.Font = narrow ? AppTheme.Body : tall ? AppTheme.CardTitle : AppTheme.CardTitleSm;
                    title.Top = (int)(h * 0.22f) - title.Height / 2;
                    num.Font = tall ? AppTheme.CardNumber : AppTheme.CardNumberSm;
                    num.Top = (int)(h * 0.56f) - num.Height / 2;
                    sub.Top = (int)(h * 0.80f) - sub.Height / 2;
                    var r = GradientCard.IconRect(w, h);
                    iconRect = r;
                    iconBox.Visible = GradientCard.ShowIcon(w);   // chỉ ẩn khi thẻ < 165
                    iconBox.Bounds = r;
                    if (lastBox != r.Width)
                    {
                        lastBox = r.Width;
                        var old = iconBox.Image;
                        iconBox.Image = Phosphor.Render(svg, r.Width * 2, (int)(r.Width * 1.1f), AppTheme.CardIcon);   // icon tím trên ô pastel như mẫu
                        old?.Dispose();
                    }
                    title.Width = Math.Max(S(60), iconBox.Visible ? r.X - GradientCard.Pad - S(6) : w - 2 * GradientCard.Pad);
                };
                // hover: hoạ tiết nền chuyển động một nhịp (GradientCard) + ô icon nhấc lên 4 px
                reveal[i] = GradientCard.ApplyStat(card, tones[i], decos[i], hv => { if (!iconRect.IsEmpty) iconBox.Top = iconRect.Y - (int)(S(4) * hv); });
                int idx = i;
                // mỗi thẻ mở P2 với đúng bộ lọc: 0 Tất cả · 1 Nam · 2 Nữ · 3 Thêm trong tháng (thứ tự = ListPreset)
                foreach (Control ch in card.Controls) ch.Click += (_, _) => view((ListPreset)idx);
                card.Click += (_, _) => view((ListPreset)idx);
            }
        }

        void SetupRecent()
        {
            recent.Columns = new ColumnCollection
            {
                new Column("Avatar", "") { Width = "56", Align = ColumnAlign.Center },
                new Column("HoTen", "Họ và tên") { Width = "200", Ellipsis = true, Style = new Table.CellStyleInfo { ForeColor = AppTheme.TextPrimary } },
                new Column("MaTacGia", "Mã") { Width = "90" },
                new Column("GioiTinh", "Giới tính") { Width = "100" },
                new Column("NgaySinh", "Ngày sinh") { Width = "110" },
                new Column("Email", "Email") { Ellipsis = true },
            };
            recent.Columns[5].SetWidthFill();
            recent.SizeChanged += (_, _) =>
            {
                bool email = recent.Width >= S(820);               // Email cần ≥ ~260 px mới không sinh thanh cuộn ngang
                recent.Columns[5].Visible = email;
                recent.Columns[4].Visible = recent.Width >= S(600);   // Ngày sinh
                recent.Columns[0].Visible = recent.Width >= S(480);   // Avatar
                if (email) { recent.Columns[1].Width = "200"; recent.Columns[5].SetWidthFill(); }
                else recent.Columns[1].SetWidthFill();              // luôn phải có 1 cột fill, không thì bảng co về trái
            };
            recent.CellClick += (_, _) => view(ListPreset.All);
        }

        /// <summary>
        /// Bố cục theo kích thước trang: thẻ số liệu cao theo bề rộng (mẫu 1,83:1, kẹp 132–176); hai hàng biểu đồ chia phần cao còn lại
        /// (A 45 %, B phần còn lại; tối thiểu 246 / 270). Các hàng nằm trong "body" rộng = ClientSize.Width để không bị thanh cuộn che.
        /// </summary>
        public void Relayout()
        {
            int w = ClientSize.Width;
            if (w <= 0) return;
            body.Width = w;
            int cardH = Math.Clamp((int)((w - S(48)) / 4 * 0.45), S(132), S(176));
            stats.Height = cardH + S(16);
            int avail = ClientSize.Height - welcomeHost.Height - stats.Height;
            int a = Math.Max(S(230 + 16), (int)(avail * 0.45));
            int b = Math.Max(S(270), avail - a);
            rowA.Height = a;
            rowB.Height = b;
            body.Height = welcomeHost.Height + stats.Height + a + b;
        }

        public void RefreshData()
        {
            var all = SampleData.TacGias;
            int nam = all.Count(t => t.GioiTinh == "Nam"), nu = all.Count - nam;
            var now = DateTime.Today;
            int thisMonth = all.Count(t => t.NgayTao.Year == now.Year && t.NgayTao.Month == now.Month);
            var prev = now.AddMonths(-1);
            int lastMonth = all.Count(t => t.NgayTao.Year == prev.Year && t.NgayTao.Month == prev.Month);
            values = [all.Count, nam, nu, thisMonth];
            int pNam = all.Count == 0 ? 0 : (int)Math.Round(nam * 100.0 / all.Count);
            int diff = thisMonth - lastMonth;
            // chữ phụ có icon nhỏ phía trước như mẫu ("⌥ 12% vs last month")
            (subs[0].PrefixSvg, subs[0].Text) = thisMonth > 0 ? ("RiseOutlined", $"+{thisMonth} trong tháng này") : ("MinusOutlined", "Chưa thêm mới tháng này");
            (subs[1].PrefixSvg, subs[1].Text) = ("PieChartOutlined", $"{pNam}% tổng số tác giả");
            (subs[2].PrefixSvg, subs[2].Text) = ("PieChartOutlined", $"{100 - pNam}% tổng số tác giả");
            (subs[3].PrefixSvg, subs[3].Text) = diff == 0 ? ("MinusOutlined", $"Bằng tháng trước ({lastMonth})")
                : (diff > 0 ? "RiseOutlined" : "FallOutlined", $"{(diff > 0 ? "+" : "−")}{Math.Abs(diff)} so với tháng trước");

            // Theo tháng: 12 tháng gần nhất
            var monthLabels = new string[12]; var monthCounts = new int[12];
            for (int i = 11; i >= 0; i--)
            {
                var m = now.AddMonths(-i);
                monthLabels[11 - i] = $"T{m.Month}";
                monthCounts[11 - i] = all.Count(t => t.NgayTao.Year == m.Year && t.NgayTao.Month == m.Month);
            }
            lblMonthsNote.Text = $"{monthCounts.Sum()} tác giả thêm trong 12 tháng gần nhất";
            chartMonths.SetData(monthLabels, monthCounts, "tác giả", AppTheme.CardViolet.Base);

            // Giới tính
            lblGenderNote.Text = $"Tỉ lệ nam / nữ trên {all.Count} tác giả";
            chartGender.SetData([("Nam", nam, AppTheme.CardBlue.Base), ("Nữ", nu, AppTheme.ChartPink)], all.Count.ToString(), "tác giả");

            // Thập niên sinh
            var decLabels = new List<string> { "<1900" }; var decCounts = new List<int> { all.Count(t => t.NgaySinh.Year < 1900) };
            for (int d = 1900; d <= 1990; d += 10)
            {
                decLabels.Add(d.ToString());
                decCounts.Add(all.Count(t => t.NgaySinh.Year >= d && t.NgaySinh.Year < d + 10));
            }
            chartDecades.SetData(decLabels.ToArray(), decCounts.ToArray(), "tác giả", AppTheme.CardBlue.Base, AppTheme.ChartBlueLight);


            recent.DataSource = all.OrderByDescending(t => t.NgayTao).ThenByDescending(t => t.MaTacGia).Take(5).Select(t => new TacGiaRow(t)).ToList();
            PrepareEntrance();
        }

        /// <summary>Trạng thái đầu của hoạt cảnh: thẻ phủ trắng, số 0, biểu đồ trống — MainForm chụp ảnh trang ở trạng thái này rồi trượt vào.</summary>
        public void PrepareEntrance()
        {
            for (int i = 0; i < 4; i++) { countAnim[i]?.Invoke(); reveal[i].Prepare?.Invoke(); numbers[i].Text = "0"; }
            chartMonths.Prepare(); chartGender.Prepare(); chartDecades.Prepare();
        }

        /// <summary>Sau khi trang trượt xong (tuần tự, không chồng lên cú trượt): thẻ i hiện dần sau i × 60 ms, số đếm lên sau đó 120 ms, biểu đồ vẽ dần 240 / 360 / 480.</summary>
        public void PlayEntrance()
        {
            for (int i = 0; i < 4; i++)
            {
                int d = i * Motion.Stagger, v = values[i], k = i; var lbl = numbers[i];
                reveal[i].Play?.Invoke(d);
                countAnim[i]?.Invoke();
                countAnim[i] = Motion.Delay(d + 120, () => countAnim[k] = Motion.CountUp(lbl, v));
            }
            chartMonths.Animate(240, 900); chartGender.Animate(360); chartDecades.Animate(480);   // area chart quét ngang 900 ms
        }
    }
}
