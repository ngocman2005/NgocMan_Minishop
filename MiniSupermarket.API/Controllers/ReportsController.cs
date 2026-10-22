using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;

        public ReportsController(IConfiguration configuration, SupermarketDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/reports/quick?date=2026-10-22
        [HttpGet("quick")]
        public async Task<IActionResult> GetQuickReport([FromQuery] DateTime? date)
        {
            var reportDate = (date ?? DateTime.Today).Date;
            var nextDate = reportDate.AddDays(1);

            var orders = _context.Orders
                .AsNoTracking()
                .Where(o => o.CreatedDate >= reportDate && o.CreatedDate < nextDate);

            var totalOrders = await orders.CountAsync();
            var totalRevenue = await orders.Select(o => (decimal?)o.FinalAmount).SumAsync() ?? 0m;

            var bestSeller = await _context.OrderDetails
                .AsNoTracking()
                .Where(d => d.Order != null && d.Order.CreatedDate >= reportDate && d.Order.CreatedDate < nextDate)
                .GroupBy(d => d.ProductId)
                .Select(g => new
                {
                    ProductName = g.Select(d => d.Product!.ProductName).FirstOrDefault() ?? string.Empty,
                    Quantity = g.Sum(d => d.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .Select(x => x.ProductName)
                .FirstOrDefaultAsync() ?? string.Empty;

            return Ok(new
            {
                totalOrders,
                totalRevenue,
                bestSeller
            });
        }

        // GET: api/reports/revenue-by-cashier
        [HttpGet("revenue-by-cashier")]
        public IActionResult GetRevenueByCashier()
        {
            var reportList = new List<CashierRevenueDto>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetRevenueByCashier", connection))
                {
                    // QUAN TRONG: bao cho ADO.NET biet day la Stored Procedure
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportList.Add(new CashierRevenueDto
                            {
                                CashierUsername = reader["CashierUsername"].ToString() ?? string.Empty,
                                TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"]),
                                TotalPointsAwarded = reader["TotalPointsAwarded"] != DBNull.Value
                                    ? Convert.ToInt32(reader["TotalPointsAwarded"])
                                    : 0
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi thực thi Stored Procedure trên SQL Server: " + ex.Message
                });
            }

            return Ok(new
            {
                success = true,
                generatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                totalCashiers = reportList.Count,
                grandTotalRevenue = reportList.Sum(x => x.TotalRevenue),
                data = reportList
            });
        }

        // Bai tap 3.1: GET api/reports/revenue-by-date?fromDate=2026-08-30&toDate=2026-08-31
        [HttpGet("revenue-by-date")]
        public IActionResult GetRevenueByDate([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var list = new List<object>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand("sp_GetRevenueByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new
                            {
                                cashierUsername = reader["CashierUsername"].ToString(),
                                totalOrders = Convert.ToInt32(reader["TotalOrders"]),
                                totalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }

            return Ok(new { success = true, data = list });
        }
    }
}
