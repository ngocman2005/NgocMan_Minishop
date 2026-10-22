using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MiniSupermarket.API.Dto
{
    public class ProductCreateRequest
    {
        [Required]
        public string Barcode { get; set; } = string.Empty;
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

    public class CustomerCreateRequest
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; }
    }

    public class OrderDetailCreateRequest
    {
        [Required]
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
    }

    public class OrderCreateRequest
    {
        [Required]
        public string OrderCode { get; set; } = string.Empty;
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string CashierUsername { get; set; } = string.Empty;
        public List<OrderDetailCreateRequest> Details { get; set; } = new();
    }

    public class UserCreateRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
    }

    public class CheckoutRequest
    {
        public string CashierUsername { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public List<CheckoutItemRequest> Items { get; set; } = new();
    }

    public class CheckoutItemRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
