#nullable disable
namespace QuanLyTacGia.Pages
{
    partial class DashboardPage
    {
        /// <summary>Biến designer bắt buộc.</summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Phương thức bắt buộc để Designer hỗ trợ — không sửa bằng tay, dùng Designer để chỉnh.
        /// </summary>
        private void InitializeComponent()
        {
            body = new System.Windows.Forms.Panel();
            welcomeHost = new System.Windows.Forms.Panel();
            welcome = new AntdUI.Panel();
            avatarUser = new AntdUI.Avatar();
            lblHello = new AntdUI.Label();
            tagRole = new AntdUI.Tag();
            pulse = new QuanLyTacGia.Ui.PulseDot();
            lblStatus = new AntdUI.Label();
            btnAdd = new AntdUI.Button();
            stats = new System.Windows.Forms.TableLayoutPanel();
            card1 = new AntdUI.Panel();
            lblTitle1 = new AntdUI.Label();
            lblNumber1 = new AntdUI.Label();
            lblSub1 = new AntdUI.Label();
            iconBox1 = new AntdUI.Avatar();
            card2 = new AntdUI.Panel();
            lblTitle2 = new AntdUI.Label();
            lblNumber2 = new AntdUI.Label();
            lblSub2 = new AntdUI.Label();
            iconBox2 = new AntdUI.Avatar();
            card3 = new AntdUI.Panel();
            lblTitle3 = new AntdUI.Label();
            lblNumber3 = new AntdUI.Label();
            lblSub3 = new AntdUI.Label();
            iconBox3 = new AntdUI.Avatar();
            card4 = new AntdUI.Panel();
            lblTitle4 = new AntdUI.Label();
            lblNumber4 = new AntdUI.Label();
            lblSub4 = new AntdUI.Label();
            iconBox4 = new AntdUI.Avatar();
            rowA = new System.Windows.Forms.TableLayoutPanel();
            cardMonths = new AntdUI.Panel();
            lblMonths = new AntdUI.Label();
            lblMonthsNote = new AntdUI.Label();
            chartMonths = new QuanLyTacGia.Ui.AreaChart();
            cardGender = new AntdUI.Panel();
            lblGender = new AntdUI.Label();
            lblGenderNote = new AntdUI.Label();
            chartGender = new QuanLyTacGia.Ui.DonutChart();
            rowB = new System.Windows.Forms.TableLayoutPanel();
            cardDecades = new AntdUI.Panel();
            lblDecades = new AntdUI.Label();
            lblDecadesNote = new AntdUI.Label();
            chartDecades = new QuanLyTacGia.Ui.HBarChart();
            recentCard = new AntdUI.Panel();
            lblRecent = new AntdUI.Label();
            btnAll = new AntdUI.Button();
            recent = new AntdUI.Table();
            body.SuspendLayout();
            welcomeHost.SuspendLayout();
            welcome.SuspendLayout();
            stats.SuspendLayout();
            card1.SuspendLayout();
            card2.SuspendLayout();
            card3.SuspendLayout();
            card4.SuspendLayout();
            rowA.SuspendLayout();
            cardMonths.SuspendLayout();
            cardGender.SuspendLayout();
            rowB.SuspendLayout();
            cardDecades.SuspendLayout();
            recentCard.SuspendLayout();
            SuspendLayout();
            //
            // body
            //
            body.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            body.Controls.Add(rowB);
            body.Controls.Add(rowA);
            body.Controls.Add(stats);
            body.Controls.Add(welcomeHost);
            body.Location = new System.Drawing.Point(0, 0);
            body.Name = "body";
            body.Size = new System.Drawing.Size(1200, 940);
            body.TabIndex = 0;
            //
            // welcomeHost
            //
            welcomeHost.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            welcomeHost.Controls.Add(welcome);
            welcomeHost.Dock = System.Windows.Forms.DockStyle.Top;
            welcomeHost.Location = new System.Drawing.Point(0, 0);
            welcomeHost.Name = "welcomeHost";
            welcomeHost.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            welcomeHost.Size = new System.Drawing.Size(1200, 120);
            welcomeHost.TabIndex = 0;
            //
            // welcome
            //
            welcome.Back = System.Drawing.Color.White;
            welcome.BackColor = System.Drawing.Color.White;
            welcome.BorderWidth = 0F;
            welcome.Controls.Add(avatarUser);
            welcome.Controls.Add(lblHello);
            welcome.Controls.Add(tagRole);
            welcome.Controls.Add(pulse);
            welcome.Controls.Add(lblStatus);
            welcome.Controls.Add(btnAdd);
            welcome.Dock = System.Windows.Forms.DockStyle.Fill;
            welcome.Location = new System.Drawing.Point(0, 0);
            welcome.Name = "welcome";
            welcome.Radius = 16;
            welcome.Shadow = 0;
            welcome.Size = new System.Drawing.Size(1200, 104);
            welcome.TabIndex = 0;
            //
            // avatarUser
            //
            avatarUser.ImageFit = AntdUI.TFit.Cover;
            avatarUser.Location = new System.Drawing.Point(24, 24);
            avatarUser.Name = "avatarUser";
            avatarUser.Round = true;
            avatarUser.Size = new System.Drawing.Size(56, 56);
            avatarUser.TabIndex = 0;
            //
            // lblHello
            //
            lblHello.BackColor = System.Drawing.Color.Transparent;
            lblHello.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblHello.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblHello.Location = new System.Drawing.Point(96, 26);
            lblHello.Name = "lblHello";
            lblHello.Size = new System.Drawing.Size(400, 26);
            lblHello.TabIndex = 1;
            lblHello.Text = "Xin chào, Quang Minh";
            //
            // tagRole
            //
            tagRole.BackColor = System.Drawing.Color.FromArgb(211, 229, 255);
            tagRole.BorderWidth = 0F;
            tagRole.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            tagRole.ForeColor = System.Drawing.Color.FromArgb(29, 90, 234);
            tagRole.Location = new System.Drawing.Point(96, 58);
            tagRole.Name = "tagRole";
            tagRole.Radius = 12;
            tagRole.Size = new System.Drawing.Size(108, 24);
            tagRole.TabIndex = 2;
            tagRole.Text = "Quản trị viên";
            tagRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            tagRole.Type = AntdUI.TTypeMini.Default;
            //
            // pulse
            //
            pulse.BackColor = System.Drawing.Color.Transparent;
            pulse.DotColor = System.Drawing.Color.FromArgb(21, 128, 61);
            pulse.Location = new System.Drawing.Point(214, 63);
            pulse.Name = "pulse";
            pulse.Size = new System.Drawing.Size(14, 14);
            pulse.TabIndex = 3;
            //
            // lblStatus
            //
            lblStatus.BackColor = System.Drawing.Color.Transparent;
            lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(21, 128, 61);
            lblStatus.Location = new System.Drawing.Point(232, 61);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new System.Drawing.Size(200, 18);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "Đang hoạt động";
            //
            // btnAdd
            //
            btnAdd.BackExtend = "135, #166BB8, #5047E0";
            btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnAdd.IconSvg = "PlusOutlined";
            btnAdd.Location = new System.Drawing.Point(1028, 34);
            btnAdd.Name = "btnAdd";
            btnAdd.Radius = 8;
            btnAdd.Size = new System.Drawing.Size(148, 36);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Thêm tác giả";
            btnAdd.Type = AntdUI.TTypeMini.Primary;
            //
            // stats
            //
            stats.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            stats.ColumnCount = 7;
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            stats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            stats.Controls.Add(card1, 0, 0);
            stats.Controls.Add(card2, 2, 0);
            stats.Controls.Add(card3, 4, 0);
            stats.Controls.Add(card4, 6, 0);
            stats.Dock = System.Windows.Forms.DockStyle.Top;
            stats.Location = new System.Drawing.Point(0, 120);
            stats.Name = "stats";
            stats.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            stats.RowCount = 1;
            stats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            stats.Size = new System.Drawing.Size(1200, 148);
            stats.TabIndex = 1;
            //
            // card1 — Tổng tác giả (cam)
            //
            card1.Back = System.Drawing.Color.FromArgb(242, 121, 69);
            card1.BorderWidth = 0F;
            card1.Controls.Add(lblTitle1);
            card1.Controls.Add(lblNumber1);
            card1.Controls.Add(lblSub1);
            card1.Controls.Add(iconBox1);
            card1.Cursor = System.Windows.Forms.Cursors.Hand;
            card1.Dock = System.Windows.Forms.DockStyle.Fill;
            card1.Location = new System.Drawing.Point(0, 0);
            card1.Margin = new System.Windows.Forms.Padding(0);
            card1.Name = "card1";
            card1.Radius = 12;
            card1.Shadow = 0;
            card1.Size = new System.Drawing.Size(288, 132);
            card1.TabIndex = 0;
            //
            // lblTitle1
            //
            lblTitle1.BackColor = System.Drawing.Color.Transparent;
            lblTitle1.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTitle1.ForeColor = System.Drawing.Color.White;
            lblTitle1.Location = new System.Drawing.Point(20, 15);
            lblTitle1.Name = "lblTitle1";
            lblTitle1.Size = new System.Drawing.Size(200, 28);
            lblTitle1.TabIndex = 0;
            lblTitle1.Text = "Tổng tác giả";
            //
            // lblNumber1
            //
            lblNumber1.BackColor = System.Drawing.Color.Transparent;
            lblNumber1.Font = new System.Drawing.Font("Segoe UI Semibold", 23F);
            lblNumber1.ForeColor = System.Drawing.Color.White;
            lblNumber1.Location = new System.Drawing.Point(20, 50);
            lblNumber1.Name = "lblNumber1";
            lblNumber1.Size = new System.Drawing.Size(220, 48);
            lblNumber1.TabIndex = 1;
            lblNumber1.Text = "0";
            //
            // lblSub1
            //
            lblSub1.BackColor = System.Drawing.Color.Transparent;
            lblSub1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            lblSub1.ForeColor = System.Drawing.Color.White;
            lblSub1.IconGap = 6;
            lblSub1.IconRatio = 0.85F;
            lblSub1.Location = new System.Drawing.Point(20, 96);
            lblSub1.Name = "lblSub1";
            lblSub1.Size = new System.Drawing.Size(260, 20);
            lblSub1.TabIndex = 2;
            //
            // iconBox1
            //
            iconBox1.BackColor = System.Drawing.Color.FromArgb(255, 213, 194);
            iconBox1.ImageFit = AntdUI.TFit.Contain;
            iconBox1.Location = new System.Drawing.Point(238, 24);
            iconBox1.Name = "iconBox1";
            iconBox1.Radius = 7;
            iconBox1.Round = false;
            iconBox1.Size = new System.Drawing.Size(30, 30);
            iconBox1.TabIndex = 3;
            //
            // card2 — Nam (tím)
            //
            card2.Back = System.Drawing.Color.FromArgb(102, 71, 191);
            card2.BorderWidth = 0F;
            card2.Controls.Add(lblTitle2);
            card2.Controls.Add(lblNumber2);
            card2.Controls.Add(lblSub2);
            card2.Controls.Add(iconBox2);
            card2.Cursor = System.Windows.Forms.Cursors.Hand;
            card2.Dock = System.Windows.Forms.DockStyle.Fill;
            card2.Location = new System.Drawing.Point(304, 0);
            card2.Margin = new System.Windows.Forms.Padding(0);
            card2.Name = "card2";
            card2.Radius = 12;
            card2.Shadow = 0;
            card2.Size = new System.Drawing.Size(288, 132);
            card2.TabIndex = 1;
            //
            // lblTitle2
            //
            lblTitle2.BackColor = System.Drawing.Color.Transparent;
            lblTitle2.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTitle2.ForeColor = System.Drawing.Color.White;
            lblTitle2.Location = new System.Drawing.Point(20, 15);
            lblTitle2.Name = "lblTitle2";
            lblTitle2.Size = new System.Drawing.Size(200, 28);
            lblTitle2.TabIndex = 0;
            lblTitle2.Text = "Nam";
            //
            // lblNumber2
            //
            lblNumber2.BackColor = System.Drawing.Color.Transparent;
            lblNumber2.Font = new System.Drawing.Font("Segoe UI Semibold", 23F);
            lblNumber2.ForeColor = System.Drawing.Color.White;
            lblNumber2.Location = new System.Drawing.Point(20, 50);
            lblNumber2.Name = "lblNumber2";
            lblNumber2.Size = new System.Drawing.Size(220, 48);
            lblNumber2.TabIndex = 1;
            lblNumber2.Text = "0";
            //
            // lblSub2
            //
            lblSub2.BackColor = System.Drawing.Color.Transparent;
            lblSub2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            lblSub2.ForeColor = System.Drawing.Color.White;
            lblSub2.IconGap = 6;
            lblSub2.IconRatio = 0.85F;
            lblSub2.Location = new System.Drawing.Point(20, 96);
            lblSub2.Name = "lblSub2";
            lblSub2.Size = new System.Drawing.Size(260, 20);
            lblSub2.TabIndex = 2;
            //
            // iconBox2
            //
            iconBox2.BackColor = System.Drawing.Color.FromArgb(220, 230, 255);
            iconBox2.ImageFit = AntdUI.TFit.Contain;
            iconBox2.Location = new System.Drawing.Point(238, 24);
            iconBox2.Name = "iconBox2";
            iconBox2.Radius = 7;
            iconBox2.Round = false;
            iconBox2.Size = new System.Drawing.Size(30, 30);
            iconBox2.TabIndex = 3;
            //
            // card3 — Nữ (xanh lá)
            //
            card3.Back = System.Drawing.Color.FromArgb(112, 186, 67);
            card3.BorderWidth = 0F;
            card3.Controls.Add(lblTitle3);
            card3.Controls.Add(lblNumber3);
            card3.Controls.Add(lblSub3);
            card3.Controls.Add(iconBox3);
            card3.Cursor = System.Windows.Forms.Cursors.Hand;
            card3.Dock = System.Windows.Forms.DockStyle.Fill;
            card3.Location = new System.Drawing.Point(608, 0);
            card3.Margin = new System.Windows.Forms.Padding(0);
            card3.Name = "card3";
            card3.Radius = 12;
            card3.Shadow = 0;
            card3.Size = new System.Drawing.Size(288, 132);
            card3.TabIndex = 2;
            //
            // lblTitle3
            //
            lblTitle3.BackColor = System.Drawing.Color.Transparent;
            lblTitle3.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTitle3.ForeColor = System.Drawing.Color.White;
            lblTitle3.Location = new System.Drawing.Point(20, 15);
            lblTitle3.Name = "lblTitle3";
            lblTitle3.Size = new System.Drawing.Size(200, 28);
            lblTitle3.TabIndex = 0;
            lblTitle3.Text = "Nữ";
            //
            // lblNumber3
            //
            lblNumber3.BackColor = System.Drawing.Color.Transparent;
            lblNumber3.Font = new System.Drawing.Font("Segoe UI Semibold", 23F);
            lblNumber3.ForeColor = System.Drawing.Color.White;
            lblNumber3.Location = new System.Drawing.Point(20, 50);
            lblNumber3.Name = "lblNumber3";
            lblNumber3.Size = new System.Drawing.Size(220, 48);
            lblNumber3.TabIndex = 1;
            lblNumber3.Text = "0";
            //
            // lblSub3
            //
            lblSub3.BackColor = System.Drawing.Color.Transparent;
            lblSub3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            lblSub3.ForeColor = System.Drawing.Color.White;
            lblSub3.IconGap = 6;
            lblSub3.IconRatio = 0.85F;
            lblSub3.Location = new System.Drawing.Point(20, 96);
            lblSub3.Name = "lblSub3";
            lblSub3.Size = new System.Drawing.Size(260, 20);
            lblSub3.TabIndex = 2;
            //
            // iconBox3
            //
            iconBox3.BackColor = System.Drawing.Color.FromArgb(220, 230, 255);
            iconBox3.ImageFit = AntdUI.TFit.Contain;
            iconBox3.Location = new System.Drawing.Point(238, 24);
            iconBox3.Name = "iconBox3";
            iconBox3.Radius = 7;
            iconBox3.Round = false;
            iconBox3.Size = new System.Drawing.Size(30, 30);
            iconBox3.TabIndex = 3;
            //
            // card4 — Thêm trong tháng (xanh dương)
            //
            card4.Back = System.Drawing.Color.FromArgb(77, 140, 214);
            card4.BorderWidth = 0F;
            card4.Controls.Add(lblTitle4);
            card4.Controls.Add(lblNumber4);
            card4.Controls.Add(lblSub4);
            card4.Controls.Add(iconBox4);
            card4.Cursor = System.Windows.Forms.Cursors.Hand;
            card4.Dock = System.Windows.Forms.DockStyle.Fill;
            card4.Location = new System.Drawing.Point(912, 0);
            card4.Margin = new System.Windows.Forms.Padding(0);
            card4.Name = "card4";
            card4.Radius = 12;
            card4.Shadow = 0;
            card4.Size = new System.Drawing.Size(288, 132);
            card4.TabIndex = 3;
            //
            // lblTitle4
            //
            lblTitle4.BackColor = System.Drawing.Color.Transparent;
            lblTitle4.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblTitle4.ForeColor = System.Drawing.Color.White;
            lblTitle4.Location = new System.Drawing.Point(20, 15);
            lblTitle4.Name = "lblTitle4";
            lblTitle4.Size = new System.Drawing.Size(200, 28);
            lblTitle4.TabIndex = 0;
            lblTitle4.Text = "Thêm trong tháng";
            //
            // lblNumber4
            //
            lblNumber4.BackColor = System.Drawing.Color.Transparent;
            lblNumber4.Font = new System.Drawing.Font("Segoe UI Semibold", 23F);
            lblNumber4.ForeColor = System.Drawing.Color.White;
            lblNumber4.Location = new System.Drawing.Point(20, 50);
            lblNumber4.Name = "lblNumber4";
            lblNumber4.Size = new System.Drawing.Size(220, 48);
            lblNumber4.TabIndex = 1;
            lblNumber4.Text = "0";
            //
            // lblSub4
            //
            lblSub4.BackColor = System.Drawing.Color.Transparent;
            lblSub4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            lblSub4.ForeColor = System.Drawing.Color.White;
            lblSub4.IconGap = 6;
            lblSub4.IconRatio = 0.85F;
            lblSub4.Location = new System.Drawing.Point(20, 96);
            lblSub4.Name = "lblSub4";
            lblSub4.Size = new System.Drawing.Size(260, 20);
            lblSub4.TabIndex = 2;
            //
            // iconBox4
            //
            iconBox4.BackColor = System.Drawing.Color.FromArgb(220, 230, 255);
            iconBox4.ImageFit = AntdUI.TFit.Contain;
            iconBox4.Location = new System.Drawing.Point(238, 24);
            iconBox4.Name = "iconBox4";
            iconBox4.Radius = 7;
            iconBox4.Round = false;
            iconBox4.Size = new System.Drawing.Size(30, 30);
            iconBox4.TabIndex = 3;
            //
            // rowA — theo tháng + giới tính
            //
            rowA.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            rowA.ColumnCount = 2;
            rowA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rowA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 336F));
            rowA.Controls.Add(cardMonths, 0, 0);
            rowA.Controls.Add(cardGender, 1, 0);
            rowA.Dock = System.Windows.Forms.DockStyle.Top;
            rowA.Location = new System.Drawing.Point(0, 268);
            rowA.Name = "rowA";
            rowA.Padding = new System.Windows.Forms.Padding(0, 0, 0, 16);
            rowA.RowCount = 1;
            rowA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rowA.Size = new System.Drawing.Size(1200, 308);
            rowA.TabIndex = 2;
            //
            // cardMonths
            //
            cardMonths.Back = System.Drawing.Color.White;
            cardMonths.BackColor = System.Drawing.Color.White;
            cardMonths.BorderWidth = 0F;
            cardMonths.Controls.Add(lblMonths);
            cardMonths.Controls.Add(lblMonthsNote);
            cardMonths.Controls.Add(chartMonths);
            cardMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            cardMonths.Location = new System.Drawing.Point(0, 0);
            cardMonths.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            cardMonths.Name = "cardMonths";
            cardMonths.Padding = new System.Windows.Forms.Padding(16, 74, 20, 10);
            cardMonths.Radius = 16;
            cardMonths.Shadow = 0;
            cardMonths.Size = new System.Drawing.Size(848, 292);
            cardMonths.TabIndex = 0;
            //
            // lblMonths
            //
            lblMonths.BackColor = System.Drawing.Color.Transparent;
            lblMonths.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblMonths.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblMonths.Location = new System.Drawing.Point(24, 20);
            lblMonths.Name = "lblMonths";
            lblMonths.Size = new System.Drawing.Size(320, 26);
            lblMonths.TabIndex = 0;
            lblMonths.Text = "Tác giả thêm theo tháng";
            //
            // lblMonthsNote
            //
            lblMonthsNote.BackColor = System.Drawing.Color.Transparent;
            lblMonthsNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblMonthsNote.ForeColor = System.Drawing.Color.FromArgb(111, 118, 137);
            lblMonthsNote.Location = new System.Drawing.Point(24, 46);
            lblMonthsNote.Name = "lblMonthsNote";
            lblMonthsNote.Size = new System.Drawing.Size(320, 18);
            lblMonthsNote.TabIndex = 1;
            lblMonthsNote.Text = "12 tháng gần nhất";
            //
            // chartMonths
            //
            chartMonths.Dock = System.Windows.Forms.DockStyle.Fill;
            chartMonths.Location = new System.Drawing.Point(16, 74);
            chartMonths.Name = "chartMonths";
            chartMonths.Size = new System.Drawing.Size(812, 208);
            chartMonths.TabIndex = 2;
            //
            // cardGender
            //
            cardGender.Back = System.Drawing.Color.White;
            cardGender.BackColor = System.Drawing.Color.White;
            cardGender.BorderWidth = 0F;
            cardGender.Controls.Add(lblGender);
            cardGender.Controls.Add(lblGenderNote);
            cardGender.Controls.Add(chartGender);
            cardGender.Dock = System.Windows.Forms.DockStyle.Fill;
            cardGender.Location = new System.Drawing.Point(864, 0);
            cardGender.Margin = new System.Windows.Forms.Padding(0);
            cardGender.Name = "cardGender";
            cardGender.Padding = new System.Windows.Forms.Padding(16, 74, 16, 10);
            cardGender.Radius = 16;
            cardGender.Shadow = 0;
            cardGender.Size = new System.Drawing.Size(336, 292);
            cardGender.TabIndex = 1;
            //
            // lblGender
            //
            lblGender.BackColor = System.Drawing.Color.Transparent;
            lblGender.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblGender.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblGender.Location = new System.Drawing.Point(24, 20);
            lblGender.Name = "lblGender";
            lblGender.Size = new System.Drawing.Size(288, 26);
            lblGender.TabIndex = 0;
            lblGender.Text = "Phân bố giới tính";
            //
            // lblGenderNote
            //
            lblGenderNote.BackColor = System.Drawing.Color.Transparent;
            lblGenderNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblGenderNote.ForeColor = System.Drawing.Color.FromArgb(111, 118, 137);
            lblGenderNote.Location = new System.Drawing.Point(24, 46);
            lblGenderNote.Name = "lblGenderNote";
            lblGenderNote.Size = new System.Drawing.Size(288, 18);
            lblGenderNote.TabIndex = 1;
            //
            // chartGender
            //
            chartGender.Dock = System.Windows.Forms.DockStyle.Fill;
            chartGender.Location = new System.Drawing.Point(16, 74);
            chartGender.Name = "chartGender";
            chartGender.Size = new System.Drawing.Size(304, 208);
            chartGender.TabIndex = 2;
            //
            // rowB — thập niên sinh + gần nhất
            //
            rowB.BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            rowB.ColumnCount = 2;
            rowB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            rowB.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            rowB.Controls.Add(cardDecades, 0, 0);
            rowB.Controls.Add(recentCard, 1, 0);
            rowB.Dock = System.Windows.Forms.DockStyle.Top;
            rowB.Location = new System.Drawing.Point(0, 576);
            rowB.Name = "rowB";
            rowB.RowCount = 1;
            rowB.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            rowB.Size = new System.Drawing.Size(1200, 364);
            rowB.TabIndex = 3;
            //
            // cardDecades
            //
            cardDecades.Back = System.Drawing.Color.White;
            cardDecades.BackColor = System.Drawing.Color.White;
            cardDecades.BorderWidth = 0F;
            cardDecades.Controls.Add(lblDecades);
            cardDecades.Controls.Add(lblDecadesNote);
            cardDecades.Controls.Add(chartDecades);
            cardDecades.Dock = System.Windows.Forms.DockStyle.Fill;
            cardDecades.Location = new System.Drawing.Point(0, 0);
            cardDecades.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            cardDecades.Name = "cardDecades";
            cardDecades.Padding = new System.Windows.Forms.Padding(16, 74, 20, 10);
            cardDecades.Radius = 16;
            cardDecades.Shadow = 0;
            cardDecades.Size = new System.Drawing.Size(488, 364);
            cardDecades.TabIndex = 0;
            //
            // lblDecades
            //
            lblDecades.BackColor = System.Drawing.Color.Transparent;
            lblDecades.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblDecades.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblDecades.Location = new System.Drawing.Point(24, 20);
            lblDecades.Name = "lblDecades";
            lblDecades.Size = new System.Drawing.Size(320, 26);
            lblDecades.TabIndex = 0;
            lblDecades.Text = "Theo thập niên sinh";
            //
            // lblDecadesNote
            //
            lblDecadesNote.BackColor = System.Drawing.Color.Transparent;
            lblDecadesNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblDecadesNote.ForeColor = System.Drawing.Color.FromArgb(111, 118, 137);
            lblDecadesNote.Location = new System.Drawing.Point(24, 46);
            lblDecadesNote.Name = "lblDecadesNote";
            lblDecadesNote.Size = new System.Drawing.Size(320, 18);
            lblDecadesNote.TabIndex = 1;
            lblDecadesNote.Text = "Số tác giả sinh trong mỗi thập niên";
            //
            // chartDecades
            //
            chartDecades.Dock = System.Windows.Forms.DockStyle.Fill;
            chartDecades.Location = new System.Drawing.Point(16, 74);
            chartDecades.Name = "chartDecades";
            chartDecades.Size = new System.Drawing.Size(452, 280);
            chartDecades.TabIndex = 2;
            //
            // recentCard
            //
            recentCard.Back = System.Drawing.Color.White;
            recentCard.BackColor = System.Drawing.Color.White;
            recentCard.BorderWidth = 0F;
            recentCard.Controls.Add(lblRecent);
            recentCard.Controls.Add(btnAll);
            recentCard.Controls.Add(recent);
            recentCard.Dock = System.Windows.Forms.DockStyle.Fill;
            recentCard.Location = new System.Drawing.Point(504, 0);
            recentCard.Margin = new System.Windows.Forms.Padding(0);
            recentCard.Name = "recentCard";
            recentCard.Padding = new System.Windows.Forms.Padding(0, 64, 0, 8);
            recentCard.Radius = 16;
            recentCard.Shadow = 0;
            recentCard.Size = new System.Drawing.Size(696, 364);
            recentCard.TabIndex = 1;
            //
            // lblRecent
            //
            lblRecent.BackColor = System.Drawing.Color.Transparent;
            lblRecent.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblRecent.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblRecent.Location = new System.Drawing.Point(24, 20);
            lblRecent.Name = "lblRecent";
            lblRecent.Size = new System.Drawing.Size(300, 26);
            lblRecent.TabIndex = 0;
            lblRecent.Text = "Tác giả thêm gần nhất";
            //
            // btnAll
            //
            btnAll.BorderWidth = 0F;
            btnAll.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnAll.ForeColor = System.Drawing.Color.FromArgb(22, 107, 184);
            btnAll.Ghost = true;
            btnAll.IconPosition = AntdUI.TAlignMini.Right;
            btnAll.IconSvg = "RightOutlined";
            btnAll.Location = new System.Drawing.Point(552, 17);
            btnAll.Name = "btnAll";
            btnAll.Radius = 8;
            btnAll.Size = new System.Drawing.Size(124, 32);
            btnAll.TabIndex = 1;
            btnAll.Text = "Xem tất cả";
            btnAll.Type = AntdUI.TTypeMini.Default;
            //
            // recent
            //
            recent.BackColor = System.Drawing.Color.White;
            recent.Bordered = false;
            recent.BorderColor = System.Drawing.Color.FromArgb(242, 244, 245);
            recent.ColumnBack = System.Drawing.Color.White;
            recent.ColumnFont = new System.Drawing.Font("Segoe UI Semibold", 9F);
            recent.ColumnFore = System.Drawing.Color.FromArgb(13, 20, 38);
            recent.Dock = System.Windows.Forms.DockStyle.Fill;
            recent.EmptyText = "Chưa có tác giả";
            recent.EnableHeaderResizing = false;
            recent.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            recent.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            recent.Gap = 12;
            recent.Location = new System.Drawing.Point(0, 64);
            recent.Name = "recent";
            recent.RowHeight = 48;
            recent.RowHeightHeader = 44;
            recent.RowHoverBg = System.Drawing.Color.FromArgb(246, 248, 251);
            recent.RowSelectedBg = System.Drawing.Color.FromArgb(234, 243, 252);
            recent.Size = new System.Drawing.Size(696, 292);
            recent.TabIndex = 2;
            //
            // DashboardPage
            //
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoScroll = true;
            BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            Controls.Add(body);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            Name = "DashboardPage";
            Size = new System.Drawing.Size(1200, 760);
            body.ResumeLayout(false);
            welcomeHost.ResumeLayout(false);
            welcome.ResumeLayout(false);
            stats.ResumeLayout(false);
            card1.ResumeLayout(false);
            card2.ResumeLayout(false);
            card3.ResumeLayout(false);
            card4.ResumeLayout(false);
            rowA.ResumeLayout(false);
            cardMonths.ResumeLayout(false);
            cardGender.ResumeLayout(false);
            rowB.ResumeLayout(false);
            cardDecades.ResumeLayout(false);
            recentCard.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Panel welcomeHost;
        private AntdUI.Panel welcome;
        private AntdUI.Avatar avatarUser;
        private AntdUI.Label lblHello;
        private AntdUI.Tag tagRole;
        private QuanLyTacGia.Ui.PulseDot pulse;
        private AntdUI.Label lblStatus;
        private AntdUI.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel stats;
        private AntdUI.Panel card1;
        private AntdUI.Label lblTitle1;
        private AntdUI.Label lblNumber1;
        private AntdUI.Label lblSub1;
        private AntdUI.Avatar iconBox1;
        private AntdUI.Panel card2;
        private AntdUI.Label lblTitle2;
        private AntdUI.Label lblNumber2;
        private AntdUI.Label lblSub2;
        private AntdUI.Avatar iconBox2;
        private AntdUI.Panel card3;
        private AntdUI.Label lblTitle3;
        private AntdUI.Label lblNumber3;
        private AntdUI.Label lblSub3;
        private AntdUI.Avatar iconBox3;
        private AntdUI.Panel card4;
        private AntdUI.Label lblTitle4;
        private AntdUI.Label lblNumber4;
        private AntdUI.Label lblSub4;
        private AntdUI.Avatar iconBox4;
        private System.Windows.Forms.TableLayoutPanel rowA;
        private AntdUI.Panel cardMonths;
        private AntdUI.Label lblMonths;
        private AntdUI.Label lblMonthsNote;
        private QuanLyTacGia.Ui.AreaChart chartMonths;
        private AntdUI.Panel cardGender;
        private AntdUI.Label lblGender;
        private AntdUI.Label lblGenderNote;
        private QuanLyTacGia.Ui.DonutChart chartGender;
        private System.Windows.Forms.TableLayoutPanel rowB;
        private AntdUI.Panel cardDecades;
        private AntdUI.Label lblDecades;
        private AntdUI.Label lblDecadesNote;
        private QuanLyTacGia.Ui.HBarChart chartDecades;
        private AntdUI.Panel recentCard;
        private AntdUI.Label lblRecent;
        private AntdUI.Button btnAll;
        private AntdUI.Table recent;
    }
}
