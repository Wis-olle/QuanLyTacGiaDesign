# Kết nối app với SQL Server

App đọc / ghi bảng `dbo.TacGia` trong CSDL `QuanLyTacGia` bằng **ADO.NET thuần** (`SqlConnection`, `SqlCommand`, `SqlDataReader`) — không dùng Entity Framework, để nhìn rõ từng câu lệnh SQL.

| Thành phần | File |
|------------|------|
| Script tạo CSDL + 48 tác giả mẫu | `sql/QuanLyTacGia.sql` |
| Câu lệnh mẫu thêm / sửa / xoá bằng SQL | `sql/ThemSuaXoa.sql` |
| Chuỗi kết nối và 4 câu lệnh SQL | `Data/Db.cs` |
| Danh sách đang mở trong bộ nhớ | `Data/SampleData.cs` |
| Nạp lúc khởi động, báo lỗi nếu hỏng | `Program.cs` |
| Gói NuGet `Microsoft.Data.SqlClient` 6.1.7 | `*.csproj` |

---

## Bước 1 — Tạo CSDL

Mở **SQL Server Management Studio** (SSMS) → nối tới máy chủ của máy mình → `File ▸ Open ▸ File…` → chọn `sql\QuanLyTacGia.sql` → nhấn **Execute** (F5).

Hoặc chạy bằng dòng lệnh:

```bash
sqlcmd -S localhost -E -C -i sql\QuanLyTacGia.sql
```

Chạy xong in ra `SoTacGia = 48`. Script tạo CSDL nếu chưa có, **xoá và tạo lại bảng** `TacGia` rồi nạp 48 tác giả mẫu, nên chạy lại là mất dữ liệu tự thêm.

Bảng `dbo.TacGia`:

| Cột | Kiểu | Ghi chú |
|-----|------|---------|
| `MaTacGia` | `NVARCHAR(10)` | khoá chính, dạng `TG0001` |
| `HoTen` | `NVARCHAR(100)` | `NVARCHAR` để giữ dấu tiếng Việt |
| `NgaySinh` | `DATE` | |
| `GioiTinh` | `NVARCHAR(5)` | ràng buộc `CHECK` chỉ nhận `Nam` / `Nữ` |
| `DienThoai` | `VARCHAR(15)` | |
| `Email` | `NVARCHAR(100)` | |
| `DiaChi` | `NVARCHAR(200)` | |
| `Avatar` | `VARBINARY(MAX)` | ảnh PNG; `NULL` thì app lấy ảnh mẫu trong `Assets/avatars` |
| `NgayTao` | `DATE` | mặc định ngày hôm nay |

---

## Bước 2 — Chuỗi kết nối

Chuỗi kết nối là **một dòng chữ** nói cho app ba điều: nối tới máy chủ nào, mở CSDL nào, đăng nhập kiểu gì. Sai một trong ba là app không mở được.

Nó nằm ở `Data/Db.cs`, ngay đầu lớp `Db` (khoảng dòng 21):

```csharp
public static string ConnectionString { get; set; } =
    Environment.GetEnvironmentVariable("QLTG_SQL")
    ?? @"Server=localhost;Database=QuanLyTacGia;Trusted_Connection=True;TrustServerCertificate=True;";
```

Dòng giữa nghĩa là: máy nào có biến môi trường `QLTG_SQL` thì dùng giá trị đó, không có thì dùng chuỗi mặc định ở dòng dưới. Bình thường bạn chỉ cần quan tâm chuỗi mặc định.

### 2.1. Tìm tên máy chủ của máy mình

Đây là chỗ sai nhiều nhất, vì mỗi máy cài SQL Server một kiểu. Có ba cách tra, dùng cách nào cũng được.

**Cách 1, nhanh nhất, xem trong SSMS.** Mở SQL Server Management Studio. Hộp thoại Connect to Server hiện lên, ô **Server name** đã điền sẵn đúng tên máy chủ trên máy bạn. Bôi đen và chép lại chính cái đó.

**Cách 2, xem tên dịch vụ.** Nhấn `Windows + R`, gõ `services.msc`, Enter. Tìm dòng bắt đầu bằng "SQL Server":

| Tên dịch vụ nhìn thấy | Nghĩa là | Điền vào `Server=` |
|-----------------------|----------|--------------------|
| `SQL Server (MSSQLSERVER)` | thực thể mặc định, không có tên riêng | `localhost` |
| `SQL Server (SQLEXPRESS)` | thực thể tên `SQLEXPRESS` | `localhost\SQLEXPRESS` |
| `SQL Server (TENKHAC)` | thực thể tên `TENKHAC` | `localhost\TENKHAC` |

