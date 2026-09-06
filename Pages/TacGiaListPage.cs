using System.ComponentModel;
using AntdUI;
using QuanLyTacGia.Data;
using QuanLyTacGia.Dialogs;
using QuanLyTacGia.Models;
using QuanLyTacGia.Panels;
using QuanLyTacGia.Ui;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Pages
{
    /// <summary>
    /// P2 Danh sách tác giả: header (tiêu đề, avatar group, tab giới tính, chip tháng, tìm kiếm, toggle bảng/thẻ, Thêm, ⋯) + bảng / lưới thẻ + phân trang.
    /// Bố cục tĩnh trong TacGiaListPage.Designer.cs; ở đây: dữ liệu, lọc, mở D1 / M1 / M2, ẩn cột theo bề rộng.
    /// </summary>
    public partial class TacGiaListPage : UserControl, IPageEntrance
    {
        readonly Avatar[] stack;
        bool monthOnly;
        List<TacGia> filtered = new();
        Form? drawer;
        TacGiaDetailPanel? detail;

        /// <summary>Editor đang mở trong modal (phục vụ ScreenshotHarness).</summary>
        public TacGiaEditControl? CurrentEditor { get; private set; }

        // ---- Hoạt cảnh xuất hiện (IPageEntrance): thẻ mới tạo giữ ẩn, MainForm trượt trang xong mới cho hiện dần ----
        public void PrepareEntrance() { }
        public void PlayEntrance() => cards.PlayAppear();

        // ---- Hook cho ScreenshotHarness ----
        public void ShowDetailAt(int i) { if (i < filtered.Count) ShowDetail(filtered[i]); }
        public void CloseDetail() => drawer?.Close();
        public void OpenAdd() => Edit(null);
        public void OpenEditAt(int i) { if (i < filtered.Count) Edit(filtered[i]); }
        public void OpenDeleteAt(int i) { if (i < filtered.Count) Delete(filtered[i]); }
        public void SetSearchForDemo(string text) { txtSearch.Text = text; debounce.Stop(); ApplyFilter(); }
        public void SetViewForDemo(bool cardMode) => segView.SelectIndex = cardMode ? 1 : 0;

        /// <summary>Bộ lọc nhanh từ thẻ số liệu P1: Tất cả / Nam / Nữ / Thêm trong tháng này (hiện chip gỡ được trên toolbar).</summary>
        public void ApplyPreset(ListPreset p)
        {
            txtSearch.Text = "";
            monthOnly = p == ListPreset.ThisMonth;
            segGioiTinh.SelectIndex = p switch { ListPreset.Nam => 1, ListPreset.Nu => 2, _ => 0 };
            ApplyFilter();
        }
        /// <summary>Mở menu ⋯ (Dropdown chỉ mở trong OnMouseClick nên PerformClick không đủ → gửi WM_LBUTTONDOWN/UP).</summary>
        public void OpenMoreMenuForDemo()
        {
            var lp = (IntPtr)((18 << 16) | 18);
            SendMessage(btnMore.Handle, 0x201, (IntPtr)1, lp);
            SendMessage(btnMore.Handle, 0x202, IntPtr.Zero, lp);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public TacGiaListPage()
        {
            InitializeComponent();
            DoubleBuffered = true;
            AppTheme.DoubleBuffer(header);
            stack = [stack1, stack2, stack3, stack4, stack5];
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime) return;   // đang mở trong Designer

            // tab giới tính + toggle bảng/thẻ (Items không đưa vào designer để chữ đếm cập nhật từ code)
            segGioiTinh.Items.Add(new SegmentedItem { Text = "Tất cả" });
            segGioiTinh.Items.Add(new SegmentedItem { Text = "Nam" });
            segGioiTinh.Items.Add(new SegmentedItem { Text = "Nữ" });
            segGioiTinh.SelectIndex = 0;
            segGioiTinh.SelectIndexChanged += (_, _) => ApplyFilter();
            segView.Items.Add(new SegmentedItem { IconSvg = "UnorderedListOutlined" });
            segView.Items.Add(new SegmentedItem { IconSvg = "AppstoreOutlined" });
            segView.SelectIndexChanged += (_, _) => SwitchView();
            chipMonth.Click += (_, _) => { monthOnly = false; ApplyFilter(); };

            // ô tìm kiếm + toggle neo mép phải đặt theo Resize (không dùng Anchor Right: WinForms tính sai khoảng neo khi DPI ≠ 100 %)
            header.Resize += (_, _) => { txtSearch.Left = header.Width - S(24 + 260); segView.Left = txtSearch.Left - S(12 + 96); };
            txtSearch.TextChanged += (_, _) => { debounce.Stop(); debounce.Start(); };
            debounce.Tick += (_, _) => { debounce.Stop(); ApplyFilter(); };

            btnAdd.Click += (_, _) => Edit(null);
            btnMore.Items.AddRange(new object[] { "Xuất Excel", "Làm mới" });
            btnMore.SelectedValueChanged += (_, e) =>
            {
                switch (e.Value as string)
                {
                    case "Làm mới": ResetFilter(); break;
                    case "Xuất Excel": Toast.Info(FindForm()!, $"Sẽ xuất {filtered.Count} tác giả ra Excel (chưa nối chức năng)"); break;
                }
            };

            // ---- Bảng ----
            table.EmptyImage = SvgExtend.SvgToBmp(SvgDb.Custom["InboxOutlined"], S(48), S(48), AppTheme.IconFaint);
            table.Columns = new ColumnCollection
            {
                new Column("Avatar", "") { Width = "56", Align = ColumnAlign.Center },
                new Column("HoTen", "Họ và tên") { Width = "200" },
                new Column("MaTacGia", "Mã") { Width = "80" },
                new Column("NgaySinh", "Ngày sinh") { Width = "100" },
                new Column("GioiTinh", "Giới tính") { Width = "92" },
                new Column("DienThoai", "Điện thoại") { Width = "136" },
                new Column("Email", "Email") { Ellipsis = true },
                new Column("NgayThem", "Ngày thêm") { Width = "112" },
                new Column("ThaoTac", "Thao tác") { Width = "132", Align = ColumnAlign.Right },
            };
            table.Columns[6].SetWidthFill();
            table.CellClick += (_, e) =>
            {
                if (e.Record is TacGiaRow r && e.Column?.Key != "ThaoTac") ShowDetail(r.Model);
            };
            table.CellDoubleClick += (_, e) =>
            {
                if (e.Record is TacGiaRow r && e.Column?.Key != "ThaoTac") Edit(r.Model);
            };
            table.CellButtonClick += (_, e) =>
            {
                if (e.Record is not TacGiaRow r) return;
                switch (e.Btn.Id)
                {
                    case "view": ShowDetail(r.Model); break;
                    case "edit": Edit(r.Model); break;
                    case "del": Delete(r.Model); break;
                }
            };
            resizeDebounce.Tick += (_, _) => { resizeDebounce.Stop(); FitColumns(); FitPageSize(); };
            table.SizeChanged += (_, _) => { resizeDebounce.Stop(); resizeDebounce.Start(); };

            pager.ShowTotalChanged += (_, e) =>
            {
                int start = e.Total == 0 ? 0 : (e.Current - 1) * e.PageSize + 1;
                int end = Math.Min(e.Current * e.PageSize, e.Total);
                return $"{start} - {end} trên {e.Total}";
            };
            pager.ValueChanged += (_, _) => RenderPage();

            cards.View = ShowDetail;
            cards.EditRequested = t => Edit(t);
            cards.DeleteRequested = Delete;

            segView.SelectIndex = 1;   // mặc định xem dạng thẻ (người dùng chọn 05/09/2026); bấm icon danh sách để về bảng
            SwitchView();
            ApplyFilter();
        }

        // ---------- Dữ liệu ----------

        void ResetFilter()
        {
            txtSearch.Text = "";
            monthOnly = false;
            segGioiTinh.SelectIndex = 0;
            ApplyFilter();
        }

        void ApplyFilter(bool resetPage = true)
        {
            var q = SampleData.RemoveDiacritics(txtSearch.Text.Trim()).ToLowerInvariant();
            string? gt = segGioiTinh.SelectIndex switch { 1 => "Nam", 2 => "Nữ", _ => null };
            var now = DateTime.Today;
            var matched = SampleData.TacGias
                .Where(t => !monthOnly || (t.NgayTao.Year == now.Year && t.NgayTao.Month == now.Month))
                .Where(t => q.Length == 0
                            || SampleData.RemoveDiacritics(t.HoTen).ToLowerInvariant().Contains(q)
                            || t.MaTacGia.ToLowerInvariant().Contains(q)
                            || t.DienThoai.Contains(q))
                .ToList();
            int nam = matched.Count(t => t.GioiTinh == "Nam"), nu = matched.Count - nam;
            chipMonth.Text = $"Thêm trong tháng {now.Month}/{now.Year}";
            chipMonth.Visible = monthOnly;
            segGioiTinh.Items[0].Text = $"Tất cả · {matched.Count}";
            segGioiTinh.Items[1].Text = $"Nam · {nam}";
            segGioiTinh.Items[2].Text = $"Nữ · {nu}";
            filtered = matched.Where(t => gt == null || t.GioiTinh == gt).ToList();

            // avatar group: 5 tác giả đầu của kết quả + "+N"
            for (int i = 0; i < 5; i++)
            {
                stack[i].Visible = i < filtered.Count;
                if (i < filtered.Count) stack[i].Image = filtered[i].Avatar ?? AvatarFactory.Get(filtered[i].HoTen, 64);
            }
            stackMore.Visible = filtered.Count > 5;
            stackMore.Text = $"+{filtered.Count - 5}";
            stackMore.Left = S(24 + Math.Min(5, filtered.Count) * 19 + 10);
            lblCount.Left = (stackMore.Visible ? stackMore.Right : S(24 + Math.Min(5, filtered.Count) * 19 + 4)) + S(10);
            lblCount.Text = filtered.Count == 0 ? "Không có tác giả phù hợp" : $"{filtered.Count} tác giả · {filtered.Count(t => t.GioiTinh == "Nam")} nam · {filtered.Count(t => t.GioiTinh == "Nữ")} nữ";

            pager.Total = filtered.Count;
            footer.Visible = filtered.Count > 0 && segView.SelectIndex == 0;
            if (resetPage) pager.Current = 1;
            RenderPage();
        }

        /// <summary>Bảng ↔ thẻ: chỉ một cái hiện; phân trang chỉ dùng cho bảng (thẻ cuộn toàn bộ kết quả).</summary>
        void SwitchView()
        {
            bool cardMode = segView.SelectIndex == 1;
            table.Visible = !cardMode;
            cards.Visible = cardMode;
            footer.Visible = !cardMode && filtered.Count > 0;
            RenderPage();
        }

        /// <summary>Ẩn dần cột phụ khi bảng hẹp để không sinh scrollbar ngang (sidebar 272 + min 1024).</summary>
        void FitColumns()
        {
            int w = table.Width;
            bool email = w >= S(1180);
            table.Columns[6].Visible = email;          // Email
            table.Columns[7].Visible = w >= S(1000);   // Ngày thêm
            table.Columns[5].Visible = w >= S(880);    // Điện thoại
            table.Columns[0].Visible = w >= S(780);    // Avatar
            // cột "fill" phải luôn có 1 cột đang hiện, không thì bảng co lại bên trái
            if (email) { table.Columns[1].Width = "200"; table.Columns[6].SetWidthFill(); }
            else table.Columns[1].SetWidthFill();
        }

        void FitPageSize()
        {
            int ps = Math.Max(5, (table.Height - S(44)) / S(52));   // AntdUI nhân RowHeight với Dpi → chiều cao thật = 52 × K
            if (ps == pager.PageSize) return;
            pager.PageSize = ps;
            RenderPage();
        }

        void RenderPage()
        {
            if (segView.SelectIndex == 1) { cards.SetData(filtered); return; }
            if (pager.PageSize <= 0) return;
            int maxPage = Math.Max(1, (int)Math.Ceiling(filtered.Count / (double)pager.PageSize));
            if (pager.Current > maxPage) { pager.Current = maxPage; return; }
            var rows = filtered.Skip((pager.Current - 1) * pager.PageSize).Take(pager.PageSize).Select(t => new TacGiaRow(t)).ToList();
            table.DataSource = rows;
        }

        void GoToRecord(TacGia t)
        {
            int idx = filtered.IndexOf(t);
            if (idx < 0) return;
            int page = idx / pager.PageSize + 1;
            if (pager.Current != page) pager.Current = page; else RenderPage();
            if (table.DataSource is List<TacGiaRow> rows)
            {
                var row = rows.FirstOrDefault(r => r.Model == t);
                if (row != null) table.SetSelected(row);
            }
        }

        // ---------- D1 Drawer ----------

        void ShowDetail(TacGia t)
        {
            if (drawer is { IsDisposed: false } && detail != null) { detail.Show(t); return; }
            detail = new TacGiaDetailPanel();
            detail.Show(t);
            detail.EditRequested += m => Edit(m);
            detail.DeleteRequested += m => Delete(m);
            detail.CloseRequested += () => drawer?.Close();
            drawer = Drawer.open(new Drawer.Config(FindForm()!, detail)
            {
                Align = TAlignMini.Right, Mask = false, Padding = 0,
                OnClose = () => { drawer = null; detail = null; },
            });
        }

        /// <summary>Nút chân modal theo spec: bo 8, chữ Semibold; nút Default có viền. Chiều cao đặt qua BtnHeight.</summary>
        static void StyleModalButton(string name, AntdUI.Button btn)
        {
            btn.Radius = 8;
            if (btn.Type == TTypeMini.Primary) btn.BackExtend = AppTheme.Extend(AppTheme.GradPrimary);
            // AntdUI xếp nút theo bề rộng tự tính; ép rộng hơn sẽ làm nút cuối tràn mép modal. Chỉ chỉnh cao qua BtnHeight.
            if (btn.Type == TTypeMini.Default)
            {
                btn.BorderWidth = 1;
                btn.DefaultBorderColor = AppTheme.Border;
                btn.ForeColor = AppTheme.TextPrimary;
            }
        }

        // ---------- M1 Modal Thêm / Sửa ----------

        void Edit(TacGia? t)
        {
            var form = FindForm()!;
            var ctrl = new TacGiaEditControl(t);
            CurrentEditor = ctrl;
            var cfg = new Modal.Config(form, t == null ? "Thêm tác giả" : $"Sửa tác giả  ·  {t.MaTacGia}", (object)ctrl)
            {
                Width = 560, OkText = "  Lưu  ", CancelText = "  Huỷ  ", CloseIcon = true, MaskClosable = false,
                Padding = new Size(24, 20),
                BtnHeight = 36, OkFont = AppTheme.BodySemi, CancelFont = AppTheme.BodySemi, OnButtonStyle = StyleModalButton,
                OnOk = _ => ctrl.TrySave(),
            };
            var dr = Modal.open(cfg);
            CurrentEditor = null;
            if (dr != DialogResult.OK || ctrl.Result == null) return;

            var saved = ctrl.Result;
            if (t == null) SampleData.TacGias.Add(saved);
            ApplyFilter(false);
            GoToRecord(saved);
            detail?.Show(saved);
            Toast.Success(form, $"Đã lưu tác giả {saved.HoTen}");
        }

        // ---------- M2 Modal xác nhận xoá ----------

        void Delete(TacGia t)
        {
            var form = FindForm()!;
            var cfg = new Modal.Config(form, "Xoá tác giả?", $"{t.HoTen} ({t.MaTacGia}) sẽ bị xoá vĩnh viễn.\nKhông thể hoàn tác.", TType.Error)
            {
                OkType = TTypeMini.Error, OkText = "  Xoá  ", CancelText = "  Huỷ  ", Width = 420, MaskClosable = false,
                BtnHeight = 36, OkFont = AppTheme.BodySemi, CancelFont = AppTheme.BodySemi, OnButtonStyle = StyleModalButton,
            };
            if (Modal.open(cfg) != DialogResult.OK) return;

            SampleData.TacGias.Remove(t);
            drawer?.Close();
            ApplyFilter(false);
            Toast.Success(form, $"Đã xoá {t.HoTen}");
        }
    }
}
