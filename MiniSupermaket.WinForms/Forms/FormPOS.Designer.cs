#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormPOS
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlpMain;
        private Panel panelLeft;
        private DataGridView dgvCart;
        private Panel panelGap;
        private TextBox txtBarcode;
        private Label lblScan;
        private Panel panelRight;
        private Label lblCustTitle;
        private TextBox txtCustomerPhone;
        private Button btnFindCustomer;
        private Label lblCustomerName;
        private Label lblTotalCaption;
        private Label lblTotalAmount;
        private Label lblCashCaption;
        private TextBox txtCashReceived;
        private Label lblChangeCaption;
        private Label lblChange;
        private Button btnCheckout;
        private Button btnClearCart;
        private Button btnRemoveItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tlpMain = new TableLayoutPanel();
            this.panelLeft = new Panel();
            this.dgvCart = new DataGridView();
            this.panelGap = new Panel();
            this.txtBarcode = new TextBox();
            this.lblScan = new Label();
            this.panelRight = new Panel();
            this.lblCustTitle = new Label();
            this.txtCustomerPhone = new TextBox();
            this.btnFindCustomer = new Button();
            this.lblCustomerName = new Label();
            this.lblTotalCaption = new Label();
            this.lblTotalAmount = new Label();
            this.lblCashCaption = new Label();
            this.txtCashReceived = new TextBox();
            this.lblChangeCaption = new Label();
            this.lblChange = new Label();
            this.btnCheckout = new Button();
            this.btnClearCart = new Button();
            this.btnRemoveItem = new Button();
            this.tlpMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain  (65% | 35%)
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            this.tlpMain.Controls.Add(this.panelLeft, 0, 0);
            this.tlpMain.Controls.Add(this.panelRight, 1, 0);
            this.tlpMain.Dock = DockStyle.Fill;
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpMain.TabIndex = 0;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.dgvCart);
            this.panelLeft.Controls.Add(this.panelGap);
            this.panelLeft.Controls.Add(this.txtBarcode);
            this.panelLeft.Controls.Add(this.lblScan);
            this.panelLeft.Dock = DockStyle.Fill;
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new Padding(15);
            this.panelLeft.TabIndex = 0;
            // 
            // lblScan
            // 
            this.lblScan.Dock = DockStyle.Top;
            this.lblScan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblScan.Height = 26;
            this.lblScan.Name = "lblScan";
            this.lblScan.TabIndex = 0;
            this.lblScan.Text = "QUÉT MÃ VẠCH  (nhập mã rồi nhấn Enter)";
            this.lblScan.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Dock = DockStyle.Top;
            this.txtBarcode.Font = new Font("Segoe UI", 16F);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.KeyDown += new KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // panelGap
            // 
            this.panelGap.Dock = DockStyle.Top;
            this.panelGap.Height = 12;
            this.panelGap.Name = "panelGap";
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AllowUserToResizeRows = false;
            this.dgvCart.BackgroundColor = Color.White;
            this.dgvCart.BorderStyle = BorderStyle.None;
            this.dgvCart.ColumnHeadersHeight = 36;
            this.dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCart.Dock = DockStyle.Fill;
            this.dgvCart.Font = new Font("Segoe UI", 11F);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.RowTemplate.Height = 34;
            this.dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.TabIndex = 1;
            this.dgvCart.KeyDown += new KeyEventHandler(this.dgvCart_KeyDown);
            // 
            // panelRight
            // 
            this.panelRight.BackColor = Color.White;
            this.panelRight.Controls.Add(this.lblCustTitle);
            this.panelRight.Controls.Add(this.txtCustomerPhone);
            this.panelRight.Controls.Add(this.btnFindCustomer);
            this.panelRight.Controls.Add(this.lblCustomerName);
            this.panelRight.Controls.Add(this.lblTotalCaption);
            this.panelRight.Controls.Add(this.lblTotalAmount);
            this.panelRight.Controls.Add(this.lblCashCaption);
            this.panelRight.Controls.Add(this.txtCashReceived);
            this.panelRight.Controls.Add(this.lblChangeCaption);
            this.panelRight.Controls.Add(this.lblChange);
            this.panelRight.Controls.Add(this.btnCheckout);
            this.panelRight.Controls.Add(this.btnClearCart);
            this.panelRight.Controls.Add(this.btnRemoveItem);
            this.panelRight.Dock = DockStyle.Fill;
            this.panelRight.Name = "panelRight";
            this.panelRight.TabIndex = 1;
            // 
            // lblCustTitle
            // 
            this.lblCustTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCustTitle.Location = new Point(15, 15);
            this.lblCustTitle.Name = "lblCustTitle";
            this.lblCustTitle.Size = new Size(335, 24);
            this.lblCustTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblCustTitle.Text = "KHÁCH HÀNG THÂN THIẾT (SĐT)";
            // 
            // txtCustomerPhone
            // 
            this.txtCustomerPhone.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtCustomerPhone.Font = new Font("Segoe UI", 12F);
            this.txtCustomerPhone.Location = new Point(15, 42);
            this.txtCustomerPhone.MaxLength = 15;
            this.txtCustomerPhone.Name = "txtCustomerPhone";
            this.txtCustomerPhone.Size = new Size(235, 29);
            this.txtCustomerPhone.TabIndex = 1;
            this.txtCustomerPhone.KeyDown += new KeyEventHandler(this.txtCustomerPhone_KeyDown);
            // 
            // btnFindCustomer
            // 
            this.btnFindCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnFindCustomer.Location = new Point(260, 41);
            this.btnFindCustomer.Name = "btnFindCustomer";
            this.btnFindCustomer.Size = new Size(90, 31);
            this.btnFindCustomer.TabIndex = 2;
            this.btnFindCustomer.Text = "Tìm";
            this.btnFindCustomer.UseVisualStyleBackColor = true;
            this.btnFindCustomer.Click += new EventHandler(this.btnFindCustomer_Click);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblCustomerName.Font = new Font("Segoe UI", 10.5F, FontStyle.Italic);
            this.lblCustomerName.ForeColor = Color.FromArgb(41, 100, 180);
            this.lblCustomerName.Location = new Point(15, 78);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new Size(335, 26);
            this.lblCustomerName.Text = "Khách vãng lai";
            // 
            // lblTotalCaption
            // 
            this.lblTotalCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalCaption.Location = new Point(15, 125);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new Size(200, 22);
            this.lblTotalCaption.Text = "TỔNG TIỀN THANH TOÁN";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblTotalAmount.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            this.lblTotalAmount.ForeColor = Color.Crimson;
            this.lblTotalAmount.Location = new Point(15, 148);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new Size(335, 58);
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCashCaption
            // 
            this.lblCashCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCashCaption.Location = new Point(15, 220);
            this.lblCashCaption.Name = "lblCashCaption";
            this.lblCashCaption.Size = new Size(200, 22);
            this.lblCashCaption.Text = "TIỀN KHÁCH ĐƯA";
            // 
            // txtCashReceived
            // 
            this.txtCashReceived.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtCashReceived.Font = new Font("Segoe UI", 16F);
            this.txtCashReceived.Location = new Point(15, 245);
            this.txtCashReceived.Name = "txtCashReceived";
            this.txtCashReceived.Size = new Size(335, 36);
            this.txtCashReceived.TabIndex = 3;
            this.txtCashReceived.TextAlign = HorizontalAlignment.Right;
            this.txtCashReceived.TextChanged += new EventHandler(this.txtCashReceived_TextChanged);
            // 
            // lblChangeCaption
            // 
            this.lblChangeCaption.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblChangeCaption.Location = new Point(15, 297);
            this.lblChangeCaption.Name = "lblChangeCaption";
            this.lblChangeCaption.Size = new Size(200, 22);
            this.lblChangeCaption.Text = "TIỀN THỪA TRẢ KHÁCH";
            // 
            // lblChange
            // 
            this.lblChange.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.lblChange.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblChange.Location = new Point(15, 320);
            this.lblChange.Name = "lblChange";
            this.lblChange.Size = new Size(335, 40);
            this.lblChange.Text = "0 đ";
            this.lblChange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnCheckout
            // 
            this.btnCheckout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnCheckout.BackColor = Color.FromArgb(27, 122, 60);
            this.btnCheckout.Cursor = Cursors.Hand;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = FlatStyle.Flat;
            this.btnCheckout.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.btnCheckout.ForeColor = Color.White;
            this.btnCheckout.Location = new Point(15, 380);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new Size(335, 62);
            this.btnCheckout.TabIndex = 4;
            this.btnCheckout.Text = "THANH TOÁN (F9)";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new EventHandler(this.btnCheckout_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnClearCart.BackColor = Color.FromArgb(200, 40, 50);
            this.btnClearCart.Cursor = Cursors.Hand;
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = FlatStyle.Flat;
            this.btnClearCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnClearCart.ForeColor = Color.White;
            this.btnClearCart.Location = new Point(15, 454);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new Size(335, 46);
            this.btnClearCart.TabIndex = 5;
            this.btnClearCart.Text = "HỦY GIỎ HÀNG";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new EventHandler(this.btnClearCart_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.btnRemoveItem.Font = new Font("Segoe UI", 10F);
            this.btnRemoveItem.Location = new Point(15, 512);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new Size(335, 38);
            this.btnRemoveItem.TabIndex = 6;
            this.btnRemoveItem.Text = "Xóa dòng đang chọn (Del)";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new EventHandler(this.btnRemoveItem_Click);
            // 
            // FormPOS
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.tlpMain);
            this.Name = "FormPOS";
            this.Text = "FormPOS";
            this.Load += new EventHandler(this.FormPOS_Load);
            this.tlpMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
