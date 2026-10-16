namespace MiniSupermarket.WinForms
{
    /// <summary>Quản lý sản phẩm & kho hàng (Warehouse, Admin).</summary>
    public partial class FormProductManagement : Form
    {
        private List<ProductDto> _allProducts = new();

        public FormProductManagement()
        {
            InitializeComponent();
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ProductId", DataPropertyName = "ProductId", HeaderText = "ID", Width = 50 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Barcode", DataPropertyName = "Barcode", HeaderText = "Mã vạch", Width = 120 });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "ProductName", DataPropertyName = "ProductName", HeaderText = "Tên sản phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "Price", DataPropertyName = "Price", HeaderText = "Đơn giá", Width = 90,
              DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "StockQuantity", DataPropertyName = "StockQuantity", HeaderText = "Tồn kho", Width = 70,
              DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            { Name = "CategoryName", DataPropertyName = "CategoryName", HeaderText = "Nhóm hàng", Width = 130 });
        }

        private async void FormProductManagement_Load(object? sender, EventArgs e)
        {
            await LoadCategoriesToComboAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCategoriesToComboAsync()
        {
            try
            {
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories")
                                 ?? new List<CategoryDto>();

                // Dùng 2 danh sách riêng để 2 ComboBox không dùng chung vị trí chọn
                cboCategory.DataSource = new List<CategoryDto>(categories);
                cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategory.ValueMember = nameof(CategoryDto.CategoryId);

                var filter = new List<CategoryDto> { new() { CategoryId = 0, CategoryName = "-- Tất cả nhóm hàng --" } };
                filter.AddRange(categories);
                cboFilterCategory.DataSource = filter;
                cboFilterCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboFilterCategory.ValueMember = nameof(CategoryDto.CategoryId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                _allProducts = await ApiClientService.Client.GetFromJsonAsync<List<ProductDto>>("products")
                               ?? new List<ProductDto>();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Tìm kiếm / lọc ----------
        private void ApplyFilter()
        {
            string key = txtSearchBarcode.Text.Trim();
            int catId = cboFilterCategory.SelectedValue is int v ? v : 0;

            dgvProducts.DataSource = _allProducts
                .Where(p => (key.Length == 0
                             || p.Barcode.Contains(key, StringComparison.OrdinalIgnoreCase)
                             || p.ProductName.Contains(key, StringComparison.OrdinalIgnoreCase))
                            && (catId == 0 || p.CategoryId == catId))
                .ToList();
        }

        private void btnSearch_Click(object? sender, EventArgs e) => ApplyFilter();

        private void txtSearchBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilter();
            }
        }

        // ---------- Chọn dòng -> đổ lên form chi tiết ----------
        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvProducts.Rows[e.RowIndex].DataBoundItem is not ProductDto p) return;

            txtId.Text = p.ProductId.ToString();
            txtBarcode.Text = p.Barcode;
            txtProductName.Text = p.ProductName;
            nudPrice.Value = Math.Min(p.Price, nudPrice.Maximum);
            nudStock.Value = Math.Min(p.StockQuantity, (int)nudStock.Maximum);
            cboCategory.SelectedValue = p.CategoryId;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtBarcode.Text) || string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Mã vạch và tên sản phẩm không được để trống!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (nudPrice.Value <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cboCategory.SelectedValue is not int)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private ProductSaveRequest BuildRequest(int id) => new()
        {
            ProductId = id,
            Barcode = txtBarcode.Text.Trim(),
            ProductName = txtProductName.Text.Trim(),
            Price = nudPrice.Value,
            StockQuantity = (int)nudStock.Value,
            CategoryId = (int)cboCategory.SelectedValue!
        };

        // ---------- CRUD ----------
        private async void btnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs()) return;
            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("products", BuildRequest(0));
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(res.StatusCode == System.Net.HttpStatusCode.Conflict
                        ? "Mã vạch đã tồn tại!" : "Thêm mới thất bại!", "Thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Hãy chọn một sản phẩm trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInputs()) return;

            int id = int.Parse(txtId.Text);
            try
            {
                var res = await ApiClientService.Client.PutAsJsonAsync($"products/{id}", BuildRequest(id));
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                }
                else
                {
                    MessageBox.Show(res.StatusCode == System.Net.HttpStatusCode.Conflict
                        ? "Mã vạch đã tồn tại ở sản phẩm khác!" : "Cập nhật thất bại!", "Thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;
            int id = int.Parse(txtId.Text);

            if (MessageBox.Show($"Xác nhận xóa sản phẩm ID = {id}?", "Xác nhận", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                var res = await ApiClientService.Client.DeleteAsync($"products/{id}");
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Đã xóa sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không thể xóa sản phẩm (có thể đã phát sinh hóa đơn).", "Thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLoad_Click(object? sender, EventArgs e)
        {
            txtSearchBarcode.Clear();
            if (cboFilterCategory.Items.Count > 0) cboFilterCategory.SelectedIndex = 0;
            await LoadProductsAsync();
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            nudPrice.Value = 0;
            nudStock.Value = 0;
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            txtBarcode.Focus();
        }
    }
}
