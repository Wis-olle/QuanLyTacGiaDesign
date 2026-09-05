using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace QuanLyTacGia.Ui;

/// <summary>Avatar chữ cái đầu khi tác giả chưa có ảnh. Cache theo tên + cỡ.</summary>
public static class AvatarFactory
{
    static readonly Dictionary<string, Image> cache = new();

    public static Image Get(string name, int size = 64)
    {
        var key = $"{name}|{size}";
        if (cache.TryGetValue(key, out var img)) return img;

        var words = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var initials = string.Concat(words.TakeLast(2).Select(w => char.ToUpperInvariant(w[0])));
        if (initials.Length == 0) initials = "?";

        var bmp = new Bitmap(size, size);
        using var g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        g.Clear(Color.Transparent);
        using var back = new SolidBrush(AppTheme.PrimarySoft);
        g.FillEllipse(back, 0, 0, size - 1, size - 1);
        using var font = AppTheme.Semibold(size * 0.3f);
        using var fore = new SolidBrush(AppTheme.Primary);
        using var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString(initials, font, fore, new RectangleF(0, 1, size, size), sf);
        cache[key] = bmp;
        return bmp;
    }
}