Cột Status của dịch vụ phải là **Running**. Nếu trống, nhấp phải chọn Start, và nên đổi Startup Type thành Automatic để lần sau máy tự bật.

**Cách 3, hỏi thẳng SQL Server.** Mở Command Prompt và gõ:

```bash
sqlcmd -S localhost -E -C -Q "SELECT @@SERVERNAME, SERVERPROPERTY('InstanceName')"
```

Chạy được và cột thứ hai in ra `NULL` nghĩa là thực thể mặc định, cứ để `localhost`. Câu lệnh báo lỗi thì thử lại với `-S localhost\SQLEXPRESS`.

### 2.2. Sửa dòng chuỗi kết nối

Trong Visual Studio, Solution Explorer, mở thư mục `Data` rồi nhấp đôi `Db.cs`. Sửa phần sau chữ `Server=` cho khớp máy bạn. Ví dụ máy dùng bản Express có tên thực thể:

```csharp
?? @"Server=localhost\SQLEXPRESS;Database=QuanLyTacGia;Trusted_Connection=True;TrustServerCertificate=True;";
```

**Chú ý dấu gạch ngược trong C#.** Chuỗi ở đây có ký tự `@` đứng trước nên viết một dấu gạch là đúng. Nếu bạn xoá mất `@` thì phải viết hai dấu, không thì trình biên dịch báo lỗi *Unrecognized escape sequence*.

```csharp
@"Server=localhost\SQLEXPRESS;..."     // có @, một dấu gạch
 "Server=localhost\\SQLEXPRESS;..."    // không @, hai dấu gạch
```

Các khoá ngăn nhau bằng dấu chấm phẩy, thừa hay thiếu một dấu là hỏng. Không cần khoảng trắng quanh dấu bằng.

### 2.3. Ý nghĩa từng khoá

| Khoá | Ý nghĩa | Sai thì gặp lỗi gì |
|------|---------|--------------------|
| `Server` | tên máy chủ, kèm tên thực thể sau dấu gạch ngược nếu có | *A network-related or instance-specific error* |
| `Database` | tên CSDL mở lúc nối, phải khớp CSDL đã tạo ở Bước 1 | *Cannot open database "…"* |
| `Trusted_Connection=True` | đăng nhập bằng chính tài khoản Windows đang dùng, không cần user và password | *Login failed for user* |
| `TrustServerCertificate=True` | chấp nhận chứng chỉ tự ký của SQL Server cài trên máy cá nhân | *The certificate chain was issued by an authority that is not trusted* |

Khoá cuối **bắt buộc phải có** với dự án này. Gói `Microsoft.Data.SqlClient` từ phiên bản 4 trở đi mặc định bật mã hoá đường truyền và đòi chứng chỉ hợp lệ. SQL Server cài trên máy cá nhân chỉ có chứng chỉ tự ký, nên thiếu dòng này là nối không được, dù mọi thứ khác đã đúng.

### 2.4. Hai kiểu đăng nhập

**Windows Authentication**, là kiểu dự án đang dùng. Bạn đăng nhập Windows bằng tài khoản nào thì SQL Server nhận tài khoản đó. Không có mật khẩu nào nằm trong mã nguồn. Dùng được khi SQL Server cài trên chính máy bạn, hoặc máy chủ trong mạng nội bộ đã cấp quyền cho tài khoản Windows của bạn.

**SQL Server Authentication**, dùng khi máy chủ đặt ở nơi khác và giáo viên cấp cho bạn một tài khoản riêng. Thay `Trusted_Connection` bằng hai khoá user và password:

```text
Server=localhost;Database=QuanLyTacGia;User Id=sa;Password=matkhau;TrustServerCertificate=True;
```

Kiểu này chỉ chạy khi máy chủ đã bật chế độ hỗn hợp. Bật bằng SSMS: nhấp phải tên máy chủ, chọn Properties, mục Security, chọn **SQL Server and Windows Authentication mode**, bấm OK rồi khởi động lại dịch vụ SQL Server.

Mật khẩu nằm thẳng trong mã nguồn là điều nên tránh khi làm thật. Với bài tập trên máy cá nhân thì Windows Authentication gọn và an toàn hơn.

### 2.5. Thử chuỗi trước khi chạy app

Thử riêng chuỗi kết nối nhanh hơn là chạy cả app rồi đoán. Mở Command Prompt, thay phần sau `-S` bằng tên máy chủ của bạn:

