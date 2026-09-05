using AntdUI;

namespace QuanLyTacGia.Ui;

/// <summary>T1: thông báo góc trên phải, tự ẩn sau 3 s (spec PHONG_CACH_THIET_KE.md mục 5).</summary>
public static class Toast
{
    public static void Success(Form form, string text) => Show(form, text, TType.Success);
    public static void Info(Form form, string text) => Show(form, text, TType.Info);
    public static void Error(Form form, string text) => Show(form, text, TType.Error);

    static void Show(Form form, string text, TType type) =>
        AntdUI.Message.open(new AntdUI.Message.Config(form, text, type, AppTheme.Body, 3) { Align = TAlignFrom.Top, Radius = 8 });   // giữa trên: không đè nút "Thêm tác giả" góc phải
}
