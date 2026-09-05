<#
.SYNOPSIS
  Tải bộ 48 avatar mặc định cho tác giả từ DiceBear (style "notionists", giấy phép CC0 1.0).

.DESCRIPTION
  Nguồn: https://www.dicebear.com/styles/notionists/  (API: https://api.dicebear.com/9.x/notionists/png)
  Mỗi avatar = seed "TG0001".."TG0048" + 1 trong 5 màu nền pastel, kích cỡ 128 px.
  Kết quả ghi vào Assets/avatars/notionists-01.png .. notionists-48.png (tên trung tính, không gắn với mã tác giả).
  Avatar của quản trị viên (Assets/avatar-quangminh.png, seed "QuangMinh") KHÔNG thuộc bộ này — chỉ tài khoản đăng nhập dùng.

.EXAMPLE
  powershell -ExecutionPolicy Bypass -File tools\Get-Avatars.ps1
  powershell -File tools\Get-Avatars.ps1 -Size 256   # tải lại độ phân giải cao hơn
#>
param(
    [int]$Size = 128,
    [string]$OutDir = (Join-Path $PSScriptRoot '..\Assets\avatars')
)

# 5 màu nền pastel (khớp token InfoSoft / PinkSoft / SuccessSoft + vàng + tím nhạt của app); thứ tự theo file đã dùng trong app
$Backgrounds = @(
    'fef3c7','fef3c7','fce7f3','fce7f3','e3f1fa','dcfce7','ede9fe','fef3c7','ede9fe','dcfce7',
    'fce7f3','fce7f3','fef3c7','e3f1fa','dcfce7','dcfce7','e3f1fa','e3f1fa','fce7f3','fef3c7',
    'ede9fe','fef3c7','e3f1fa','e3f1fa','fef3c7','fce7f3','e3f1fa','fce7f3','ede9fe','e3f1fa',
    'dcfce7','e3f1fa','e3f1fa','e3f1fa','fce7f3','ede9fe','ede9fe','e3f1fa','fef3c7','e3f1fa',
    'fef3c7','e3f1fa','ede9fe','dcfce7','fef3c7','dcfce7','fce7f3','dcfce7'
)

New-Item -ItemType Directory -Force $OutDir | Out-Null
for ($i = 1; $i -le 48; $i++) {
    $seed = 'TG{0:0000}' -f $i
    $bg = $Backgrounds[$i - 1]
    $url = "https://api.dicebear.com/9.x/notionists/png?seed=$seed&size=$Size&backgroundColor=$bg"
    $out = Join-Path $OutDir ('notionists-{0:00}.png' -f $i)
    Invoke-WebRequest -UseBasicParsing -Uri $url -OutFile $out -TimeoutSec 60
    Write-Host ("{0} <- {1}" -f (Split-Path $out -Leaf), $url)
}
Write-Host "Xong: 48 avatar $Size px trong $OutDir"
