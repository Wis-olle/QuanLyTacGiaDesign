# Quản lý tác giả — bản KÉO THẢ (WinForms .NET 10 + AntdUI, Windows Forms Designer)

Cùng giao diện và chức năng với `../QuanLyTacGia` (bản dựng bằng code), nhưng **mọi màn hình đều có file `*.Designer.cs`** nên mở được trong Windows Forms Designer: kéo control từ Toolbox, chỉnh trong Properties, nhấp đôi tạo sự kiện — đúng cách xây giao diện bằng Toolbox. Dự án gốc giữ nguyên, không sửa.

Dữ liệu nằm trên **SQL Server** (CSDL `QuanLyTacGia`, 48 tác giả mẫu): chạy `sql/QuanLyTacGia.sql` một lần rồi F5 — chi tiết và cách đổi chuỗi kết nối xem [KET_NOI_CSDL.md](KET_NOI_CSDL.md). Phong cách, tài liệu thiết kế và review dùng chung: `../PHONG_CACH_THIET_KE.md`, `../KIEN_TRUC_MAN_HINH.md`, `../MAU_THIET_KE.md`.

## Tải về và chạy
Mã nguồn: https://github.com/Wis-olle/QuanLyTacGiaDesign

```bash
git clone https://github.com/Wis-olle/QuanLyTacGiaDesign.git
cd QuanLyTacGiaDesign
```

Mở `QuanLyTacGiaDesign.slnx` (hoặc `QuanLyTacGiaDesign.csproj`) bằng Visual Studio 2022 17.14+ / VS 2026 (hỗ trợ .NET 10) rồi F5, hoặc chạy bằng dòng lệnh:

```bash
dotnet run --project QuanLyTacGiaDesign.csproj
```

Yêu cầu: .NET 10 SDK; gói AntdUI 2.4.8 tự tải từ NuGet khi build lần đầu.

## Mở màn hình trong Designer (kéo thả)
1. **Build** dự án một lần (Designer của .NET chạy ngoài tiến trình, cần assembly đã build để nạp control AntdUI và control tự viết vào Toolbox).
2. Solution Explorer → nhấp đôi file màn hình (bảng dưới), hoặc chọn file rồi **Shift+F7**. Lần đầu mở mất vài giây.
3. Toolbox có nhóm **AntdUI** (Panel, Label, Input, Button, Table, Avatar, Tag, Segmented…), **QuanLyTacGiaDesign Components** (NavItem, PulseDot, HeaderView, AreaChart, DonutChart, HBarChart, TacGiaCardView) và **All Windows Forms**. Toolbox chỉ hiện control khi một designer đang mở; mở file .cs / .csproj thường thì trống là bình thường. Nếu vẫn trống: chuột phải Toolbox → **Reset Toolbox**, hoặc Build lại rồi đóng/mở designer.
4. Toạ độ / kích thước trong Designer là **px 96 DPI** (`ForceDesignerDPIUnaware` giữ designer ở 100 %).

| Màn hình | Mở file | Designer cho thấy | Phần còn ở code-behind (Designer không diễn tả được) |
|----------|---------|-------------------|------------------------------------------------------|
| Shell | `MainForm.cs` (+ `.Designer.cs`) | thanh tiêu đề PageHeader, sidebar, vùng nội dung | wordmark Plus Jakarta Sans (font nạp từ file), logo mark vẽ tay, điều hướng trang |
| Sidebar | `Ui/SidebarNav.cs` | 2 nhóm, 3 `NavItem` (ItemId / Caption / IconSvg trong Properties), vạch chỉ báo, người dùng, Đăng xuất | vạch trượt tới mục chọn (motion) |
| P1 Tổng quan | `Pages/DashboardPage.cs` | thẻ chào, 4 thẻ số liệu (màu phẳng), 3 thẻ biểu đồ, bảng gần nhất | hoạ tiết nền thẻ + hover + hiện dần, chữ trong thẻ đặt theo % chiều cao, chia chiều cao 2 hàng biểu đồ (`Relayout`), số liệu / cột bảng / đếm số / biểu đồ vẽ dần |
| P2 Tác giả | `Pages/TacGiaListPage.cs` | header (tiêu đề, avatar group, tab, chip tháng, tìm kiếm, toggle, Thêm, ⋯), bảng, lưới thẻ, phân trang, 2 Timer | mục của Segmented / Dropdown, cột bảng, lọc, ẩn cột theo bề rộng, mở D1 / M1 / M2 |
| P3 Cài đặt | `Pages/SettingsPage.cs` | thẻ, 2 nhãn + 2 ô, nút Lưu | giá trị mặc định, toast |
| D1 Drawer chi tiết | `Panels/TacGiaDetailPanel.cs` | `HeaderView` (banner + avatar + ×), tên, chip mã, 5 hàng nhãn / giá trị / vạch, 2 nút | đổ dữ liệu |
| M1 Thêm / Sửa | `Dialogs/TacGiaEditControl.cs` | avatar + 2 nút, 7 ô nhập theo lưới 2 cột (nhãn · ô · dòng lỗi) | dữ liệu, validate, popover chọn avatar mẫu |

