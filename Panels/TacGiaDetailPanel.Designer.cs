#nullable disable
namespace QuanLyTacGia.Panels
{
    partial class TacGiaDetailPanel
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
            header = new QuanLyTacGia.Panels.HeaderView();
            lblTen = new AntdUI.Label();
            chipMa = new AntdUI.Tag();
            lblNgaySinh = new AntdUI.Label();
            valNgaySinh = new AntdUI.Label();
            div1 = new System.Windows.Forms.Panel();
            lblGioiTinh = new AntdUI.Label();
            valGioiTinh = new AntdUI.Label();
            div2 = new System.Windows.Forms.Panel();
            lblDienThoai = new AntdUI.Label();
            valDienThoai = new AntdUI.Label();
            div3 = new System.Windows.Forms.Panel();
            lblEmail = new AntdUI.Label();
            valEmail = new AntdUI.Label();
            div4 = new System.Windows.Forms.Panel();
            lblDiaChi = new AntdUI.Label();
            valDiaChi = new AntdUI.Label();
            div5 = new System.Windows.Forms.Panel();
            btnEdit = new AntdUI.Button();
            btnDelete = new AntdUI.Button();
            border = new System.Windows.Forms.Panel();
            SuspendLayout();
            //
            // header — banner gradient + avatar + nút × (tự vẽ)
            //
            header.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            header.BackColor = System.Drawing.Color.White;
            header.Location = new System.Drawing.Point(0, 0);
            header.Name = "header";
            header.Size = new System.Drawing.Size(380, 164);
            header.TabIndex = 0;
            //
            // lblTen
            //
            lblTen.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblTen.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblTen.Location = new System.Drawing.Point(24, 176);
            lblTen.Name = "lblTen";
            lblTen.Size = new System.Drawing.Size(332, 26);
            lblTen.TabIndex = 1;
            lblTen.Text = "Họ và tên";
            lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // chipMa
            //
            chipMa.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            chipMa.BorderWidth = 0F;
            chipMa.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            chipMa.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            chipMa.Location = new System.Drawing.Point(142, 210);
            chipMa.Name = "chipMa";
            chipMa.Radius = 12;
            chipMa.Size = new System.Drawing.Size(96, 24);
            chipMa.TabIndex = 2;
            chipMa.Text = "TG0000";
            chipMa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            chipMa.Type = AntdUI.TTypeMini.Default;
            //
            // Ngày sinh
            //
            lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblNgaySinh.Location = new System.Drawing.Point(24, 260);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new System.Drawing.Size(104, 43);
            lblNgaySinh.TabIndex = 3;
            lblNgaySinh.Text = "Ngày sinh";
            lblNgaySinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            valNgaySinh.AutoEllipsis = true;
            valNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            valNgaySinh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            valNgaySinh.Location = new System.Drawing.Point(132, 260);
            valNgaySinh.Name = "valNgaySinh";
            valNgaySinh.Size = new System.Drawing.Size(224, 43);
            valNgaySinh.TabIndex = 4;
            valNgaySinh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            div1.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            div1.Location = new System.Drawing.Point(24, 303);
            div1.Name = "div1";
            div1.Size = new System.Drawing.Size(332, 1);
            div1.TabIndex = 5;
            //
            // Giới tính
            //
            lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblGioiTinh.Location = new System.Drawing.Point(24, 304);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new System.Drawing.Size(104, 43);
            lblGioiTinh.TabIndex = 6;
            lblGioiTinh.Text = "Giới tính";
            lblGioiTinh.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            valGioiTinh.AutoEllipsis = true;
            valGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            valGioiTinh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            valGioiTinh.Location = new System.Drawing.Point(132, 304);
            valGioiTinh.Name = "valGioiTinh";
            valGioiTinh.Size = new System.Drawing.Size(224, 43);
            valGioiTinh.TabIndex = 7;
            valGioiTinh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            div2.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            div2.Location = new System.Drawing.Point(24, 347);
            div2.Name = "div2";
            div2.Size = new System.Drawing.Size(332, 1);
            div2.TabIndex = 8;
            //
            // Điện thoại
            //
            lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDienThoai.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblDienThoai.Location = new System.Drawing.Point(24, 348);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new System.Drawing.Size(104, 43);
            lblDienThoai.TabIndex = 9;
            lblDienThoai.Text = "Điện thoại";
            lblDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            valDienThoai.AutoEllipsis = true;
            valDienThoai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            valDienThoai.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            valDienThoai.Location = new System.Drawing.Point(132, 348);
            valDienThoai.Name = "valDienThoai";
            valDienThoai.Size = new System.Drawing.Size(224, 43);
            valDienThoai.TabIndex = 10;
            valDienThoai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            div3.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            div3.Location = new System.Drawing.Point(24, 391);
            div3.Name = "div3";
            div3.Size = new System.Drawing.Size(332, 1);
            div3.TabIndex = 11;
            //
            // Email
            //
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblEmail.Location = new System.Drawing.Point(24, 392);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(104, 43);
            lblEmail.TabIndex = 12;
            lblEmail.Text = "Email";
            lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            valEmail.AutoEllipsis = true;
            valEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            valEmail.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            valEmail.Location = new System.Drawing.Point(132, 392);
            valEmail.Name = "valEmail";
            valEmail.Size = new System.Drawing.Size(224, 43);
            valEmail.TabIndex = 13;
            valEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            div4.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            div4.Location = new System.Drawing.Point(24, 435);
            div4.Name = "div4";
            div4.Size = new System.Drawing.Size(332, 1);
            div4.TabIndex = 14;
            //
            // Địa chỉ — nhãn trên, giá trị dưới 2 dòng
            //
            lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblDiaChi.Location = new System.Drawing.Point(24, 448);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new System.Drawing.Size(332, 18);
            lblDiaChi.TabIndex = 15;
            lblDiaChi.Text = "Địa chỉ";
            lblDiaChi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            valDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            valDiaChi.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            valDiaChi.Location = new System.Drawing.Point(24, 470);
            valDiaChi.Name = "valDiaChi";
            valDiaChi.Size = new System.Drawing.Size(332, 44);
            valDiaChi.TabIndex = 16;
            valDiaChi.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            valDiaChi.TextMultiLine = true;
            div5.BackColor = System.Drawing.Color.FromArgb(242, 244, 245);
            div5.Location = new System.Drawing.Point(24, 525);
            div5.Name = "div5";
            div5.Size = new System.Drawing.Size(332, 1);
            div5.TabIndex = 17;
            //
            // btnEdit
            //
            btnEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnEdit.BackExtend = "135, #166BB8, #5047E0";
            btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnEdit.IconSvg = "EditOutlined";
            btnEdit.Location = new System.Drawing.Point(24, 580);
            btnEdit.Name = "btnEdit";
            btnEdit.Radius = 8;
            btnEdit.Size = new System.Drawing.Size(160, 36);
            btnEdit.TabIndex = 18;
            btnEdit.Text = "Sửa";
            btnEdit.Type = AntdUI.TTypeMini.Primary;
            //
            // btnDelete
            //
            btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnDelete.BorderWidth = 1F;
            btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnDelete.Ghost = true;
            btnDelete.IconSvg = "DeleteOutlined";
            btnDelete.Location = new System.Drawing.Point(196, 580);
            btnDelete.Name = "btnDelete";
            btnDelete.Radius = 8;
            btnDelete.Size = new System.Drawing.Size(160, 36);
            btnDelete.TabIndex = 19;
            btnDelete.Text = "Xoá";
            btnDelete.Type = AntdUI.TTypeMini.Error;
            //
            // border — vạch 1 px mép trái drawer
            //
            border.BackColor = System.Drawing.Color.FromArgb(238, 240, 244);
            border.Dock = System.Windows.Forms.DockStyle.Left;
            border.Location = new System.Drawing.Point(0, 0);
            border.Name = "border";
            border.Size = new System.Drawing.Size(1, 640);
            border.TabIndex = 20;
            //
            // TacGiaDetailPanel
            //
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.White;
            Controls.Add(header);
            Controls.Add(lblTen);
            Controls.Add(chipMa);
            Controls.Add(lblNgaySinh);
            Controls.Add(valNgaySinh);
            Controls.Add(div1);
            Controls.Add(lblGioiTinh);
            Controls.Add(valGioiTinh);
            Controls.Add(div2);
            Controls.Add(lblDienThoai);
            Controls.Add(valDienThoai);
            Controls.Add(div3);
            Controls.Add(lblEmail);
            Controls.Add(valEmail);
            Controls.Add(div4);
            Controls.Add(lblDiaChi);
            Controls.Add(valDiaChi);
            Controls.Add(div5);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(border);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            MinimumSize = new System.Drawing.Size(380, 520);
            Name = "TacGiaDetailPanel";
            Size = new System.Drawing.Size(380, 640);
            ResumeLayout(false);
        }

        #endregion

        private QuanLyTacGia.Panels.HeaderView header;
        private AntdUI.Label lblTen;
        private AntdUI.Tag chipMa;
        private AntdUI.Label lblNgaySinh;
        private AntdUI.Label valNgaySinh;
        private System.Windows.Forms.Panel div1;
        private AntdUI.Label lblGioiTinh;
        private AntdUI.Label valGioiTinh;
        private System.Windows.Forms.Panel div2;
        private AntdUI.Label lblDienThoai;
        private AntdUI.Label valDienThoai;
        private System.Windows.Forms.Panel div3;
        private AntdUI.Label lblEmail;
        private AntdUI.Label valEmail;
        private System.Windows.Forms.Panel div4;
        private AntdUI.Label lblDiaChi;
        private AntdUI.Label valDiaChi;
        private System.Windows.Forms.Panel div5;
        private AntdUI.Button btnEdit;
        private AntdUI.Button btnDelete;
        private System.Windows.Forms.Panel border;
    }
}
