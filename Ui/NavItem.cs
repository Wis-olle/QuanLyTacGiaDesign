using System.ComponentModel;
using AntdUI;

namespace QuanLyTacGia.Ui
{
    /// <summary>
    /// 1 mục sidebar: ô icon 32 (xanh khi chọn) + nhãn; hover đổi nền cả hàng.
    /// Kéo từ Toolbox (nhóm "QuanLyTacGiaDesign Components") vào SidebarNav rồi đặt ItemId / Caption / IconSvg trong Properties.
    /// </summary>
    [ToolboxItem(true)]
    public class NavItem : AntdUI.Panel
    {
        public event Action? Clicked;
        readonly Avatar icon;
        readonly AntdUI.Label label;
        string caption = "", iconSvg = "";
        bool active;
        Action? anim;

        public NavItem()
        {
            Radius = AppTheme.RadiusControl; Shadow = 0; BorderWidth = 0; Back = Color.White; Cursor = Cursors.Hand;
            Size = new Size(216, 44);   // px 96 DPI — WinForms tự nhân theo DPI cùng với SidebarNav
            icon = new Avatar { Size = new Size(32, 32), Location = new Point(8, 6), Round = false, Radius = 8, Cursor = Cursors.Hand };
            label = new AntdUI.Label { Location = new Point(52, 6), Size = new Size(156, 32), TextAlign = ContentAlignment.MiddleLeft, BackColor = Color.Transparent, Cursor = Cursors.Hand };
            Controls.Add(icon); Controls.Add(label);
            foreach (Control c in new Control[] { this, icon, label })
            {
                c.Click += (_, _) => Clicked?.Invoke();
                c.MouseEnter += (_, _) => { if (!active) Back = AppTheme.SurfaceHover; };
                c.MouseLeave += (_, _) => { if (!active && !ClientRectangle.Contains(PointToClient(Cursor.Position))) Back = Color.White; };
            }
            Active = false;
        }

        [Category("QLTG"), Description("Khoá trang mở khi bấm: dashboard · tacgia · settings"), DefaultValue("")]
        public string ItemId { get; set; } = "";

        [Category("QLTG"), Description("Chữ hiện trên mục"), DefaultValue("")]
        public string Caption { get => caption; set { caption = value; label.Text = value; } }

        [Category("QLTG"), Description("Tên icon AntdUI, ví dụ DashboardOutlined"), DefaultValue("")]
        public string IconSvg { get => iconSvg; set { iconSvg = value; icon.ImageSvg = value; } }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Active
        {
            get => active;
            set
            {
                active = value;
                Back = Color.White;
                label.Font = active ? AppTheme.BodySemi : AppTheme.Body;
                label.ForeColor = active ? AppTheme.TextPrimary : AppTheme.TextSecondary;
                // ô icon đổi màu mượt (160 ms) thay vì nhảy
                Color fromBg = icon.BackColor, toBg = active ? AppTheme.NavActive : Color.White, fromFg = icon.ForeColor, toFg = active ? Color.White : AppTheme.Icon;
                anim?.Invoke();
                anim = Motion.Run(Motion.Fast, p => { icon.BackColor = Motion.Lerp(fromBg, toBg, p); icon.ForeColor = Motion.Lerp(fromFg, toFg, p); });
            }
        }
    }
}
