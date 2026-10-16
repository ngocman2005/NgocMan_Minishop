namespace MiniSupermarket.WinForms.Security;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Cashier = "Cashier";
    public const string Warehouse = "Warehouse";
}

/// <summary>Các màn hình (chức năng) của hệ thống, tương ứng với các nút trên Sidebar.</summary>
public enum AppScreen
{
    Pos,
    Category,
    Product,
    Customer,
    Report,
    UserManage
}

/// <summary>
/// Ma trận phân quyền tập trung (RBAC phía Client).
/// Dùng chung cho 2 lớp bảo vệ: (1) ẩn nút ở Sidebar, (2) chặn logic khi bấm nút / bấm Action.
/// </summary>
public static class RolePermissions
{
    private static readonly Dictionary<string, HashSet<AppScreen>> Matrix =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [Roles.Admin] = new()
            {
                AppScreen.Pos, AppScreen.Category, AppScreen.Product,
                AppScreen.Customer, AppScreen.Report, AppScreen.UserManage
            },
            [Roles.Cashier] = new() { AppScreen.Pos, AppScreen.Customer },
            [Roles.Warehouse] = new() { AppScreen.Category, AppScreen.Product }
        };

    public static bool IsValidRole(string? role) =>
        !string.IsNullOrWhiteSpace(role) && Matrix.ContainsKey(role.Trim());

    public static bool CanAccess(string? role, AppScreen screen) =>
        IsValidRole(role) && Matrix[role!.Trim()].Contains(screen);

    public static bool IsAdmin(string? role) =>
        string.Equals(role?.Trim(), Roles.Admin, StringComparison.OrdinalIgnoreCase);

    /// <summary>Màn hình mở mặc định ngay sau khi đăng nhập (theo bảng tổng kết mục 4 của đề).</summary>
    public static AppScreen DefaultScreen(string? role)
    {
        if (string.Equals(role?.Trim(), Roles.Cashier, StringComparison.OrdinalIgnoreCase)) return AppScreen.Pos;
        if (string.Equals(role?.Trim(), Roles.Warehouse, StringComparison.OrdinalIgnoreCase)) return AppScreen.Product;
        return AppScreen.Category; // Admin
    }
}
