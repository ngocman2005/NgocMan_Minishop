using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using MiniSupermarket.API.Dto;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        public OrdersController(SupermarketDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Orders.Include(o => o.OrderDetails).AsNoTracking().ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // POST: api/orders/checkout
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng đang trống." });

            var customer = string.IsNullOrWhiteSpace(request.CustomerPhone)
                ? await _context.Customers.OrderBy(c => c.CustomerId).FirstOrDefaultAsync()
                : await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhone.Trim());

            if (customer == null)
                return BadRequest(new { message = "Không tìm thấy khách hàng." });

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var products = new List<Product>();
                foreach (var item in request.Items)
                {
                    if (item.Quantity < 1)
                        return BadRequest(new { message = "Số lượng sản phẩm phải lớn hơn 0." });

                    var product = await _context.Products.FindAsync(item.ProductId);
                    if (product == null)
                        return BadRequest(new { message = $"Không tìm thấy sản phẩm #{item.ProductId}." });
                    if (product.StockQuantity < item.Quantity)
                        return BadRequest(new { message = $"Sản phẩm {product.ProductName} không đủ tồn kho." });
                    products.Add(product);
                }

                var order = new Order
                {
                    OrderCode = $"HD{DateTime.Now:yyyyMMddHHmmssfff}",
                    CustomerId = customer.CustomerId,
                    CashierUsername = string.IsNullOrWhiteSpace(request.CashierUsername) ? "unknown" : request.CashierUsername,
                    CreatedDate = DateTime.Now,
                    TotalItems = request.Items.Sum(i => i.Quantity),
                    FinalAmount = request.Items.Sum(i => i.UnitPrice * i.Quantity)
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                for (var index = 0; index < request.Items.Count; index++)
                {
                    var item = request.Items[index];
                    var product = products[index];
                    product.StockQuantity -= item.Quantity;
                    _context.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        LineTotal = item.UnitPrice * item.Quantity
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok(new { orderId = order.OrderId, orderCode = order.OrderCode, totalAmount = order.FinalAmount });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Transactional creation: create Order and OrderDetails, update stock
            await using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    OrderCode = request.OrderCode,
                    CustomerId = request.CustomerId,
                    CashierUsername = request.CashierUsername,
                    CreatedDate = DateTime.Now,
                    TotalItems = request.Details.Sum(d => d.Quantity),
                    FinalAmount = request.Details.Sum(d => d.UnitPrice * d.Quantity)
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var d in request.Details)
                {
                    var product = await _context.Products.FindAsync(d.ProductId);
                    if (product == null)
                    {
                        await tx.RollbackAsync();
                        return BadRequest(new { message = $"Product {d.ProductId} not found" });
                    }
                    if (product.StockQuantity < d.Quantity)
                    {
                        await tx.RollbackAsync();
                        return BadRequest(new { message = $"Insufficient stock for product {product.ProductName}" });
                    }

                    product.StockQuantity -= d.Quantity;

                    var detail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = d.ProductId,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice,
                        LineTotal = d.UnitPrice * d.Quantity
                    };
                    _context.OrderDetails.Add(detail);
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                var created = await _context.Orders.Include(o => o.OrderDetails).ThenInclude(x => x.Product).FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
                return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, created);
            }
            catch (Exception)
            {
                await tx.RollbackAsync();
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Order update)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing == null) return NotFound();
            existing.OrderCode = update.OrderCode;
            existing.CustomerId = update.CustomerId;
            existing.CashierUsername = update.CashierUsername;
            existing.TotalItems = update.TotalItems;
            existing.FinalAmount = update.FinalAmount;
            existing.RewardPoints = update.RewardPoints;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
