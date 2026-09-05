namespace QuanLyTacGia.Ui;

/// <summary>
/// Bộ avatar mặc định cho tác giả: mọi file PNG trong Assets/avatars (DiceBear "notionists", CC0 — xem Assets/avatars/README.md,
/// tải lại bằng tools/Get-Avatars.ps1). Dùng cho dữ liệu mẫu và ô "Chọn mẫu" trong form Thêm / Sửa.
/// Avatar của quản trị viên nằm ở Assets/avatar-quangminh.png (ngoài thư mục này) nên không thuộc bộ chọn.
/// </summary>
public static class AvatarLibrary
{
    public static readonly string Dir = Path.Combine(AppContext.BaseDirectory, "Assets", "avatars");
    static List<Image>? all;

    public static IReadOnlyList<Image> All => all ??= Directory.Exists(Dir)
        ? Directory.GetFiles(Dir, "*.png").OrderBy(f => f, StringComparer.OrdinalIgnoreCase).Select(Load).ToList()
        : [];

    /// <summary>Avatar thứ index (0-based), quay vòng nếu vượt số file; null khi không có file nào.</summary>
    public static Image? Get(int index) => All.Count == 0 ? null : All[index % All.Count];

    static Image Load(string path)
    {
        using var fs = File.OpenRead(path);
        return Image.FromStream(fs);   // đọc hết vào bộ nhớ để không giữ khoá file
    }
}
