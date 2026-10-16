#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReports;
        private Button btnUserManage;
        private Panel panelUserFooter;
        private Button btnLogout;
        private Panel panelTopHeader;
        private Label lblTitle;
        private Label lblUserInfo;
        private Panel panelMainContent;

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
            this.panelSidebar = new Panel();
            this.lblLogo = new Label();
            this.btnPOS = new Button();
            this.btnCategory = new Button();
            this.btnProduct = new Button();
            this.btnCustomer = new Button();
            this.btnReports = new Button();
            this.btnUserManage = new Button();
            this.panelUserFooter = new Panel();
            this.btnLogout = new Button();
            this.panelTopHeader = new Panel();
            this.lblTitle = new Label();
            this.lblUserInfo = new Label();
            this.panelMainContent = new Panel();
            this.panelSidebar.SuspendLayout();
            this.panelUserFooter.SuspendLayout();
            this.panelTopHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            this.panelSidebar.Controls.Add(this.panelUserFooter);
            this.panelSidebar.Controls.Add(this.btnUserManage);
            this.panelSidebar.Controls.Add(this.btnReports);
            this.panelSidebar.Controls.Add(this.btnCustomer);
            this.panelSidebar.Controls.Add(this.btnProduct);
            this.panelSidebar.Controls.Add(this.btnCategory);
            this.panelSidebar.Controls.Add(this.btnPOS);
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new Size(230, 720);
            this.panelSidebar.TabIndex = 2;
            // 
            // lblLogo
            // 
            this.lblLogo.Dock = DockStyle.Top;
            this.lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Height = 70;
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "🛒 MiniMart POS";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPOS
            // 
            this.btnPOS.BackColor = Color.FromArgb(24, 30, 48);
            this.btnPOS.Cursor = Cursors.Hand;
            this.btnPOS.Dock = DockStyle.Top;
            this.btnPOS.FlatAppearance.BorderSize = 0;
            this.btnPOS.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnPOS.FlatStyle = FlatStyle.Flat;
            this.btnPOS.Font = new Font("Segoe UI", 10.5F);
            this.btnPOS.ForeColor = Color.White;
            this.btnPOS.Height = 45;
            this.btnPOS.Name = "btnPOS";
            this.btnPOS.Padding = new Padding(18, 0, 0, 0);
            this.btnPOS.TabIndex = 1;
            this.btnPOS.Text = "🛒  Bán hàng (POS)";
            this.btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            this.btnPOS.UseVisualStyleBackColor = false;
            this.btnPOS.Click += new EventHandler(this.btnPOS_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.BackColor = Color.FromArgb(24, 30, 48);
            this.btnCategory.Cursor = Cursors.Hand;
            this.btnCategory.Dock = DockStyle.Top;
            this.btnCategory.FlatAppearance.BorderSize = 0;
            this.btnCategory.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnCategory.FlatStyle = FlatStyle.Flat;
            this.btnCategory.Font = new Font("Segoe UI", 10.5F);
            this.btnCategory.ForeColor = Color.White;
            this.btnCategory.Height = 45;
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Padding = new Padding(18, 0, 0, 0);
            this.btnCategory.TabIndex = 2;
            this.btnCategory.Text = "📁  Quản lý Danh mục";
            this.btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCategory.UseVisualStyleBackColor = false;
            this.btnCategory.Click += new EventHandler(this.btnCategory_Click);
            // 
            // btnProduct
            // 
            this.btnProduct.BackColor = Color.FromArgb(24, 30, 48);
            this.btnProduct.Cursor = Cursors.Hand;
            this.btnProduct.Dock = DockStyle.Top;
            this.btnProduct.FlatAppearance.BorderSize = 0;
            this.btnProduct.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnProduct.FlatStyle = FlatStyle.Flat;
            this.btnProduct.Font = new Font("Segoe UI", 10.5F);
            this.btnProduct.ForeColor = Color.White;
            this.btnProduct.Height = 45;
            this.btnProduct.Name = "btnProduct";
            this.btnProduct.Padding = new Padding(18, 0, 0, 0);
            this.btnProduct.TabIndex = 3;
            this.btnProduct.Text = "📦  Quản lý Sản phẩm";
            this.btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            this.btnProduct.UseVisualStyleBackColor = false;
            this.btnProduct.Click += new EventHandler(this.btnProduct_Click);
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = Color.FromArgb(24, 30, 48);
            this.btnCustomer.Cursor = Cursors.Hand;
            this.btnCustomer.Dock = DockStyle.Top;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnCustomer.FlatStyle = FlatStyle.Flat;
            this.btnCustomer.Font = new Font("Segoe UI", 10.5F);
            this.btnCustomer.ForeColor = Color.White;
            this.btnCustomer.Height = 45;
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Padding = new Padding(18, 0, 0, 0);
            this.btnCustomer.TabIndex = 4;
            this.btnCustomer.Text = "👥  Quản lý Khách hàng";
            this.btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new EventHandler(this.btnCustomer_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = Color.FromArgb(24, 30, 48);
            this.btnReports.Cursor = Cursors.Hand;
            this.btnReports.Dock = DockStyle.Top;
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnReports.FlatStyle = FlatStyle.Flat;
            this.btnReports.Font = new Font("Segoe UI", 10.5F);
            this.btnReports.ForeColor = Color.White;
            this.btnReports.Height = 45;
            this.btnReports.Name = "btnReports";
            this.btnReports.Padding = new Padding(18, 0, 0, 0);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "📊  Báo cáo Doanh thu";
            this.btnReports.TextAlign = ContentAlignment.MiddleLeft;
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new EventHandler(this.btnReports_Click);
            // 
            // btnUserManage
            // 
            this.btnUserManage.BackColor = Color.FromArgb(24, 30, 48);
            this.btnUserManage.Cursor = Cursors.Hand;
            this.btnUserManage.Dock = DockStyle.Top;
            this.btnUserManage.FlatAppearance.BorderSize = 0;
            this.btnUserManage.FlatAppearance.MouseOverBackColor = Color.FromArgb(38, 48, 76);
            this.btnUserManage.FlatStyle = FlatStyle.Flat;
            this.btnUserManage.Font = new Font("Segoe UI", 10.5F);
            this.btnUserManage.ForeColor = Color.White;
            this.btnUserManage.Height = 45;
            this.btnUserManage.Name = "btnUserManage";
            this.btnUserManage.Padding = new Padding(18, 0, 0, 0);
            this.btnUserManage.TabIndex = 6;
            this.btnUserManage.Text = "🛡  Quản trị Tài khoản";
            this.btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            this.btnUserManage.UseVisualStyleBackColor = false;
            this.btnUserManage.Click += new EventHandler(this.btnUserManage_Click);
            // 
            // panelUserFooter
            // 
            this.panelUserFooter.BackColor = Color.FromArgb(18, 23, 38);
            this.panelUserFooter.Controls.Add(this.btnLogout);
            this.panelUserFooter.Dock = DockStyle.Bottom;
            this.panelUserFooter.Height = 60;
            this.panelUserFooter.Name = "panelUserFooter";
            this.panelUserFooter.TabIndex = 8;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = Color.FromArgb(18, 23, 38);
            this.btnLogout.Cursor = Cursors.Hand;
            this.btnLogout.Dock = DockStyle.Fill;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(160, 40, 50);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 10.5F);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new Padding(18, 0, 0, 0);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "🚪  Đăng xuất";
            this.btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);
            // 
            // panelTopHeader
            // 
            this.panelTopHeader.BackColor = Color.White;
            this.panelTopHeader.Controls.Add(this.lblTitle);
            this.panelTopHeader.Controls.Add(this.lblUserInfo);
            this.panelTopHeader.Dock = DockStyle.Top;
            this.panelTopHeader.Height = 60;
            this.panelTopHeader.Name = "panelTopHeader";
            this.panelTopHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = DockStyle.Fill;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(24, 30, 48);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new Padding(20, 0, 0, 0);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";
            this.lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Dock = DockStyle.Right;
            this.lblUserInfo.Font = new Font("Segoe UI", 10F);
            this.lblUserInfo.ForeColor = Color.FromArgb(80, 88, 110);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Padding = new Padding(0, 0, 20, 0);
            this.lblUserInfo.Size = new Size(430, 60);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Xin chào: ....";
            this.lblUserInfo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelMainContent
            // 
            this.panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            this.panelMainContent.Dock = DockStyle.Fill;
            this.panelMainContent.Name = "panelMainContent";
            this.panelMainContent.TabIndex = 0;
            // 
            // FormMainShell
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 720);
            this.Controls.Add(this.panelMainContent);
            this.Controls.Add(this.panelTopHeader);
            this.Controls.Add(this.panelSidebar);
            this.MinimumSize = new Size(1100, 650);
            this.Name = "FormMainShell";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini";
            this.Load += new EventHandler(this.FormMainShell_Load);
            this.panelSidebar.ResumeLayout(false);
            this.panelUserFooter.ResumeLayout(false);
            this.panelTopHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
