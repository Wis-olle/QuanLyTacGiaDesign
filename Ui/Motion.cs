using System.Diagnostics;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Motion tối giản theo nguyên tắc Ant Design / Material Motion: ease-out cho xuất hiện, 160 / 240 / 700 ms, phần tử cùng nhóm
/// xuất hiện lệch nhịp (stagger 60 ms). Dùng Timer 15 ms; mọi animation tắt được bằng AntdUI.Config.Animation = false.
/// Mẫu motion tham khảo trên Dribbble: Profy Admin Dashboard Animation (20804840), Healthcare Dashboard Animation (27528928),
/// Horizon Fleet Dashboard Animation (26972593), Atlas AI CRM Micro-Interaction (27650560).
/// </summary>
public static class Motion
{
    public const int Fast = 160;    // hover, nhấn
    public const int Base = 240;    // chuyển trang, xuất hiện
    public const int Slow = 700;    // đếm số, biểu đồ
    public const int Stagger = 60;  // lệch nhịp giữa các phần tử cùng nhóm

    public static float EaseOut(float t) => 1f - MathF.Pow(1f - t, 3f);
    public static float EaseInOut(float t) => t < 0.5f ? 4f * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

    /// <summary>Chạy step(p) với p từ 0 → 1 (đã ease-out) trong ms mili giây. Trả về hàm huỷ.</summary>
    public static Action Run(int ms, Action<float> step, Action? done = null)
    {
        if (!AntdUI.Config.Animation) { step(1f); done?.Invoke(); return () => { }; }
        var sw = Stopwatch.StartNew();
        var timer = new System.Windows.Forms.Timer { Interval = 15 };
        timer.Tick += (_, _) =>
        {
            float t = Math.Min(1f, sw.ElapsedMilliseconds / (float)ms);
            step(EaseOut(t));
            if (t < 1f) return;
            timer.Stop();
            timer.Dispose();
            done?.Invoke();
        };
        step(0f);
        timer.Start();
        return () => { timer.Stop(); timer.Dispose(); };
    }

    /// <summary>Gọi then() sau ms mili giây (0 hoặc tắt animation → gọi ngay). Trả về hàm huỷ.</summary>
    public static Action Delay(int ms, Action then)
    {
        if (ms <= 0 || !AntdUI.Config.Animation) { then(); return () => { }; }
        var timer = new System.Windows.Forms.Timer { Interval = ms };
        timer.Tick += (_, _) => { timer.Stop(); timer.Dispose(); then(); };
        timer.Start();
        return () => { timer.Stop(); timer.Dispose(); };
    }

    /// <summary>Lặp vô hạn: step(t) với t chạy 0 → 1 mỗi periodMs (nhịp đập, xoay…). Trả về hàm huỷ — NHỚ gọi khi control huỷ.</summary>
    public static Action Loop(int periodMs, Action<float> step)
    {
        if (!AntdUI.Config.Animation) { step(0f); return () => { }; }
        var sw = Stopwatch.StartNew();
        var timer = new System.Windows.Forms.Timer { Interval = 33 };
        timer.Tick += (_, _) => step(sw.ElapsedMilliseconds % periodMs / (float)periodMs);
        timer.Start();
        return () => { timer.Stop(); timer.Dispose(); };
    }

    public static Color Lerp(Color a, Color b, float t)
    {
        t = Math.Clamp(t, 0f, 1f);
        return Color.FromArgb(L(a.A, b.A, t), L(a.R, b.R, t), L(a.G, b.G, t), L(a.B, b.B, t));
    }
    static int L(int a, int b, float t) => (int)Math.Round(a + (b - a) * t);

    /// <summary>Số liệu đếm từ 0 lên target.</summary>
    public static Action CountUp(Control label, int target, int ms = Slow) =>
        Run(ms, p => label.Text = ((int)Math.Round(target * p)).ToString());

    /// <summary>Trang trượt lên 24 px (× K) khi xuất hiện: animate Padding.Top của panel chứa trang.</summary>
    public static Action SlideIn(Control host, Padding basePadding, int offset = -1, int ms = Base)
    {
        if (offset < 0) offset = AppTheme.S(24);
        return Run(ms, p => host.Padding = new Padding(basePadding.Left, basePadding.Top + (int)(offset * (1f - p)), basePadding.Right, basePadding.Bottom));
    }
}
