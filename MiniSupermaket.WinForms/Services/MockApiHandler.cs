using System.Net;
using System.Web;

namespace MiniSupermarket.WinForms.Services;

/// <summary>
/// Giả lập Web API trong bộ nhớ để chạy/kiểm thử giao diện khi chưa có Backend.
/// Chuyển sang API thật bằng cách đặt AppConfig.UseMockApi = false.
/// </summary>
public class MockApiHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct)
    {
        await Task.Delay(100, ct); // mô phỏng độ trễ mạng

        var uri = request.RequestUri!;
        string path = uri.AbsolutePath;
        int idx = path.IndexOf("/api/", StringComparison.OrdinalIgnoreCase);
        path = (idx >= 0 ? path[(idx + 5)..] : path).Trim('/');
        var seg = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var m = request.Method;

        if (seg.Length == 0) return Status(HttpStatusCode.NotFound);

        bool isLogin = seg[0].Equals("auth", StringComparison.OrdinalIgnoreCase);
        if (!isLogin && request.Headers.Authorization == null) return Status(HttpStatusCode.Unauthorized);

        switch (seg[0].ToLowerInvariant())
        {
            case "auth" when m == HttpMethod.Post && seg.Length == 2 && seg[1] == "login":
            {
                var req = await request.Content!.ReadFromJsonAsync<LoginRequest>(cancellationToken: ct);
                var u = MockData.Users.FirstOrDefault(x =>
                    x.Username.Equals(req?.Username?.Trim(), StringComparison.OrdinalIgnoreCase));
                if (u == null || !MockData.Passwords.TryGetValue(u.Username, out var pw) || pw != req!.Password)
                    return Status(HttpStatusCode.Unauthorized);
                if (!u.IsActive) return Status(HttpStatusCode.Forbidden);
                return Ok(new LoginResponse
                {
                    Token = "mock-jwt-" + Guid.NewGuid().ToString("N"),
                    Username = u.Username, FullName = u.FullName, Role = u.Role
                });
            }

            case "categories" when m == HttpMethod.Get:
                return Ok(MockData.Categories);

            case "products":
                return await Products(request, seg, ct);

            case "customers" when m == HttpMethod.Get:
                if (seg.Length == 1) return Ok(MockData.Customers);
                if (seg.Length == 3 && seg[1] == "phone")
                {
                    var c = MockData.Customers.FirstOrDefault(x => x.Phone == seg[2]);
                    return c == null ? Status(HttpStatusCode.NotFound) : Ok(c);
                }
                break;

            case "users":
                return await Users(request, seg, ct);

            case "orders" when m == HttpMethod.Post && seg.Length == 2 && seg[1] == "checkout":
                return await Checkout(request, ct);

            case "reports" when m == HttpMethod.Get && seg.Length == 2 && seg[1] == "quick":
                return QuickReport(uri);
        }
        return Status(HttpStatusCode.NotFound);
    }

    // ---------------- PRODUCTS ----------------
    private static async Task<HttpResponseMessage> Products(HttpRequestMessage request, string[] seg, CancellationToken ct)
    {
        var m = request.Method;
        var list = MockData.Products;

        if (m == HttpMethod.Get && seg.Length == 1)
            return Ok(list.Select(MockData.WithCategory).ToList());

        if (m == HttpMethod.Get && seg.Length == 3 && seg[1] == "barcode")
        {
            var p = list.FirstOrDefault(x => x.Barcode == seg[2]);
            return p == null ? Status(HttpStatusCode.NotFound) : Ok(MockData.WithCategory(p));
        }

        if (m == HttpMethod.Post && seg.Length == 1)
        {
            var r = await request.Content!.ReadFromJsonAsync<ProductSaveRequest>(cancellationToken: ct);
            if (r == null || list.Any(x => x.Barcode == r.Barcode)) return Status(HttpStatusCode.Conflict);
            var p = new ProductDto
            {
                ProductId = list.Max(x => x.ProductId) + 1, Barcode = r.Barcode, ProductName = r.ProductName,
                Price = r.Price, StockQuantity = r.StockQuantity, CategoryId = r.CategoryId
            };
            list.Add(p);
            return Ok(MockData.WithCategory(p));
        }

        if (seg.Length == 2 && int.TryParse(seg[1], out int id))
        {
            var p = list.FirstOrDefault(x => x.ProductId == id);
            if (p == null) return Status(HttpStatusCode.NotFound);

            if (m == HttpMethod.Put)
            {
                var r = await request.Content!.ReadFromJsonAsync<ProductSaveRequest>(cancellationToken: ct);
                if (r == null) return Status(HttpStatusCode.BadRequest);
                if (list.Any(x => x.ProductId != id && x.Barcode == r.Barcode)) return Status(HttpStatusCode.Conflict);
                p.Barcode = r.Barcode; p.ProductName = r.ProductName; p.Price = r.Price;
                p.StockQuantity = r.StockQuantity; p.CategoryId = r.CategoryId;
                return Ok(MockData.WithCategory(p));
            }
            if (m == HttpMethod.Delete)
            {
                list.Remove(p);
                return Status(HttpStatusCode.NoContent);
            }
        }
        return Status(HttpStatusCode.NotFound);
    }

    // ---------------- USERS ----------------
    private static async Task<HttpResponseMessage> Users(HttpRequestMessage request, string[] seg, CancellationToken ct)
    {
        var m = request.Method;
        var users = MockData.Users;

        if (m == HttpMethod.Get && seg.Length == 1) return Ok(users.ToList());

        if (m == HttpMethod.Post && seg.Length == 1)
        {
            var r = await request.Content!.ReadFromJsonAsync<UserCreateRequest>(cancellationToken: ct);
            if (r == null || users.Any(x => x.Username.Equals(r.Username, StringComparison.OrdinalIgnoreCase)))
                return Status(HttpStatusCode.Conflict);
            users.Add(new UserDto
            {
                Id = users.Max(x => x.Id) + 1, Username = r.Username, FullName = r.FullName,
                Email = r.Email, Role = r.Role, IsActive = true
            });
            MockData.Passwords[r.Username] = r.Password;
            return Status(HttpStatusCode.Created);
        }

        if (m == HttpMethod.Put && seg.Length == 3 && int.TryParse(seg[1], out int id))
        {
            var u = users.FirstOrDefault(x => x.Id == id);
            if (u == null) return Status(HttpStatusCode.NotFound);
            if (seg[2] == "reset-password") { MockData.Passwords[u.Username] = "123456"; return Status(HttpStatusCode.NoContent); }
            if (seg[2] == "toggle-lock") { u.IsActive = !u.IsActive; return Ok(u); }
        }
        return Status(HttpStatusCode.NotFound);
    }

    // ---------------- ORDERS / REPORT ----------------
    private static async Task<HttpResponseMessage> Checkout(HttpRequestMessage request, CancellationToken ct)
    {
        var r = await request.Content!.ReadFromJsonAsync<CheckoutRequest>(cancellationToken: ct);
        if (r == null || r.Items.Count == 0) return Status(HttpStatusCode.BadRequest);

        foreach (var it in r.Items)
        {
            var p = MockData.Products.FirstOrDefault(x => x.ProductId == it.ProductId);
            if (p == null || p.StockQuantity < it.Quantity) return Status(HttpStatusCode.BadRequest);
        }

        decimal total = 0;
        var lines = new List<(string, int)>();
        foreach (var it in r.Items)
        {
            var p = MockData.Products.First(x => x.ProductId == it.ProductId);
            p.StockQuantity -= it.Quantity;
            total += p.Price * it.Quantity;
            lines.Add((p.ProductName, it.Quantity));
        }
        MockData.Orders.Add(new MockData.MockOrder(DateTime.Now, total, lines));

        var cust = MockData.Customers.FirstOrDefault(c => c.Phone == r.CustomerPhone);
        if (cust != null) cust.Points += (int)(total / 10000);

        return Ok(new { OrderId = MockData.Orders.Count, Total = total });
    }

    private static HttpResponseMessage QuickReport(Uri uri)
    {
        var q = HttpUtility.ParseQueryString(uri.Query);
        var date = DateTime.TryParse(q["date"], out var d) ? d.Date : DateTime.Today;
        var orders = MockData.Orders.Where(o => o.Date.Date == date).ToList();

        string best = orders.SelectMany(o => o.Lines)
            .GroupBy(l => l.Name)
            .OrderByDescending(g => g.Sum(x => x.Qty))
            .Select(g => $"{g.Key} ({g.Sum(x => x.Qty)})")
            .FirstOrDefault() ?? "—";

        return Ok(new QuickReportDto { TotalOrders = orders.Count, TotalRevenue = orders.Sum(o => o.Total), BestSeller = best });
    }

    // ---------------- helpers ----------------
    private static HttpResponseMessage Ok<T>(T body) =>
        new(HttpStatusCode.OK) { Content = JsonContent.Create(body) };

    private static HttpResponseMessage Status(HttpStatusCode code) => new(code);
}

