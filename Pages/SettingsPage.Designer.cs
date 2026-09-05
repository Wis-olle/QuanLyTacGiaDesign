#nullable disable
namespace QuanLyTacGia.Pages
{
    partial class SettingsPage
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
            card = new AntdUI.Panel();
            lblTitle = new AntdUI.Label();
            lblFolder = new AntdUI.Label();
            txtFolder = new AntdUI.Input();
            lblDb = new AntdUI.Label();
            txtDb = new AntdUI.Input();
            btnSave = new AntdUI.Button();
            card.SuspendLayout();
            SuspendLayout();
            //
            // card
            //
            card.Back = System.Drawing.Color.White;
            card.BackColor = System.Drawing.Color.White;
            card.BorderWidth = 0F;
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblFolder);
            card.Controls.Add(txtFolder);
            card.Controls.Add(lblDb);
            card.Controls.Add(txtDb);
            card.Controls.Add(btnSave);
            card.Dock = System.Windows.Forms.DockStyle.Top;
            card.Location = new System.Drawing.Point(0, 0);
            card.Name = "card";
            card.Radius = 16;
            card.Shadow = 0;
            card.Size = new System.Drawing.Size(1200, 276);
            card.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblTitle.Location = new System.Drawing.Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(300, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cài đặt chung";
            //
            // lblFolder
            //
            lblFolder.BackColor = System.Drawing.Color.Transparent;
            lblFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblFolder.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblFolder.Location = new System.Drawing.Point(24, 64);
            lblFolder.Name = "lblFolder";
            lblFolder.Size = new System.Drawing.Size(480, 18);
            lblFolder.TabIndex = 1;
            lblFolder.Text = "Thư mục xuất file";
            //
            // txtFolder
            //
            txtFolder.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtFolder.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtFolder.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtFolder.Location = new System.Drawing.Point(24, 88);
            txtFolder.Name = "txtFolder";
            txtFolder.Radius = 8;
            txtFolder.Size = new System.Drawing.Size(480, 36);
            txtFolder.TabIndex = 2;
            //
            // lblDb
            //
            lblDb.BackColor = System.Drawing.Color.Transparent;
            lblDb.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDb.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblDb.Location = new System.Drawing.Point(24, 140);
            lblDb.Name = "lblDb";
            lblDb.Size = new System.Drawing.Size(480, 18);
            lblDb.TabIndex = 3;
            lblDb.Text = "Đường dẫn cơ sở dữ liệu";
            //
            // txtDb
            //
            txtDb.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            txtDb.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtDb.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtDb.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            txtDb.Location = new System.Drawing.Point(24, 164);
            txtDb.Name = "txtDb";
            txtDb.Radius = 8;
            txtDb.ReadOnly = true;
            txtDb.Size = new System.Drawing.Size(480, 36);
            txtDb.TabIndex = 4;
            txtDb.Text = "(chưa kết nối, dữ liệu mẫu in-memory)";
            //
            // btnSave
            //
            btnSave.BackExtend = "135, #166BB8, #5047E0";
            btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnSave.Location = new System.Drawing.Point(24, 216);
            btnSave.Name = "btnSave";
            btnSave.Radius = 8;
            btnSave.Size = new System.Drawing.Size(132, 36);
            btnSave.TabIndex = 5;
            btnSave.Text = "Lưu cài đặt";
            btnSave.Type = AntdUI.TTypeMini.Primary;
            //
            // SettingsPage
            //
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            Controls.Add(card);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            Name = "SettingsPage";
            Size = new System.Drawing.Size(1200, 760);
            card.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel card;
        private AntdUI.Label lblTitle;
        private AntdUI.Label lblFolder;
        private AntdUI.Input txtFolder;
        private AntdUI.Label lblDb;
        private AntdUI.Input txtDb;
        private AntdUI.Button btnSave;
    }
}
