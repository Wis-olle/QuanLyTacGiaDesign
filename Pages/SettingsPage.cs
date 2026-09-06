using System.ComponentModel;
using AntdUI;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Pages
{
    /// <summary>P3 Cài đặt: tối thiểu, chỉ UI. Bố cục trong SettingsPage.Designer.cs. ponytail: nối lưu cấu hình khi có nơi lưu.</summary>
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            DoubleBuffered = true;
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime) return;
            txtFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            btnSave.Click += (_, _) => Toast.Success(FindForm()!, "Đã lưu cài đặt");
        }
    }
}
