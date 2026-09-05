using AntdUI;
using QuanLyTacGia.Pages;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia
{
    /// <summary>
    /// SHELL: thanh tiêu đề (logo + tên app) · sidebar · vùng nội dung. Bố cục nằm trong MainForm.Designer.cs — mở bằng Designer (Shift+F7) để kéo thả.
    /// DPI: WinForms AutoScaleMode.Dpi tự nhân bố cục designer theo DPI màn (cả khi kéo sang màn khác); AntdUI AutoHandDpi tắt để không nhân hai lần.
    /// AppTheme.K (DPI/96) chỉ dùng cho phần vẽ tay và tính toán lúc chạy. Xem KIEN_TRUC_MAN_HINH.md.
    /// </summary>
    public partial class MainForm : Window
    {
        const string UserName = "Quang Minh";
        const string UserRole = "Quản trị viên";

        readonly Image userAvatar;
        DashboardPage? dashboard;
        TacGiaListPage? tacGiaList;
        SettingsPage? settings;
        string currentId = "dashboard";

        public TacGiaListPage? ListPage => tacGiaList;

        public MainForm()
        {
            AppTheme.SetScale(DeviceDpi / 96f, SystemDpi());   // trước InitializeComponent để font / icon vẽ tay đúng K
            InitializeComponent();
            AutoHandDpi = false;   // WinForms (AutoScaleMode.Dpi) đã co giãn; không để AntdUI Control.Scale lần nữa

            userAvatar = AppTheme.UserAvatar(UserName);
            Icon = Brand.WindowIcon();
            ApplyBrand();
            nav.SetUser(UserName, UserRole, userAvatar);
            nav.Selected += Navigate;

            Load += (_, _) => nav.Select("dashboard");
            ScreenshotHarness.TryAttach(this);
        }

        /// <summary>Wordmark Plus Jakarta Sans (font nạp từ file) và logo mark vẽ tay — designer không giữ được nên đặt ở đây.</summary>
        void ApplyBrand()
        {
            titleBar.Font = AppTheme.Wordmark;
            titleBar.Icon = Brand.Mark(AppTheme.S(68));   // vẽ 2× rồi PageHeader thu về IconRatio × cao chữ cho nét (≈ 34 px)
        }

        protected override void OnDpiChanged(DpiChangedEventArgs e)
        {
            base.OnDpiChanged(e);   // WinForms tự nhân bố cục designer theo DPI mới
            AppTheme.SetScale(e.DeviceDpiNew / 96f, SystemDpi());
            ApplyBrand();
            dashboard?.Relayout();
            Invalidate(true);      // phần vẽ tay vẽ lại theo K mới
        }

        /// <summary>DPI hệ thống (màn chính) — GDI+ đổi point → pixel theo DPI này, dùng để tính AppTheme.FontK.</summary>
        static float SystemDpi() { using var g = Graphics.FromHwnd(IntPtr.Zero); return g.DpiX; }

        public void GoTo(string id) => nav.Select(id);

        void Navigate(string id)
        {
            currentId = id;
            Control page = id switch
            {
                "tacgia" => tacGiaList ??= new TacGiaListPage(),
                "settings" => settings ??= new SettingsPage(),
                _ => Dashboard(),
            };
            if (content.Controls.Count == 1 && content.Controls[0] == page) return;
            content.SuspendLayout();
            content.Controls.Clear();
            page.Dock = DockStyle.Fill;
            content.Controls.Add(page);
            content.ResumeLayout();
            Motion.SlideIn(content, new Padding(0, AppTheme.S(16), AppTheme.S(16), AppTheme.S(16)));
        }

        DashboardPage Dashboard()
        {
            dashboard ??= new DashboardPage(UserName, UserRole, userAvatar,
                view: p => { GoTo("tacgia"); tacGiaList?.ApplyPreset(p); },
                onAdd: () => { GoTo("tacgia"); BeginInvoke(() => tacGiaList?.OpenAdd()); });
            dashboard.RefreshData();
            return dashboard;
        }

        private void nav_Load(object sender, EventArgs e)
        {

        }
    }
}
