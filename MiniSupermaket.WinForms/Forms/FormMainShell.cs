namespace MiniSupermarket.WinForms
{
    public partial class FormMainShell : Form
    {
        private static readonly Color SidebarColor = Color.FromArgb(24, 30, 48);
        private static readonly Color ActiveColor = Color.FromArgb(41, 100, 180);

        // Form con đang được nhúng trong panelMainContent
        private Form? _activeForm;
        private AppScreen? _activeScreen;
        private readonly List<(Button Button, AppScreen Screen)> _navItems;

        /// <summary>true khi người dùng bấm "Đăng xuất" (Program sẽ quay lại màn hình Login).</summary>
        public bool LogoutRequested { get; private set; }

        public FormMainShell()
        {
            InitializeComponent();
            _navItems = new()
            {
                (btnPOS, AppScreen.Pos),
                (btnCategory, AppScreen.Category),
                (btnProduct, AppScreen.Product),
                (btnCustomer, AppScreen.Customer),
                (btnReports, AppScreen.Report),
                (btnUserManage, AppScreen.UserManage)
            };
        }

        private void FormMainShell_Load(object? sender, EventArgs e)
        {
            string role = SessionManager.CurrentRole;

            // Vai trò không hợp lệ: khóa toàn bộ, quay lại đăng nhập
            if (!RolePermissions.IsValidRole(role))
            {
                MessageBox.Show("Tài khoản chưa được cấp quyền hạn hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LogoutRequested = true;
                SessionManager.Clear();
                ApiClientService.ClearToken();
                BeginInvoke(new Action(Close));
                return;
            }

            // 1. Thông tin phiên đăng nhập
            string name = string.IsNullOrWhiteSpace(SessionManager.CurrentFullName)
                ? SessionManager.CurrentUsername : SessionManager.CurrentFullName;
            lblUserInfo.Text = $"Nhân viên: {name} ({SessionManager.CurrentUsername}) | Vai trò: [{role}]";

            // 2. Lớp bảo vệ 1: ẩn/hiện nút theo vai trò
            ApplyRolePermissions(role);

            // 3. Mở màn hình mặc định theo vai trò
            OpenDefaultScreenByRole(role);
        }

        // =====================================================================
        //  NHÚNG FORM CON ĐỘNG VÀO panelMainContent
        // =====================================================================

        /// <summary>Nhúng một Form con vào vùng panelMainContent (không mở cửa sổ mới).</summary>
        private void OpenChildForm(Form childForm, string screenTitle, Button senderButton)
        {
            // Giải phóng form cũ
            if (_activeForm != null)
            {
                _activeForm.Close();
                _activeForm.Dispose();
            }

            HighlightActiveButton(senderButton);
            _activeForm = childForm;

            childForm.TopLevel = false;                        // không còn là cửa sổ độc lập
            childForm.FormBorderStyle = FormBorderStyle.None;  // bỏ viền & thanh tiêu đề
            childForm.Dock = DockStyle.Fill;                   // tràn vùng chứa

            panelMainContent.Controls.Clear();
            panelMainContent.Controls.Add(childForm);
            panelMainContent.Tag = childForm;

            lblTitle.Text = screenTitle;
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>Làm nổi bật nút menu đang chọn.</summary>
        private void HighlightActiveButton(Button activeButton)
        {
            foreach (var (btn, _) in _navItems)
                btn.BackColor = SidebarColor;
            activeButton.BackColor = ActiveColor;
        }

        // =====================================================================
        //  PHÂN QUYỀN (RBAC) - 2 LỚP BẢO VỆ
        // =====================================================================

        /// <summary>Lớp 1 (giao diện): ẩn các nút Sidebar mà vai trò không được phép dùng.</summary>
        private void ApplyRolePermissions(string role)
        {
            foreach (var (btn, screen) in _navItems)
                btn.Visible = RolePermissions.CanAccess(role, screen);
        }

        private void OpenDefaultScreenByRole(string role) =>
            Navigate(RolePermissions.DefaultScreen(role));

        /// <summary>
        /// Lớp 2 (logic): kiểm tra quyền thật sự trước khi mở màn hình.
        /// Dù nút bị ẩn hay bị gọi bằng cách khác, vai trò không hợp lệ vẫn bị chặn.
        /// </summary>
        private void Navigate(AppScreen screen)
        {
            if (!RolePermissions.CanAccess(SessionManager.CurrentRole, screen))
            {
                string msg = screen == AppScreen.Report
                    ? "Bạn không có quyền xem dữ liệu tài chính của siêu thị!"
                    : "Bạn không có quyền truy cập chức năng này!";
                MessageBox.Show(msg, "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_activeScreen == screen && _activeForm is { IsDisposed: false }) return; // đang ở màn hình này

            switch (screen)
            {
                case AppScreen.Pos:
                    OpenChildForm(new FormPOS(), "HỆ THỐNG QUẦY BÁN HÀNG & THU NGÂN (POS)", btnPOS);
                    break;
                case AppScreen.Category:
                    OpenChildForm(new FormCategoryManagement(), "QUẢN LÝ DANH MỤC NHÓM HÀNG", btnCategory);
                    break;
                case AppScreen.Product:
                    OpenChildForm(new FormProductManagement(), "QUẢN LÝ THÔNG TIN SẢN PHẨM & KHO HÀNG", btnProduct);
                    break;
                case AppScreen.Customer:
                    OpenChildForm(new FormCustomerManagement(), "QUẢN LÝ KHÁCH HÀNG & TÍCH ĐIỂM", btnCustomer);
                    break;
                case AppScreen.Report:
                    OpenChildForm(new FormQuickReport(), "BÁO CÁO DOANH THU & HIỆU SUẤT CA BÁN", btnReports);
                    break;
                case AppScreen.UserManage:
                    OpenChildForm(new FormUserManagement(), "QUẢN TRỊ TÀI KHOẢN VÀ PHÂN QUYỀN HỆ THỐNG", btnUserManage);
                    break;
            }
            _activeScreen = screen;
        }

        // =====================================================================
        //  SỰ KIỆN CLICK TRÊN SIDEBAR
        // =====================================================================
        private void btnPOS_Click(object? sender, EventArgs e) => Navigate(AppScreen.Pos);
        private void btnCategory_Click(object? sender, EventArgs e) => Navigate(AppScreen.Category);
        private void btnProduct_Click(object? sender, EventArgs e) => Navigate(AppScreen.Product);
        private void btnCustomer_Click(object? sender, EventArgs e) => Navigate(AppScreen.Customer);
        private void btnReports_Click(object? sender, EventArgs e) => Navigate(AppScreen.Report);      // chỉ Admin
        private void btnUserManage_Click(object? sender, EventArgs e) => Navigate(AppScreen.UserManage); // chỉ Admin

        private void btnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất phiên làm việc?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            SessionManager.Clear();
            ApiClientService.ClearToken();
            LogoutRequested = true;
            Close(); // Program.cs sẽ hiện lại FormLogin
        }
    }
}
