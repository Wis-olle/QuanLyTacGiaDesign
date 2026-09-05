using System.ComponentModel;
using System.Text.RegularExpressions;
using AntdUI;
using QuanLyTacGia.Data;
using QuanLyTacGia.Models;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Dialogs
{
    /// <summary>
    /// M1: nội dung modal Thêm / Sửa, rộng 512 (560 trừ padding 24 × 2). Lỗi hiện inline dưới từng ô.
    /// Bố cục trong TacGiaEditControl.Designer.cs theo px 96 DPI, AutoScaleMode = None vì AntdUI Modal tự nhân Dpi cho content.
    /// </summary>
    public partial class TacGiaEditControl : UserControl
    {
        TacGia? original;
        public TacGia? Result { get; private set; }

        Form? picker;
        readonly Dictionary<Input, AntdUI.Label> errors;
        Image? pickedAvatar;

        public TacGiaEditControl()
        {
            InitializeComponent();
            errors = new()
            {
                [txtMa] = errMa, [txtHoTen] = errHoTen, [dtpNgaySinh] = errNgaySinh,
                [cboGioiTinh] = errGioiTinh, [txtDienThoai] = errDienThoai, [txtEmail] = errEmail,
            };
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime) return;   // đang mở trong Designer

            dtpNgaySinh.MaxDate = DateTime.Today;
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            btnAnh.Click += (_, _) => PickAvatar();
            btnMau.Click += (_, _) => OpenAvatarPicker();
            txtHoTen.TextChanged += (_, _) => { if (pickedAvatar == null && original == null) avatar.Image = AvatarFactory.Get(txtHoTen.Text.Length == 0 ? "?" : txtHoTen.Text, 128); };
            Load += (_, _) => txtHoTen.Focus();
        }

        public TacGiaEditControl(TacGia? tacGia) : this()
        {
            original = tacGia;
            if (tacGia != null)
            {
                txtMa.Text = tacGia.MaTacGia;
                txtMa.ReadOnly = true;
                txtMa.BackColor = AppTheme.Canvas;
                txtMa.BorderColor = AppTheme.BorderSubtle;
                txtMa.ForeColor = AppTheme.TextSecondary;
                txtHoTen.Text = tacGia.HoTen;
                dtpNgaySinh.Value = tacGia.NgaySinh;
                cboGioiTinh.SelectedIndex = tacGia.GioiTinh == "Nữ" ? 1 : 0;
                txtDienThoai.Text = tacGia.DienThoai;
                txtEmail.Text = tacGia.Email;
                txtDiaChi.Text = tacGia.DiaChi;
                avatar.Image = tacGia.Avatar ?? AvatarFactory.Get(tacGia.HoTen, 128);
            }
            else
            {
                txtMa.Text = SampleData.NextMa();
                avatar.Image = AvatarFactory.Get("?", 128);
            }
        }

        void PickAvatar()
        {
            using var dlg = new OpenFileDialog { Title = "Chọn ảnh đại diện", Filter = "Ảnh|*.png;*.jpg;*.jpeg" };
            if (dlg.ShowDialog(FindForm()) != DialogResult.OK) return;
            using var fs = File.OpenRead(dlg.FileName);
            pickedAvatar = Image.FromStream(fs);
            avatar.Image = pickedAvatar;
        }

        /// <summary>Popover dưới nút "Chọn mẫu": lưới 8 cột avatar mặc định (Ui/AvatarLibrary), avatar đang dùng viền Primary; bấm là chọn và đóng.</summary>
        void OpenAvatarPicker()
        {
            const int cell = 40, gap = 8, cols = 8, head = 30;
            var lib = AvatarLibrary.All;
            if (lib.Count == 0) { Toast.Info(FindForm()!, "Chưa có avatar mẫu trong Assets/avatars"); return; }
            int rows = (lib.Count + cols - 1) / cols;
            var grid = new System.Windows.Forms.Panel { Size = new Size(cols * (cell + gap) + gap, head + rows * (cell + gap) + gap), BackColor = Color.White };
            grid.Controls.Add(new AntdUI.Label
            {
                Text = "Chọn ảnh đại diện mẫu", Font = AppTheme.SmallSemi, ForeColor = AppTheme.TextSecondary,
                Location = new Point(gap, 6), Size = new Size(grid.Width - 2 * gap, 20), BackColor = Color.Transparent,
            });
            for (int i = 0; i < lib.Count; i++)
            {
                var img = lib[i];
                var a = new Avatar
                {
                    Image = img, Round = true, ImageFit = TFit.Cover, Size = new Size(cell, cell), Cursor = Cursors.Hand,
                    Location = new Point(gap + i % cols * (cell + gap), head + gap + i / cols * (cell + gap)),
                    BorderWidth = ReferenceEquals(img, avatar.Image) ? 2 : 0, BorderColor = AppTheme.Primary,
                };
                a.Click += (_, _) => { pickedAvatar = img; avatar.Image = img; picker?.Close(); };
                grid.Controls.Add(a);
            }
            picker = Popover.open(new Popover.Config(btnMau, grid) { Radius = 12, ArrowAlign = TAlign.Bottom });
        }

        /// <summary>Mở popover chọn avatar mẫu (ScreenshotHarness).</summary>
        public void OpenAvatarPickerForDemo() => OpenAvatarPicker();

        /// <summary>Điền giá trị sai để chụp trạng thái lỗi (ScreenshotHarness).</summary>
        public void SetForErrorDemo()
        {
            txtMa.Text = "TG1";
            txtHoTen.Text = "";
            txtDienThoai.Text = "12345";
            txtEmail.Text = "abc@";
        }

        /// <summary>Validate; đúng thì ghi vào Result và trả true để modal đóng.</summary>
        public bool TrySave()
        {
            foreach (var (input, err) in errors) { input.Status = TType.None; err.Text = ""; }
            Input? first = null;
            void Fail(Input input, string msg)
            {
                input.Status = TType.Error;
                errors[input].Text = msg;
                first ??= input;
            }

            var ma = txtMa.Text.Trim().ToUpperInvariant();
            if (!Regex.IsMatch(ma, @"^TG\d{4}$")) Fail(txtMa, "Mã tác giả phải có dạng TG0001");
            else if (SampleData.TacGias.Any(t => t != original && t.MaTacGia == ma)) Fail(txtMa, "Mã tác giả đã tồn tại");

            if (txtHoTen.Text.Trim().Length == 0) Fail(txtHoTen, "Vui lòng nhập họ tên");
            if (dtpNgaySinh.Value is not { } ns || ns > DateTime.Today) Fail(dtpNgaySinh, "Ngày sinh không hợp lệ");
            if (cboGioiTinh.SelectedIndex < 0) Fail(cboGioiTinh, "Vui lòng chọn giới tính");
            if (!Regex.IsMatch(txtDienThoai.Text.Trim(), @"^0\d{9}$")) Fail(txtDienThoai, "Số điện thoại phải 10 số, bắt đầu bằng 0");
            if (!Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) Fail(txtEmail, "Email không hợp lệ");

            if (first != null) { first.Focus(); return false; }

            var t = original ?? new TacGia { NgayTao = DateTime.Today };
            t.MaTacGia = ma;
            t.HoTen = txtHoTen.Text.Trim();
            t.NgaySinh = dtpNgaySinh.Value!.Value;
            t.GioiTinh = cboGioiTinh.SelectedIndex == 1 ? "Nữ" : "Nam";
            t.DienThoai = txtDienThoai.Text.Trim();
            t.Email = txtEmail.Text.Trim();
            t.DiaChi = txtDiaChi.Text.Trim();
            if (pickedAvatar != null) t.Avatar = pickedAvatar;
            Result = t;
            return true;
        }
    }
}