Không dùng hàm tự viết (`S()`, `AppTheme.*`) trong `*.Designer.cs`: Designer ghi đè file đó mỗi lần kéo thả và chỉ giữ được hằng số. Muốn thêm màn hình mới: Add → User Control (Windows Forms), rồi thêm `NavItem` trong `SidebarNav` (Designer) và nhánh `switch` trong `MainForm.Navigate`.

## Cấu trúc
| File | Vai trò |
|------|---------|
| `MainForm.cs` + `MainForm.Designer.cs` | Shell |
| `Ui/SidebarNav.cs` + `.Designer.cs`, `Ui/NavItem.cs` | Sidebar; `NavItem` là control kéo thả được |
| `Pages/*.cs` + `*.Designer.cs` | P1 / P2 / P3; `ListPreset.cs` (bộ lọc nhanh), `TacGiaRow.cs` (dòng bảng), `TacGiaCardView.cs` (lưới thẻ tự vẽ) |
| `Panels/TacGiaDetailPanel.cs` + `.Designer.cs`, `Panels/HeaderView.cs` | D1 |
| `Dialogs/TacGiaEditControl.cs` + `.Designer.cs` | M1 |
| `Ui/AppTheme.cs` | Token màu / chữ, `K` cho phần vẽ tay, khởi tạo theme AntdUI |
| `Ui/VietLocalization.cs` | Nhãn tiếng Việt cho AntdUI (lịch bố cục quốc tế, "Hôm nay", "Huỷ"…) — mặc định AntdUI là tiếng Trung |
| `Ui/Charts.cs`, `Ui/GradientCard.cs`, `Ui/Motion.cs`, `Ui/PulseDot.cs`, `Ui/Brand.cs` | Biểu đồ tự vẽ, hoạ tiết thẻ số liệu, motion, chấm nhịp đập, logo |
| `Ui/ScreenshotHarness.cs` | Tự chụp 22 màn hình để review (`QLTG_SHOT=<thư mục>`) |
| `Data/Db.cs` | ADO.NET: SELECT / INSERT / UPDATE / DELETE trên SQL Server, chuỗi kết nối |
| `sql/QuanLyTacGia.sql` | Tạo CSDL + bảng `TacGia` + 48 tác giả mẫu |
| `Assets/`, `tools/`, `Models/` | Như bản gốc |

## DPI / nhiều màn hình
`PerMonitorV2` + **AutoScaleMode.Dpi** chuẩn của WinForms trên form và các trang (`AutoScaleDimensions = 96×96`): bố cục designer tự nhân theo DPI màn, kể cả khi kéo sang màn khác (WinForms xử lý WM_DPICHANGED). MainForm tắt `AutoHandDpi` của AntdUI để không nhân hai lần. Nội dung Drawer / Modal (`TacGiaDetailPanel`, `TacGiaEditControl`) để `AutoScaleMode.None` vì AntdUI đã tự nhân Dpi cho content.

`AppTheme.K` (= DPI/96, đặt trong `MainForm` lúc mở và khi đổi DPI) chỉ dùng cho **phần vẽ tay** (biểu đồ, thẻ tác giả, hoạ tiết, HeaderView) và tính toán lúc chạy (`Relayout`, ngưỡng ẩn cột). Control tự viết đặt kích thước trong constructor bằng px 96 thuần (WinForms nhân sau), không dùng `S()`.

Đã kiểm chứng bằng harness ở 96 DPI: 26 ảnh trùng bản gốc (khác < 0,5 % điểm ảnh, chỉ ở trạng thái hover / thời điểm animation).

**Không dùng `Anchor` Bottom / Right cho control cần bám mép** (khối avatar + Đăng xuất ở đáy sidebar, nút "Thêm tác giả", "Xem tất cả", ô tìm kiếm, toggle bảng/thẻ): WinForms tính sai khoảng neo khi DPI ≠ 100 % (người dùng gặp trên màn 8.8 inch: avatar trôi lên sát "Cài đặt"). Các control này để `Anchor` mặc định trong Designer và được đặt vị trí trong code-behind theo kích thước thật (`OnResize` / `Resize`, offset × `AppTheme.K`). Muốn thêm control bám mép: làm cùng cách.

## Chụp ảnh kiểm tra giao diện
```bash
QLTG_SHOT="C:/tmp/shots" ./bin/Debug/net10.0-windows/QuanLyTacGiaDesign.exe
```
