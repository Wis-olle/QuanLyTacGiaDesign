using AntdUI;
using QuanLyTacGia.Models;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Panels
{
    /// <summary>
    /// D1: nội dung drawer chi tiết tác giả, rộng 380. Bố cục trong TacGiaDetailPanel.Designer.cs theo px 96 DPI, AutoScaleMode = None
    /// vì AntdUI Drawer tự nhân Dpi cho content (Helper.DpiAuto); chỉ phần vẽ tay (HeaderView) dùng S().
    /// </summary>
    public partial class TacGiaDetailPanel : UserControl
    {
        public event Action<TacGia>? EditRequested;
        public event Action<TacGia>? DeleteRequested;
        public event Action? CloseRequested;

        TacGia? model;

        public TacGiaDetailPanel()
        {
            InitializeComponent();
            header.CloseClicked += () => CloseRequested?.Invoke();
            btnEdit.Click += (_, _) => { if (model != null) EditRequested?.Invoke(model); };
            btnDelete.Click += (_, _) => { if (model != null) DeleteRequested?.Invoke(model); };
        }

        public void Show(TacGia t)
        {
            model = t;
            header.SetAvatar(t.Avatar ?? AvatarFactory.Get(t.HoTen, 160));
            lblTen.Text = t.HoTen;
            chipMa.Text = t.MaTacGia;
            valNgaySinh.Text = t.NgaySinh.ToString("dd/MM/yyyy");
            valGioiTinh.Text = t.GioiTinh;
            valDienThoai.Text = t.DienThoai;
            valEmail.Text = t.Email;
            valDiaChi.Text = string.IsNullOrWhiteSpace(t.DiaChi) ? "—" : t.DiaChi;
        }
    }
}
