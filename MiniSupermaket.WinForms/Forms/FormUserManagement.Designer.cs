#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlpMain;
        private Panel panelGrid;
        private DataGridView dgvUsers;
        private Panel panelDetail;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnToggleLock;

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
            this.tlpMain = new TableLayoutPanel();
            this.panelGrid = new Panel();
            this.dgvUsers = new DataGridView();
            this.panelDetail = new Panel();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblRole = new Label();
            this.cboRole = new ComboBox();
            this.btnAddUser = new Button();
            this.btnResetPassword = new Button();
            this.btnToggleLock = new Button();
            this.tlpMain.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain  (68% | 32%)
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.tlpMain.Controls.Add(this.panelGrid, 0, 0);
            this.tlpMain.Controls.Add(this.panelDetail, 1, 0);
            this.tlpMain.Dock = DockStyle.Fill;
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpMain.TabIndex = 0;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvUsers);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(15);
            this.panelGrid.TabIndex = 0;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AllowUserToResizeRows = false;
            this.dgvUsers.BackgroundColor = Color.White;
            this.dgvUsers.BorderStyle = BorderStyle.None;
            this.dgvUsers.ColumnHeadersHeight = 34;
            this.dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUsers.Dock = DockStyle.Fill;
            this.dgvUsers.MultiSelect = false;
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersVisible = false;
            this.dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.TabIndex = 0;
            this.dgvUsers.CellClick += new DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            this.dgvUsers.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.dgvUsers_DataBindingComplete);
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = Color.White;
            this.panelDetail.Controls.Add(this.btnToggleLock);
            this.panelDetail.Controls.Add(this.btnResetPassword);
            this.panelDetail.Controls.Add(this.btnAddUser);
            this.panelDetail.Controls.Add(this.cboRole);
            this.panelDetail.Controls.Add(this.lblRole);
            this.panelDetail.Controls.Add(this.txtPassword);
            this.panelDetail.Controls.Add(this.lblPassword);
            this.panelDetail.Controls.Add(this.txtEmail);
            this.panelDetail.Controls.Add(this.lblEmail);
            this.panelDetail.Controls.Add(this.txtFullName);
            this.panelDetail.Controls.Add(this.lblFullName);
            this.panelDetail.Controls.Add(this.txtUsername);
            this.panelDetail.Controls.Add(this.lblUsername);
            this.panelDetail.Dock = DockStyle.Fill;
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblUsername.Location = new Point(15, 15);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "TÊN ĐĂNG NHẬP";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtUsername.Location = new Point(15, 37);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(290, 23);
            this.txtUsername.TabIndex = 0;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblFullName.Location = new Point(15, 70);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Text = "HỌ VÀ TÊN";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFullName.Location = new Point(15, 92);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new Size(290, 23);
            this.txtFullName.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblEmail.Location = new Point(15, 125);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "EMAIL";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtEmail.Location = new Point(15, 147);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(290, 23);
            this.txtEmail.TabIndex = 2;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblPassword.Location = new Point(15, 180);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "MẬT KHẨU (khi tạo mới)";
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtPassword.Location = new Point(15, 202);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(290, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblRole.Location = new Point(15, 235);
            this.lblRole.Name = "lblRole";
            this.lblRole.Text = "VAI TRÒ";
            // 
            // cboRole
            // 
            this.cboRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cboRole.Location = new Point(15, 257);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new Size(290, 23);
            this.cboRole.TabIndex = 4;
            this.cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // btnAddUser
            // 
            this.btnAddUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnAddUser.BackColor = Color.FromArgb(27, 122, 60);
            this.btnAddUser.Cursor = Cursors.Hand;
            this.btnAddUser.FlatAppearance.BorderSize = 0;
            this.btnAddUser.FlatStyle = FlatStyle.Flat;
            this.btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAddUser.ForeColor = Color.White;
            this.btnAddUser.Location = new Point(15, 310);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new Size(290, 40);
            this.btnAddUser.TabIndex = 5;
            this.btnAddUser.Text = "➕ Tạo tài khoản";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnResetPassword.BackColor = Color.FromArgb(41, 100, 180);
            this.btnResetPassword.Cursor = Cursors.Hand;
            this.btnResetPassword.FlatAppearance.BorderSize = 0;
            this.btnResetPassword.FlatStyle = FlatStyle.Flat;
            this.btnResetPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnResetPassword.ForeColor = Color.White;
            this.btnResetPassword.Location = new Point(15, 360);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new Size(290, 40);
            this.btnResetPassword.TabIndex = 6;
            this.btnResetPassword.Text = "🔑 Đặt lại mật khẩu (123456)";
            this.btnResetPassword.UseVisualStyleBackColor = false;
            this.btnResetPassword.Click += new EventHandler(this.btnResetPassword_Click);
            // 
            // btnToggleLock
            // 
            this.btnToggleLock.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnToggleLock.BackColor = Color.FromArgb(200, 120, 20);
            this.btnToggleLock.Cursor = Cursors.Hand;
            this.btnToggleLock.FlatAppearance.BorderSize = 0;
            this.btnToggleLock.FlatStyle = FlatStyle.Flat;
            this.btnToggleLock.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnToggleLock.ForeColor = Color.White;
            this.btnToggleLock.Location = new Point(15, 410);
            this.btnToggleLock.Name = "btnToggleLock";
            this.btnToggleLock.Size = new Size(290, 40);
            this.btnToggleLock.TabIndex = 7;
            this.btnToggleLock.Text = "🔒 Khóa / Mở khóa";
            this.btnToggleLock.UseVisualStyleBackColor = false;
            this.btnToggleLock.Click += new EventHandler(this.btnToggleLock_Click);
            // 
            // FormUserManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.tlpMain);
            this.Name = "FormUserManagement";
            this.Text = "FormUserManagement";
            this.Load += new EventHandler(this.FormUserManagement_Load);
            this.tlpMain.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
