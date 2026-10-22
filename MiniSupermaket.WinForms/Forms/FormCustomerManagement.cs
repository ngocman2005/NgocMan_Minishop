using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCustomerManagement : Form
    {
        private List<CustomerDto> _items = new();
        public FormCustomerManagement() { InitializeComponent(); SetupGrid(); }

        private void SetupGrid()
        {
            dgvCustomers.AutoGenerateColumns = false; dgvCustomers.Columns.Clear();
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.CustomerId), HeaderText = "Mã KH", Width = 70 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.CustomerName), HeaderText = "Họ tên", Width = 180 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.PhoneNumber), HeaderText = "Điện thoại", Width = 130 });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.Address), HeaderText = "Địa chỉ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CustomerDto.RewardPoints), HeaderText = "Điểm", Width = 70 });
        }

        private async void FormCustomerManagement_Load(object? sender, EventArgs e) => await LoadAsync();
        private async void btnReload_Click(object? sender, EventArgs e) => await LoadAsync();
        private async Task LoadAsync()
        {
            try
            {
                var result = await ApiClientService.Client.GetFromJsonAsync<PagedCustomersResult>("customers?page=1&pageSize=1000");
                _items = result?.Items ?? new();
                dgvCustomers.DataSource = _items;
                ClearInputs();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvCustomers.Rows[e.RowIndex].DataBoundItem is not CustomerDto c) return;
            txtCustomerId.Text = c.CustomerId.ToString(); txtCustomerName.Text = c.CustomerName; txtPhone.Text = c.PhoneNumber; txtAddress.Text = c.Address ?? string.Empty;
        }
        private bool Valid() { if (!string.IsNullOrWhiteSpace(txtCustomerName.Text) && !string.IsNullOrWhiteSpace(txtPhone.Text)) return true; MessageBox.Show("Họ tên và số điện thoại không được trống."); return false; }
        private async void btnAdd_Click(object? sender, EventArgs e) { if (!Valid()) return; await SendAsync(HttpMethod.Post, "customers", new { CustomerName = txtCustomerName.Text.Trim(), PhoneNumber = txtPhone.Text.Trim(), Address = txtAddress.Text.Trim() }, "Thêm khách hàng thành công."); }
        private async void btnUpdate_Click(object? sender, EventArgs e) { if (!Valid() || !int.TryParse(txtCustomerId.Text, out var id)) return; await SendAsync(HttpMethod.Put, $"customers/{id}", new { CustomerName = txtCustomerName.Text.Trim(), PhoneNumber = txtPhone.Text.Trim(), Address = txtAddress.Text.Trim() }, "Cập nhật khách hàng thành công."); }
        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtCustomerId.Text, out var id)) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            await SendAsync(HttpMethod.Delete, $"customers/{id}", null, "Xóa khách hàng thành công.");
        }
        private async Task SendAsync(HttpMethod method, string url, object? body, string success)
        {
            using var request = new HttpRequestMessage(method, url); if (body != null) request.Content = JsonContent.Create(body);
            try { using var response = await ApiClientService.Client.SendAsync(request); if (response.IsSuccessStatusCode) { MessageBox.Show(success); await LoadAsync(); } else MessageBox.Show("Thao tác thất bại: " + await response.Content.ReadAsStringAsync()); }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message); }
        }
        private void ClearInputs() { txtCustomerId.Clear(); txtCustomerName.Clear(); txtPhone.Clear(); txtAddress.Clear(); }
    }
}
