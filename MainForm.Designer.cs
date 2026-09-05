#nullable disable
namespace QuanLyTacGia
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Phương thức bắt buộc để Designer hỗ trợ — không sửa bằng tay, dùng Designer để chỉnh.
        /// </summary>
        private void InitializeComponent()
        {
            titleBar = new AntdUI.PageHeader();
            sidebarHost = new Panel();
            sidebarCard = new AntdUI.Panel();
            nav = new QuanLyTacGia.Ui.SidebarNav();
            content = new Panel();
            sidebarHost.SuspendLayout();
            sidebarCard.SuspendLayout();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.White;
            titleBar.DividerColor = Color.FromArgb(238, 240, 244);
            titleBar.DividerShow = true;
            titleBar.Dock = DockStyle.Top;
            titleBar.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            titleBar.ForeColor = Color.FromArgb(13, 20, 38);
            titleBar.Gap = 12;
            titleBar.IconRatio = 1.3F;
            titleBar.Location = new Point(0, 0);
            titleBar.Name = "titleBar";
            titleBar.ShowButton = true;
            titleBar.ShowIcon = true;
            titleBar.Size = new Size(1280, 56);
            titleBar.TabIndex = 0;
            titleBar.Text = "Quản lý tác giả";
            titleBar.UseSystemStyleColor = true;
            // 
            // sidebarHost
            // 
            sidebarHost.BackColor = Color.FromArgb(243, 245, 249);
            sidebarHost.Controls.Add(sidebarCard);
            sidebarHost.Dock = DockStyle.Left;
            sidebarHost.Location = new Point(0, 56);
            sidebarHost.Name = "sidebarHost";
            sidebarHost.Padding = new Padding(16);
            sidebarHost.Size = new Size(272, 744);
            sidebarHost.TabIndex = 1;
            // 
            // sidebarCard
            // 
            sidebarCard.Back = Color.White;
            sidebarCard.BackColor = Color.White;
            sidebarCard.Controls.Add(nav);
            sidebarCard.Dock = DockStyle.Fill;
            sidebarCard.Location = new Point(16, 16);
            sidebarCard.Name = "sidebarCard";
            sidebarCard.Radius = 16;
            sidebarCard.Size = new Size(240, 712);
            sidebarCard.TabIndex = 0;
            // 
            // nav
            // 
            nav.BackColor = Color.White;
            nav.Dock = DockStyle.Fill;
            nav.Font = new Font("Segoe UI", 9.75F);
            nav.Location = new Point(0, 0);
            nav.Name = "nav";
            nav.Size = new Size(240, 712);
            nav.TabIndex = 0;
            nav.Load += nav_Load;
            // 
            // content
            // 
            content.BackColor = Color.FromArgb(243, 245, 249);
            content.Dock = DockStyle.Fill;
            content.Location = new Point(272, 56);
            content.Name = "content";
            content.Padding = new Padding(0, 16, 16, 16);
            content.Size = new Size(1008, 744);
            content.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(243, 245, 249);
            ClientSize = new Size(1280, 800);
            Controls.Add(content);
            Controls.Add(sidebarHost);
            Controls.Add(titleBar);
            Font = new Font("Segoe UI", 9.75F);
            MinimumSize = new Size(1024, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tác giả";
            WindowState = FormWindowState.Maximized;
            sidebarHost.ResumeLayout(false);
            sidebarCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.PageHeader titleBar;
        private System.Windows.Forms.Panel sidebarHost;
        private AntdUI.Panel sidebarCard;
        private QuanLyTacGia.Ui.SidebarNav nav;
        private System.Windows.Forms.Panel content;
    }
}
