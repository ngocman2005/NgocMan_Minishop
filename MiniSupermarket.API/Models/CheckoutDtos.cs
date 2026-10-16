namespace MiniSupermarket.API.Models
{
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
