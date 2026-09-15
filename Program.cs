namespace QuanLyTacGia;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Ui.AppTheme.Apply();

        // Nạp tác giả từ SQL Server. Không kết nối được thì báo rõ rồi thoát (xem Data/Db.cs và sql/QuanLyTacGia.sql).
        try { Data.SampleData.Load(); }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Không kết nối được SQL Server.\n\n" + ex.Message
                + "\n\nChuỗi kết nối đang dùng:\n" + Data.Db.ConnectionString
                + "\n\nMở SQL Server Management Studio chạy tệp sql\\QuanLyTacGia.sql, hoặc sửa chuỗi kết nối trong Data\\Db.cs.",
                "Quản lý tác giả", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        Application.Run(new MainForm());
    }
}
