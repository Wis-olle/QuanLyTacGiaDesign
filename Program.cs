namespace QuanLyTacGia;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Ui.AppTheme.Apply();
        Application.Run(new MainForm());
    }
}
