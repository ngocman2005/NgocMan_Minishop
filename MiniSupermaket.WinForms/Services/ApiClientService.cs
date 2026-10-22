/*
 * Họ tên sinh viên: Nguyen Ngoc Man
 * Mã sinh viên: 2123110100
 * Mô tả: Lớp tĩnh quản lý HttpClient kết nối Web API, tự động gắn JWT Bearer Token
 */
using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms.Services
{
    public static class ApiClientService
    {
        // Khởi tạo một phiên bản HttpClient dùng chung cho toàn ứng dụng
        public static readonly HttpClient Client = new HttpClient
        {
            // Đảm bảo số cổng Port trùng khớp với số Port đang chạy của Web API Backend
            BaseAddress = new Uri("https://localhost:7086/api/")
        };

        // Phương thức cập nhật Bearer Token sau khi đăng nhập thành công
        public static void AttachToken(string token)
        {
            Client.DefaultRequestHeaders.Authorization = null; // Xóa token cũ
            if (!string.IsNullOrEmpty(token))
            {
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Tên tương thích với các form đăng nhập/đăng xuất hiện có.
        public static void SetToken(string token) => AttachToken(token);

        public static void ClearToken()
        {
            Client.DefaultRequestHeaders.Authorization = null;
        }
    }
}

