using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
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
