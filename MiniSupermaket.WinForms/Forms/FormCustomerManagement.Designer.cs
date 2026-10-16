#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelTop;
        private Button btnReload;
        private Label lblNote;
        private Panel panelGrid;
        private DataGridView dgvCustomers;

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
            this.panelTop = new Panel();
            this.btnReload = new Button();
            this.lblNote = new Label();
            this.panelGrid = new Panel();
            this.dgvCustomers = new DataGridView();
            this.panelTop.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.lblNote);
            this.panelTop.Controls.Add(this.btnReload);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.Name = "panelTop";
            this.panelTop.TabIndex = 1;
            // 
            // btnReload
            // 
            this.btnReload.Location = new Point(15, 15);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new Size(110, 30);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "🔄 Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new EventHandler(this.btnReload_Click);
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.ForeColor = Color.Gray;
            this.lblNote.Location = new Point(140, 22);
            this.lblNote.Name = "lblNote";
            this.lblNote.Text = "Danh sách khách hàng thân thiết (form mẫu - thay bằng bản đầy đủ của Buổi 2-3).";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvCustomers);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(15);
            this.panelGrid.TabIndex = 0;
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.BackgroundColor = Color.White;
            this.dgvCustomers.BorderStyle = BorderStyle.None;
            this.dgvCustomers.Dock = DockStyle.Fill;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.TabIndex = 0;
            // 
            // FormCustomerManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelTop);
            this.Name = "FormCustomerManagement";
            this.Text = "FormCustomerManagement";
            this.Load += new EventHandler(this.FormCustomerManagement_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
