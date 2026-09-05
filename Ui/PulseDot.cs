using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace QuanLyTacGia.Ui
{
    /// <summary>Chấm trạng thái "đang hoạt động" có nhịp đập: chấm đặc + vòng lan toả mờ dần, chu kỳ 1,8 s. Kéo được từ Toolbox; màu đặt qua DotColor.</summary>
    [ToolboxItem(true)]
    public sealed class PulseDot : Control
    {
        Color color = AppTheme.SuccessText;
        float t;
        Action? loop;

        public PulseDot()
        {
            Size = new Size(14, 14);   // px 96 DPI, WinForms tự nhân
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        public PulseDot(Color color) : this() { this.color = color; }

        [Category("QLTG"), Description("Màu chấm"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DotColor { get => color; set { color = value; Invalidate(); } }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime) loop ??= Motion.Loop(1800, p => { t = p; Invalidate(); });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { loop?.Invoke(); loop = null; }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float cx = Width / 2f, cy = Height / 2f, core = Width / 4f;   // chấm = 1/4 bề rộng (3,5 px ở 14 px)
            // vòng lan toả: bán kính core → 2×core, alpha 120 → 0 (ease-out)
            float k = Motion.EaseOut(t);
            float ring = core + core * 1.1f * k;
            using (var rb = new SolidBrush(Color.FromArgb((int)(120 * (1f - k)), color)))
                g.FillEllipse(rb, cx - ring, cy - ring, ring * 2, ring * 2);
            using var b = new SolidBrush(color);
            g.FillEllipse(b, cx - core, cy - core, core * 2, core * 2);
        }
    }
}
