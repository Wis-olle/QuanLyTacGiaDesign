using AntdUI;

namespace QuanLyTacGia.Ui
{
    /// <summary>Sidebar: nhóm "Chính" / "Hệ thống", các <see cref="NavItem"/>, vạch chỉ báo trượt; dưới cùng là người dùng và Đăng xuất. Bố cục trong SidebarNav.Designer.cs.</summary>
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
        }

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
