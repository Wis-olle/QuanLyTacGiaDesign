using AntdUI;
using QuanLyTacGia.Models;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Pages;

/// <summary>1 dòng bảng: avatar · tên (+ tag "Mới" nếu thêm ≤ 30 ngày) · mã · ngày sinh · pill giới tính · điện thoại / email có icon · "Ngày thêm" tương đối · 3 nút icon có tooltip. Key cột = tên thuộc tính.</summary>
public class TacGiaRow
{
    public TacGia Model { get; }
    public CellImage Avatar { get; }
    public ICell[] HoTen { get; }
    public string MaTacGia => Model.MaTacGia;
    public string NgaySinh => Model.NgaySinh.ToString("dd/MM/yyyy");
    public CellTag GioiTinh { get; }
    public CellText DienThoai { get; }
    public CellText Email { get; }
    public string NgayThem => Relative(Model.NgayTao);
    public CellButton[] ThaoTac { get; }

    public TacGiaRow(TacGia t)
    {
        Model = t;
        // AntdUI trừ 2 x Gap (12) khỏi Size khi vẽ → 56 cho ảnh thật 32 px
        Avatar = new CellImage(t.Avatar ?? AvatarFactory.Get(t.HoTen)) { Round = true, Size = new Size(56, 56) };
        var name = new CellText(t.HoTen, AppTheme.TextPrimary) { Font = AppTheme.BodySemi };
        HoTen = (DateTime.Today - t.NgayTao).TotalDays <= 30
            ? [name, new CellTag("Mới") { Back = AppTheme.SuccessSoft, Fore = AppTheme.SuccessText, BorderWidth = 0 }]
            : [name];
        GioiTinh = t.GioiTinh == "Nữ"
            ? new CellTag("Nữ") { Back = AppTheme.PinkSoft, Fore = AppTheme.PinkText, BorderWidth = 0 }
            : new CellTag("Nam") { Back = AppTheme.PrimarySoft, Fore = AppTheme.Primary, BorderWidth = 0 };
        DienThoai = new CellText(t.DienThoai, AppTheme.TextSecondary) { PrefixSvg = "PhoneOutlined", IconRatio = 0.8f };
        Email = new CellText(t.Email, AppTheme.TextSecondary) { PrefixSvg = "MailOutlined", IconRatio = 0.8f };
        ThaoTac =
        [
            MakeBtn("view", "Xem", "EyeOutlined", AppTheme.TextSecondary),
            MakeBtn("edit", "Sửa", "EditOutlined", AppTheme.TextSecondary),
            MakeBtn("del", "Xoá", "DeleteOutlined", AppTheme.Danger),
        ];
    }

    /// <summary>"Hôm nay" · "3 ngày trước" · "2 tuần trước" · "5 tháng trước" · dd/MM/yyyy.</summary>
    public static string Relative(DateTime d)
    {
        int days = (int)(DateTime.Today - d.Date).TotalDays;
        return days switch
        {
            <= 0 => "Hôm nay",
            1 => "Hôm qua",
            < 7 => $"{days} ngày trước",
            < 30 => $"{days / 7} tuần trước",
            < 365 => $"{days / 30} tháng trước",
            _ => d.ToString("dd/MM/yyyy"),
        };
    }

    // nút chỉ icon 32×32 như mẫu 27274985 (3 ô vuông viền mảnh), tên nút hiện ở tooltip
    static CellButton MakeBtn(string id, string tip, string svg, Color fore) => new(id, TTypeMini.Default)
    {
        IconSvg = svg, Fore = fore, Radius = 6, BorderWidth = 1, Tooltip = tip,
        DefaultBorderColor = AppTheme.Border, DefaultBack = Color.White, Gap = 15, IconRatio = 0.9f,
    };
}