```bash
sqlcmd -S localhost -d QuanLyTacGia -E -C -Q "SELECT COUNT(*) FROM dbo.TacGia"
```

In ra số 48 nghĩa là tên máy chủ đó đúng, đem nguyên nó điền vào `Db.cs`. Báo lỗi thì đọc câu lỗi rồi tra ở mục Lỗi thường gặp cuối tài liệu.

Ý nghĩa các tham số: `-S` máy chủ, `-d` tên CSDL, `-E` dùng Windows Authentication, `-C` tin chứng chỉ tự ký, `-Q` chạy một câu lệnh rồi thoát.

### 2.6. Cách không phải sửa mã nguồn

Nếu không muốn đụng vào `Db.cs`, ví dụ khi mỗi máy trong nhóm một tên máy chủ khác nhau, hãy đặt biến môi trường `QLTG_SQL`. App ưu tiên dùng giá trị đó.

Mở Command Prompt, chạy một lần rồi mở lại Visual Studio:

```bash
setx QLTG_SQL "Server=localhost\SQLEXPRESS;Database=QuanLyTacGia;Trusted_Connection=True;TrustServerCertificate=True;"
```

Biến này lưu vĩnh viễn cho tài khoản Windows của bạn. Muốn bỏ thì chạy `setx QLTG_SQL ""`. Phải khởi động lại Visual Studio thì nó mới đọc được biến mới.

---

## Bước 3 — Build và chạy

### 3.1. Gói NuGet

Dự án cần gói `Microsoft.Data.SqlClient` để nói chuyện với SQL Server. Gói đã khai báo sẵn trong file `.csproj`, bạn không phải cài tay:

```xml
<PackageReference Include="Microsoft.Data.SqlClient" Version="6.1.7" />
```

Lần build đầu tiên Visual Studio tự tải gói về, lúc đó máy cần nối mạng. Những lần sau không cần nữa vì gói đã nằm trong bộ nhớ đệm của máy.

Kiểm tra gói đã về chưa: Solution Explorer, mở nhánh **Dependencies**, mở tiếp **Packages**, phải thấy hai dòng `AntdUI` và `Microsoft.Data.SqlClient`. Dòng nào có dấu chấm than vàng là chưa tải được. Nhấp phải tên dự án chọn **Restore NuGet Packages**, hoặc chạy `dotnet restore` trong Terminal.

### 3.2. Chạy

Nhấn **F5** để chạy có gỡ lỗi, hoặc **Ctrl + F5** để chạy suông cho nhanh.

Chạy đúng thì cửa sổ app mở ra ở trang Tổng quan, các con số đếm lên, trang Tác giả có đủ 48 người. Dữ liệu đó lấy từ SQL Server chứ không còn nằm trong mã nguồn.

### 3.3. Khi nối hỏng

App không im lặng rồi hiện danh sách rỗng. Nó hiện một hộp thoại đỏ gồm ba phần: câu lỗi gốc do SQL Server trả về, chuỗi kết nối app đang dùng, và lời nhắc chạy lại script. Bấm OK là app tự đóng.

Cách đọc hộp thoại đó:

1. Nhìn dòng **chuỗi kết nối đang dùng**. Nếu nó khác cái bạn vừa sửa thì hoặc máy còn biến môi trường `QLTG_SQL` cũ đè lên, hoặc bạn sửa nhầm dự án, vì bản gốc và bản Design là hai thư mục riêng.
2. Chép **câu lỗi gốc** rồi tra ở bảng Lỗi thường gặp cuối tài liệu.
3. Sửa xong chạy lại. Không cần build lại bằng tay, F5 tự build.

---

## App đọc / ghi như thế nào

`Program.Main` gọi `SampleData.Load()` một lần lúc khởi động → `Db.LoadAll()` đọc hết bảng vào `SampleData.TacGias`. Giao diện (danh sách, tìm kiếm, biểu đồ Tổng quan) làm việc trên danh sách trong bộ nhớ cho nhanh.

Mỗi lần thêm / sửa / xoá thì **ghi xuống CSDL trước, đổi danh sách sau** (`Pages/TacGiaListPage.cs`):

```csharp
if (t == null) { Db.Insert(saved); SampleData.TacGias.Add(saved); }
else Db.Update(saved);
```

```csharp
Db.Delete(t.MaTacGia);
SampleData.TacGias.Remove(t);
```

Ghi hỏng thì hiện toast đỏ và **không** đổi danh sách, tránh chuyện màn hình báo đã lưu mà CSDL không có.

Bốn câu lệnh trong `Data/Db.cs`:

