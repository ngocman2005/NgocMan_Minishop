namespace MiniSupermarket.WinForms
{
    /// <summary>Form mẫu hiển thị khách hàng thân thiết. Thay bằng bản đầy đủ của Buổi 2-3 nếu đã có.</summary>
    public partial class FormCustomerManagement : Form
    {
        public FormCustomerManagement()
        {
            InitializeComponent();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.CustomerId), HeaderText = "Mã KH", Width = 80 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.FullName), HeaderText = "Họ và tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.Phone), HeaderText = "Số điện thoại", Width = 130 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.Rank), HeaderText = "Hạng thẻ", Width = 100 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.Points), HeaderText = "Điểm", Width = 80 });
        }

        private async void FormCustomerManagement_Load(object? sender, EventArgs e) => await LoadAsync();
        private async void btnReload_Click(object? sender, EventArgs e) => await LoadAsync();

        private async Task LoadAsync()
        {
            try
            {
                dgvCustomers.DataSource = await ApiClientService.Client.GetFromJsonAsync<List<CustomerDto>>("customers");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
