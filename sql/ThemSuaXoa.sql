/* =====================================================================
   Them / sua / xoa tac gia bang cau lenh SQL (chay trong SSMS)
   Mo file nay trong SSMS, boi den doan can chay roi nhan F5.
   LUU Y: app nap du lieu MOT LAN luc khoi dong. Sua o day xong phai
   dong app va mo lai (F5 trong Visual Studio) moi thay.
   ===================================================================== */

USE QuanLyTacGia;
GO

-- ---------------------------------------------------------------------
-- 1. THEM MOT TAC GIA
-- ---------------------------------------------------------------------
-- Tien to N truoc moi chuoi tieng Viet, thieu N la mat dau thanh "???".
-- Ngay viet dang 'yyyy-MM-dd' cho chac, khong phu thuoc dinh dang may.
-- Cot Avatar bo qua duoc: de trong thi app lay anh mau trong Assets/avatars.

INSERT INTO dbo.TacGia (MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayTao)
VALUES (N'TG0049', N'Nguyễn Văn A', '1990-05-20', N'Nam', '0912000049',
        N'nguyenvana@gmail.com', N'Quận 1, TP. Hồ Chí Minh', CAST(GETDATE() AS date));
GO

-- ---------------------------------------------------------------------
-- 2. THEM NHIEU TAC GIA MOT LUC
-- ---------------------------------------------------------------------

INSERT INTO dbo.TacGia (MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayTao)
VALUES
    (N'TG0050', N'Trần Thị B', '1985-11-02', N'Nữ', '0912000050', N'tranthib@gmail.com', N'Hải Phòng', CAST(GETDATE() AS date)),
    (N'TG0051', N'Lê Văn C',   '1978-03-14', N'Nam', '0912000051', N'levanc@gmail.com',  N'Đà Nẵng',   CAST(GETDATE() AS date));
GO

-- ---------------------------------------------------------------------
-- 3. THEM MA KHONG PHAI TU NGHI MA
-- ---------------------------------------------------------------------
-- Tu lay ma ke tiep (TG0049, TG0050...) giong ham NextMa() trong app.

DECLARE @ma NVARCHAR(10);

SELECT @ma = N'TG' + RIGHT('0000' + CAST(ISNULL(MAX(CAST(SUBSTRING(MaTacGia, 3, 4) AS int)), 0) + 1 AS varchar(4)), 4)
FROM dbo.TacGia;

INSERT INTO dbo.TacGia (MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, Email, DiaChi, NgayTao)
VALUES (@ma, N'Phạm Thị D', '1995-07-09', N'Nữ', '0912000052',
        N'phamthid@gmail.com', N'Cần Thơ', CAST(GETDATE() AS date));

SELECT @ma AS MaVuaThem;
GO

-- ---------------------------------------------------------------------
-- 4. SUA
-- ---------------------------------------------------------------------
-- Luon co WHERE, thieu WHERE la sua het ca bang.

UPDATE dbo.TacGia
SET HoTen = N'Nguyễn Văn An',
    DiaChi = N'Quận 3, TP. Hồ Chí Minh',
    DienThoai = '0987654321'
WHERE MaTacGia = N'TG0049';
GO

-- ---------------------------------------------------------------------
-- 5. XOA
-- ---------------------------------------------------------------------

DELETE FROM dbo.TacGia WHERE MaTacGia = N'TG0049';
GO

-- Xoa nhieu ma mot luc
DELETE FROM dbo.TacGia WHERE MaTacGia IN (N'TG0050', N'TG0051');
GO

-- Xoa het tac gia tu them, giu lai 48 tac gia mau ban dau
DELETE FROM dbo.TacGia WHERE CAST(SUBSTRING(MaTacGia, 3, 4) AS int) > 48;
GO

-- ---------------------------------------------------------------------
-- 6. XEM LAI
-- ---------------------------------------------------------------------

SELECT COUNT(*) AS Tong FROM dbo.TacGia;
SELECT TOP 10 MaTacGia, HoTen, NgaySinh, GioiTinh, DienThoai, NgayTao
FROM dbo.TacGia
ORDER BY MaTacGia DESC;
GO

-- ---------------------------------------------------------------------
-- 7. THU TRUOC KHI LAM THAT
-- ---------------------------------------------------------------------
-- Boc cau lenh trong giao dich de thu: ROLLBACK la khong co gi thay doi.
-- Doi ROLLBACK thanh COMMIT khi da chac chan.

BEGIN TRANSACTION;

    DELETE FROM dbo.TacGia WHERE MaTacGia = N'TG0001';
    SELECT COUNT(*) AS SauKhiXoa FROM dbo.TacGia;

ROLLBACK TRANSACTION;

SELECT COUNT(*) AS SauKhiHoanTac FROM dbo.TacGia;
GO
