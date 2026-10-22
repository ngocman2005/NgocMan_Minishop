namespace MiniSupermarket.WinForms
{
    /// <summary>Quản lý tài khoản & phân quyền (chỉ Admin).</summary>
    public partial class FormUserManagement : Form
    {
        private UserDto? _selected;

        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new object[] { Roles.Admin, Roles.Cashier, Roles.Warehouse });
            cboRole.SelectedIndex = 1;
            SetupGrid();
        }

        private void SetupGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.Username), HeaderText = "Tên đăng nhập", Width = 120 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.FullName), HeaderText = "Họ và tên", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.Role), HeaderText = "Vai trò", Width = 100 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.StatusText), HeaderText = "Trạng thái", Width = 100 });
        }

        private async void FormUserManagement_Load(object? sender, EventArgs e)
        {
            // Lớp bảo vệ thứ 2: dù form được mở bằng cách nào, chỉ Admin mới được thao tác
            if (!RolePermissions.IsAdmin(SessionManager.CurrentRole))
            {
                MessageBox.Show("Chỉ Admin mới được quản trị tài khoản!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Enabled = false;
                return;
            }
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users") ?? new();
                dgvUsers.DataSource = users;
                _selected = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tô xám các tài khoản đã bị khóa
        private void dgvUsers_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                bool locked = row.DataBoundItem is UserDto { IsActive: false };
                row.DefaultCellStyle.ForeColor = locked ? Color.Gray : Color.Black;
                row.DefaultCellStyle.Font = locked ? new Font(dgvUsers.Font, FontStyle.Strikeout) : null;
            }
        }

        private void dgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvUsers.Rows[e.RowIndex].DataBoundItem is not UserDto user) return;
            _selected = user;
            txtUsername.Text = user.Username;
            txtFullName.Text = user.FullName;
            txtPassword.Clear();
            cboRole.SelectedItem = user.Role;
        }

        private async void btnAddUser_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Tên đăng nhập, họ tên và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var request = new UserCreateRequest
            {
                Username = txtUsername.Text.Trim(),
                PasswordHash = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? Roles.Cashier
            };

            try
            {
                var response = await ApiClientService.Client.PostAsJsonAsync("users", request);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Tạo tài khoản thất bại: " + await response.Content.ReadAsStringAsync(), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                await LoadUsersAsync();
                txtUsername.Clear(); txtFullName.Clear(); txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnResetPassword_Click(object? sender, EventArgs e)
        {
            if (!EnsureSelected()) return;
            _selected!.PasswordHash = "123456";
            await UpdateSelectedAsync();
        }

        private async void btnToggleLock_Click(object? sender, EventArgs e)
        {
            if (!EnsureSelected()) return;
            if (_selected!.Username.Equals(SessionManager.CurrentUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể tự khóa tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _selected.IsActive = !_selected.IsActive;
            await UpdateSelectedAsync();
        }

        private async Task UpdateSelectedAsync()
        {
            if (_selected is not { } selected)
                return;

            var request = new UserUpdateRequest
            {
                Username = selected.Username,
                PasswordHash = selected.PasswordHash,
                FullName = selected.FullName,
                Role = selected.Role,
                IsActive = selected.IsActive
            };
            var response = await ApiClientService.Client.PutAsJsonAsync($"users/{selected.UserId}", request);
            if (response.IsSuccessStatusCode) await LoadUsersAsync();
            else MessageBox.Show("Cập nhật tài khoản thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool EnsureSelected()
        {
            if (_selected != null) return true;
            MessageBox.Show("Hãy chọn một tài khoản trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
}