```csharp
SELECT MaTacGia, HoTen, ... FROM dbo.TacGia ORDER BY MaTacGia
INSERT INTO dbo.TacGia (...) VALUES (@Ma, @HoTen, ...)
UPDATE dbo.TacGia SET HoTen = @HoTen, ... WHERE MaTacGia = @Ma
DELETE FROM dbo.TacGia WHERE MaTacGia = @Ma
```

Mọi giá trị đi qua `cmd.Parameters` chứ không nối chuỗi — đây là cách chặn SQL injection.

Ảnh đại diện: `Image` → `byte[]` PNG khi lưu, `byte[]` → `Bitmap` khi đọc (hai hàm `ToBytes` / `ToImage` cuối `Db.cs`).

---

## Kiểm tra nhanh

Harness chụp màn hình có sẵn phép thử vòng đời CSDL: thêm `TG9999` → đọc lại → sửa → xoá.

```bash
QLTG_SHOT="C:/tmp/shots" ./bin/Debug/net10.0-windows/QuanLyTacGiaDesign.exe
```

Xem dòng cuối `C:/tmp/shots/log.txt`, phải là `db-check OK`.

Đếm bằng SQL:

```sql
USE QuanLyTacGia;
SELECT COUNT(*) FROM dbo.TacGia;
SELECT TOP 5 MaTacGia, HoTen, GioiTinh FROM dbo.TacGia ORDER BY NgayTao DESC;
```

---

## Lỗi thường gặp

| Câu lỗi | Nguyên nhân | Cách sửa |
|---------|-------------|----------|
| *A network-related or instance-specific error* | sai tên máy chủ, hoặc dịch vụ SQL chưa chạy | mở `services.msc` bật `SQL Server (MSSQLSERVER)`; thử `localhost\SQLEXPRESS` |
| *The certificate chain was issued by an authority that is not trusted* | thiếu `TrustServerCertificate=True` | thêm vào chuỗi kết nối |
| *Cannot open database "QuanLyTacGia"* | chưa chạy script | làm lại Bước 1 |
| *Login failed for user …* | tài khoản Windows chưa có quyền | trong SSMS: `Security ▸ Logins`, thêm tài khoản Windows, gán `db_owner` trên `QuanLyTacGia` |
| Chữ tiếng Việt thành `???` | cột để `VARCHAR`, hoặc chuỗi SQL thiếu tiền tố `N` | dùng `NVARCHAR` và `N'…'` như trong script |
| *Violation of PRIMARY KEY constraint* | thêm trùng mã tác giả | mã sinh tự động ở `SampleData.NextMa()`; kiểm tra bảng có mã lạ không |

---

## Thêm / sửa / xoá thẳng trong SSMS

Không qua giao diện app mà gõ SQL: mở `sql/ThemSuaXoa.sql`, bôi đen đoạn cần chạy rồi F5. File có sẵn mẫu thêm một dòng, thêm nhiều dòng, tự sinh mã kế tiếp, sửa, xoá, và cách bọc giao dịch để thử trước.

Ba điều dễ sai:

- **Thiếu tiền tố `N`** trước chuỗi tiếng Việt thì chữ có dấu thành `???`. Viết `N'Nguyễn Văn A'`, không viết `'Nguyễn Văn A'`.
- **Thiếu `WHERE`** ở `UPDATE` / `DELETE` là sửa hoặc xoá sạch cả bảng. Chạy thử bằng `SELECT` cùng điều kiện trước cho chắc.
- **App chỉ nạp dữ liệu một lần lúc khởi động.** Thêm dòng trong SSMS xong phải đóng app rồi mở lại (F5 trong Visual Studio) mới thấy.

Mã tác giả là khoá chính nên không được trùng. Thêm tay mã `TG0049` trong lúc app đang mở thì app cũng đang định lấy `TG0049` cho lần thêm kế tiếp, lưu sẽ báo trùng khoá; mở lại app là hết.

Cột `GioiTinh` có ràng buộc `CHECK` chỉ nhận đúng `Nam` hoặc `Nữ`, viết khác là câu lệnh bị từ chối.

## Muốn tự viết thêm câu lệnh

Thêm hàm vào `Data/Db.cs` theo đúng khuôn 5 dòng:

```csharp
public static int DemTheoGioiTinh(string gioiTinh)
{
    using var cn = Open();
    using var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.TacGia WHERE GioiTinh = @gt", cn);
    cmd.Parameters.AddWithValue("@gt", gioiTinh);
    return (int)cmd.ExecuteScalar()!;
}
```

`using` tự đóng kết nối khi hàm kết thúc, nên không cần `cn.Close()`.
