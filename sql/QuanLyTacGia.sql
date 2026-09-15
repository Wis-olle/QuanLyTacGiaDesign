/* =====================================================================
   QuanLyTacGia - CSDL cho app WinForms "Quan ly tac gia"
   Chay 1 lan trong SQL Server Management Studio (SSMS) hoac sqlcmd:
       sqlcmd -S localhost -E -C -i QuanLyTacGia.sql
   Chay lai script se XOA va tao lai toan bo du lieu.
   ===================================================================== */

IF DB_ID('QuanLyTacGia') IS NULL
    CREATE DATABASE QuanLyTacGia;
GO

USE QuanLyTacGia;
GO

DROP TABLE IF EXISTS dbo.TacGia;
GO

CREATE TABLE dbo.TacGia
(
    MaTacGia  NVARCHAR(10)   NOT NULL CONSTRAINT PK_TacGia PRIMARY KEY,
    HoTen     NVARCHAR(100)  NOT NULL,
    NgaySinh  DATE           NOT NULL,
    GioiTinh  NVARCHAR(5)    NOT NULL CONSTRAINT CK_TacGia_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ')),
    DienThoai VARCHAR(15)    NOT NULL,
    Email     NVARCHAR(100)  NOT NULL,
    DiaChi    NVARCHAR(200)  NOT NULL,
    Avatar    VARBINARY(MAX) NULL,   -- NULL = dung anh mau trong Assets/avatars
    NgayTao   DATE           NOT NULL CONSTRAINT DF_TacGia_NgayTao DEFAULT (CAST(GETDATE() AS date))
);
GO

