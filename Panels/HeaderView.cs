using System.ComponentModel;
using AntdUI;
using QuanLyTacGia.Ui;
using static QuanLyTacGia.Ui.AppTheme;

namespace QuanLyTacGia.Panels
{
    /// <summary>
    /// Đầu drawer chi tiết tự vẽ: banner gradient GradPrimary cao 120 (2 vòng tròn sáng), avatar 88 tròn viền trắng 4 đè lên mép banner
    /// (tâm ngay mép dưới banner) và nút × 32 ở góc phải có hover. Vẽ chung một control để không có control con lộ nền vuông trên gradient.
    /// Kéo được từ Toolbox (nhóm QuanLyTacGiaDesign Components).
    /// </summary>
    [ToolboxItem(true)]
    public sealed class HeaderView : Control
    {
        public const int BannerH = 120, AvatarSize = 88, AvatarTop = 76;
        public event Action? CloseClicked;
        Image? avatar;
        Bitmap? banner;
        bool hoverClose;
        readonly Bitmap closeIcon = SvgExtend.SvgToBmp(SvgDb.Custom["CloseOutlined"], S(16), S(16), Color.White)!;

        public HeaderView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Color.White;
            Size = new Size(380, AvatarTop + AvatarSize);
        }

        public void SetAvatar(Image img) { avatar = img; Invalidate(); }
        Rectangle CloseRect => new(Width - S(24 + 32), S(16), S(32), S(32));

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.Clear(Color.White);
            if (banner == null || banner.Width != Width) { banner?.Dispose(); banner = GradientCard.Make(Width, S(BannerH), AppTheme.GradPrimary.A, AppTheme.GradPrimary.B); }
            g.DrawImage(banner, 0, 0);

            var cr = CloseRect;
            if (hoverClose)
            {
                int d = S(16);
                using var path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddArc(cr.X, cr.Y, d, d, 180, 90); path.AddArc(cr.Right - d, cr.Y, d, d, 270, 90);
                path.AddArc(cr.Right - d, cr.Bottom - d, d, d, 0, 90); path.AddArc(cr.X, cr.Bottom - d, d, d, 90, 90); path.CloseFigure();
                using var hb = new SolidBrush(Color.FromArgb(50, 255, 255, 255)); g.FillPath(hb, path);
            }
            g.DrawImage(closeIcon, cr.X + S(8), cr.Y + S(8));

            var ring = new Rectangle((Width - S(AvatarSize)) / 2, S(AvatarTop), S(AvatarSize), S(AvatarSize));
            using (var white = new SolidBrush(Color.White)) g.FillEllipse(white, ring);
            if (avatar == null) return;
            var inner = Rectangle.Inflate(ring, -S(4), -S(4));
            var st = g.Save();
            using (var clip = new System.Drawing.Drawing2D.GraphicsPath()) { clip.AddEllipse(inner); g.SetClip(clip); g.DrawImage(avatar, inner); }
            g.Restore(st);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool h = CloseRect.Contains(e.Location);
            if (h != hoverClose) { hoverClose = h; Cursor = h ? Cursors.Hand : Cursors.Default; Invalidate(CloseRect); }
        }
        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); if (hoverClose) { hoverClose = false; Invalidate(CloseRect); } }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left && CloseRect.Contains(e.Location)) CloseClicked?.Invoke();
        }
    }
}
