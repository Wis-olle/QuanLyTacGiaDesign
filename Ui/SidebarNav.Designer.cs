#nullable disable
namespace QuanLyTacGia.Ui
{
    partial class SidebarNav
    {
        /// <summary>Biến designer bắt buộc.</summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Phương thức bắt buộc để Designer hỗ trợ — không sửa bằng tay, dùng Designer để chỉnh.
        /// </summary>
        private void InitializeComponent()
        {
            lblChinh = new AntdUI.Label();
            itemDashboard = new QuanLyTacGia.Ui.NavItem();
            itemTacGia = new QuanLyTacGia.Ui.NavItem();
            lblHeThong = new AntdUI.Label();
            itemSettings = new QuanLyTacGia.Ui.NavItem();
            indicator = new AntdUI.Panel();
            line = new System.Windows.Forms.Panel();
            avatarUser = new AntdUI.Avatar();
            lblName = new AntdUI.Label();
            lblRole = new AntdUI.Label();
            btnSignOut = new AntdUI.Button();
            SuspendLayout();
            //
            // lblChinh
            //
            lblChinh.BackColor = System.Drawing.Color.Transparent;
            lblChinh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            lblChinh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblChinh.Location = new System.Drawing.Point(20, 20);
            lblChinh.Name = "lblChinh";
            lblChinh.Size = new System.Drawing.Size(200, 22);
            lblChinh.TabIndex = 0;
            lblChinh.Text = "Chính";
            //
            // itemDashboard
            //
            itemDashboard.Caption = "Tổng quan";
            itemDashboard.IconSvg = "DashboardOutlined";
            itemDashboard.ItemId = "dashboard";
            itemDashboard.Location = new System.Drawing.Point(12, 50);
            itemDashboard.Name = "itemDashboard";
            itemDashboard.Size = new System.Drawing.Size(216, 44);
            itemDashboard.TabIndex = 1;
            //
            // itemTacGia
            //
            itemTacGia.Caption = "Tác giả";
            itemTacGia.IconSvg = "TeamOutlined";
            itemTacGia.ItemId = "tacgia";
            itemTacGia.Location = new System.Drawing.Point(12, 98);
            itemTacGia.Name = "itemTacGia";
            itemTacGia.Size = new System.Drawing.Size(216, 44);
            itemTacGia.TabIndex = 2;
            //
            // lblHeThong
            //
            lblHeThong.BackColor = System.Drawing.Color.Transparent;
            lblHeThong.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            lblHeThong.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblHeThong.Location = new System.Drawing.Point(20, 158);
            lblHeThong.Name = "lblHeThong";
            lblHeThong.Size = new System.Drawing.Size(200, 22);
            lblHeThong.TabIndex = 3;
            lblHeThong.Text = "Hệ thống";
            //
            // itemSettings
            //
            itemSettings.Caption = "Cài đặt";
            itemSettings.IconSvg = "SettingOutlined";
            itemSettings.ItemId = "settings";
            itemSettings.Location = new System.Drawing.Point(12, 188);
            itemSettings.Name = "itemSettings";
            itemSettings.Size = new System.Drawing.Size(216, 44);
            itemSettings.TabIndex = 4;
            //
            // indicator
            //
            indicator.Back = System.Drawing.Color.FromArgb(80, 71, 224);
            indicator.BorderWidth = 0F;
            indicator.Location = new System.Drawing.Point(0, 58);
            indicator.Name = "indicator";
            indicator.Radius = 2;
            indicator.Shadow = 0;
            indicator.Size = new System.Drawing.Size(4, 28);
            indicator.TabIndex = 5;
            indicator.Visible = false;
            //
            // line
            //
            line.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            line.Location = new System.Drawing.Point(20, 508);
            line.Name = "line";
            line.Size = new System.Drawing.Size(200, 1);
            line.TabIndex = 6;
            //
            // avatarUser
            //
            avatarUser.ImageFit = AntdUI.TFit.Cover;
            avatarUser.Location = new System.Drawing.Point(20, 524);
            avatarUser.Name = "avatarUser";
            avatarUser.Round = true;
            avatarUser.Size = new System.Drawing.Size(40, 40);
            avatarUser.TabIndex = 7;
            //
            // lblName
            //
            lblName.BackColor = System.Drawing.Color.Transparent;
            lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            lblName.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblName.Location = new System.Drawing.Point(70, 524);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(150, 20);
            lblName.TabIndex = 8;
            lblName.Text = "Quang Minh";
            //
            // lblRole
            //
            lblRole.BackColor = System.Drawing.Color.Transparent;
            lblRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblRole.ForeColor = System.Drawing.Color.FromArgb(111, 118, 137);
            lblRole.Location = new System.Drawing.Point(70, 544);
            lblRole.Name = "lblRole";
            lblRole.Size = new System.Drawing.Size(150, 18);
            lblRole.TabIndex = 9;
            lblRole.Text = "Quản trị viên";
            //
            // btnSignOut
            //
            btnSignOut.BorderWidth = 0F;
            btnSignOut.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnSignOut.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            btnSignOut.Ghost = true;
            btnSignOut.IconPosition = AntdUI.TAlignMini.Left;
            btnSignOut.IconSvg = "LogoutOutlined";
            btnSignOut.Location = new System.Drawing.Point(20, 580);
            btnSignOut.Name = "btnSignOut";
            btnSignOut.Radius = 8;
            btnSignOut.Size = new System.Drawing.Size(200, 40);
            btnSignOut.TabIndex = 10;
            btnSignOut.Text = "Đăng xuất";
            btnSignOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            btnSignOut.Type = AntdUI.TTypeMini.Default;
            //
            // SidebarNav
            //
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.White;
            Controls.Add(indicator);
            Controls.Add(lblChinh);
            Controls.Add(itemDashboard);
            Controls.Add(itemTacGia);
            Controls.Add(lblHeThong);
            Controls.Add(itemSettings);
            Controls.Add(line);
            Controls.Add(avatarUser);
            Controls.Add(lblName);
            Controls.Add(lblRole);
            Controls.Add(btnSignOut);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            Name = "SidebarNav";
            Size = new System.Drawing.Size(240, 640);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Label lblChinh;
        private QuanLyTacGia.Ui.NavItem itemDashboard;
        private QuanLyTacGia.Ui.NavItem itemTacGia;
        private AntdUI.Label lblHeThong;
        private QuanLyTacGia.Ui.NavItem itemSettings;
        private AntdUI.Panel indicator;
        private System.Windows.Forms.Panel line;
        private AntdUI.Avatar avatarUser;
        private AntdUI.Label lblName;
        private AntdUI.Label lblRole;
        private AntdUI.Button btnSignOut;
    }
}
