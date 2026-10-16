using System.ComponentModel;
using System.Globalization;

namespace MiniSupermarket.WinForms
{
    /// <summary>Màn hình bán hàng POS (Cashier, Admin).</summary>
    public partial class FormPOS : Form
    {
        private readonly BindingList<CartItemDto> _cart = new();
        private CustomerDto? _customer;
        private static readonly CultureInfo Vn = new("vi-VN");

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
            dgvCart.DataSource = _cart;
            lblCustomerName.Text = "Khách vãng lai";
        }

        private void FormPOS_Load(object? sender, EventArgs e)
        {
            txtBarcode.Select(); // sẵn sàng nhận mã vạch từ máy quét
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CartItemDto.ProductId), HeaderText = "Mã SP", Width = 80 });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CartItemDto.ProductName), HeaderText = "Tên Sản Phẩm", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CartItemDto.UnitPrice), HeaderText = "Đơn Giá", Width = 110,
              DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CartItemDto.Quantity), HeaderText = "SL", Width = 70,
              DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            { DataPropertyName = nameof(CartItemDto.TotalPrice), HeaderText = "Thành Tiền", Width = 120,
              DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
        }

        // ---------- Phím tắt ----------
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F9)
            {
                btnCheckout.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dgvCart_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedItem();
                e.Handled = true;
            }
        }

        // ---------- Quét mã vạch ----------
        private async void txtBarcode_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || string.IsNullOrWhiteSpace(txtBarcode.Text)) return;

            e.SuppressKeyPress = true; // tránh tiếng "bíp" của Windows
            string barcode = txtBarcode.Text.Trim();
            txtBarcode.Clear();
            await AddProductToCartByBarcodeAsync(barcode);
            txtBarcode.Select();
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await ApiClientService.Client.GetFromJsonAsync<ProductDto>(
                    $"products/barcode/{Uri.EscapeDataString(barcode)}");

                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existing = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                int newQty = (existing?.Quantity ?? 0) + 1;
                if (newQty > product.StockQuantity)
                {
                    MessageBox.Show($"Sản phẩm \"{product.ProductName}\" chỉ còn {product.StockQuantity} trong kho!",
                        "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (existing != null)
                {
                    existing.Quantity = newQty;
                    existing.MaxStock = product.StockQuantity;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1,
                        MaxStock = product.StockQuantity
                    });
                }
                UpdateCartDisplay();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            _cart.ResetBindings(); // làm mới Số lượng / Thành tiền
            lblTotalAmount.Text = FormatMoney(CartTotal());
            CalculateChange();
        }

        private decimal CartTotal() => _cart.Sum(x => x.TotalPrice);

        private static string FormatMoney(decimal v) => v.ToString("N0", Vn) + " đ";

        // ---------- Tiền khách đưa / tiền thừa ----------
        private void txtCashReceived_TextChanged(object? sender, EventArgs e) => CalculateChange();

        private bool TryGetCashReceived(out decimal cash)
        {
            string digits = new(txtCashReceived.Text.Where(char.IsDigit).ToArray());
            return decimal.TryParse(digits, NumberStyles.None, CultureInfo.InvariantCulture, out cash);
        }

        private void CalculateChange()
        {
            decimal total = CartTotal();
            if (TryGetCashReceived(out decimal cash))
            {
                decimal change = cash - total;
                lblChange.Text = change >= 0 ? FormatMoney(change) : "Chưa đủ tiền!";
                lblChange.ForeColor = change >= 0 ? Color.Black : Color.Red;
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.Black;
            }
        }

        // ---------- Khách hàng thân thiết ----------
        private async void txtCustomerPhone_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            await LookupCustomerAsync();
        }

        private async void btnFindCustomer_Click(object? sender, EventArgs e) => await LookupCustomerAsync();

        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();
            _customer = null;
            if (phone.Length == 0)
            {
                lblCustomerName.Text = "Khách vãng lai";
                return;
            }

            try
            {
                _customer = await ApiClientService.Client.GetFromJsonAsync<CustomerDto>(
                    $"customers/phone/{Uri.EscapeDataString(phone)}");
                lblCustomerName.Text = _customer == null
                    ? "Khách vãng lai (chưa đăng ký)"
                    : $"{_customer.FullName} - Hạng {_customer.Rank} ({_customer.Points} điểm)";
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                lblCustomerName.Text = "Khách vãng lai (chưa đăng ký)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Giỏ hàng ----------
        private void btnRemoveItem_Click(object? sender, EventArgs e) => RemoveSelectedItem();

        private void RemoveSelectedItem()
        {
            if (dgvCart.CurrentRow?.DataBoundItem is CartItemDto item)
            {
                _cart.Remove(item);
                UpdateCartDisplay();
            }
        }

        private void btnClearCart_Click(object? sender, EventArgs e)
        {
            if (_cart.Count == 0) return;
            if (MessageBox.Show("Hủy toàn bộ giỏ hàng hiện tại?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetSale();
            }
        }

        private void ResetSale()
        {
            _cart.Clear();
            txtCashReceived.Clear();
            txtCustomerPhone.Clear();
            _customer = null;
            lblCustomerName.Text = "Khách vãng lai";
            UpdateCartDisplay();
            txtBarcode.Select();
        }

        // ---------- Thanh toán ----------
        private async void btnCheckout_Click(object? sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = CartTotal();
            if (!TryGetCashReceived(out decimal cash) || cash < total)
            {
                MessageBox.Show("Tiền khách đưa chưa đủ để thanh toán!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCashReceived.Focus();
                return;
            }

            var request = new CheckoutRequest
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new CheckoutItem
                { ProductId = i.ProductId, Quantity = i.Quantity, UnitPrice = i.UnitPrice }).ToList()
            };

            btnCheckout.Enabled = false;
            try
            {
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", request);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Thanh toán thành công!\nTổng tiền: {FormatMoney(total)}\nTiền thừa: {FormatMoney(cash - total)}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetSale();
                }
                else
                {
                    MessageBox.Show("Thanh toán thất bại từ máy chủ! (Kiểm tra lại tồn kho)", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckout.Enabled = true;
            }
        }
    }
}
