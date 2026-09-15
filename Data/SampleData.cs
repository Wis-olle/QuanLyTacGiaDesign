using QuanLyTacGia.Models;

namespace QuanLyTacGia.Data;

/// <summary>
/// Danh sách tác giả đang mở trong bộ nhớ, nạp từ SQL Server lúc khởi động (<see cref="Load"/> gọi trong Program.Main).
/// Mọi thêm / sửa / xoá trên giao diện phải ghi xuống CSDL qua <see cref="Db"/> rồi mới đổi danh sách này.
/// </summary>
public static class SampleData
{
    public static List<TacGia> TacGias { get; private set; } = [];

    /// <summary>Nạp lại toàn bộ tác giả từ CSDL. Ném ngoại lệ nếu không kết nối được (Program.Main bắt và báo lỗi).</summary>
    public static void Load() => TacGias = Db.LoadAll();

    public static string NextMa()
    {
        var max = TacGias.Select(t => int.TryParse(t.MaTacGia.AsSpan(2), out var n) ? n : 0).DefaultIfEmpty(0).Max();
        return $"TG{max + 1:0000}";
    }

    public static string RemoveDiacritics(string s)
    {
        var norm = s.Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder(norm.Length);
        foreach (var c in norm)
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
    }
}
