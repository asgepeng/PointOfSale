using System.Windows.Forms;
using System.Drawing;

namespace PointOfSale.Dialogs
{
    partial class UpdatePriceDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            grid = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            qtyInDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            Unit = new DataGridViewTextBoxColumn();
            priceInDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            oldCogsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            newCogsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            oldStockDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            newStockDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            newPriceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            newMarginDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            newPercentMarginDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceSetItemBindingSource = new BindingSource(components);
            btnCancel = new Button();
            btnOK = new Button();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)priceSetItemBindingSource).BeginInit();
            SuspendLayout();
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
            grid.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, qtyInDataGridViewTextBoxColumn, Unit, priceInDataGridViewTextBoxColumn, oldCogsDataGridViewTextBoxColumn, newCogsDataGridViewTextBoxColumn, oldStockDataGridViewTextBoxColumn, newStockDataGridViewTextBoxColumn, newPriceDataGridViewTextBoxColumn, newMarginDataGridViewTextBoxColumn, newPercentMarginDataGridViewTextBoxColumn });
            grid.DataSource = priceSetItemBindingSource;
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = SystemColors.Window;
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle9.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Info;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            grid.DefaultCellStyle = dataGridViewCellStyle9;
            grid.Location = new Point(12, 14);
            grid.Name = "grid";
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Size = new Size(948, 447);
            grid.TabIndex = 6;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // qtyInDataGridViewTextBoxColumn
            // 
            qtyInDataGridViewTextBoxColumn.DataPropertyName = "QtyIn";
            qtyInDataGridViewTextBoxColumn.HeaderText = "Qty. Beli";
            qtyInDataGridViewTextBoxColumn.Name = "qtyInDataGridViewTextBoxColumn";
            // 
            // Unit
            // 
            Unit.DataPropertyName = "Unit";
            Unit.HeaderText = "Satuan";
            Unit.Name = "Unit";
            Unit.ReadOnly = true;
            // 
            // priceInDataGridViewTextBoxColumn
            // 
            priceInDataGridViewTextBoxColumn.DataPropertyName = "PriceIn";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N0";
            priceInDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            priceInDataGridViewTextBoxColumn.HeaderText = "Hrg. Beli";
            priceInDataGridViewTextBoxColumn.Name = "priceInDataGridViewTextBoxColumn";
            // 
            // oldCogsDataGridViewTextBoxColumn
            // 
            oldCogsDataGridViewTextBoxColumn.DataPropertyName = "OldCogs";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            oldCogsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            oldCogsDataGridViewTextBoxColumn.HeaderText = "HPP Lama";
            oldCogsDataGridViewTextBoxColumn.Name = "oldCogsDataGridViewTextBoxColumn";
            // 
            // newCogsDataGridViewTextBoxColumn
            // 
            newCogsDataGridViewTextBoxColumn.DataPropertyName = "NewCogs";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            newCogsDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            newCogsDataGridViewTextBoxColumn.HeaderText = "HPP Baru";
            newCogsDataGridViewTextBoxColumn.Name = "newCogsDataGridViewTextBoxColumn";
            // 
            // oldStockDataGridViewTextBoxColumn
            // 
            oldStockDataGridViewTextBoxColumn.DataPropertyName = "OldStock";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            oldStockDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            oldStockDataGridViewTextBoxColumn.HeaderText = "Stok Lama";
            oldStockDataGridViewTextBoxColumn.Name = "oldStockDataGridViewTextBoxColumn";
            // 
            // newStockDataGridViewTextBoxColumn
            // 
            newStockDataGridViewTextBoxColumn.DataPropertyName = "NewStock";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            newStockDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            newStockDataGridViewTextBoxColumn.HeaderText = "Stok Baru";
            newStockDataGridViewTextBoxColumn.Name = "newStockDataGridViewTextBoxColumn";
            // 
            // newPriceDataGridViewTextBoxColumn
            // 
            newPriceDataGridViewTextBoxColumn.DataPropertyName = "NewPrice";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            newPriceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            newPriceDataGridViewTextBoxColumn.HeaderText = "Set Harga";
            newPriceDataGridViewTextBoxColumn.Name = "newPriceDataGridViewTextBoxColumn";
            // 
            // newMarginDataGridViewTextBoxColumn
            // 
            newMarginDataGridViewTextBoxColumn.DataPropertyName = "NewMargin";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            newMarginDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            newMarginDataGridViewTextBoxColumn.HeaderText = "Margin";
            newMarginDataGridViewTextBoxColumn.Name = "newMarginDataGridViewTextBoxColumn";
            newMarginDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // newPercentMarginDataGridViewTextBoxColumn
            // 
            newPercentMarginDataGridViewTextBoxColumn.DataPropertyName = "NewPercentMargin";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "0.00%";
            newPercentMarginDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            newPercentMarginDataGridViewTextBoxColumn.HeaderText = "% Margin";
            newPercentMarginDataGridViewTextBoxColumn.Name = "newPercentMarginDataGridViewTextBoxColumn";
            newPercentMarginDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // priceSetItemBindingSource
            // 
            priceSetItemBindingSource.DataSource = typeof(PriceSetItem);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(764, 467);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(95, 31);
            btnCancel.TabIndex = 34;
            btnCancel.Text = "Tutup";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(865, 467);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(95, 31);
            btnOK.TabIndex = 33;
            btnOK.Text = "Simpan";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // UpdatePriceDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 510);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(grid);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Name = "UpdatePriceDialog";
            Text = "UpdatePriceDialog";
            Load += FormLoadHandler;
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ((System.ComponentModel.ISupportInitialize)priceSetItemBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView grid;
        private BindingSource priceSetItemBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn qtyInDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn Unit;
        private DataGridViewTextBoxColumn priceInDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn oldCogsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn newCogsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn oldStockDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn newStockDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn newPriceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn newMarginDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn newPercentMarginDataGridViewTextBoxColumn;
        private Button btnCancel;
        private Button btnOK;
    }
}