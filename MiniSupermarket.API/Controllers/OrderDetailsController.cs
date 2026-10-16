using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        public OrderDetailsController(SupermarketDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.OrderDetails.Include(d => d.Product).AsNoTracking().ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var detail = await _context.OrderDetails.Include(d => d.Product).FirstOrDefaultAsync(d => d.OrderDetailId == id);
            if (detail == null) return NotFound();
            return Ok(detail);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDetail model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.OrderDetails.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.OrderDetailId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OrderDetail update)
        {
            var exist = await _context.OrderDetails.FindAsync(id);
            if (exist == null) return NotFound();
            exist.ProductId = update.ProductId;
            exist.Quantity = update.Quantity;
            exist.UnitPrice = update.UnitPrice;
            exist.LineTotal = update.LineTotal;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.OrderDetails.FindAsync(id);
            if (item == null) return NotFound();
            _context.OrderDetails.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
