using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        public CustomersController(SupermarketDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var query = _context.Customers.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(c => c.CustomerName.Contains(q) || c.PhoneNumber.Contains(q));

            var total = await query.CountAsync();
            var items = await query.OrderBy(c => c.CustomerId).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, page, pageSize, items });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.Customers.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _context.Customers.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.CustomerId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer update)
        {
            var existing = await _context.Customers.FindAsync(id);
            if (existing == null) return NotFound();
            existing.CustomerName = update.CustomerName;
            existing.PhoneNumber = update.PhoneNumber;
            existing.Address = update.Address;
            existing.RewardPoints = update.RewardPoints;
            existing.MembershipRank = update.MembershipRank;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Customers.FindAsync(id);
            if (item == null) return NotFound();
            _context.Customers.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
