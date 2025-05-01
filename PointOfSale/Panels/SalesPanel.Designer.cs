using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Panels
{
    partial class SalesPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            label3 = new Label();
            tbPaidAmount = new TextBox();
            textBox1 = new TextBox();
            btnSupplier = new Button();
            btnPrint = new Button();
            btnSave = new Button();
            btnCost = new Button();
            label7 = new Label();
            tbGrandTotal = new TextBox();
            label6 = new Label();
            tbCost = new TextBox();
            label5 = new Label();
            label4 = new Label();
            tbDiscount = new TextBox();
            tbSubTotal = new TextBox();
            purchaseItemBindingSource = new BindingSource(components);
            totalProductPriceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            productUnitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            quantityDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            productNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            productIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lblGrandTotal = new Label();
            grid = new DataGridView();
            tbBarcode = new TextBox();
            tbSupplier = new TextBox();
            label2 = new Label();
            date = new DateTimePicker();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)purchaseItemBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(17, 474);
            label3.Name = "label3";
            label3.Size = new Size(84, 17);
            label3.TabIndex = 43;
            label3.Text = "Jumlah Bayar";
            // 
            // tbPaidAmount
            // 
            tbPaidAmount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbPaidAmount.Location = new Point(120, 471);
            tbPaidAmount.MaxLength = 15;
            tbPaidAmount.Name = "tbPaidAmount";
            tbPaidAmount.Size = new Size(158, 25);
            tbPaidAmount.TabIndex = 42;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(255, 255, 192);
            textBox1.Location = new Point(17, 111);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(50, 25);
            textBox1.TabIndex = 41;
            // 
            // btnSupplier
            // 
            btnSupplier.BackColor = SystemColors.AppWorkspace;
            btnSupplier.FlatAppearance.BorderSize = 0;
            btnSupplier.FlatStyle = FlatStyle.Flat;
            btnSupplier.ForeColor = Color.White;
            btnSupplier.Location = new Point(367, 49);
            btnSupplier.Name = "btnSupplier";
            btnSupplier.Size = new Size(28, 25);
            btnSupplier.TabIndex = 40;
            btnSupplier.Text = "...";
            btnSupplier.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrint.BackColor = SystemColors.ActiveCaption;
            btnPrint.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Image = Properties.Resources.diskette;
            btnPrint.Location = new Point(404, 424);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(74, 77);
            btnPrint.TabIndex = 39;
            btnPrint.Text = "Simpan";
            btnPrint.TextImageRelation = TextImageRelation.ImageAboveText;
            btnPrint.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSave.BackColor = SystemColors.ActiveCaption;
            btnSave.FlatAppearance.BorderColor = SystemColors.ButtonFace;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Image = Properties.Resources.diskette;
            btnSave.Location = new Point(324, 424);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(74, 77);
            btnSave.TabIndex = 38;
            btnSave.Text = "Simpan";
            btnSave.TextImageRelation = TextImageRelation.ImageAboveText;
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCost
            // 
            btnCost.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCost.BackColor = SystemColors.AppWorkspace;
            btnCost.FlatAppearance.BorderSize = 0;
            btnCost.FlatStyle = FlatStyle.Flat;
            btnCost.ForeColor = Color.White;
            btnCost.Location = new Point(250, 419);
            btnCost.Name = "btnCost";
            btnCost.Size = new Size(28, 25);
            btnCost.TabIndex = 37;
            btnCost.Text = "...";
            btnCost.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Location = new Point(17, 448);
            label7.Name = "label7";
            label7.Size = new Size(76, 17);
            label7.TabIndex = 36;
            label7.Text = "Grand Total";
            // 
            // tbGrandTotal
            // 
            tbGrandTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbGrandTotal.Location = new Point(120, 445);
            tbGrandTotal.Name = "tbGrandTotal";
            tbGrandTotal.ReadOnly = true;
            tbGrandTotal.Size = new Size(158, 25);
            tbGrandTotal.TabIndex = 35;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Location = new Point(17, 422);
            label6.Name = "label6";
            label6.Size = new Size(38, 17);
            label6.TabIndex = 34;
            label6.Text = "Biaya";
            // 
            // tbCost
            // 
            tbCost.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbCost.Location = new Point(120, 419);
            tbCost.Name = "tbCost";
            tbCost.ReadOnly = true;
            tbCost.Size = new Size(129, 25);
            tbCost.TabIndex = 33;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Location = new Point(17, 396);
            label5.Name = "label5";
            label5.Size = new Size(47, 17);
            label5.TabIndex = 32;
            label5.Text = "Diskon";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Location = new Point(17, 370);
            label4.Name = "label4";
            label4.Size = new Size(62, 17);
            label4.TabIndex = 31;
            label4.Text = "Sub Total";
            // 
            // tbDiscount
            // 
            tbDiscount.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbDiscount.Location = new Point(120, 393);
            tbDiscount.MaxLength = 15;
            tbDiscount.Name = "tbDiscount";
            tbDiscount.Size = new Size(158, 25);
            tbDiscount.TabIndex = 30;
            // 
            // tbSubTotal
            // 
            tbSubTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbSubTotal.Location = new Point(120, 367);
            tbSubTotal.Name = "tbSubTotal";
            tbSubTotal.ReadOnly = true;
            tbSubTotal.Size = new Size(158, 25);
            tbSubTotal.TabIndex = 29;
            // 
            // purchaseItemBindingSource
            // 
            purchaseItemBindingSource.DataSource = typeof(Models.PurchaseItem);
            // 
            // totalProductPriceDataGridViewTextBoxColumn
            // 
            totalProductPriceDataGridViewTextBoxColumn.DataPropertyName = "TotalProductPrice";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            totalProductPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            totalProductPriceDataGridViewTextBoxColumn.HeaderText = "Jumlah";
            totalProductPriceDataGridViewTextBoxColumn.Name = "totalProductPriceDataGridViewTextBoxColumn";
            totalProductPriceDataGridViewTextBoxColumn.ReadOnly = true;
            totalProductPriceDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.False;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 192);
            dataGridViewCellStyle2.Format = "N0";
            priceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            priceDataGridViewTextBoxColumn.HeaderText = "Harga";
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            // 
            // productUnitDataGridViewTextBoxColumn
            // 
            productUnitDataGridViewTextBoxColumn.DataPropertyName = "ProductUnit";
            productUnitDataGridViewTextBoxColumn.HeaderText = "Unit";
            productUnitDataGridViewTextBoxColumn.Name = "productUnitDataGridViewTextBoxColumn";
            productUnitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // quantityDataGridViewTextBoxColumn
            // 
            quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(255, 255, 192);
            quantityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            quantityDataGridViewTextBoxColumn.HeaderText = "Qty";
            quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
            // 
            // productNameDataGridViewTextBoxColumn
            // 
            productNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName";
            productNameDataGridViewTextBoxColumn.HeaderText = "Nama Barang";
            productNameDataGridViewTextBoxColumn.Name = "productNameDataGridViewTextBoxColumn";
            productNameDataGridViewTextBoxColumn.ReadOnly = true;
            productNameDataGridViewTextBoxColumn.Width = 350;
            // 
            // productIdDataGridViewTextBoxColumn
            // 
            productIdDataGridViewTextBoxColumn.DataPropertyName = "ProductId";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(244, 244, 244);
            dataGridViewCellStyle4.Format = "000000";
            productIdDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            productIdDataGridViewTextBoxColumn.HeaderText = "Kode";
            productIdDataGridViewTextBoxColumn.Name = "productIdDataGridViewTextBoxColumn";
            productIdDataGridViewTextBoxColumn.ReadOnly = true;
            productIdDataGridViewTextBoxColumn.Width = 70;
            // 
            // lblGrandTotal
            // 
            lblGrandTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblGrandTotal.AutoEllipsis = true;
            lblGrandTotal.BackColor = Color.White;
            lblGrandTotal.BorderStyle = BorderStyle.FixedSingle;
            lblGrandTotal.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGrandTotal.ForeColor = SystemColors.HotTrack;
            lblGrandTotal.Location = new Point(438, 23);
            lblGrandTotal.Name = "lblGrandTotal";
            lblGrandTotal.Size = new Size(449, 51);
            lblGrandTotal.TabIndex = 28;
            lblGrandTotal.Text = "0";
            lblGrandTotal.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grid
            // 
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grid.AutoGenerateColumns = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.ColumnHeadersHeight = 26;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.Columns.AddRange(new DataGridViewColumn[] { productIdDataGridViewTextBoxColumn, productNameDataGridViewTextBoxColumn, quantityDataGridViewTextBoxColumn, productUnitDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, totalProductPriceDataGridViewTextBoxColumn });
            grid.DataSource = purchaseItemBindingSource;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Info;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            grid.DefaultCellStyle = dataGridViewCellStyle5;
            grid.Location = new Point(17, 142);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(870, 197);
            grid.TabIndex = 27;
            // 
            // tbBarcode
            // 
            tbBarcode.BackColor = Color.FromArgb(255, 255, 192);
            tbBarcode.Location = new Point(68, 111);
            tbBarcode.Name = "tbBarcode";
            tbBarcode.Size = new Size(152, 25);
            tbBarcode.TabIndex = 26;
            // 
            // tbSupplier
            // 
            tbSupplier.Location = new Point(101, 49);
            tbSupplier.Name = "tbSupplier";
            tbSupplier.ReadOnly = true;
            tbSupplier.Size = new Size(265, 25);
            tbSupplier.TabIndex = 25;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 52);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 24;
            label2.Text = "Supplier";
            // 
            // date
            // 
            date.CustomFormat = "dd/MM/yyyy";
            date.Format = DateTimePickerFormat.Custom;
            date.Location = new Point(101, 23);
            date.Name = "date";
            date.RightToLeft = RightToLeft.Yes;
            date.Size = new Size(119, 25);
            date.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 26);
            label1.Name = "label1";
            label1.Size = new Size(54, 17);
            label1.TabIndex = 22;
            label1.Text = "Tanggal";
            // 
            // SalesPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(tbPaidAmount);
            Controls.Add(textBox1);
            Controls.Add(btnSupplier);
            Controls.Add(btnPrint);
            Controls.Add(btnSave);
            Controls.Add(btnCost);
            Controls.Add(label7);
            Controls.Add(tbGrandTotal);
            Controls.Add(label6);
            Controls.Add(tbCost);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(tbDiscount);
            Controls.Add(tbSubTotal);
            Controls.Add(lblGrandTotal);
            Controls.Add(grid);
            Controls.Add(tbBarcode);
            Controls.Add(tbSupplier);
            Controls.Add(label2);
            Controls.Add(date);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "SalesPanel";
            Size = new Size(904, 524);
            ((System.ComponentModel.ISupportInitialize)purchaseItemBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox tbPaidAmount;
        private TextBox textBox1;
        private Button btnSupplier;
        private Button btnPrint;
        private Button btnSave;
        private Button btnCost;
        private Label label7;
        private TextBox tbGrandTotal;
        private Label label6;
        private TextBox tbCost;
        private Label label5;
        private Label label4;
        private TextBox tbDiscount;
        private TextBox tbSubTotal;
        private BindingSource purchaseItemBindingSource;
        private DataGridViewTextBoxColumn totalProductPriceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productUnitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn productIdDataGridViewTextBoxColumn;
        private Label lblGrandTotal;
        private DataGridView grid;
        private TextBox tbBarcode;
        private TextBox tbSupplier;
        private Label label2;
        private DateTimePicker date;
        private Label label1;
    }
}
