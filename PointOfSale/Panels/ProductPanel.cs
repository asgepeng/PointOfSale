using PointOfSale.Data;
using PointOfSale.Dialogs;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Panels
{
    public partial class ProductPanel : PanelBase
    {
        public ProductPanel()
        {
            InitializeComponent();
            cbCategory.DisplayMember = "Name";
            cbCategory.ValueMember = "Id";
        }
        public override async Task<bool> Save(IRepository repository)
        {
            if (tbName.Text.Trim() == "")
            {
                MessageBox.Show("Nama produk tidak boleh kosong", "data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbName.Focus();
                return false;
            }

            if (tbSku.Text.Trim() == "")
            {
                MessageBox.Show("SKU / kode barcode tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbSku.Focus();
                return false;
            }
            if (cbUnit.Text.Trim() == "")
            {
                MessageBox.Show("Satuan barang tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbUnit.Focus();
                return false;
            }


            var view = Tag is null ? new ProductView() : (ProductView)Tag;
            view.Product.Name = tbName.Text.Trim();
            view.Product.Description = tbDescription.Text.Trim();
            view.Product.Category = cbCategory.SelectedItem != null ? (int)((Category)cbCategory.SelectedItem).Id : 0;
            view.Product.Unit = cbUnit.Text;
            view.Product.Sku = tbSku.Text.Trim();
            view.Product.Price = FormatHelpers.GetDecimal(tbPrice.Text);
            view.Product.Unit = cbUnit.Text;

            if (view.Product.Id > 0)
            {
                var result = (bool)await repository.UpdateAsync(view.Product);
                return result;
            }
            else
            {
                var product = (Product)(await repository.CreateAsync(view.Product));
                return product.Id > 0;
            }
        }
        private void FilterOnlyNumber (object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyNumber(e);
        }
        private void OnPanelLoad(object sender, EventArgs e)
        {
            if (Tag is null) return;

            var view = (ProductView)Tag;
            if (view.Product != null)
            {
                tbName.Text = view.Product.Name;
                tbSku.Text = view.Product.Sku;
                tbDescription.Text = view.Product.Description;
                InitializeCategoryComboBox(view.Categories, view.Product.Category);
                InitializeUnitComboBox(view.Units, view.Product.Unit);
                tbBasicPrice.Text = view.Product.BasicPrice.ToString("0.00");
                tbPrice.Text = view.Product.Price.ToString("0.00");
                tbMargin.Text = view.Product.Margin.ToString("0.00");
            }
            else
            {
                InitializeCategoryComboBox(view.Categories);
                InitializeUnitComboBox(view.Units);
            }
            tbName.Focus();
        }
        private void InitializeCategoryComboBox(List<Category> categories, int categoryId = 0)
        {
            for (int i = 0; i < categories.Count; i++)
            {
                cbCategory.Items.Add(categories[i]);
                if (categories[i].Id == categoryId)
                {
                    cbCategory.SelectedIndex = i;
                }
            }
        }
        private void InitializeUnitComboBox(List<string> units, string unit = "")
        {
            for (int i = 0; i < units.Count; i++)
            {
                cbUnit.Items.Add(units[i]);
                if (units[i] == unit)
                {
                    cbUnit.SelectedIndex = i;
                }
            }
        }
    }
}
