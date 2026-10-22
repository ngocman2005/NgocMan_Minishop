using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormCategoryManagement : Form
    {
        private List<CategoryDto> _items = new();

        public FormCategoryManagement() { InitializeComponent(); SetupGrid(); }

        private void SetupGrid()
        {
            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.Columns.Clear();
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CategoryDto.CategoryId), HeaderText = "Mã nhóm", Width = 90 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CategoryDto.CategoryName), HeaderText = "Tên nhóm hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async void FormCategoryManagement_Load(object? sender, EventArgs e) => await LoadAsync();
        private async void btnReload_Click(object? sender, EventArgs e) => await LoadAsync();

        private async Task LoadAsync()
        {
            try
            {
                _items = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories") ?? new();
                dgvCategories.DataSource = _items;
                ClearInputs();
            }
            catch (Exception ex) { ShowError(ex); }
        }

        private void dgvCategories_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvCategories.Rows[e.RowIndex].DataBoundItem is CategoryDto item)
            {
                txtCategoryId.Text = item.CategoryId.ToString();
                txtCategoryName.Text = item.CategoryName;
            }
        }

        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateName()) return;
            var response = await ApiClientService.Client.PostAsJsonAsync("categories", new { CategoryName = txtCategoryName.Text.Trim(), Description = txtDescription.Text.Trim() });
            await HandleResponse(response, "Thêm danh mục thành công.");
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtCategoryId.Text, out var id) || !ValidateName()) return;
            var response = await ApiClientService.Client.PutAsJsonAsync($"categories/{id}", new { CategoryName = txtCategoryName.Text.Trim(), Description = txtDescription.Text.Trim() });
            await HandleResponse(response, "Cập nhật danh mục thành công.");
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtCategoryId.Text, out var id)) return;
            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            var response = await ApiClientService.Client.DeleteAsync($"categories/{id}");
            await HandleResponse(response, "Xóa danh mục thành công.");
        }

        private bool ValidateName()
        {
            if (!string.IsNullOrWhiteSpace(txtCategoryName.Text)) return true;
            MessageBox.Show("Tên danh mục không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private async Task HandleResponse(HttpResponseMessage response, string success)
        {
            if (response.IsSuccessStatusCode) { MessageBox.Show(success); await LoadAsync(); }
            else MessageBox.Show("Thao tác thất bại: " + await response.Content.ReadAsStringAsync(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ClearInputs() { txtCategoryId.Clear(); txtCategoryName.Clear(); txtDescription.Clear(); }
        private static void ShowError(Exception ex) => MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
