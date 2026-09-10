using System.Drawing.Imaging;
using QuanLyTacGia.Pages;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Tự chụp từng màn hình để kiểm tra giao diện. Chỉ chạy khi có biến môi trường QLTG_SHOT=&lt;thư mục&gt;.
/// ponytail: đây là "runnable check" của UI; không ảnh hưởng khi chạy bình thường.
/// </summary>
public static class ScreenshotHarness
{
    public static void TryAttach(MainForm form)
    {
        var dir = Environment.GetEnvironmentVariable("QLTG_SHOT");
        if (string.IsNullOrWhiteSpace(dir)) return;
        Directory.CreateDirectory(dir);
        form.Shown += async (_, _) =>
        {
            form.TopMost = true;   // người dùng có thể đang mở app khác; giữ cửa sổ trên cùng khi chụp
            form.Activate();
            try { await Run(form, dir); }
            catch (Exception ex) { File.WriteAllText(Path.Combine(dir, "error.txt"), ex.ToString()); }
            Application.Exit();
        };
    }

    static async Task Run(MainForm form, string dir)
    {
        async Task Shot(string name, int delay = 900)
        {
            await Task.Delay(delay);
            Capture(form, Path.Combine(dir, name + ".png"));
            Log(dir, "shot " + name);
        }
        // Chụp trong khi modal đang mở (modal chặn lời gọi nhưng vẫn bơm message nên await vẫn chạy)
        async void DuringModal(string name, Action? before = null)
        {
            await Task.Delay(1100);
            before?.Invoke();
            await Task.Delay(500);
            Capture(form, Path.Combine(dir, name + ".png"));
            foreach (var f in Application.OpenForms.Cast<Form>().ToList())
                if (f != form) f.Close();
            Log(dir, "closed modal " + name);
        }

        await Shot("21-dashboard-entrance", 380);   // giữa lúc thẻ hiện dần / số đang đếm
        await Shot("01-dashboard", 1500);

        form.GoTo("dashboard");   // bấm lại trang đang mở: phải chạy lại hoạt cảnh, không được kẹt ở trạng thái trắng
        await Shot("29-dashboard-again", 1500);

        form.GoTo("dashboard");
        await Shot("31-chart-reveal", 430);   // giữa lúc biểu đồ theo tháng quét từ trái sang phải (đầu bút + chấm bật lên)

        // rê chuột vào thẻ "Nam" để hoạ tiết chuyển động (di chuyển con trỏ thật → MouseEnter)
        Cursor.Position = form.PointToScreen(new Point((int)(form.ClientSize.Width * 0.46), (int)(form.ClientSize.Height * 0.26)));
        await Shot("22-card-hover", 900);

        // rê chuột vào biểu đồ theo tháng: tooltip + đường dóng, các chấm khác mờ
        Cursor.Position = form.PointToScreen(new Point((int)(form.ClientSize.Width * 0.40), (int)(form.ClientSize.Height * 0.50)));
        await Shot("23-chart-hover", 700);

        // rê chuột vào biểu đồ thập niên: gradient đổi sắc + vệt sáng chạy qua (chụp giữa nhịp và sau khi xong)
        Cursor.Position = form.PointToScreen(new Point((int)(form.ClientSize.Width * 0.30), (int)(form.ClientSize.Height * 0.80)));
        await Shot("25-bar-hover-mid", 160);
        await Shot("26-bar-hover-end", 700);
        Cursor.Position = form.PointToScreen(new Point(form.ClientSize.Width / 2, form.ClientSize.Height - 8));
        await Task.Delay(400);

        form.GoTo("tacgia");
        await Shot("27-list-slide", 90);   // giữa cú trượt trang (ảnh chụp trang đang trượt lên)
        await Shot("02-list", 1000);   // thẻ hiện dần xong sau trượt 240 + 14 × 30 + 320 ms
        var page = form.ListPage!;

        // rê chuột vào thẻ tác giả đầu tiên: nhấc lên + bóng + avatar phóng
        Cursor.Position = form.PointToScreen(new Point((int)(form.ClientSize.Width * 0.20), (int)(form.ClientSize.Height * 0.25)));
        await Shot("24-author-card-hover", 700);
        Cursor.Position = form.PointToScreen(new Point(form.ClientSize.Width / 2, form.ClientSize.Height - 8));
        await Task.Delay(400);

        page.SetViewForDemo(false);   // 02 là chế độ thẻ mặc định; 16 chụp chế độ bảng
        await Shot("16-list-table", 900);
        page.SetViewForDemo(true);
        await Task.Delay(300);

        page.OpenMoreMenuForDemo();
        await Shot("17-list-menu", 700);
        foreach (var f in Application.OpenForms.Cast<Form>().ToList())
            if (f != form) f.Close();
        await Task.Delay(300);

        page.ApplyPreset(Pages.ListPreset.ThisMonth);
        await Shot("18-list-month", 700);
        page.ApplyPreset(Pages.ListPreset.All);
        await Task.Delay(300);

        page.ShowDetailAt(0);
        await Shot("03-drawer");
        page.CloseDetail();
        await Task.Delay(500);

        Log(dir, "open 04"); DuringModal("04-modal-add");
        page.OpenAdd();

        DuringModal("05-modal-add-errors", () => { page.CurrentEditor!.SetForErrorDemo(); page.CurrentEditor!.TrySave(); });
        page.OpenAdd();

        Log(dir, "open 15"); DuringModal("15-avatar-picker", () => page.CurrentEditor!.OpenAvatarPickerForDemo());
        page.OpenAdd();

        Log(dir, "open 30"); DuringModal("30-date-picker", () => page.CurrentEditor!.OpenDatePickerForDemo());   // lịch tiếng Việt, bố cục quốc tế
        page.OpenAdd();

        Log(dir, "open 06"); DuringModal("06-modal-edit");
        page.OpenEditAt(0);

        Log(dir, "open 07"); DuringModal("07-modal-delete");
        page.OpenDeleteAt(0);
        Log(dir, "after 07");

        page.SetSearchForDemo("zzzz");
        await Shot("08-list-empty", 1200);
        page.SetSearchForDemo("");

        form.GoTo("settings");
        await Shot("09-settings");

        form.WindowState = FormWindowState.Normal;
        form.Size = new Size(1024, 600);
        form.GoTo("tacgia");
        await Shot("10-list-1024x600", 1200);

        page.ShowDetailAt(0);
        await Shot("11-drawer-1024x600");
        page.CloseDetail();
        await Task.Delay(500);

        Log(dir, "open 12"); DuringModal("12-modal-add-1024x600");
        page.OpenAdd();

        Toast.Success(form, "Đã lưu tác giả Nguyễn Du");
        await Shot("13-message", 700);

        form.GoTo("dashboard");
        await Shot("14-dashboard-1024x600", 1600);

        // laptop 1920×1080 @125 % → vùng logic 1536×864 (người dùng báo vỡ bố cục khi kéo sang màn này)
        form.Size = new Size(1536, 864);
        await Shot("19-dashboard-1536x864", 1200);
        form.GoTo("tacgia");
        await Shot("20-list-1536x864", 900);

        form.GoTo("dashboard");
        await Shot("28-dashboard-slide", 90);   // giữa cú trượt: thẻ phủ trắng, số 0, biểu đồ trống (trạng thái đầu chụp vào ảnh)
    }

    static void Log(string dir, string msg) => File.AppendAllText(Path.Combine(dir, "log.txt"), $"{DateTime.Now:HH:mm:ss.fff} {msg}{Environment.NewLine}");

    static void Capture(Form form, string path)
    {
        form.Activate();
        Application.DoEvents();
        var r = form.RectangleToScreen(form.ClientRectangle);
        using var bmp = new Bitmap(r.Width, r.Height);
        using var g = Graphics.FromImage(bmp);
        g.CopyFromScreen(r.Location, Point.Empty, r.Size);
        bmp.Save(path, ImageFormat.Png);
    }
}
