using System.Drawing.Imaging;
using Microsoft.Data.SqlClient;
using QuanLyTacGia.Models;
using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Data;

/// <summary>
/// Truy cập bảng dbo.TacGia trên SQL Server bằng ADO.NET thuần (SqlConnection / SqlCommand / SqlDataReader).
/// Tạo CSDL một lần bằng <c>sql/QuanLyTacGia.sql</c>.
/// Đổi máy chủ: sửa <see cref="ConnectionString"/> bên dưới, hoặc đặt biến môi trường QLTG_SQL (không phải sửa mã).
/// </summary>
public static class Db
{
    /// <summary>
    /// Chuỗi kết nối. Trusted_Connection = đăng nhập bằng tài khoản Windows đang dùng (không cần user / password).
    /// TrustServerCertificate = chấp nhận chứng chỉ tự ký của SQL Server cài trên máy cá nhân.
    /// Máy dùng bản Express có tên thực thể: đổi "localhost" thành "localhost\\SQLEXPRESS".
    /// Đăng nhập kiểu SQL: Server=localhost;Database=QuanLyTacGia;User Id=sa;Password=...;TrustServerCertificate=True;
    /// </summary>
    public static string ConnectionString { get; set; } =
        Environment.GetEnvironmentVariable("QLTG_SQL")
        ?? @"Server=localhost;Database=QuanLyTacGia;Trusted_Connection=True;TrustServerCertificate=True;";

    const string Cols = "MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, Avatar, NgayTao";

    static SqlConnection Open()
    {
        var cn = new SqlConnection(ConnectionString);
        cn.Open();
        return cn;
    }

    /// <summary>Đọc toàn bộ tác giả, sắp theo mã.</summary>
    public static List<TacGia> LoadAll()
    {
        var list = new List<TacGia>();
        using var cn = Open();
        using var cmd = new SqlCommand($"SELECT {Cols} FROM dbo.TacGia ORDER BY MaTacGia", cn);
        using var r = cmd.ExecuteReader();
        while (r.Read())
            list.Add(new TacGia
            {
                MaTacGia = r.GetString(0),
                HoTen = r.GetString(1),
                NgaySinh = r.GetDateTime(2),
                GioiTinh = r.GetString(3),
                DienThoai = r.GetString(4),
                Email = r.GetString(5),
                DiaChi = r.GetString(6),
                Avatar = r.IsDBNull(7) ? AvatarLibrary.Get(list.Count) : ToImage((byte[])r[7]),   // NULL → ảnh mẫu thứ i
                NgayTao = r.GetDateTime(8),
            });
        return list;
    }

    public static void Insert(TacGia t) => Run(
        $"INSERT INTO dbo.TacGia ({Cols}) VALUES (@Ma, @HoTen, @NgaySinh, @GioiTinh, @DienThoai, @Email, @DiaChi, @Avatar, @NgayTao)", t);

    public static void Update(TacGia t) => Run(
        "UPDATE dbo.TacGia SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, DienThoai = @DienThoai, " +
        "Email = @Email, DiaChi = @DiaChi, Avatar = @Avatar, NgayTao = @NgayTao WHERE MaTacGia = @Ma", t);

    public static void Delete(string maTacGia)
    {
        using var cn = Open();
        using var cmd = new SqlCommand("DELETE FROM dbo.TacGia WHERE MaTacGia = @Ma", cn);
        cmd.Parameters.AddWithValue("@Ma", maTacGia);
        cmd.ExecuteNonQuery();
    }

    /// <summary>Chạy INSERT / UPDATE với đủ tham số của một tác giả (tham số hoá để tránh SQL injection).</summary>
    static void Run(string sql, TacGia t)
    {
        using var cn = Open();
        using var cmd = new SqlCommand(sql, cn);
        cmd.Parameters.AddWithValue("@Ma", t.MaTacGia);
        cmd.Parameters.AddWithValue("@HoTen", t.HoTen);
        cmd.Parameters.AddWithValue("@NgaySinh", t.NgaySinh.Date);
        cmd.Parameters.AddWithValue("@GioiTinh", t.GioiTinh);
        cmd.Parameters.AddWithValue("@DienThoai", t.DienThoai);
        cmd.Parameters.AddWithValue("@Email", t.Email);
        cmd.Parameters.AddWithValue("@DiaChi", t.DiaChi);
        cmd.Parameters.Add("@Avatar", System.Data.SqlDbType.VarBinary, -1).Value = ToBytes(t.Avatar);
        cmd.Parameters.AddWithValue("@NgayTao", t.NgayTao.Date);
        cmd.ExecuteNonQuery();
    }

    /// <summary>byte[] trong CSDL → Image. Sao ra Bitmap mới để không phải giữ MemoryStream sống theo ảnh.</summary>
    static Image ToImage(byte[] bytes)
    {
        using var ms = new MemoryStream(bytes);
        using var src = Image.FromStream(ms);
        return new Bitmap(src);
    }

    /// <summary>Image → byte[] PNG; null → DBNull để cột Avatar nhận NULL.</summary>
    static object ToBytes(Image? img)
    {
        if (img == null) return DBNull.Value;
        using var ms = new MemoryStream();
        img.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}
