using QuanLyTacGia.Models;

using QuanLyTacGia.Ui;

namespace QuanLyTacGia.Data;

/// <summary>Dữ liệu mẫu in-memory. ponytail: thay bằng repository SQLite khi cần lưu bền.</summary>
public static class SampleData
{
    public static readonly List<TacGia> TacGias = Build();

    public static string NextMa()
    {
        var max = TacGias.Select(t => int.TryParse(t.MaTacGia.AsSpan(2), out var n) ? n : 0).DefaultIfEmpty(0).Max();
        return $"TG{max + 1:0000}";
    }

    static List<TacGia> Build()
    {
        (string ten, string ns, string gt, string dc)[] seed =
        [
            ("Nguyễn Nhật Ánh", "07/05/1955", "Nam", "Số 69, Nguyễn Du, Quận 1, TP. Hồ Chí Minh"),
            ("Nam Cao", "29/10/1915", "Nam", "Lý Nhân, Hà Nam"),
            ("Tô Hoài", "27/09/1920", "Nam", "Hà Đông, Hà Nội"),
            ("Xuân Quỳnh", "06/10/1942", "Nữ", "La Khê, Hà Đông, Hà Nội"),
            ("Nguyễn Du", "03/01/1765", "Nam", "Tiên Điền, Nghi Xuân, Hà Tĩnh"),
            ("Hồ Xuân Hương", "01/01/1772", "Nữ", "Quỳnh Đôi, Quỳnh Lưu, Nghệ An"),
            ("Vũ Trọng Phụng", "20/10/1912", "Nam", "Hàng Bạc, Hoàn Kiếm, Hà Nội"),
            ("Ngô Tất Tố", "01/01/1894", "Nam", "Lộc Hà, Đông Anh, Hà Nội"),
            ("Nguyễn Tuân", "10/07/1910", "Nam", "Nhân Mục, Thanh Xuân, Hà Nội"),
            ("Thạch Lam", "07/07/1910", "Nam", "Cẩm Giàng, Hải Dương"),
            ("Nguyễn Ngọc Tư", "01/01/1976", "Nữ", "Tân Duyệt, Đầm Dơi, Cà Mau"),
            ("Bảo Ninh", "18/10/1952", "Nam", "Bảo Ninh, Đồng Hới, Quảng Bình"),
            ("Nguyễn Huy Thiệp", "29/04/1950", "Nam", "Thanh Trì, Hà Nội"),
            ("Dương Thu Hương", "01/01/1947", "Nữ", "Thái Bình"),
            ("Phan Thị Vàng Anh", "18/08/1968", "Nữ", "Hà Nội"),
            ("Nguyễn Quang Sáng", "12/01/1932", "Nam", "Mỹ Luông, Chợ Mới, An Giang"),
            ("Trần Đăng Khoa", "24/04/1958", "Nam", "Quốc Tuấn, Nam Sách, Hải Dương"),
            ("Nguyễn Bính", "13/02/1918", "Nam", "Vụ Bản, Nam Định"),
            ("Hàn Mặc Tử", "22/09/1912", "Nam", "Đồng Hới, Quảng Bình"),
            ("Xuân Diệu", "02/02/1916", "Nam", "Can Lộc, Hà Tĩnh"),
            ("Huy Cận", "31/05/1919", "Nam", "Hương Sơn, Hà Tĩnh"),
            ("Chế Lan Viên", "20/10/1920", "Nam", "Cam Lộ, Quảng Trị"),
            ("Tố Hữu", "04/10/1920", "Nam", "Quảng Điền, Thừa Thiên Huế"),
            ("Lưu Quang Vũ", "17/04/1948", "Nam", "Hạ Hoà, Phú Thọ"),
            ("Đoàn Thị Điểm", "01/01/1705", "Nữ", "Văn Giang, Hưng Yên"),
            ("Bà Huyện Thanh Quan", "01/01/1805", "Nữ", "Nghi Tàm, Tây Hồ, Hà Nội"),
            ("Nguyễn Đình Chiểu", "01/07/1822", "Nam", "Tân Thới, Gia Định"),
            ("Nguyễn Khuyến", "15/02/1835", "Nam", "Bình Lục, Hà Nam"),
            ("Tú Xương", "05/09/1870", "Nam", "Vị Xuyên, Nam Định"),
            ("Kim Lân", "01/08/1920", "Nam", "Từ Sơn, Bắc Ninh"),
            ("Nguyên Hồng", "05/11/1918", "Nam", "Vụ Bản, Nam Định"),
            ("Anh Đức", "05/05/1935", "Nam", "Châu Thành, An Giang"),
            ("Ma Văn Kháng", "01/12/1936", "Nam", "Kim Liên, Đống Đa, Hà Nội"),
            ("Lê Minh Khuê", "06/12/1949", "Nữ", "Tĩnh Gia, Thanh Hoá"),
            ("Nguyễn Thị Thu Huệ", "12/08/1966", "Nữ", "Hà Nội"),
            ("Y Ban", "01/07/1961", "Nữ", "Nam Định"),
            ("Võ Thị Hảo", "01/01/1954", "Nữ", "Diễn Châu, Nghệ An"),
            ("Đỗ Bích Thuý", "13/04/1975", "Nữ", "Hà Giang"),
            ("Nguyễn Phan Quế Mai", "12/08/1973", "Nữ", "Ninh Bình"),
            ("Nguyễn Nhật Minh", "20/05/1980", "Nam", "Quận 3, TP. Hồ Chí Minh"),
            ("Hoàng Anh Tú", "01/04/1978", "Nam", "Hà Nội"),
            ("Trang Hạ", "01/01/1975", "Nữ", "Hà Nội"),
            ("Phong Điệp", "06/06/1976", "Nữ", "Nam Định"),
            ("Nguyễn Ngọc Thuần", "26/03/1972", "Nam", "Hàm Tân, Bình Thuận"),
            ("Nguyễn Vĩnh Nguyên", "10/10/1979", "Nam", "Ninh Thuận"),
            ("Vũ Đình Giang", "01/01/1976", "Nam", "Vĩnh Long"),
            ("Dương Thuỵ", "15/07/1975", "Nữ", "TP. Hồ Chí Minh"),
            ("Nguyễn Thiên Ngân", "11/11/1988", "Nữ", "Gia Lai"),
        ];

        var list = new List<TacGia>(seed.Length);
        for (int i = 0; i < seed.Length; i++)
        {
            var (ten, ns, gt, dc) = seed[i];
            var email = RemoveDiacritics(ten).Replace(" ", "").ToLowerInvariant() + "@gmail.com";
            list.Add(new TacGia
            {
                MaTacGia = $"TG{i + 1:0000}",
                HoTen = ten,
                NgaySinh = DateTime.ParseExact(ns, "dd/MM/yyyy", null),
                GioiTinh = gt,
                DienThoai = $"09{(12345678 + i * 7919) % 100000000:00000000}",
                Email = email,
                DiaChi = dc,
                NgayTao = DateTime.Today.AddDays(-(int)((seed.Length - 1 - i) * 7.3 + (i % 3 == 0 ? 0 : i * 37 % 11))),
                Avatar = AvatarLibrary.Get(i),   // avatar mẫu thứ i trong Assets/avatars
            });
        }
        return list;
    }

    public static string RemoveDiacritics(string s)
    {
        var norm = s.Normalize(System.Text.NormalizationForm.FormD);
        var sb = new System.Text.StringBuilder(norm.Length);
        foreach (var c in norm)
            if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        return sb.ToString().Normalize(System.Text.NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
    }
}
