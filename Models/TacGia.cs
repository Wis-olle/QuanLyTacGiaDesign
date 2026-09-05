namespace QuanLyTacGia.Models;

public class TacGia
{
    public string MaTacGia { get; set; } = "";
    public string HoTen { get; set; } = "";
    public DateTime NgaySinh { get; set; }
    public string GioiTinh { get; set; } = "Nam";
    public string DienThoai { get; set; } = "";
    public string Email { get; set; } = "";
    public string DiaChi { get; set; } = "";
    public Image? Avatar { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Today;

    public TacGia Clone() => (TacGia)MemberwiseClone();
}
