namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// Màn hình đăng nhập (đã làm ở Buổi 3). Bản này chỉ để chạy độc lập Buổi 4:
    /// nếu đã có FormLogin riêng, giữ lại bản của bạn và bỏ file này.
    /// </summary>
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (user.Length == 0 || pass.Length == 0)
            {
                lblError.Text = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return;
            }

            btnLogin.Enabled = false;
            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("auth/login",
                    new LoginRequest { Username = user, Password = pass });

                if (!res.IsSuccessStatusCode)
                {
                    lblError.Text = res.StatusCode == System.Net.HttpStatusCode.Forbidden
                        ? "Tài khoản đã bị khóa."
                        : "Sai tên đăng nhập hoặc mật khẩu.";
                    return;
                }

                var data = await res.Content.ReadFromJsonAsync<LoginResponse>();
                if (data == null) { lblError.Text = "Phản hồi đăng nhập không hợp lệ."; return; }

                SessionManager.JwtToken = data.Token;
                SessionManager.CurrentUsername = data.Username;
                SessionManager.CurrentFullName = data.FullName;
                SessionManager.CurrentRole = data.Role;
                ApiClientService.SetToken(data.Token);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi kết nối: " + ex.Message;
            }
            finally
            {
                btnLogin.Enabled = true;
            }
        }
    }
}
