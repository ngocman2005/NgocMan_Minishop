#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelGrid, panelDetail; private DataGridView dgvCustomers;
        private Label lblId, lblName, lblPhone, lblAddress; private TextBox txtCustomerId, txtCustomerName, txtPhone, txtAddress;
        private Button btnAdd, btnUpdate, btnDelete, btnReload;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            panelGrid = new Panel(); panelDetail = new Panel(); dgvCustomers = new DataGridView(); lblId = new Label(); lblName = new Label(); lblPhone = new Label(); lblAddress = new Label(); txtCustomerId = new TextBox(); txtCustomerName = new TextBox(); txtPhone = new TextBox(); txtAddress = new TextBox(); btnAdd = new Button(); btnUpdate = new Button(); btnDelete = new Button(); btnReload = new Button();
            panelGrid.Dock = DockStyle.Fill; panelGrid.Padding = new Padding(15); panelGrid.Controls.Add(dgvCustomers); panelDetail.Dock = DockStyle.Right; panelDetail.Width = 300; panelDetail.BackColor = Color.White; panelDetail.Padding = new Padding(15);
            dgvCustomers.Dock = DockStyle.Fill; dgvCustomers.BackgroundColor = Color.White; dgvCustomers.ReadOnly = true; dgvCustomers.AllowUserToAddRows = false; dgvCustomers.RowHeadersVisible = false; dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect; dgvCustomers.CellClick += dgvCustomers_CellClick;
            lblId.Text = "Mã khách hàng"; lblId.Location = new Point(15, 20); lblId.AutoSize = true; txtCustomerId.Location = new Point(15, 42); txtCustomerId.Width = 260; txtCustomerId.ReadOnly = true;
            lblName.Text = "Họ và tên"; lblName.Location = new Point(15, 78); lblName.AutoSize = true; txtCustomerName.Location = new Point(15, 100); txtCustomerName.Width = 260;
            lblPhone.Text = "Số điện thoại"; lblPhone.Location = new Point(15, 136); lblPhone.AutoSize = true; txtPhone.Location = new Point(15, 158); txtPhone.Width = 260;
            lblAddress.Text = "Địa chỉ"; lblAddress.Location = new Point(15, 194); lblAddress.AutoSize = true; txtAddress.Location = new Point(15, 216); txtAddress.Width = 260;
            btnAdd.Text = "Thêm"; btnAdd.Location = new Point(15, 260); btnAdd.Click += btnAdd_Click; btnUpdate.Text = "Sửa"; btnUpdate.Location = new Point(95, 260); btnUpdate.Click += btnUpdate_Click; btnDelete.Text = "Xóa"; btnDelete.Location = new Point(175, 260); btnDelete.Click += btnDelete_Click; btnReload.Text = "Tải lại"; btnReload.Location = new Point(15, 300); btnReload.Click += btnReload_Click;
            panelDetail.Controls.AddRange(new Control[] { lblId, txtCustomerId, lblName, txtCustomerName, lblPhone, txtPhone, lblAddress, txtAddress, btnAdd, btnUpdate, btnDelete, btnReload }); Controls.Add(panelGrid); Controls.Add(panelDetail); ClientSize = new Size(1000, 600); Text = "Quản lý khách hàng"; Load += FormCustomerManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit(); panelGrid.ResumeLayout(false); panelDetail.ResumeLayout(false); panelDetail.PerformLayout(); ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit(); ResumeLayout(false);
        }
    }
}
