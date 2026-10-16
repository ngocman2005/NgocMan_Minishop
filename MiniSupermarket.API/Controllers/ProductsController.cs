using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        public ProductsController(SupermarketDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] int? categoryId = null)
        {
            var query = _context.Products.Include(p => p.Category).AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p => p.ProductName.Contains(q) || p.Barcode.Contains(q));
            if (categoryId.HasValue && categoryId.Value > 0)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            var total = await query.CountAsync();
            var items = await query.OrderBy(p => p.ProductId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, page, pageSize, items });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpGet("barcode/{code}")]
        public async Task<IActionResult> GetByBarcode(string code)
        {
            var item = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Barcode == code);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Products.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.ProductId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product update)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Barcode = update.Barcode;
            existing.ProductName = update.ProductName;
            existing.Price = update.Price;
            existing.StockQuantity = update.StockQuantity;
            existing.CategoryId = update.CategoryId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) return NotFound();
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
