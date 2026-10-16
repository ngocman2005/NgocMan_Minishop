using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    // DTO tra cuu san pham cho quay POS
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    [Route("api/ado-products")]
    [ApiController]
    public class AdoProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdoProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/ado-products/barcode/893456789001  (Connected mode - SqlDataReader)
        [HttpGet("barcode/{barcode}")]
        public IActionResult GetProductByBarcode(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return BadRequest(new { message = "Mã vạch không được để trống!" });

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                     FROM Products
                                     WHERE Barcode = @Barcode";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Barcode", barcode.Trim());
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Ok(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
            }

            return NotFound(new { message = $"Không tìm thấy sản phẩm có mã vạch: {barcode}" });
        }

        // GET: api/ado-products/disconnected-cache  (Disconnected mode - DataTable + SqlDataAdapter)
        [HttpGet("disconnected-cache")]
        public IActionResult GetProductsDisconnected()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var productTable = new DataTable("CachedProducts");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                     FROM Products
                                     ORDER BY ProductId ASC";

                    using (var command = new SqlCommand(query, connection))
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        // Fill(): tu mo ket noi -> do du lieu -> tu dong ket noi
                        adapter.Fill(productTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
            }

            // Tu day ket noi DA DONG, thao tac hoan toan tren RAM
            var resultList = new List<ProductLookupDto>();
            foreach (DataRow row in productTable.Rows)
            {
                resultList.Add(new ProductLookupDto
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Barcode = row["Barcode"].ToString() ?? string.Empty,
                    ProductName = row["ProductName"].ToString() ?? string.Empty,
                    Price = Convert.ToDecimal(row["Price"]),
                    StockQuantity = Convert.ToInt32(row["StockQuantity"])
                });
            }

            return Ok(new
            {
                totalLoaded = productTable.Rows.Count,
                cachedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                data = resultList
            });
        }

        // Bai tap 2.1: GET api/ado-products/filter-stock?minStock=50
        // (lay san pham co ton kho <= minStock de canh bao nhap hang)
        [HttpGet("filter-stock")]
        public IActionResult FilterStock([FromQuery] int minStock = 50)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;
            var list = new List<ProductLookupDto>();

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    string query = @"SELECT ProductId, Barcode, ProductName, Price, StockQuantity
                                     FROM Products
                                     WHERE StockQuantity <= @MinStock
                                     ORDER BY StockQuantity ASC";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MinStock", minStock);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(new ProductLookupDto
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Barcode = reader["Barcode"].ToString() ?? string.Empty,
                                    ProductName = reader["ProductName"].ToString() ?? string.Empty,
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    StockQuantity = Convert.ToInt32(reader["StockQuantity"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Lỗi truy vấn SQL: " + ex.Message });
            }

            return Ok(new { total = list.Count, data = list });
        }
    }
}
