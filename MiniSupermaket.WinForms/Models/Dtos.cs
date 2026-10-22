using System.Text.Json.Serialization;

namespace MiniSupermarket.WinForms.Models;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public override string ToString() => CategoryName;
}

public class PagedCustomersResult
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<CustomerDto> Items { get; set; } = new();
}

public class ProductDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int MaxStock { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
}

public class CustomerDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string MembershipRank { get; set; } = string.Empty;
    public int RewardPoints { get; set; }

    public string FullName { get => CustomerName; set => CustomerName = value; }
    public string Phone { get => PhoneNumber; set => PhoneNumber = value; }
    public string Rank { get => MembershipRank; set => MembershipRank = value; }
    public int Points { get => RewardPoints; set => RewardPoints = value; }
}

public class UserDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public int Id { get => UserId; set => UserId = value; }
    public string Email { get; set; } = string.Empty;

    [JsonIgnore]
    public string StatusText => IsActive ? "Hoạt động" : "Đã khóa";
}

public class LoginRequest
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Password { get => PasswordHash; set => PasswordHash = value; }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}

public class QuickReportDto
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public string BestSeller { get; set; } = string.Empty;
}

public class CheckoutItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public class CheckoutRequest
{
    public string CashierUsername { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public List<CheckoutItem> Items { get; set; } = new();
}

public class ProductSaveRequest
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
}

public class UserCreateRequest
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Password { get => PasswordHash; set => PasswordHash = value; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "Cashier";
}

public class UserUpdateRequest
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
