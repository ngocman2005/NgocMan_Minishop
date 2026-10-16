namespace MiniSupermarket.WinForms
{
    /// <summary>Form mẫu hiển thị nhóm hàng. Thay bằng FormCategoryManagement đầy đủ của Buổi 2-3 nếu đã có.</summary>
    public partial class FormCategoryManagement : Form
    {
        public FormCategoryManagement()
        {
            InitializeComponent();
            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CategoryDto.CategoryId), HeaderText = "Mã nhóm", Width = 100 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CategoryDto.CategoryName), HeaderText = "Tên nhóm hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        }

        private async void FormCategoryManagement_Load(object? sender, EventArgs e) => await LoadAsync();
        private async void btnReload_Click(object? sender, EventArgs e) => await LoadAsync();

        private async Task LoadAsync()
        {
            try
            {
                dgvCategories.DataSource = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
