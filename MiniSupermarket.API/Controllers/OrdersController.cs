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
