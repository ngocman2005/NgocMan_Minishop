using System.Globalization;

namespace MiniSupermarket.WinForms
{
    /// <summary>Báo cáo doanh thu nhanh (chỉ Admin) - bài tập mở rộng Buổi 4.</summary>
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
            dtpReportDate.MaxDate = DateTime.Today;
            dtpReportDate.Value = DateTime.Today;
        }

        private async void FormQuickReport_Load(object? sender, EventArgs e)
        {
            // Lớp bảo vệ cấp Action: chỉ Admin được bấm nút xem doanh thu
            bool isAdmin = RolePermissions.IsAdmin(SessionManager.CurrentRole);
            btnRunReport.Enabled = isAdmin;
            dtpReportDate.Enabled = isAdmin;
            lblPermissionNote.Text = isAdmin ? "" : "Chỉ tài khoản Admin mới được xem doanh thu ca.";

            if (isAdmin) await RunReportAsync();
        }

        private async void btnRunReport_Click(object? sender, EventArgs e) => await RunReportAsync();

        private async Task RunReportAsync()
        {
            // Kiểm tra lại quyền ngay trước khi gọi dữ liệu tài chính
            if (!RolePermissions.IsAdmin(SessionManager.CurrentRole))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            btnRunReport.Enabled = false;
            try
            {
                string date = dtpReportDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var report = await ApiClientService.Client.GetFromJsonAsync<QuickReportDto>($"reports/quick?date={date}");

                if (report == null)
                {
                    MessageBox.Show("Không có dữ liệu báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lblTotalOrders.Text = report.TotalOrders.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
                lblTotalRevenue.Text = report.TotalRevenue.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
                lblBestSeller.Text = string.IsNullOrWhiteSpace(report.BestSeller) ? "—" : report.BestSeller;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunReport.Enabled = RolePermissions.IsAdmin(SessionManager.CurrentRole);
            }
        }
    }
}
