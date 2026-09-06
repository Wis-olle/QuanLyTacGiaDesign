using AntdUI;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Ui
{
    /// <summary>
    /// Sidebar: nhóm "Chính" / "Hệ thống", các <see cref="NavItem"/>, vạch chỉ báo trượt; dưới cùng là người dùng và Đăng xuất. Bố cục trong SidebarNav.Designer.cs.
    /// Khối đáy KHÔNG dùng Anchor Bottom (WinForms tính sai khoảng neo khi DPI ≠ 100 %, người dùng gặp trên màn 8.8 inch: avatar trôi lên sát "Cài đặt")
    /// mà đặt Y theo Height thật trong <see cref="PlaceFooter"/>; trong Designer 5 control này nằm ở vị trí thiết kế cho cao 640.
    /// </summary>
    public partial class SidebarNav : UserControl
    {
        public event Action<string>? Selected;
        Action? slide;

        public SidebarNav()
        {
            InitializeComponent();
            foreach (var it in Controls.OfType<NavItem>()) it.Clicked += () => Select(it.ItemId);
            btnSignOut.TabStop = false;
            btnSignOut.Click += (_, _) => Application.Exit();
            PlaceFooter();
        }

        /// <summary>Khối đáy cách mép dưới: vạch 132 · avatar và tên 116 · vai trò 96 · Đăng xuất 60 (px 96 DPI × K; K = DPI/96 do MainForm đặt, bằng hệ số WinForms đã nhân cho control).</summary>
        void PlaceFooter()
        {
            int h = Height;
            line.Top = h - S(132);
            avatarUser.Top = h - S(116);
            lblName.Top = h - S(116);
            lblRole.Top = h - S(96);
            btnSignOut.Top = h - S(60);
        }

        protected override void OnResize(EventArgs e) { base.OnResize(e); if (line != null) PlaceFooter(); }

        public void SetUser(string name, string role, Image avatar)
        {
            lblName.Text = name;
            lblRole.Text = role;
            avatarUser.Image = avatar;
        }

        public void Select(string id)
        {
            NavItem? target = null;
            foreach (var it in Controls.OfType<NavItem>()) { it.Active = it.ItemId == id; if (it.Active) target = it; }
            if (target != null)
            {
                int to = target.Top + (target.Height - indicator.Height) / 2;
                if (!indicator.Visible) { indicator.Top = to; indicator.Visible = true; }
                else { int from = indicator.Top; slide?.Invoke(); slide = Motion.Run(Motion.Base, p => indicator.Top = from + (int)((to - from) * p)); }
            }
            Selected?.Invoke(id);
        }
    }
}
