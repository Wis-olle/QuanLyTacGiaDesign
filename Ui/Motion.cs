using System.Diagnostics;
using System.Drawing.Imaging;

namespace QuanLyTacGia.Ui;

/// <summary>
/// Motion tối giản theo nguyên tắc Ant Design / Material Motion: ease-out cho xuất hiện, 160 / 240 / 700 ms, phần tử cùng nhóm
/// xuất hiện lệch nhịp (stagger 60 ms). Mọi animation tắt được bằng AntdUI.Config.Animation = false.
/// Mẫu motion tham khảo trên Dribbble: Profy Admin Dashboard Animation (20804840), Healthcare Dashboard Animation (27528928),
/// Horizon Fleet Dashboard Animation (26972593), Atlas AI CRM Micro-Interaction (27650560).
///
/// HIỆU NĂNG (05/09/2026, người dùng thấy chuyển trang giật): MỘT timer chung 15 ms chạy mọi animation đang hoạt động — một tick gọi hết
/// các step rồi WinForms vẽ một lượt, thay vì mỗi animation một timer vẽ riêng. Thời gian tính theo Stopwatch nên rớt khung không làm chậm.
/// Chuyển trang trượt bằng ẢNH CHỤP của trang (<see cref="SlideIn"/>) thay vì đổi Padding — không dựng lại bố cục mỗi khung.
/// </summary>
public static class Motion
{
    public const int Fast = 160;    // hover, nhấn
    public const int Base = 240;    // chuyển trang, xuất hiện
    public const int Slow = 700;    // đếm số, biểu đồ
    public const int Stagger = 60;  // lệch nhịp giữa các phần tử cùng nhóm

    public static float EaseOut(float t) => 1f - MathF.Pow(1f - t, 3f);
    public static float EaseInOut(float t) => t < 0.5f ? 4f * t * t * t : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

    // ---------- Timer chung ----------
    sealed class Anim
    {
        public readonly Stopwatch Sw = Stopwatch.StartNew();
        public int Ms;                 // thời lượng (Run / Delay) hoặc chu kỳ (Loop)
        public Action<float>? Step;
        public Action? Done;
        public bool Loop, Delay, Alive = true;
    }
    static readonly List<Anim> anims = new();
    static System.Windows.Forms.Timer? ticker;

    static Action Add(Anim a)
    {
        anims.Add(a);
        if (ticker == null) { ticker = new System.Windows.Forms.Timer { Interval = 15 }; ticker.Tick += (_, _) => Tick(); }
        if (!ticker.Enabled) ticker.Start();
        return () => a.Alive = false;
    }

    static void Tick()
    {
        foreach (var a in anims.ToArray())   // step / done có thể thêm hoặc huỷ animation khác
        {
            if (!a.Alive) continue;
            long el = a.Sw.ElapsedMilliseconds;
            if (a.Loop) { a.Step!(el % a.Ms / (float)a.Ms); continue; }
            if (a.Delay) { if (el >= a.Ms) { a.Alive = false; a.Done?.Invoke(); } continue; }
            float t = Math.Min(1f, el / (float)a.Ms);
            a.Step!(EaseOut(t));
            if (t >= 1f) { a.Alive = false; a.Done?.Invoke(); }
        }
        anims.RemoveAll(x => !x.Alive);
        if (anims.Count == 0) ticker!.Stop();
    }

    /// <summary>Chạy step(p) với p từ 0 → 1 (đã ease-out) trong ms mili giây. Trả về hàm huỷ.</summary>
    public static Action Run(int ms, Action<float> step, Action? done = null)
    {
        if (!AntdUI.Config.Animation) { step(1f); done?.Invoke(); return () => { }; }
        step(0f);
        return Add(new Anim { Ms = Math.Max(1, ms), Step = step, Done = done });
    }

    /// <summary>Gọi then() sau ms mili giây (0 hoặc tắt animation → gọi ngay). Trả về hàm huỷ.</summary>
    public static Action Delay(int ms, Action then)
    {
        if (ms <= 0 || !AntdUI.Config.Animation) { then(); return () => { }; }
        return Add(new Anim { Ms = ms, Done = then, Delay = true });
    }

    /// <summary>Lặp vô hạn: step(t) với t chạy 0 → 1 mỗi periodMs (nhịp đập, xoay…). Trả về hàm huỷ — NHỚ gọi khi control huỷ.</summary>
    public static Action Loop(int periodMs, Action<float> step)
    {
        if (!AntdUI.Config.Animation) { step(0f); return () => { }; }
        return Add(new Anim { Ms = Math.Max(1, periodMs), Step = step, Loop = true });
    }

    public static Color Lerp(Color a, Color b, float t)
    {
        t = Math.Clamp(t, 0f, 1f);
        return Color.FromArgb(L(a.A, b.A, t), L(a.R, b.R, t), L(a.G, b.G, t), L(a.B, b.B, t));
    }
    static int L(int a, int b, float t) => (int)Math.Round(a + (b - a) * t);

    /// <summary>Số liệu đếm từ 0 lên target; chỉ gán Text khi giá trị đổi (mỗi lần gán là một lần vẽ lại cả nền thẻ).</summary>
    public static Action CountUp(Control label, int target, int ms = Slow) =>
        Run(ms, p => { var s = ((int)Math.Round(target * p)).ToString(); if (label.Text != s) label.Text = s; });

    /// <summary>
    /// Trang trượt lên 24 px (× K) khi xuất hiện. Cách làm: chụp trang (đã bố cục xong) thành bitmap, ẩn trang thật, trượt bitmap trong
    /// một control nhẹ rồi hiện lại trang và gọi done — mỗi khung chỉ dán một ảnh, không dựng lại bố cục / vẽ lại từng control như đổi Padding.
    /// Trả về hàm huỷ (kết thúc ngay, hiện trang thật).
    /// </summary>
    public static Action SlideIn(Control host, Control page, Action? done = null, int offset = -1, int ms = Base)
    {
        if (offset < 0) offset = AppTheme.S(24);
        if (!AntdUI.Config.Animation || page.Width <= 0 || page.Height <= 0 || !page.IsHandleCreated) { done?.Invoke(); return () => { }; }
        Bitmap shot;
        try
        {
            shot = new Bitmap(page.Width, page.Height);
            page.DrawToBitmap(shot, new Rectangle(0, 0, page.Width, page.Height));
        }
        catch { done?.Invoke(); return () => { }; }

        var view = new SnapshotView(shot, host.BackColor) { Bounds = page.Bounds };
        host.Controls.Add(view);
        view.BringToFront();
        page.Visible = false;
        bool finished = false;
        void Finish()
        {
            if (finished) return;
            finished = true;
            page.Visible = true;
            if (!view.IsDisposed) { host.Controls.Remove(view); view.Dispose(); }
        }
        var cancel = Run(ms, p => view.Dy = (int)(offset * (1f - p)), () => { Finish(); done?.Invoke(); });
        return () => { cancel(); Finish(); };
    }

    /// <summary>Control tạm hiện ảnh chụp trang, dịch xuống Dy px; nền = màu nền vùng nội dung.</summary>
    sealed class SnapshotView : Control
    {
        readonly Bitmap bmp;
        int dy;
        public SnapshotView(Bitmap bmp, Color back)
        {
            this.bmp = bmp;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.Opaque, true);
            BackColor = back;
        }
        [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int Dy { get => dy; set { if (dy == value) return; dy = value; Invalidate(); } }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            e.Graphics.DrawImageUnscaled(bmp, 0, dy);
        }
        protected override void Dispose(bool disposing) { if (disposing) bmp.Dispose(); base.Dispose(disposing); }
    }
}
