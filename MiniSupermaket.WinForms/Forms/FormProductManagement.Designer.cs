#nullable disable
namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTop;
        private Label lblSearch;
        private TextBox txtSearchBarcode;
        private Label lblFilter;
        private ComboBox cboFilterCategory;
        private Button btnSearch;
        private TableLayoutPanel tlpMain;
        private Panel panelGrid;
        private DataGridView dgvProducts;
        private Panel panelDetail;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblName;
        private TextBox txtProductName;
        private Label lblPrice;
        private NumericUpDown nudPrice;
        private Label lblStock;
        private NumericUpDown nudStock;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnLoad;

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
            this.panelTop = new Panel();
            this.lblSearch = new Label();
            this.txtSearchBarcode = new TextBox();
            this.lblFilter = new Label();
            this.cboFilterCategory = new ComboBox();
            this.btnSearch = new Button();
            this.tlpMain = new TableLayoutPanel();
            this.panelGrid = new Panel();
            this.dgvProducts = new DataGridView();
            this.panelDetail = new Panel();
            this.lblId = new Label();
            this.txtId = new TextBox();
            this.lblBarcode = new Label();
            this.txtBarcode = new TextBox();
            this.lblName = new Label();
            this.txtProductName = new TextBox();
            this.lblPrice = new Label();
            this.nudPrice = new NumericUpDown();
            this.lblStock = new Label();
            this.nudStock = new NumericUpDown();
            this.lblCategory = new Label();
            this.cboCategory = new ComboBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnLoad = new Button();
            this.panelTop.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = Color.White;
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.cboFilterCategory);
            this.panelTop.Controls.Add(this.lblFilter);
            this.panelTop.Controls.Add(this.txtSearchBarcode);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 60;
            this.panelTop.Name = "panelTop";
            this.panelTop.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new Point(15, 22);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Text = "Mã vạch:";
            // 
            // txtSearchBarcode
            // 
            this.txtSearchBarcode.Location = new Point(80, 18);
            this.txtSearchBarcode.Name = "txtSearchBarcode";
            this.txtSearchBarcode.Size = new Size(200, 23);
            this.txtSearchBarcode.TabIndex = 0;
            this.txtSearchBarcode.KeyDown += new KeyEventHandler(this.txtSearchBarcode_KeyDown);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new Point(305, 22);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Text = "Nhóm hàng:";
            // 
            // cboFilterCategory
            // 
            this.cboFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboFilterCategory.Location = new Point(385, 18);
            this.cboFilterCategory.Name = "cboFilterCategory";
            this.cboFilterCategory.Size = new Size(210, 23);
            this.cboFilterCategory.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = Color.FromArgb(41, 100, 180);
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.ForeColor = Color.White;
            this.btnSearch.Location = new Point(615, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(110, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍 Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            // 
            // tlpMain  (68% | 32%)
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            this.tlpMain.Controls.Add(this.panelGrid, 0, 0);
            this.tlpMain.Controls.Add(this.panelDetail, 1, 0);
            this.tlpMain.Dock = DockStyle.Fill;
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tlpMain.TabIndex = 0;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvProducts);
            this.panelGrid.Dock = DockStyle.Fill;
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new Padding(15);
            this.panelGrid.TabIndex = 0;
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            this.dgvProducts.BackgroundColor = Color.White;
            this.dgvProducts.BorderStyle = BorderStyle.None;
            this.dgvProducts.ColumnHeadersHeight = 34;
            this.dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.Dock = DockStyle.Fill;
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.TabIndex = 0;
            this.dgvProducts.CellClick += new DataGridViewCellEventHandler(this.dgvProducts_CellClick);
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = Color.White;
            this.panelDetail.Controls.Add(this.btnLoad);
            this.panelDetail.Controls.Add(this.btnDelete);
            this.panelDetail.Controls.Add(this.btnUpdate);
            this.panelDetail.Controls.Add(this.btnAdd);
            this.panelDetail.Controls.Add(this.cboCategory);
            this.panelDetail.Controls.Add(this.lblCategory);
            this.panelDetail.Controls.Add(this.nudStock);
            this.panelDetail.Controls.Add(this.lblStock);
            this.panelDetail.Controls.Add(this.nudPrice);
            this.panelDetail.Controls.Add(this.lblPrice);
            this.panelDetail.Controls.Add(this.txtProductName);
            this.panelDetail.Controls.Add(this.lblName);
            this.panelDetail.Controls.Add(this.txtBarcode);
            this.panelDetail.Controls.Add(this.lblBarcode);
            this.panelDetail.Controls.Add(this.txtId);
            this.panelDetail.Controls.Add(this.lblId);
            this.panelDetail.Dock = DockStyle.Fill;
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.TabIndex = 1;
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblId.Location = new Point(15, 15);
            this.lblId.Name = "lblId";
            this.lblId.Text = "MÃ SẢN PHẨM (tự sinh)";
            // 
            // txtId
            // 
            this.txtId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtId.Location = new Point(15, 37);
            this.txtId.Name = "txtId";
            this.txtId.Size = new Size(290, 23);
            this.txtId.TabIndex = 0;
            this.txtId.ReadOnly = true;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblBarcode.Location = new Point(15, 70);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Text = "MÃ VẠCH";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtBarcode.Location = new Point(15, 92);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new Size(290, 23);
            this.txtBarcode.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblName.Location = new Point(15, 125);
            this.lblName.Name = "lblName";
            this.lblName.Text = "TÊN SẢN PHẨM";
            // 
            // txtProductName
            // 
            this.txtProductName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtProductName.Location = new Point(15, 147);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new Size(290, 23);
            this.txtProductName.TabIndex = 2;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblPrice.Location = new Point(15, 180);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Text = "ĐƠN GIÁ (đ)";
            // 
            // nudPrice
            // 
            this.nudPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nudPrice.Location = new Point(15, 202);
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new Size(290, 23);
            this.nudPrice.TabIndex = 3;
            this.nudPrice.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            this.nudPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudPrice.ThousandsSeparator = true;
            this.nudPrice.TextAlign = HorizontalAlignment.Right;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblStock.Location = new Point(15, 235);
            this.lblStock.Name = "lblStock";
            this.lblStock.Text = "TỒN KHO";
            // 
            // nudStock
            // 
            this.nudStock.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.nudStock.Location = new Point(15, 257);
            this.nudStock.Name = "nudStock";
            this.nudStock.Size = new Size(290, 23);
            this.nudStock.TabIndex = 4;
            this.nudStock.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudStock.ThousandsSeparator = true;
            this.nudStock.TextAlign = HorizontalAlignment.Right;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.lblCategory.Location = new Point(15, 290);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Text = "NHÓM HÀNG";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.cboCategory.Location = new Point(15, 312);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new Size(290, 23);
            this.cboCategory.TabIndex = 5;
            this.cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnAdd.BackColor = Color.FromArgb(27, 122, 60);
            this.btnAdd.Cursor = Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.Location = new Point(15, 375);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(140, 40);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "➕ Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnUpdate.BackColor = Color.FromArgb(41, 100, 180);
            this.btnUpdate.Cursor = Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Location = new Point(165, 375);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new Size(140, 40);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "✏ Cập nhật";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnDelete.BackColor = Color.FromArgb(200, 40, 50);
            this.btnDelete.Cursor = Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Location = new Point(15, 425);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new Size(140, 40);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "🗑 Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            this.btnLoad.BackColor = Color.FromArgb(108, 117, 125);
            this.btnLoad.Cursor = Cursors.Hand;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = FlatStyle.Flat;
            this.btnLoad.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLoad.ForeColor = Color.White;
            this.btnLoad.Location = new Point(165, 425);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new Size(140, 40);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "🔄 Tải lại";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
            // 
            // FormProductManagement
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(244, 245, 247);
            this.ClientSize = new Size(1050, 660);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.panelTop);
            this.Name = "FormProductManagement";
            this.Text = "FormProductManagement";
            this.Load += new EventHandler(this.FormProductManagement_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
