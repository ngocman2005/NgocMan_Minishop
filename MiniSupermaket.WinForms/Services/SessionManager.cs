namespace MiniSupermarket.WinForms.Services;

/// <summary>Lưu phiên đăng nhập hiện tại (JWT, tên, vai trò).</summary>
public static class SessionManager
{
    public static string JwtToken { get; set; } = string.Empty;
    public static string CurrentUsername { get; set; } = string.Empty;
    public static string CurrentFullName { get; set; } = string.Empty;
    public static string CurrentRole { get; set; } = string.Empty;

    public static bool IsAuthenticated => !string.IsNullOrEmpty(JwtToken);

    public static void Clear()
    {
        JwtToken = string.Empty;
        CurrentUsername = string.Empty;
        CurrentFullName = string.Empty;
        CurrentRole = string.Empty;
    }
}
