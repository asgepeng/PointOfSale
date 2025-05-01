using PointOfSale.Dialogs;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Panels
{
    public partial class PurchasePanel : UserControl
    {
        private readonly Database db;
        private readonly PurchaseManager manager;
        public PurchasePanel(Database _db)
        {
            InitializeComponent();
            db = _db;
            manager = new PurchaseManager(db);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dock = DockStyle.Fill;
            Text = "PEMBELIAN";
        }
        public Purchase Purchase { get; private set; } = new Purchase();
        public CostCollection Costs { get; private set; } = new CostCollection();
        private void UpdateSummary()
        {
            (var bruto, var subtotal, var grandTotal) = Purchase.Calculate();
            tbSubTotal.Text = bruto.ToString("N0");
            tbCost.Text = Purchase.Cost.ToString("N0");
            tbGrandTotal.Text = grandTotal.ToString("N0");
            lblGrandTotal.Text = tbGrandTotal.Text;
        }
        private void OpenCostDialog(object sender, EventArgs e)
        {
            CostDialog dlg = new CostDialog();
            dlg.Costs.AddRange(Purchase.Costs.ToArray());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Purchase.Costs.Clear();
                Purchase.Costs.AddRange(dlg.Costs.ToArray());
                Purchase.Cost = Purchase.Costs.GetTotal();
                UpdateSummary();
            }
        }

        private void OnPanelLoad(object sender, EventArgs e)
        {
            if (Tag != null)
            {

            }
            tbDate.Text = Purchase.Date.ToString("dd-MM-yyyy HH:mm");
            UpdateSummary();
            purchaseItemBindingSource.DataSource = Purchase.Items;
        }

        private async void FindProduct(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) return;
            PurchaseItem item = null;
            var qty = 1;
            if (tbBarcode.Text.Trim() != "")
            {                
                item = await manager.GetPurchaseItemAsync(tbBarcode.Text.Trim(), qty);
            }
            if (item != null)
            {
                AddPurchaseItem(item);
            }
            else
            {
                DataDialog dlg = new DataDialog(db);
                dlg.TextFilter = tbBarcode.Text.Trim();
                dlg.AddColumn("Kode", "id", 70, DataGridViewContentAlignment.MiddleCenter, "000000");
                dlg.AddColumn("SKU", "sku", 120);
                dlg.AddColumn("Nama barang", "name", 250);
                dlg.AddColumn("Stok", "stock", 100, DataGridViewContentAlignment.MiddleRight, "N0");
                dlg.AddColumn("Satuan", "unit", 100);
                dlg.AddColumn("Harga", "price", 100, DataGridViewContentAlignment.MiddleRight, "N2");
                dlg.CommandText = "SELECT [id], [sku], [name], [stock], [unit], [price] FROM products WHERE (deleted = 0)";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AddPurchaseItem(await manager.GetPurchaseItemAsync(dlg.SelectedID, qty));
                }
            }
        }
        private void AddPurchaseItem(PurchaseItem item)
        {
            if (item is null) return;
            if (!Purchase.ItemExists(item.ProductId, item.Quantity))
            {
                purchaseItemBindingSource.Add(item);
                grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells[3];
                grid.BeginEdit(true);
            }
            else purchaseItemBindingSource.ResetBindings(false);
            tbBarcode.Clear();
            UpdateSummary();
        }
        private void OnGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateSummary();
            var currentRow = grid.Rows[e.RowIndex];
            var isLastRow = e.RowIndex == grid.RowCount - 1;
            if (isLastRow)
            {
                if (e.ColumnIndex == 3)
                {
                    grid.CurrentCell = currentRow.Cells[5];
                    grid.BeginEdit(true);
                }
                else if (e.ColumnIndex == 5)
                {
                    tbBarcode.Focus();
                }
            }
        }
        private void FilterOnlyNumber(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyNumber(e);
        }

        private async void SavePurchase(object sender, EventArgs e)
        {
            if (Purchase is null) return;
            if (Purchase.Items.Count == 0)
            {
                MessageBox.Show("Tidak ada daftar belanja untuk disimpan", "Kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            (_, _, decimal netto) = Purchase.Calculate();
            if (Purchase.PaidAmount < netto && !(await UserAccessSetting.AllowToCreateAccountPayable(db)))
            {
                var username = My.Application.User != null ? My.Application.User.Name : "";
                MessageBox.Show("Transaksi hutang tidak diijinkan untuk user " + username + ", Jumlah bayar tidak boleh kurang dari " + netto.ToString("N2"), "Invalid entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbPaidAmount.Focus();
                return;
            }
            if (Purchase.PaidAmount < netto && Purchase.Supplier == 0)
            {
                MessageBox.Show("Supplier tidak boleh kosong untuk transaksi pembelian hutang", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSupplier.Focus();
                return;
            }
            if (Purchase.PaidAmount > netto) Purchase.PaidAmount = netto;
            if (Purchase.PaidAmount < netto)
            {
                var ap = netto - Purchase.PaidAmount;
                if (MessageBox.Show("Transaksi ini akan membentuk hutang sebesar Rp" + ap.ToString("N0") + " kepada " + tbSupplier.Text + ". Yakin tetap lanjutkan transaksi? Klik No untuk membatalkan.", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.No)
                {
                    return;
                }
            }
            var purchase = await manager.CreateAsync(Purchase);
            if (purchase.Id > 0)
            {
                MessageBox.Show("Transaksi pembelian berhasil disimpan dengan nomor invoice '" + purchase.Invoice + "'", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdatePriceDialog dlg = new UpdatePriceDialog(db);
                dlg.Tag = purchase.Id;
                dlg.ShowDialog();
                ClearData();
            }
        }
        private void ClearData()
        {
            tbSupplier.Clear();
            Purchase = new Purchase();
            tbDate.Text = Purchase.Date.ToString("dd-MM-yyyy HH:mm");
            purchaseItemBindingSource.DataSource = null;
            purchaseItemBindingSource.DataSource = Purchase.Items;
            tbDiscount.Clear();
            tbPaidAmount.Clear();
            UpdateSummary();
        }
        private void OpenSupplierDialog(object sender, EventArgs e)
        {
            DataDialog dlg = new DataDialog(db);
            dlg.AddColumn("Nama supplier", "name", 300);
            dlg.AddColumn("Alamat", "streetline", 350);
            dlg.CommandText = @"SELECT c.id, c.[name], d.streetline
FROM contacts AS c WITH (NOLOCK)
INNER JOIN addresses AS d WITH (NOLOCK) ON c.id = d.contact AND d.isPrimary = 1
WHERE c.deleted = 0 AND c.[type] = 1";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Purchase.Supplier = dlg.SelectedID;
                tbSupplier.Text = dlg.GetString(1);
            }
        }
        private void OnTextBoxDiscountTextChanged(object sender, EventArgs e)
        {
            Purchase.Discount = FormatHelpers.GetDecimal(tbDiscount.Text);
            UpdateSummary();
        }
        private void OnTextBoxDiscountEnter(object sender, EventArgs e)
        {
            FormatHelpers.ConvertToNumber((TextBox)sender);
            tbDiscount.TextChanged += OnTextBoxDiscountTextChanged;
        }

        private void OnTextBoxDiscountLeave(object sender, EventArgs e)
        {
            tbDiscount.TextChanged -= OnTextBoxDiscountTextChanged;
            FormatHelpers.RollBackToDecimal((TextBox)sender);
        }
        private void OnTextBoxAmountTextChanged(object sender, EventArgs e)
        {
            var value = FormatHelpers.GetDecimal(tbPaidAmount.Text);
            Purchase.PaidAmount = value;
        }
        private void OnTextBoxAmountLeave(object sender, EventArgs e)
        {
            tbPaidAmount.TextChanged -= OnTextBoxAmountTextChanged;
            FormatHelpers.RollBackToDecimal(tbPaidAmount);
        }

        private void OnTextBoxAmountEnter(object sender, EventArgs e)
        {
            FormatHelpers.ConvertToNumber(tbPaidAmount);
            tbPaidAmount.TextChanged += OnTextBoxAmountTextChanged;
        }

        private void HandleButtonClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                if (purchaseItemBindingSource.Current != null)
                {
                    purchaseItemBindingSource.RemoveCurrent();
                    UpdateSummary();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            UpdateSummary();
        }

        private void CancelPurchase(object sender, EventArgs e)
        {
            ClearData();
        }
    }
}
