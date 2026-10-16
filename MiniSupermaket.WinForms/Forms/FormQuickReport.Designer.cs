#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelFilter;
        private Label lblDate;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;
        private Label lblPermissionNote;
        private TableLayoutPanel tlpCards;
        private Panel cardOrders;
        private Label cardOrdersCaption;
        private Label lblTotalOrders;
        private Panel cardRevenue;
        private Label cardRevenueCaption;
        private Label lblTotalRevenue;
        private Panel cardBest;
        private Label cardBestCaption;
        private Label lblBestSeller;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelFilter = new Panel();
            this.lblDate = new Label();
            this.dtpReportDate = new DateTimePicker();
            this.btnRunReport = new Button();
            this.lblPermissionNote = new Label();
            this.tlpCards = new TableLayoutPanel();
            this.cardOrders = new Panel();
            this.cardOrdersCaption = new Label();
            this.lblTotalOrders = new Label();
            this.cardRevenue = new Panel();
            this.cardRevenueCaption = new Label();
            this.lblTotalRevenue = new Label();
            this.cardBest = new Panel();
            this.cardBestCaption = new Label();
            this.lblBestSeller = new Label();
            this.panelFilter.SuspendLayout();
            this.tlpCards.SuspendLayout();
            this.cardOrders.SuspendLayout();
            this.cardRevenue.SuspendLayout();
            this.cardBest.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = Color.White;
            this.panelFilter.Controls.Add(this.lblPermissionNote);
            this.panelFilter.Controls.Add(this.btnRunReport);
            this.panelFilter.Controls.Add(this.dtpReportDate);
            this.panelFilter.Controls.Add(this.lblDate);
            this.panelFilter.Dock = DockStyle.Top;
            this.panelFilter.Height = 64;
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblDate.Location = new Point(20, 22);
            this.lblDate.Name = "lblDate";
            this.lblDate.Text = "Ngày báo cáo:";
            // 
            // dtpReportDate
            // 
            this.dtpReportDate.Font = new Font("Segoe UI", 10.5F);
            this.dtpReportDate.Format = DateTimePickerFormat.Short;
            this.dtpReportDate.Location = new Point(135, 17);
            this.dtpReportDate.Name = "dtpReportDate";
            this.dtpReportDate.Size = new Size(140, 27);
            this.dtpReportDate.TabIndex = 0;
            // 
            // btnRunReport
            // 
            this.btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            this.btnRunReport.Cursor = Cursors.Hand;
            this.btnRunReport.FlatAppearance.BorderSize = 0;
            this.btnRunReport.FlatStyle = FlatStyle.Flat;
            this.btnRunReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnRunReport.ForeColor = Color.White;
            this.btnRunReport.Location = new Point(295, 13);
            this.btnRunReport.Name = "btnRunReport";
            this.btnRunReport.Size = new Size(190, 36);
            this.btnRunReport.TabIndex = 1;
            this.btnRunReport.Text = "📊 Xem doanh thu ca";
            this.btnRunReport.UseVisualStyleBackColor = false;
            this.btnRunReport.Click += new EventHandler(this.btnRunReport_Click);
            // 
            // lblPermissionNote
            // 
            this.lblPermissionNote.AutoSize = true;
            this.lblPermissionNote.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            this.lblPermissionNote.ForeColor = Color.Firebrick;
            this.lblPermissionNote.Location = new Point(505, 22);
            this.lblPermissionNote.Name = "lblPermissionNote";
            this.lblPermissionNote.Text = "";
            // 
            // tlpCards
            // 
            this.tlpCards.ColumnCount = 3;
            this.tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            this.tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            this.tlpCards.Controls.Add(this.cardOrders, 0, 0);
            this.tlpCards.Controls.Add(this.cardRevenue, 1, 0);
            this.tlpCards.Controls.Add(this.cardBest, 2, 0);
            this.tlpCards.Dock = DockStyle.Top;
            this.tlpCards.Height = 210;
            this.tlpCards.Name = "tlpCards";
            this.tlpCards.Padding = new Padding(12);
            this.tlpCards.RowCount = 1;
            this.tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpCards.TabIndex = 0;
            // 
            // cardOrders
            // 
            this.cardOrders.BackColor = Color.FromArgb(41, 100, 180);
            this.cardOrders.Controls.Add(this.lblTotalOrders);
            this.cardOrders.Controls.Add(this.cardOrdersCaption);
            this.cardOrders.Dock = DockStyle.Fill;
            this.cardOrders.Margin = new Padding(8);
            this.cardOrders.Name = "cardOrders";
            this.cardOrders.TabIndex = 0;
            // 
            // cardOrdersCaption
            // 
            this.cardOrdersCaption.Dock = DockStyle.Top;
            this.cardOrdersCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.cardOrdersCaption.ForeColor = Color.White;
            this.cardOrdersCaption.Height = 44;
            this.cardOrdersCaption.Name = "cardOrdersCaption";
            this.cardOrdersCaption.Padding = new Padding(16, 0, 0, 0);
            this.cardOrdersCaption.Text = "TỔNG SỐ HÓA ĐƠN";
            this.cardOrdersCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoEllipsis = true;
            this.lblTotalOrders.Dock = DockStyle.Fill;
            this.lblTotalOrders.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            this.lblTotalOrders.ForeColor = Color.White;
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Padding = new Padding(10, 0, 10, 20);
            this.lblTotalOrders.Text = "—";
            this.lblTotalOrders.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardRevenue
            // 
            this.cardRevenue.BackColor = Color.FromArgb(27, 122, 60);
            this.cardRevenue.Controls.Add(this.lblTotalRevenue);
            this.cardRevenue.Controls.Add(this.cardRevenueCaption);
            this.cardRevenue.Dock = DockStyle.Fill;
            this.cardRevenue.Margin = new Padding(8);
            this.cardRevenue.Name = "cardRevenue";
            this.cardRevenue.TabIndex = 1;
            // 
            // cardRevenueCaption
            // 
            this.cardRevenueCaption.Dock = DockStyle.Top;
            this.cardRevenueCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.cardRevenueCaption.ForeColor = Color.White;
            this.cardRevenueCaption.Height = 44;
            this.cardRevenueCaption.Name = "cardRevenueCaption";
            this.cardRevenueCaption.Padding = new Padding(16, 0, 0, 0);
            this.cardRevenueCaption.Text = "TỔNG DOANH THU";
            this.cardRevenueCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoEllipsis = true;
            this.lblTotalRevenue.Dock = DockStyle.Fill;
            this.lblTotalRevenue.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = Color.White;
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Padding = new Padding(10, 0, 10, 20);
            this.lblTotalRevenue.Text = "—";
            this.lblTotalRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardBest
            // 
            this.cardBest.BackColor = Color.FromArgb(222, 130, 20);
            this.cardBest.Controls.Add(this.lblBestSeller);
            this.cardBest.Controls.Add(this.cardBestCaption);
            this.cardBest.Dock = DockStyle.Fill;
            this.cardBest.Margin = new Padding(8);
            this.cardBest.Name = "cardBest";
            this.cardBest.TabIndex = 2;
            // 
            // cardBestCaption
            // 
            this.cardBestCaption.Dock = DockStyle.Top;
            this.cardBestCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.cardBestCaption.ForeColor = Color.White;
            this.cardBestCaption.Height = 44;
            this.cardBestCaption.Name = "cardBestCaption";
            this.cardBestCaption.Padding = new Padding(16, 0, 0, 0);
            this.cardBestCaption.Text = "MẶT HÀNG BÁN CHẠY NHẤT";
            this.cardBestCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBestSeller
            // 
            this.lblBestSeller.AutoEllipsis = true;
            this.lblBestSeller.Dock = DockStyle.Fill;
            this.lblBestSeller.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblBestSeller.ForeColor = Color.White;
            this.lblBestSeller.Name = "lblBestSeller";
            this.lblBestSeller.Padding = new Padding(10, 0, 10, 20);
            this.lblBestSeller.Text = "—";
            this.lblBestSeller.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormQuickReport
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.tlpCards);
            this.Controls.Add(this.panelFilter);
            this.Name = "FormQuickReport";
            this.Text = "FormQuickReport";
            this.Load += new EventHandler(this.FormQuickReport_Load);
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.tlpCards.ResumeLayout(false);
            this.cardOrders.ResumeLayout(false);
            this.cardRevenue.ResumeLayout(false);
            this.cardBest.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
