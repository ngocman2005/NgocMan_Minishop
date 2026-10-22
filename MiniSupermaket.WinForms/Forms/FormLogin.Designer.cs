#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelBrand;
        private Label lblBrand;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblError;
        private Button btnLogin;
        private Label lblHint;

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
            this.panelBrand = new Panel();
            this.lblBrand = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblError = new Label();
            this.btnLogin = new Button();
            this.lblHint = new Label();
            this.panelBrand.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBrand
            // 
            this.panelBrand.BackColor = Color.FromArgb(24, 30, 48);
            this.panelBrand.Controls.Add(this.lblBrand);
            this.panelBrand.Dock = DockStyle.Top;
            this.panelBrand.Height = 90;
            this.panelBrand.Name = "panelBrand";
            // 
            // lblBrand
            // 
            this.lblBrand.Dock = DockStyle.Fill;
            this.lblBrand.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblBrand.ForeColor = Color.White;
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Text = "🛒 MiniMart POS";
            this.lblBrand.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblUsername.Location = new Point(40, 115);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Text = "TÊN ĐĂNG NHẬP";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new Font("Segoe UI", 12F);
            this.txtUsername.Location = new Point(40, 138);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new Size(320, 29);
            this.txtUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblPassword.Location = new Point(40, 182);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Text = "MẬT KHẨU";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new Font("Segoe UI", 12F);
            this.txtPassword.Location = new Point(40, 205);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new Size(320, 29);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblError
            // 
            this.lblError.ForeColor = Color.Firebrick;
            this.lblError.Location = new Point(40, 242);
            this.lblError.Name = "lblError";
            this.lblError.Size = new Size(320, 22);
            this.lblError.Text = "";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = Color.FromArgb(41, 100, 180);
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(40, 270);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new Size(320, 46);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            // 
            // lblHint
            // 
            this.lblHint.ForeColor = Color.Gray;
            this.lblHint.Font = new Font("Segoe UI", 8.5F);
            this.lblHint.Location = new Point(40, 332);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new Size(320, 50);
            this.lblHint.Text = "Tài khoản mẫu: admin / cashier01 / warehouse01\r\nMật khẩu chung: 123456";
            // 
            // FormLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(400, 400);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.panelBrand);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            this.panelBrand.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