INSERT INTO dbo.TacGia (MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayTao) VALUES
(N'TG0001', N'Nguyễn Nhật Ánh', '1955-05-07', N'Nam', '0912345678', N'nguyennhatanh@gmail.com', N'Số 69, Nguyễn Du, Quận 1, TP. Hồ Chí Minh', DATEADD(day, -343, CAST(GETDATE() AS date))),
(N'TG0002', N'Nam Cao', '1915-10-29', N'Nam', '0912353597', N'namcao@gmail.com', N'Lý Nhân, Hà Nam', DATEADD(day, -339, CAST(GETDATE() AS date))),
(N'TG0003', N'Tô Hoài', '1920-09-27', N'Nam', '0912361516', N'tohoai@gmail.com', N'Hà Đông, Hà Nội', DATEADD(day, -336, CAST(GETDATE() AS date))),
(N'TG0004', N'Xuân Quỳnh', '1942-10-06', N'Nữ', '0912369435', N'xuanquynh@gmail.com', N'La Khê, Hà Đông, Hà Nội', DATEADD(day, -321, CAST(GETDATE() AS date))),
(N'TG0005', N'Nguyễn Du', '1765-01-03', N'Nam', '0912377354', N'nguyendu@gmail.com', N'Tiên Điền, Nghi Xuân, Hà Tĩnh', DATEADD(day, -318, CAST(GETDATE() AS date))),
(N'TG0006', N'Hồ Xuân Hương', '1772-01-01', N'Nữ', '0912385273', N'hoxuanhuong@gmail.com', N'Quỳnh Đôi, Quỳnh Lưu, Nghệ An', DATEADD(day, -315, CAST(GETDATE() AS date))),
(N'TG0007', N'Vũ Trọng Phụng', '1912-10-20', N'Nam', '0912393192', N'vutrongphung@gmail.com', N'Hàng Bạc, Hoàn Kiếm, Hà Nội', DATEADD(day, -299, CAST(GETDATE() AS date))),
(N'TG0008', N'Ngô Tất Tố', '1894-01-01', N'Nam', '0912401111', N'ngotatto@gmail.com', N'Lộc Hà, Đông Anh, Hà Nội', DATEADD(day, -298, CAST(GETDATE() AS date))),
(N'TG0009', N'Nguyễn Tuân', '1910-07-10', N'Nam', '0912409030', N'nguyentuan@gmail.com', N'Nhân Mục, Thanh Xuân, Hà Nội', DATEADD(day, -294, CAST(GETDATE() AS date))),
(N'TG0010', N'Thạch Lam', '1910-07-07', N'Nam', '0912416949', N'thachlam@gmail.com', N'Cẩm Giàng, Hải Dương', DATEADD(day, -277, CAST(GETDATE() AS date))),
(N'TG0011', N'Nguyễn Ngọc Tư', '1976-01-01', N'Nữ', '0912424868', N'nguyenngoctu@gmail.com', N'Tân Duyệt, Đầm Dơi, Cà Mau', DATEADD(day, -277, CAST(GETDATE() AS date))),
(N'TG0012', N'Bảo Ninh', '1952-10-18', N'Nam', '0912432787', N'baoninh@gmail.com', N'Bảo Ninh, Đồng Hới, Quảng Bình', DATEADD(day, -262, CAST(GETDATE() AS date))),
(N'TG0013', N'Nguyễn Huy Thiệp', '1950-04-29', N'Nam', '0912440706', N'nguyenhuythiep@gmail.com', N'Thanh Trì, Hà Nội', DATEADD(day, -255, CAST(GETDATE() AS date))),
(N'TG0014', N'Dương Thu Hương', '1947-01-01', N'Nữ', '0912448625', N'duongthuhuong@gmail.com', N'Thái Bình', DATEADD(day, -256, CAST(GETDATE() AS date))),
(N'TG0015', N'Phan Thị Vàng Anh', '1968-08-18', N'Nữ', '0912456544', N'phanthivanganh@gmail.com', N'Hà Nội', DATEADD(day, -241, CAST(GETDATE() AS date))),
(N'TG0016', N'Nguyễn Quang Sáng', '1932-01-12', N'Nam', '0912464463', N'nguyenquangsang@gmail.com', N'Mỹ Luông, Chợ Mới, An Giang', DATEADD(day, -233, CAST(GETDATE() AS date))),
(N'TG0017', N'Trần Đăng Khoa', '1958-04-24', N'Nam', '0912472382', N'trandangkhoa@gmail.com', N'Quốc Tuấn, Nam Sách, Hải Dương', DATEADD(day, -235, CAST(GETDATE() AS date))),
(N'TG0018', N'Nguyễn Bính', '1918-02-13', N'Nam', '0912480301', N'nguyenbinh@gmail.com', N'Vụ Bản, Nam Định', DATEADD(day, -221, CAST(GETDATE() AS date))),
(N'TG0019', N'Hàn Mặc Tử', '1912-09-22', N'Nam', '0912488220', N'hanmactu@gmail.com', N'Đồng Hới, Quảng Bình', DATEADD(day, -211, CAST(GETDATE() AS date))),
(N'TG0020', N'Xuân Diệu', '1916-02-02', N'Nam', '0912496139', N'xuandieu@gmail.com', N'Can Lộc, Hà Tĩnh', DATEADD(day, -214, CAST(GETDATE() AS date))),
(N'TG0021', N'Huy Cận', '1919-05-31', N'Nam', '0912504058', N'huycan@gmail.com', N'Hương Sơn, Hà Tĩnh', DATEADD(day, -200, CAST(GETDATE() AS date))),
(N'TG0022', N'Chế Lan Viên', '1920-10-20', N'Nam', '0912511977', N'chelanvien@gmail.com', N'Cam Lộ, Quảng Trị', DATEADD(day, -189, CAST(GETDATE() AS date))),
(N'TG0023', N'Tố Hữu', '1920-10-04', N'Nam', '0912519896', N'tohuu@gmail.com', N'Quảng Điền, Thừa Thiên Huế', DATEADD(day, -182, CAST(GETDATE() AS date))),
(N'TG0024', N'Lưu Quang Vũ', '1948-04-17', N'Nam', '0912527815', N'luuquangvu@gmail.com', N'Hạ Hoà, Phú Thọ', DATEADD(day, -179, CAST(GETDATE() AS date))),
(N'TG0025', N'Đoàn Thị Điểm', '1705-01-01', N'Nữ', '0912535734', N'doanthidiem@gmail.com', N'Văn Giang, Hưng Yên', DATEADD(day, -167, CAST(GETDATE() AS date))),
(N'TG0026', N'Bà Huyện Thanh Quan', '1805-01-01', N'Nữ', '0912543653', N'bahuyenthanhquan@gmail.com', N'Nghi Tàm, Tây Hồ, Hà Nội', DATEADD(day, -161, CAST(GETDATE() AS date))),
(N'TG0027', N'Nguyễn Đình Chiểu', '1822-07-01', N'Nam', '0912551572', N'nguyendinhchieu@gmail.com', N'Tân Thới, Gia Định', DATEADD(day, -158, CAST(GETDATE() AS date))),
(N'TG0028', N'Nguyễn Khuyến', '1835-02-15', N'Nam', '0912559491', N'nguyenkhuyen@gmail.com', N'Bình Lục, Hà Nam', DATEADD(day, -146, CAST(GETDATE() AS date))),
(N'TG0029', N'Tú Xương', '1870-09-05', N'Nam', '0912567410', N'tuxuong@gmail.com', N'Vị Xuyên, Nam Định', DATEADD(day, -140, CAST(GETDATE() AS date))),
(N'TG0030', N'Kim Lân', '1920-08-01', N'Nam', '0912575329', N'kimlan@gmail.com', N'Từ Sơn, Bắc Ninh', DATEADD(day, -137, CAST(GETDATE() AS date))),
(N'TG0031', N'Nguyên Hồng', '1918-11-05', N'Nam', '0912583248', N'nguyenhong@gmail.com', N'Vụ Bản, Nam Định', DATEADD(day, -124, CAST(GETDATE() AS date))),
(N'TG0032', N'Anh Đức', '1935-05-05', N'Nam', '0912591167', N'anhduc@gmail.com', N'Châu Thành, An Giang', DATEADD(day, -119, CAST(GETDATE() AS date))),
(N'TG0033', N'Ma Văn Kháng', '1936-12-01', N'Nam', '0912599086', N'mavankhang@gmail.com', N'Kim Liên, Đống Đa, Hà Nội', DATEADD(day, -116, CAST(GETDATE() AS date))),
(N'TG0034', N'Lê Minh Khuê', '1949-12-06', N'Nữ', '0912607005', N'leminhkhue@gmail.com', N'Tĩnh Gia, Thanh Hoá', DATEADD(day, -102, CAST(GETDATE() AS date))),
(N'TG0035', N'Nguyễn Thị Thu Huệ', '1966-08-12', N'Nữ', '0912614924', N'nguyenthithuhue@gmail.com', N'Hà Nội', DATEADD(day, -98, CAST(GETDATE() AS date))),
(N'TG0036', N'Y Ban', '1961-07-01', N'Nữ', '0912622843', N'yban@gmail.com', N'Nam Định', DATEADD(day, -95, CAST(GETDATE() AS date))),
(N'TG0037', N'Võ Thị Hảo', '1954-01-01', N'Nữ', '0912630762', N'vothihao@gmail.com', N'Diễn Châu, Nghệ An', DATEADD(day, -80, CAST(GETDATE() AS date))),
(N'TG0038', N'Đỗ Bích Thuý', '1975-04-13', N'Nữ', '0912638681', N'dobichthuy@gmail.com', N'Hà Giang', DATEADD(day, -78, CAST(GETDATE() AS date))),
(N'TG0039', N'Nguyễn Phan Quế Mai', '1973-08-12', N'Nữ', '0912646600', N'nguyenphanquemai@gmail.com', N'Ninh Bình', DATEADD(day, -74, CAST(GETDATE() AS date))),
(N'TG0040', N'Nguyễn Nhật Minh', '1980-05-20', N'Nam', '0912654519', N'nguyennhatminh@gmail.com', N'Quận 3, TP. Hồ Chí Minh', DATEADD(day, -58, CAST(GETDATE() AS date))),
(N'TG0041', N'Hoàng Anh Tú', '1978-04-01', N'Nam', '0912662438', N'hoanganhtu@gmail.com', N'Hà Nội', DATEADD(day, -57, CAST(GETDATE() AS date))),
(N'TG0042', N'Trang Hạ', '1975-01-01', N'Nữ', '0912670357', N'trangha@gmail.com', N'Hà Nội', DATEADD(day, -53, CAST(GETDATE() AS date))),
(N'TG0043', N'Phong Điệp', '1976-06-06', N'Nữ', '0912678276', N'phongdiep@gmail.com', N'Nam Định', DATEADD(day, -36, CAST(GETDATE() AS date))),
(N'TG0044', N'Nguyễn Ngọc Thuần', '1972-03-26', N'Nam', '0912686195', N'nguyenngocthuan@gmail.com', N'Hàm Tân, Bình Thuận', DATEADD(day, -36, CAST(GETDATE() AS date))),
(N'TG0045', N'Nguyễn Vĩnh Nguyên', '1979-10-10', N'Nam', '0912694114', N'nguyenvinhnguyen@gmail.com', N'Ninh Thuận', DATEADD(day, -21, CAST(GETDATE() AS date))),
(N'TG0046', N'Vũ Đình Giang', '1976-01-01', N'Nam', '0912702033', N'vudinhgiang@gmail.com', N'Vĩnh Long', DATEADD(day, -14, CAST(GETDATE() AS date))),
(N'TG0047', N'Dương Thuỵ', '1975-07-15', N'Nữ', '0912709952', N'duongthuy@gmail.com', N'TP. Hồ Chí Minh', DATEADD(day, -15, CAST(GETDATE() AS date))),
(N'TG0048', N'Nguyễn Thiên Ngân', '1988-11-11', N'Nữ', '0912717871', N'nguyenthienngan@gmail.com', N'Gia Lai', DATEADD(day, -1, CAST(GETDATE() AS date)));
GO

SELECT COUNT(*) AS SoTacGia FROM dbo.TacGia;
GO
