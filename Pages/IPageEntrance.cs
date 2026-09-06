namespace QuanLyTacGia.Pages;

/// <summary>
/// Trang có hoạt cảnh xuất hiện (thẻ hiện dần, số đếm, biểu đồ vẽ dần). MainForm gọi <see cref="PrepareEntrance"/> để trang ở trạng thái đầu
/// (phủ trắng, số 0, biểu đồ trống) rồi chụp ảnh và trượt trang vào; trượt xong mới gọi <see cref="PlayEntrance"/> — tuần tự thay vì chạy chồng.
/// </summary>
public interface IPageEntrance
{
    void PrepareEntrance();
    void PlayEntrance();
}
