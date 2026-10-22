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

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDto>>> GetProducts()
        {
            return Ok(await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .OrderBy(p => p.ProductId)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category == null ? string.Empty : p.Category.CategoryName
                })
                .ToListAsync());
        }

        // GET: api/products/barcode/893456789001
        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<ProductItemDto>> GetByBarcode(string barcode)
        {
            var value = barcode.Trim();
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.Barcode == value)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category == null ? string.Empty : p.Category.CategoryName
                })
                .FirstOrDefaultAsync();

            return product == null
                ? NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch {value}." })
                : Ok(product);
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductItemDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.ProductId == id)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category == null ? string.Empty : p.Category.CategoryName
                })
                .FirstOrDefaultAsync();

            return product == null ? NotFound(new { message = "Không tìm thấy sản phẩm." }) : Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == product.CategoryId);
            if (!categoryExists)
                return BadRequest(new { message = "CategoryId không tồn tại." });

            var barcodeExists = await _context.Products.AnyAsync(p => p.Barcode == product.Barcode);
            if (barcodeExists)
                return Conflict(new { message = "Mã vạch đã tồn tại." });

            product.ProductId = 0;
            product.Category = null;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product update)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm." });

            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == update.CategoryId);
            if (!categoryExists)
                return BadRequest(new { message = "CategoryId không tồn tại." });

            var barcodeExists = await _context.Products
                .AnyAsync(p => p.ProductId != id && p.Barcode == update.Barcode);
            if (barcodeExists)
                return Conflict(new { message = "Mã vạch đã tồn tại." });

            product.Barcode = update.Barcode;
            product.ProductName = update.ProductName;
            product.Price = update.Price;
            product.StockQuantity = update.StockQuantity;
            product.CategoryId = update.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { message = "Không tìm thấy sản phẩm." });

            var isUsedInOrder = await _context.OrderDetails.AnyAsync(d => d.ProductId == id);
            if (isUsedInOrder)
                return Conflict(new { message = "Không thể xóa sản phẩm đã có trong hóa đơn." });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/products/paged?keyword=sua&pageNumber=1&pageSize=5
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            // 1. Kiểm tra tham số trang đầu vào hợp lệ
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 5; // Mặc định 5 dòng, tối đa 50 dòng

            // 2. Khởi tạo truy vấn LINQ IQueryable kết hợp Eager Loading (.Include)
            // AsNoTracking giúp tối ưu hóa bộ nhớ RAM cho các truy vấn chỉ đọc dữ liệu
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 3. Lọc theo từ khóa tìm kiếm (nếu người dùng có nhập)
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                                      || p.Barcode.Contains(search));
            }

            // 4. Đếm tổng số lượng bản ghi thỏa mãn điều kiện
            int totalItems = await query.CountAsync();

            // 5. Tính toán tổng số trang
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 6. Áp dụng toán tử Skip và Take để phân trang trực tiếp dưới SQL Server
            var items = await query
                .OrderBy(p => p.ProductId) // Bắt buộc phải OrderBy trước khi Skip/Take
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                })
                .ToListAsync();

            // 7. Đóng gói kết quả trả về cho Client
            var response = new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }
    }
}