/// <summary>Dữ liệu mẫu trong bộ nhớ (gồm 15 tài khoản kiểm thử của đề bài).</summary>
public static class MockData
{
    public record MockOrder(DateTime Date, decimal Total, List<(string Name, int Qty)> Lines);

    public static readonly List<CategoryDto> Categories = new()
    {
        new() { CategoryId = 1, CategoryName = "Đồ uống" },
        new() { CategoryId = 2, CategoryName = "Bánh kẹo" },
        new() { CategoryId = 3, CategoryName = "Sữa & chế phẩm" },
        new() { CategoryId = 4, CategoryName = "Mì & thực phẩm khô" },
        new() { CategoryId = 5, CategoryName = "Gia vị" },
        new() { CategoryId = 6, CategoryName = "Đồ gia dụng" }
    };

    public static readonly List<ProductDto> Products = new()
    {
        P(1, "8934588012013", "Nước ngọt Coca-Cola lon 330ml", 10500, 120, 1),
        P(2, "8934588063051", "Nước suối Aquafina 500ml", 6000, 200, 1),
        P(3, "8935001720010", "Trà xanh Không Độ 455ml", 9500, 90, 1),
        P(4, "8934563138165", "Mì Hảo Hảo tôm chua cay", 4500, 300, 4),
        P(5, "8934563001018", "Mì Omachi sườn hầm", 9000, 150, 4),
        P(6, "8934673100011", "Sữa tươi Vinamilk 1L", 34000, 60, 3),
        P(7, "8934673200012", "Sữa chua Vinamilk hộp 100g", 7500, 110, 3),
        P(8, "8934804012007", "Bánh Oreo vị vani 133g", 18500, 75, 2),
        P(9, "8935024140014", "Bánh Choco Pie hộp 6 cái", 36000, 40, 2),
        P(10, "8936036020018", "Nước mắm Nam Ngư 500ml", 38000, 55, 5),
        P(11, "8936079110015", "Đường tinh luyện 1kg", 24000, 80, 5),
        P(12, "8936020300017", "Nước rửa chén Sunlight 750g", 32000, 45, 6)
    };

