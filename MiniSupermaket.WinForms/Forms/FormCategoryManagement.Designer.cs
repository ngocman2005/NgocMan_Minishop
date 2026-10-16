#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelTop;
        private Button btnReload;
        private Label lblNote;
        private Panel panelGrid;
        private DataGridView dgvCategories;

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
            this.dgvCategories = new DataGridView();
            this.panelTop.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
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
            this.lblNote.Text = "Danh sách nhóm hàng (màn hình CRUD đầy đủ làm ở Buổi 2-3 sẽ thay thế form mẫu này).";
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvCategories);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(15);
            this.panelGrid.TabIndex = 0;
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AllowUserToDeleteRows = false;
            this.dgvCategories.BackgroundColor = Color.White;
            this.dgvCategories.BorderStyle = BorderStyle.None;
            this.dgvCategories.Dock = DockStyle.Fill;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.TabIndex = 0;
            // 
            // FormCategoryManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelTop);
            this.Name = "FormCategoryManagement";
            this.Text = "FormCategoryManagement";
            this.Load += new EventHandler(this.FormCategoryManagement_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
