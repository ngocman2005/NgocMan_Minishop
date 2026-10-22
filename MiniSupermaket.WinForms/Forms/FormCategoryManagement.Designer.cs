#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelGrid, panelDetail;
        private DataGridView dgvCategories;
        private Label lblId, lblName, lblDescription;
        private TextBox txtCategoryId, txtCategoryName, txtDescription;
        private Button btnAdd, btnUpdate, btnDelete, btnReload;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelGrid = new Panel(); panelDetail = new Panel(); dgvCategories = new DataGridView();
            lblId = new Label(); lblName = new Label(); lblDescription = new Label();
            txtCategoryId = new TextBox(); txtCategoryName = new TextBox(); txtDescription = new TextBox();
            btnAdd = new Button(); btnUpdate = new Button(); btnDelete = new Button(); btnReload = new Button();
            panelGrid.SuspendLayout(); panelDetail.SuspendLayout(); ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit(); SuspendLayout();
            panelGrid.Dock = DockStyle.Fill; panelGrid.Padding = new Padding(15); panelGrid.Controls.Add(dgvCategories);
            dgvCategories.Dock = DockStyle.Fill; dgvCategories.BackgroundColor = Color.White; dgvCategories.ReadOnly = true; dgvCategories.AllowUserToAddRows = false; dgvCategories.RowHeadersVisible = false; dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dgvCategories.CellClick += dgvCategories_CellClick;
            panelDetail.Dock = DockStyle.Right; panelDetail.Width = 300; panelDetail.BackColor = Color.White; panelDetail.Padding = new Padding(15);
            lblId.Text = "Mã danh mục"; lblId.Location = new Point(15, 20); lblId.AutoSize = true; txtCategoryId.Location = new Point(15, 42); txtCategoryId.Width = 260; txtCategoryId.ReadOnly = true;
            lblName.Text = "Tên danh mục"; lblName.Location = new Point(15, 78); lblName.AutoSize = true; txtCategoryName.Location = new Point(15, 100); txtCategoryName.Width = 260;
            lblDescription.Text = "Mô tả"; lblDescription.Location = new Point(15, 136); lblDescription.AutoSize = true; txtDescription.Location = new Point(15, 158); txtDescription.Width = 260; txtDescription.Height = 60; txtDescription.Multiline = true;
            btnAdd.Text = "Thêm"; btnAdd.Location = new Point(15, 240); btnAdd.Click += btnAdd_Click; btnUpdate.Text = "Sửa"; btnUpdate.Location = new Point(95, 240); btnUpdate.Click += btnUpdate_Click; btnDelete.Text = "Xóa"; btnDelete.Location = new Point(175, 240); btnDelete.Click += btnDelete_Click; btnReload.Text = "Tải lại"; btnReload.Location = new Point(15, 280); btnReload.Click += btnReload_Click;
            panelDetail.Controls.AddRange(new Control[] { lblId, txtCategoryId, lblName, txtCategoryName, lblDescription, txtDescription, btnAdd, btnUpdate, btnDelete, btnReload });
            Controls.Add(panelGrid); Controls.Add(panelDetail); Text = "Quản lý danh mục"; ClientSize = new Size(1000, 600); Load += FormCategoryManagement_Load;
            panelGrid.ResumeLayout(false); panelDetail.ResumeLayout(false); panelDetail.PerformLayout(); ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit(); ResumeLayout(false);
        }
    }
}
