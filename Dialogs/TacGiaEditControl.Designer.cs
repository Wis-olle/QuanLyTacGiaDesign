#nullable disable
namespace QuanLyTacGia.Dialogs
{
    partial class TacGiaEditControl
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
        /// Lưới: cột trái x = 0, cột phải x = 264, mỗi cột rộng 248; hàng cao 84 (nhãn 18 · ô 36 · lỗi 18).
        /// </summary>
        private void InitializeComponent()
        {
            avatar = new AntdUI.Avatar();
            btnAnh = new AntdUI.Button();
            btnMau = new AntdUI.Button();
            lblMa = new AntdUI.Label();
            txtMa = new AntdUI.Input();
            errMa = new AntdUI.Label();
            lblHoTen = new AntdUI.Label();
            txtHoTen = new AntdUI.Input();
            errHoTen = new AntdUI.Label();
            lblNgaySinh = new AntdUI.Label();
            dtpNgaySinh = new AntdUI.DatePicker();
            errNgaySinh = new AntdUI.Label();
            lblGioiTinh = new AntdUI.Label();
            cboGioiTinh = new AntdUI.Select();
            errGioiTinh = new AntdUI.Label();
            lblDienThoai = new AntdUI.Label();
            txtDienThoai = new AntdUI.Input();
            errDienThoai = new AntdUI.Label();
            lblEmail = new AntdUI.Label();
            txtEmail = new AntdUI.Input();
            errEmail = new AntdUI.Label();
            lblDiaChi = new AntdUI.Label();
            txtDiaChi = new AntdUI.Input();
            SuspendLayout();
            //
            // avatar + 2 nút Tải ảnh / Chọn mẫu (cột trái hàng 1)
            //
            avatar.ImageFit = AntdUI.TFit.Cover;
            avatar.Location = new System.Drawing.Point(92, 36);
            avatar.Name = "avatar";
            avatar.Round = true;
            avatar.Size = new System.Drawing.Size(64, 64);
            avatar.TabIndex = 0;
            btnAnh.BorderWidth = 1F;
            btnAnh.DefaultBorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            btnAnh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnAnh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            btnAnh.IconSvg = "UploadOutlined";
            btnAnh.Location = new System.Drawing.Point(4, 116);
            btnAnh.Name = "btnAnh";
            btnAnh.Radius = 8;
            btnAnh.Size = new System.Drawing.Size(116, 36);
            btnAnh.TabIndex = 1;
            btnAnh.Text = "Tải ảnh";
            btnAnh.Type = AntdUI.TTypeMini.Default;
            btnMau.BorderWidth = 1F;
            btnMau.DefaultBorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            btnMau.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            btnMau.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            btnMau.IconSvg = "SmileOutlined";
            btnMau.Location = new System.Drawing.Point(128, 116);
            btnMau.Name = "btnMau";
            btnMau.Radius = 8;
            btnMau.Size = new System.Drawing.Size(116, 36);
            btnMau.TabIndex = 2;
            btnMau.Text = "Chọn mẫu";
            btnMau.Type = AntdUI.TTypeMini.Default;
            //
            // Mã tác giả *
            //
            lblMa.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblMa.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblMa.Location = new System.Drawing.Point(264, 12);
            lblMa.Name = "lblMa";
            lblMa.Size = new System.Drawing.Size(248, 18);
            lblMa.Suffix = " *";
            lblMa.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblMa.TabIndex = 3;
            lblMa.Text = "Mã tác giả";
            txtMa.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtMa.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtMa.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtMa.Location = new System.Drawing.Point(264, 36);
            txtMa.Name = "txtMa";
            txtMa.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtMa.Radius = 8;
            txtMa.Size = new System.Drawing.Size(248, 36);
            txtMa.TabIndex = 4;
            txtMa.WaveSize = 0;
            errMa.Font = new System.Drawing.Font("Segoe UI", 9F);
            errMa.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errMa.Location = new System.Drawing.Point(264, 74);
            errMa.Name = "errMa";
            errMa.Size = new System.Drawing.Size(248, 18);
            errMa.TabIndex = 5;
            //
            // Họ và tên *
            //
            lblHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblHoTen.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblHoTen.Location = new System.Drawing.Point(264, 96);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new System.Drawing.Size(248, 18);
            lblHoTen.Suffix = " *";
            lblHoTen.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblHoTen.TabIndex = 6;
            lblHoTen.Text = "Họ và tên";
            txtHoTen.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtHoTen.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtHoTen.Location = new System.Drawing.Point(264, 120);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtHoTen.Radius = 8;
            txtHoTen.Size = new System.Drawing.Size(248, 36);
            txtHoTen.TabIndex = 7;
            txtHoTen.WaveSize = 0;
            errHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            errHoTen.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errHoTen.Location = new System.Drawing.Point(264, 158);
            errHoTen.Name = "errHoTen";
            errHoTen.Size = new System.Drawing.Size(248, 18);
            errHoTen.TabIndex = 8;
            //
            // Ngày sinh *
            //
            lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblNgaySinh.Location = new System.Drawing.Point(0, 180);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new System.Drawing.Size(248, 18);
            lblNgaySinh.Suffix = " *";
            lblNgaySinh.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblNgaySinh.TabIndex = 9;
            lblNgaySinh.Text = "Ngày sinh";
            dtpNgaySinh.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dtpNgaySinh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            dtpNgaySinh.Format = "dd/MM/yyyy";
            dtpNgaySinh.Location = new System.Drawing.Point(0, 204);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            dtpNgaySinh.PlaceholderText = "dd/mm/yyyy";
            dtpNgaySinh.Radius = 8;
            dtpNgaySinh.Size = new System.Drawing.Size(248, 36);
            dtpNgaySinh.TabIndex = 10;
            dtpNgaySinh.WaveSize = 0;
            errNgaySinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            errNgaySinh.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errNgaySinh.Location = new System.Drawing.Point(0, 242);
            errNgaySinh.Name = "errNgaySinh";
            errNgaySinh.Size = new System.Drawing.Size(248, 18);
            errNgaySinh.TabIndex = 11;
            //
            // Giới tính *
            //
            lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblGioiTinh.Location = new System.Drawing.Point(264, 180);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new System.Drawing.Size(248, 18);
            lblGioiTinh.Suffix = " *";
            lblGioiTinh.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblGioiTinh.TabIndex = 12;
            lblGioiTinh.Text = "Giới tính";
            cboGioiTinh.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            cboGioiTinh.DropDownRadius = 8;
            cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            cboGioiTinh.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            cboGioiTinh.ListAutoWidth = true;
            cboGioiTinh.Location = new System.Drawing.Point(264, 204);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            cboGioiTinh.PlaceholderText = "Chọn giới tính";
            cboGioiTinh.Radius = 8;
            cboGioiTinh.Size = new System.Drawing.Size(248, 36);
            cboGioiTinh.TabIndex = 13;
            cboGioiTinh.WaveSize = 0;
            errGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9F);
            errGioiTinh.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errGioiTinh.Location = new System.Drawing.Point(264, 242);
            errGioiTinh.Name = "errGioiTinh";
            errGioiTinh.Size = new System.Drawing.Size(248, 18);
            errGioiTinh.TabIndex = 14;
            //
            // Điện thoại *
            //
            lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDienThoai.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblDienThoai.Location = new System.Drawing.Point(0, 264);
            lblDienThoai.Name = "lblDienThoai";
            lblDienThoai.Size = new System.Drawing.Size(248, 18);
            lblDienThoai.Suffix = " *";
            lblDienThoai.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblDienThoai.TabIndex = 15;
            lblDienThoai.Text = "Điện thoại";
            txtDienThoai.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtDienThoai.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtDienThoai.Location = new System.Drawing.Point(0, 288);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtDienThoai.Radius = 8;
            txtDienThoai.Size = new System.Drawing.Size(248, 36);
            txtDienThoai.TabIndex = 16;
            txtDienThoai.WaveSize = 0;
            errDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            errDienThoai.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errDienThoai.Location = new System.Drawing.Point(0, 326);
            errDienThoai.Name = "errDienThoai";
            errDienThoai.Size = new System.Drawing.Size(248, 18);
            errDienThoai.TabIndex = 17;
            //
            // Email *
            //
            lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblEmail.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblEmail.Location = new System.Drawing.Point(264, 264);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new System.Drawing.Size(248, 18);
            lblEmail.Suffix = " *";
            lblEmail.SuffixColor = System.Drawing.Color.FromArgb(220, 38, 38);
            lblEmail.TabIndex = 18;
            lblEmail.Text = "Email";
            txtEmail.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtEmail.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtEmail.Location = new System.Drawing.Point(264, 288);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtEmail.Radius = 8;
            txtEmail.Size = new System.Drawing.Size(248, 36);
            txtEmail.TabIndex = 19;
            txtEmail.WaveSize = 0;
            errEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            errEmail.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            errEmail.Location = new System.Drawing.Point(264, 326);
            errEmail.Name = "errEmail";
            errEmail.Size = new System.Drawing.Size(248, 18);
            errEmail.TabIndex = 20;
            //
            // Địa chỉ (nhiều dòng, không bắt buộc)
            //
            lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            lblDiaChi.Location = new System.Drawing.Point(0, 348);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new System.Drawing.Size(512, 18);
            lblDiaChi.TabIndex = 21;
            lblDiaChi.Text = "Địa chỉ";
            txtDiaChi.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtDiaChi.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtDiaChi.Location = new System.Drawing.Point(0, 372);
            txtDiaChi.Multiline = true;
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtDiaChi.PlaceholderText = "Số nhà, đường, phường, quận, tỉnh / thành";
            txtDiaChi.Radius = 8;
            txtDiaChi.Size = new System.Drawing.Size(512, 56);
            txtDiaChi.TabIndex = 22;
            txtDiaChi.WaveSize = 0;
            //
            // TacGiaEditControl
            //
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.White;
            Controls.Add(avatar);
            Controls.Add(btnAnh);
            Controls.Add(btnMau);
            Controls.Add(lblMa);
            Controls.Add(txtMa);
            Controls.Add(errMa);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(errHoTen);
            Controls.Add(lblNgaySinh);
            Controls.Add(dtpNgaySinh);
            Controls.Add(errNgaySinh);
            Controls.Add(lblGioiTinh);
            Controls.Add(cboGioiTinh);
            Controls.Add(errGioiTinh);
            Controls.Add(lblDienThoai);
            Controls.Add(txtDienThoai);
            Controls.Add(errDienThoai);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(errEmail);
            Controls.Add(lblDiaChi);
            Controls.Add(txtDiaChi);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            Name = "TacGiaEditControl";
            Size = new System.Drawing.Size(512, 432);
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Avatar avatar;
        private AntdUI.Button btnAnh;
        private AntdUI.Button btnMau;
        private AntdUI.Label lblMa;
        private AntdUI.Input txtMa;
        private AntdUI.Label errMa;
        private AntdUI.Label lblHoTen;
        private AntdUI.Input txtHoTen;
        private AntdUI.Label errHoTen;
        private AntdUI.Label lblNgaySinh;
        private AntdUI.DatePicker dtpNgaySinh;
        private AntdUI.Label errNgaySinh;
        private AntdUI.Label lblGioiTinh;
        private AntdUI.Select cboGioiTinh;
        private AntdUI.Label errGioiTinh;
        private AntdUI.Label lblDienThoai;
        private AntdUI.Input txtDienThoai;
        private AntdUI.Label errDienThoai;
        private AntdUI.Label lblEmail;
        private AntdUI.Input txtEmail;
        private AntdUI.Label errEmail;
        private AntdUI.Label lblDiaChi;
        private AntdUI.Input txtDiaChi;
    }
}