    public static readonly List<CustomerDto> Customers = new()
    {
        new() { CustomerId = 1, FullName = "Nguyễn Văn An", Phone = "0901000001", Rank = "Vàng", Points = 520 },
        new() { CustomerId = 2, FullName = "Trần Thị Bình", Phone = "0901000002", Rank = "Bạc", Points = 240 },
        new() { CustomerId = 3, FullName = "Lê Hoàng Cường", Phone = "0901000003", Rank = "Đồng", Points = 80 },
        new() { CustomerId = 4, FullName = "Phạm Minh Dũng", Phone = "0901000004", Rank = "Kim cương", Points = 1500 },
        new() { CustomerId = 5, FullName = "Võ Ngọc Em", Phone = "0901000005", Rank = "Đồng", Points = 35 }
    };

    public static readonly List<MockOrder> Orders = new()
    {
        new(DateTime.Today.AddHours(8.5), 63000, new() { ("Mì Hảo Hảo tôm chua cay", 6), ("Sữa tươi Vinamilk 1L", 1) }),
        new(DateTime.Today.AddHours(10), 31500, new() { ("Nước ngọt Coca-Cola lon 330ml", 3) }),
        new(DateTime.Today.AddHours(11), 90000, new() { ("Mì Hảo Hảo tôm chua cay", 10), ("Bánh Oreo vị vani 133g", 1), ("Nước suối Aquafina 500ml", 4) })
    };

    // 15 tài khoản kiểm thử theo đề: mật khẩu ban đầu 123456
    public static readonly List<UserDto> Users = new()
    {
        U(1, "admin01", "Nguyễn Quản Trị", Roles.Admin),
        U(2, "admin02", "Trần Giám Đốc", Roles.Admin),
        U(3, "cashier01", "Lê Thu Ngân", Roles.Cashier),
        U(4, "cashier02", "Phạm Bán Hàng", Roles.Cashier),
        U(5, "cashier03", "Hoàng Thu Ngân", Roles.Cashier),
        U(6, "cashier04", "Vũ Thị Quầy", Roles.Cashier),
        U(7, "cashier05", "Đỗ Bán Lẻ", Roles.Cashier),
        U(8, "ware01", "Ngô Quản Kho", Roles.Warehouse),
        U(9, "ware02", "Bùi Kiểm Kê", Roles.Warehouse),
        U(10, "ware03", "Dương Thủ Kho", Roles.Warehouse),
        U(11, "ware04", "Lý Nhập Hàng", Roles.Warehouse),
        U(12, "admin_backup", "Đặng Hỗ Trợ", Roles.Admin),
        U(13, "cashier06", "Hồ Ca Chiều", Roles.Cashier),
        U(14, "ware05", "Trương Vận Chuyển", Roles.Warehouse),
        U(15, "supervisor", "Mai Giám Sát", Roles.Admin)
    };

    public static readonly Dictionary<string, string> Passwords =
        Users.ToDictionary(u => u.Username, _ => "123456", StringComparer.OrdinalIgnoreCase);

    public static ProductDto WithCategory(ProductDto p) => new()
    {
        ProductId = p.ProductId, Barcode = p.Barcode, ProductName = p.ProductName, Price = p.Price,
        StockQuantity = p.StockQuantity, CategoryId = p.CategoryId,
        CategoryName = Categories.FirstOrDefault(c => c.CategoryId == p.CategoryId)?.CategoryName ?? ""
    };

    private static ProductDto P(int id, string barcode, string name, decimal price, int stock, int cat) =>
        new() { ProductId = id, Barcode = barcode, ProductName = name, Price = price, StockQuantity = stock, CategoryId = cat };

    private static UserDto U(int id, string username, string fullName, string role) =>
        new() { Id = id, Username = username, FullName = fullName, Role = role, Email = $"{username}@minimart.vn", IsActive = true };
}
