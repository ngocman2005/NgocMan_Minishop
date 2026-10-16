using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/checkout
        [HttpPost]
        public IActionResult ProcessCheckout([FromBody] CheckoutRequestDto request)
        {
            if (request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng rỗng, không thể tiến hành thanh toán!" });

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
                return BadRequest(new { message = "Thiếu thông tin tài khoản thu ngân lập hóa đơn!" });

            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    decimal finalAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice);
                    int totalItems = request.Items.Sum(i => i.Quantity);
                    int rewardPoints = (int)(finalAmount / 10000);   // 10.000d = 1 diem
                    string orderCode = $"HD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";

                    // BUOC 1: Tao hoa don, lay OrderId vua sinh bang OUTPUT INSERTED
                    string insertOrderSql = @"
                        INSERT INTO Orders (OrderCode, CreatedDate, CashierUsername, CustomerId, TotalItems, FinalAmount, RewardPoints)
                        OUTPUT INSERTED.OrderId
                        VALUES (@OrderCode, GETDATE(), @CashierUsername, @CustomerId, @TotalItems, @FinalAmount, @RewardPoints)";

                    int orderId;
                    using (var cmdOrder = new SqlCommand(insertOrderSql, connection, transaction))
                    {
                        cmdOrder.Parameters.AddWithValue("@OrderCode", orderCode);
                        cmdOrder.Parameters.AddWithValue("@CashierUsername", request.CashierUsername.Trim());
                        cmdOrder.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                        cmdOrder.Parameters.AddWithValue("@TotalItems", totalItems);
                        cmdOrder.Parameters.AddWithValue("@FinalAmount", finalAmount);
                        cmdOrder.Parameters.AddWithValue("@RewardPoints", rewardPoints);
                        orderId = Convert.ToInt32(cmdOrder.ExecuteScalar());
                    }

                    // BUOC 2 + 3: Tung mon hang: kiem tra kho -> luu chi tiet -> tru kho
                    foreach (var item in request.Items)
                    {
                        // UPDLOCK: khoa dong san pham de 2 quay POS khong cung tru mot luc
                        string checkStockSql = @"SELECT StockQuantity, ProductName
                                                 FROM Products WITH (UPDLOCK, ROWLOCK)
                                                 WHERE ProductId = @ProductId";
                        int currentStock;
                        string productName;

                        using (var cmdCheck = new SqlCommand(checkStockSql, connection, transaction))
                        {
                            cmdCheck.Parameters.AddWithValue("@ProductId", item.ProductId);
                            using (SqlDataReader reader = cmdCheck.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {item.ProductId}");

                                currentStock = Convert.ToInt32(reader["StockQuantity"]);
                                productName = reader["ProductName"].ToString() ?? string.Empty;
                            }   // reader PHAI dong truoc khi chay lenh tiep theo
                        }

                        if (currentStock < item.Quantity)
                        {
                            throw new InvalidOperationException(
                                $"Sản phẩm '{productName}' (ID: {item.ProductId}) không đủ tồn kho! " +
                                $"(Tồn kho thực tế: {currentStock}, Số lượng yêu cầu mua: {item.Quantity})");
                        }

                        string insertDetailSql = @"
                            INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal)";
                        using (var cmdDetail = new SqlCommand(insertDetailSql, connection, transaction))
                        {
                            cmdDetail.Parameters.AddWithValue("@OrderId", orderId);
                            cmdDetail.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdDetail.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@LineTotal", item.Quantity * item.UnitPrice);
                            cmdDetail.ExecuteNonQuery();
                        }

                        string updateStockSql = @"UPDATE Products
                                                  SET StockQuantity = StockQuantity - @Qty
                                                  WHERE ProductId = @ProductId";
                        using (var cmdStock = new SqlCommand(updateStockSql, connection, transaction))
                        {
                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdStock.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdStock.ExecuteNonQuery();
                        }
                    }

                    // BUOC 4: Cong diem cho khach hang
                    if (rewardPoints > 0)
                    {
                        string updatePointsSql = @"UPDATE Customers
                                                   SET RewardPoints = RewardPoints + @Points
                                                   WHERE CustomerId = @CustomerId";
                        using (var cmdCustomer = new SqlCommand(updatePointsSql, connection, transaction))
                        {
                            cmdCustomer.Parameters.AddWithValue("@Points", rewardPoints);
                            cmdCustomer.Parameters.AddWithValue("@CustomerId", request.CustomerId);
                            cmdCustomer.ExecuteNonQuery();
                        }
                    }

                    // BUOC 5: Moi thu OK -> luu vinh vien
                    transaction.Commit();

                    return Ok(new
                    {
                        success = true,
                        message = "Thanh toán hóa đơn POS thành công!",
                        orderId,
                        orderCode,
                        totalItems,
                        finalAmount,
                        rewardPointsAdded = rewardPoints
                    });
                }
                catch (Exception ex)
                {
                    // BUOC 6: Co bat ky loi nao -> hoan tac toan bo
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception rollbackEx)
                    {
                        return StatusCode(500, new
                        {
                            success = false,
                            message = "Lỗi nghiêm trọng khi thực thi Rollback: " + rollbackEx.Message
                        });
                    }

                    return BadRequest(new
                    {
                        success = false,
                        message = "Giao dịch thanh toán bị hủy và đã được Rollback an toàn!",
                        errorDetail = ex.Message
                    });
                }
            }
        }

        // DELETE: api/checkout/cancel/{orderId}   (Bai tap mo rong Tiet 5)
        [HttpDelete("cancel/{orderId}")]
        public IActionResult CancelOrder(int orderId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection")!;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // 1. Kiem tra hoa don
                    int customerId, pointsToDeduct;
                    using (var cmdGet = new SqlCommand(
                        "SELECT CustomerId, RewardPoints FROM Orders WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdGet.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = cmdGet.ExecuteReader())
                        {
                            if (!reader.Read())
                                throw new InvalidOperationException($"Không tìm thấy hóa đơn ID = {orderId} để hủy!");

                            customerId = Convert.ToInt32(reader["CustomerId"]);
                            pointsToDeduct = Convert.ToInt32(reader["RewardPoints"]);
                        }
                    }

                    // 2. Doc chi tiet hoa don
                    var itemsToRestore = new List<(int ProductId, int Quantity)>();
                    using (var cmdDetails = new SqlCommand(
                        "SELECT ProductId, Quantity FROM OrderDetails WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDetails.Parameters.AddWithValue("@OrderId", orderId);
                        using (SqlDataReader reader = cmdDetails.ExecuteReader())
                        {
                            while (reader.Read())
                                itemsToRestore.Add((Convert.ToInt32(reader["ProductId"]), Convert.ToInt32(reader["Quantity"])));
                        }
                    }

                    // 3. Hoan tra ton kho
                    foreach (var item in itemsToRestore)
                    {
                        using (var cmdRestore = new SqlCommand(
                            "UPDATE Products SET StockQuantity = StockQuantity + @Qty WHERE ProductId = @ProductId",
                            connection, transaction))
                        {
                            cmdRestore.Parameters.AddWithValue("@Qty", item.Quantity);
                            cmdRestore.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmdRestore.ExecuteNonQuery();
                        }
                    }

                    // 4. Tru lai diem thuong
                    if (pointsToDeduct > 0)
                    {
                        string deductSql = @"UPDATE Customers
                                             SET RewardPoints = CASE WHEN RewardPoints >= @Pts THEN RewardPoints - @Pts ELSE 0 END
                                             WHERE CustomerId = @CustomerId";
                        using (var cmdDeduct = new SqlCommand(deductSql, connection, transaction))
                        {
                            cmdDeduct.Parameters.AddWithValue("@Pts", pointsToDeduct);
                            cmdDeduct.Parameters.AddWithValue("@CustomerId", customerId);
                            cmdDeduct.ExecuteNonQuery();
                        }
                    }

                    // 5. Xoa chi tiet truoc (khoa ngoai), roi xoa hoa don
                    using (var cmdDelDetails = new SqlCommand(
                        "DELETE FROM OrderDetails WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDelDetails.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelDetails.ExecuteNonQuery();
                    }
                    using (var cmdDelOrder = new SqlCommand(
                        "DELETE FROM Orders WHERE OrderId = @OrderId", connection, transaction))
                    {
                        cmdDelOrder.Parameters.AddWithValue("@OrderId", orderId);
                        cmdDelOrder.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return Ok(new
                    {
                        success = true,
                        message = $"Đã hủy thành công hóa đơn #{orderId} và hoàn trả toàn bộ tồn kho vào CSDL!"
                    });
                }
                catch (Exception ex)
                {
                    try { transaction.Rollback(); } catch { /* bo qua */ }
                    return BadRequest(new
                    {
                        success = false,
                        message = "Hủy hóa đơn thất bại, giao dịch đã được Rollback!",
                        errorDetail = ex.Message
                    });
                }
            }
        }
    }
}
