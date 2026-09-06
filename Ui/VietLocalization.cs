using AntdUI;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Nhãn tiếng Việt cho AntdUI. Mặc định AntdUI không có Provider → mọi khoá trả về giá trị tiếng Trung (khoá "ID" = zh-CN → lịch kiểu
/// 年 / 月, thứ 一二三, nút "今天"). Provider này trả "ID" = en-US để lịch dùng bố cục quốc tế (tháng trước năm) và ghi đè các nhãn bằng tiếng Việt.
/// Khoá lấy từ mã nguồn AntdUI 2.4.8 (Helper.Date.InitLanguage, Calendar, LayeredFormDatePicker, Pagination, Modal, Table).
/// Đăng ký một lần trong <see cref="AppTheme.Apply"/>: <c>AntdUI.Localization.Provider = new VietLocalization()</c>.
/// </summary>
public sealed class VietLocalization : ILocalization
{
    static readonly Dictionary<string, string> map = new()
    {
        ["ID"] = "en-US",             // bố cục lịch quốc tế: tháng đứng trước năm, trái → phải
        ["YearFormat"] = "yyyy",
        ["MonthFormat"] = "'Tháng' M",  // "Tháng 9" (dùng cả ở lưới chọn tháng)
        ["Mon"] = "T2", ["Tue"] = "T3", ["Wed"] = "T4", ["Thu"] = "T5", ["Fri"] = "T6", ["Sat"] = "T7", ["Sun"] = "CN",
        ["ToDay"] = "Hôm nay",
        ["Now"] = "Bây giờ",
        ["OK"] = "OK",
        ["Cancel"] = "Huỷ",
        ["NoData"] = "Không có dữ liệu",
        ["ItemsPerPage"] = "/ trang",
        ["Filter"] = "Lọc",
        ["Filter.Search"] = "Tìm kiếm",
        ["Transfer.Source"] = "Nguồn",
        ["Transfer.Target"] = "Đích",
    };

    /// <summary>Khoá không có trong bảng → null để AntdUI dùng mặc định của nó.</summary>
    public string? GetLocalizedString(string key) => map.TryGetValue(key, out var v) ? v : null;
}
