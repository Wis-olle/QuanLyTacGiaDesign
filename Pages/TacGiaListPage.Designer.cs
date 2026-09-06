#nullable disable
namespace QuanLyTacGia.Pages
{
    partial class TacGiaListPage
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
            components = new System.ComponentModel.Container();
            card = new AntdUI.Panel();
            cards = new QuanLyTacGia.Pages.TacGiaCardView();
            table = new AntdUI.Table();
            footer = new System.Windows.Forms.Panel();
            pager = new AntdUI.Pagination();
            header = new System.Windows.Forms.Panel();
            lblTitle = new AntdUI.Label();
            stack1 = new AntdUI.Avatar();
            stack2 = new AntdUI.Avatar();
            stack3 = new AntdUI.Avatar();
            stack4 = new AntdUI.Avatar();
            stack5 = new AntdUI.Avatar();
            stackMore = new AntdUI.Tag();
            lblCount = new AntdUI.Label();
            segGioiTinh = new AntdUI.Segmented();
            chipMonth = new AntdUI.Button();
            txtSearch = new AntdUI.Input();
            segView = new AntdUI.Segmented();
            right = new System.Windows.Forms.FlowLayoutPanel();
            btnAdd = new AntdUI.Button();
            btnMore = new AntdUI.Dropdown();
            debounce = new System.Windows.Forms.Timer(components);
            resizeDebounce = new System.Windows.Forms.Timer(components);
            card.SuspendLayout();
            footer.SuspendLayout();
            header.SuspendLayout();
            right.SuspendLayout();
            SuspendLayout();
            //
            // card
            //
            card.Back = System.Drawing.Color.White;
            card.BackColor = System.Drawing.Color.White;
            card.BorderWidth = 0F;
            card.Controls.Add(cards);
            card.Controls.Add(table);
            card.Controls.Add(footer);
            card.Controls.Add(header);
            card.Dock = System.Windows.Forms.DockStyle.Fill;
            card.Location = new System.Drawing.Point(0, 0);
            card.Name = "card";
            card.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            card.Radius = 16;
            card.Shadow = 0;
            card.Size = new System.Drawing.Size(1200, 760);
            card.TabIndex = 0;
            //
            // cards — lưới thẻ (mặc định), tự vẽ từng thẻ
            //
            cards.AutoScroll = true;
            cards.BackColor = System.Drawing.Color.White;
            cards.Dock = System.Windows.Forms.DockStyle.Fill;
            cards.Location = new System.Drawing.Point(0, 118);
            cards.Name = "cards";
            cards.Padding = new System.Windows.Forms.Padding(16, 4, 16, 8);
            cards.Size = new System.Drawing.Size(1200, 586);
            cards.TabIndex = 0;
            cards.Visible = false;
            //
            // table
            //
            table.BackColor = System.Drawing.Color.White;
            table.Bordered = false;
            table.BorderColor = System.Drawing.Color.FromArgb(242, 244, 245);
            table.ColumnBack = System.Drawing.Color.White;
            table.ColumnFont = new System.Drawing.Font("Segoe UI Semibold", 9F);
            table.ColumnFore = System.Drawing.Color.FromArgb(13, 20, 38);
            table.Dock = System.Windows.Forms.DockStyle.Fill;
            table.EmptyHeader = true;
            table.EmptyText = "Không tìm thấy tác giả";
            table.EnableHeaderResizing = false;
            table.FixedHeader = true;
            table.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            table.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            table.Gap = 12;
            table.Location = new System.Drawing.Point(0, 118);
            table.LostFocusClearSelection = false;
            table.Name = "table";
            table.RowHeight = 52;
            table.RowHeightHeader = 44;
            table.RowHoverBg = System.Drawing.Color.FromArgb(246, 248, 251);
            table.RowSelectedBg = System.Drawing.Color.FromArgb(234, 243, 252);
            table.RowSelectedFore = System.Drawing.Color.FromArgb(13, 20, 38);
            table.Size = new System.Drawing.Size(1200, 586);
            table.TabIndex = 1;
            //
            // footer
            //
            footer.BackColor = System.Drawing.Color.White;
            footer.Controls.Add(pager);
            footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            footer.Location = new System.Drawing.Point(0, 704);
            footer.Name = "footer";
            footer.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            footer.Size = new System.Drawing.Size(1200, 48);
            footer.TabIndex = 2;
            //
            // pager
            //
            pager.BackColor = System.Drawing.Color.White;
            pager.Current = 1;
            pager.Dock = System.Windows.Forms.DockStyle.Right;
            pager.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            pager.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            pager.Gap = 8;
            pager.Location = new System.Drawing.Point(764, 8);
            pager.Name = "pager";
            pager.PageSize = 10;
            pager.Radius = 8;
            pager.Size = new System.Drawing.Size(420, 32);
            pager.TabIndex = 0;
            pager.Total = 0;
            //
            // header
            //
            header.BackColor = System.Drawing.Color.White;
            header.Controls.Add(lblTitle);
            header.Controls.Add(stack1);
            header.Controls.Add(stack2);
            header.Controls.Add(stack3);
            header.Controls.Add(stack4);
            header.Controls.Add(stack5);
            header.Controls.Add(stackMore);
            header.Controls.Add(lblCount);
            header.Controls.Add(segGioiTinh);
            header.Controls.Add(chipMonth);
            header.Controls.Add(txtSearch);
            header.Controls.Add(segView);
            header.Controls.Add(right);
            header.Dock = System.Windows.Forms.DockStyle.Top;
            header.Location = new System.Drawing.Point(0, 0);
            header.Name = "header";
            header.Size = new System.Drawing.Size(1200, 118);
            header.TabIndex = 3;
            //
            // lblTitle
            //
            lblTitle.BackColor = System.Drawing.Color.Transparent;
            lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            lblTitle.Location = new System.Drawing.Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(320, 26);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Danh sách tác giả";
            //
            // stack1..5 — 5 avatar xếp chồng (kiểu "Members" Dribbble 21103831), thẻ đầu nằm trên cùng
            //
            stack1.BackColor = System.Drawing.Color.Transparent;
            stack1.BorderColor = System.Drawing.Color.White;
            stack1.BorderWidth = 2F;
            stack1.ImageFit = AntdUI.TFit.Cover;
            stack1.Location = new System.Drawing.Point(24, 44);
            stack1.Name = "stack1";
            stack1.Round = true;
            stack1.Size = new System.Drawing.Size(26, 26);
            stack1.TabIndex = 1;
            stack2.BackColor = System.Drawing.Color.Transparent;
            stack2.BorderColor = System.Drawing.Color.White;
            stack2.BorderWidth = 2F;
            stack2.ImageFit = AntdUI.TFit.Cover;
            stack2.Location = new System.Drawing.Point(43, 44);
            stack2.Name = "stack2";
            stack2.Round = true;
            stack2.Size = new System.Drawing.Size(26, 26);
            stack2.TabIndex = 2;
            stack3.BackColor = System.Drawing.Color.Transparent;
            stack3.BorderColor = System.Drawing.Color.White;
            stack3.BorderWidth = 2F;
            stack3.ImageFit = AntdUI.TFit.Cover;
            stack3.Location = new System.Drawing.Point(62, 44);
            stack3.Name = "stack3";
            stack3.Round = true;
            stack3.Size = new System.Drawing.Size(26, 26);
            stack3.TabIndex = 3;
            stack4.BackColor = System.Drawing.Color.Transparent;
            stack4.BorderColor = System.Drawing.Color.White;
            stack4.BorderWidth = 2F;
            stack4.ImageFit = AntdUI.TFit.Cover;
            stack4.Location = new System.Drawing.Point(81, 44);
            stack4.Name = "stack4";
            stack4.Round = true;
            stack4.Size = new System.Drawing.Size(26, 26);
            stack4.TabIndex = 4;
            stack5.BackColor = System.Drawing.Color.Transparent;
            stack5.BorderColor = System.Drawing.Color.White;
            stack5.BorderWidth = 2F;
            stack5.ImageFit = AntdUI.TFit.Cover;
            stack5.Location = new System.Drawing.Point(100, 44);
            stack5.Name = "stack5";
            stack5.Round = true;
            stack5.Size = new System.Drawing.Size(26, 26);
            stack5.TabIndex = 5;
            //
            // stackMore
            //
            stackMore.BackColor = System.Drawing.Color.FromArgb(233, 236, 243);
            stackMore.BorderWidth = 0F;
            stackMore.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            stackMore.ForeColor = System.Drawing.Color.FromArgb(74, 80, 100);
            stackMore.Location = new System.Drawing.Point(129, 44);
            stackMore.Name = "stackMore";
            stackMore.Radius = 13;
            stackMore.Size = new System.Drawing.Size(40, 26);
            stackMore.TabIndex = 6;
            stackMore.Text = "+0";
            stackMore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            stackMore.Type = AntdUI.TTypeMini.Default;
            //
            // lblCount
            //
            lblCount.BackColor = System.Drawing.Color.Transparent;
            lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            lblCount.ForeColor = System.Drawing.Color.FromArgb(111, 118, 137);
            lblCount.Location = new System.Drawing.Point(179, 48);
            lblCount.Name = "lblCount";
            lblCount.Size = new System.Drawing.Size(360, 18);
            lblCount.TabIndex = 7;
            //
            // segGioiTinh
            //
            segGioiTinh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            segGioiTinh.Location = new System.Drawing.Point(24, 74);
            segGioiTinh.Name = "segGioiTinh";
            segGioiTinh.Radius = 8;
            segGioiTinh.Size = new System.Drawing.Size(318, 34);
            segGioiTinh.TabIndex = 8;
            //
            // chipMonth — chip bộ lọc "Thêm trong tháng m/yyyy ×", chỉ hiện khi vào từ thẻ số liệu thứ 4
            //
            chipMonth.BackColor = System.Drawing.Color.FromArgb(211, 229, 255);
            chipMonth.BorderWidth = 0F;
            chipMonth.DefaultBack = System.Drawing.Color.FromArgb(211, 229, 255);
            chipMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            chipMonth.ForeColor = System.Drawing.Color.FromArgb(29, 90, 234);
            chipMonth.IconPosition = AntdUI.TAlignMini.Right;
            chipMonth.IconRatio = 0.6F;
            chipMonth.IconSvg = "CloseOutlined";
            chipMonth.Location = new System.Drawing.Point(354, 74);
            chipMonth.Name = "chipMonth";
            chipMonth.Radius = 17;
            chipMonth.Size = new System.Drawing.Size(212, 34);
            chipMonth.TabIndex = 9;
            chipMonth.Text = "Thêm trong tháng";
            chipMonth.Type = AntdUI.TTypeMini.Default;
            chipMonth.Visible = false;
            //
            // txtSearch
            //
            txtSearch.AllowClear = true;
            txtSearch.BackColor = System.Drawing.Color.White;
            txtSearch.BorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            txtSearch.ForeColor = System.Drawing.Color.FromArgb(13, 20, 38);
            txtSearch.Location = new System.Drawing.Point(916, 73);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderColor = System.Drawing.Color.FromArgb(138, 144, 162);
            txtSearch.PlaceholderText = "Tìm kiếm mã, tên, số điện thoại";
            txtSearch.PrefixSvg = "SearchOutlined";
            txtSearch.Radius = 8;
            txtSearch.Size = new System.Drawing.Size(260, 36);
            txtSearch.TabIndex = 10;
            //
            // segView — toggle Bảng ↔ Thẻ (Full để 2 ô icon chia đều 96 px)
            //
            segView.Full = true;
            segView.IconRatio = 0.75F;
            segView.Location = new System.Drawing.Point(808, 73);
            segView.Name = "segView";
            segView.Radius = 8;
            segView.Size = new System.Drawing.Size(96, 36);
            segView.TabIndex = 11;
            //
            // right — Thêm + ⋯ neo phải
            //
            right.AutoSize = true;
            right.BackColor = System.Drawing.Color.White;
            right.Controls.Add(btnAdd);
            right.Controls.Add(btnMore);
            right.Dock = System.Windows.Forms.DockStyle.Right;
            right.Location = new System.Drawing.Point(980, 0);
            right.Name = "right";
            right.Padding = new System.Windows.Forms.Padding(0, 20, 24, 0);
            right.Size = new System.Drawing.Size(220, 118);
            right.TabIndex = 12;
            right.WrapContents = false;
            //
            // btnAdd
            //
            btnAdd.BackExtend = "135, #166BB8, #5047E0";
            btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            btnAdd.IconSvg = "PlusOutlined";
            btnAdd.Location = new System.Drawing.Point(0, 20);
            btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            btnAdd.Name = "btnAdd";
            btnAdd.Radius = 8;
            btnAdd.Size = new System.Drawing.Size(148, 36);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Thêm tác giả";
            btnAdd.Type = AntdUI.TTypeMini.Primary;
            //
            // btnMore — menu ⋯ canh mép phải nút (Placement BR) để không tràn ra ngoài cửa sổ
            //
            btnMore.BorderWidth = 1F;
            btnMore.DefaultBack = System.Drawing.Color.White;
            btnMore.DefaultBorderColor = System.Drawing.Color.FromArgb(227, 231, 238);
            btnMore.DropDownArrow = false;
            btnMore.DropDownPadding = new System.Drawing.Size(14, 8);
            btnMore.DropDownRadius = 8;
            btnMore.ForeColor = System.Drawing.Color.FromArgb(65, 70, 80);
            btnMore.IconSvg = "EllipsisOutlined";
            btnMore.ListAutoWidth = true;
            btnMore.Location = new System.Drawing.Point(160, 20);
            btnMore.Margin = new System.Windows.Forms.Padding(0);
            btnMore.Name = "btnMore";
            btnMore.Placement = AntdUI.TAlignFrom.BR;
            btnMore.Radius = 8;
            btnMore.Size = new System.Drawing.Size(36, 36);
            btnMore.TabIndex = 1;
            btnMore.Type = AntdUI.TTypeMini.Default;
            //
            // debounce — gõ tìm kiếm 300 ms rồi mới lọc
            //
            debounce.Interval = 300;
            //
            // resizeDebounce — đổi kích thước bảng 120 ms rồi mới ẩn cột / tính số dòng
            //
            resizeDebounce.Interval = 120;
            //
            // TacGiaListPage
            //
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.FromArgb(243, 245, 249);
            Controls.Add(card);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            Name = "TacGiaListPage";
            Size = new System.Drawing.Size(1200, 760);
            card.ResumeLayout(false);
            footer.ResumeLayout(false);
            header.ResumeLayout(false);
            right.ResumeLayout(false);
            right.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private AntdUI.Panel card;
        private QuanLyTacGia.Pages.TacGiaCardView cards;
        private AntdUI.Table table;
        private System.Windows.Forms.Panel footer;
        private AntdUI.Pagination pager;
        private System.Windows.Forms.Panel header;
        private AntdUI.Label lblTitle;
        private AntdUI.Avatar stack1;
        private AntdUI.Avatar stack2;
        private AntdUI.Avatar stack3;
        private AntdUI.Avatar stack4;
        private AntdUI.Avatar stack5;
        private AntdUI.Tag stackMore;
        private AntdUI.Label lblCount;
        private AntdUI.Segmented segGioiTinh;
        private AntdUI.Button chipMonth;
        private AntdUI.Input txtSearch;
        private AntdUI.Segmented segView;
        private System.Windows.Forms.FlowLayoutPanel right;
        private AntdUI.Button btnAdd;
        private AntdUI.Dropdown btnMore;
        private System.Windows.Forms.Timer debounce;
        private System.Windows.Forms.Timer resizeDebounce;
    }
}
