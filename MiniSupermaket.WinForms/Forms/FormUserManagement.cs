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
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.Email), HeaderText = "Email", Width = 180 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.Role), HeaderText = "Vai trò", Width = 90 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(UserDto.StatusText), HeaderText = "Trạng thái", Width = 90 });
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
            if (e.RowIndex < 0 || dgvUsers.Rows[e.RowIndex].DataBoundItem is not UserDto u) return;
            _selected = u;
            txtUsername.Text = u.Username;
            txtFullName.Text = u.FullName;
            txtEmail.Text = u.Email;
            txtPassword.Clear();
            cboRole.SelectedItem = u.Role;
        }

        private async void btnAddUser_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtPassword.Text.Trim().Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new UserCreateRequest
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = cboRole.SelectedItem?.ToString() ?? Roles.Cashier
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                    txtUsername.Clear(); txtPassword.Clear(); txtFullName.Clear(); txtEmail.Clear();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnResetPassword_Click(object? sender, EventArgs e)
        {
            if (!EnsureSelected()) return;
            if (MessageBox.Show($"Đặt lại mật khẩu của \"{_selected!.Username}\" về 123456?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                var res = await ApiClientService.Client.PutAsync($"users/{_selected.Id}/reset-password", null);
                MessageBox.Show(res.IsSuccessStatusCode ? "Đã đặt lại mật khẩu về 123456." : "Đặt lại mật khẩu thất bại!",
                    "Thông báo", MessageBoxButtons.OK,
                    res.IsSuccessStatusCode ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnToggleLock_Click(object? sender, EventArgs e)
        {
            if (!EnsureSelected()) return;
            if (_selected!.Username.Equals(SessionManager.CurrentUsername, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Không thể tự khóa tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string action = _selected.IsActive ? "KHÓA" : "MỞ KHÓA";
            if (MessageBox.Show($"Xác nhận {action} tài khoản \"{_selected.Username}\"?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                var res = await ApiClientService.Client.PutAsync($"users/{_selected.Id}/toggle-lock", null);
                if (res.IsSuccessStatusCode) await LoadUsersAsync();
                else MessageBox.Show("Thao tác thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EnsureSelected()
        {
            if (_selected != null) return true;
            MessageBox.Show("Hãy chọn một tài khoản trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
}
