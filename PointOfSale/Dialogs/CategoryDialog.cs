using PointOfSale.Data;
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

namespace PointOfSale.Dialogs
{
    public partial class CategoryDialog : Form
    {
        private readonly BindingSource bs;
        private readonly CategoryRepository repository;
        public CategoryDialog(Database db)
        {
            InitializeComponent();
            repository = new CategoryRepository(db);
            bs = new BindingSource();
            GridHelpers.InitializeDataGridColumns(grid, repository.GetColumns(), bs);
        }

        private async void OnDialogLoaded(object sender, EventArgs e)
        {
            bs.DataSource = await repository.GetDataTableAsync(0);
            tbFilter.TextChanged += FilterData;
        }
        private void FilterData(object sender, EventArgs e)
        {
            bs.Filter = "[name] LIKE '%" + tbFilter.Text.Replace("'", "''") + "%'";
        }
        private async void CreateNewCategory(object sender, EventArgs e)
        {
            CategoryDetailDialog dlg = new CategoryDetailDialog();
        start:
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.Tag is null) return;
                var category = (Category)dlg.Tag;
                if (await repository.CategoryNameExist(category.Name))
                {
                    MessageBox.Show("Kategori produk dengan nama '" + category.Name + "' sudah terdaftar di database. Silakan membuat dengan nama lain");
                    goto start;
                }
                if (((Category)await repository.CreateAsync(category)).Id > 0)
                {
                    bs.DataSource = await repository.GetDataTableAsync(0);
                }
            }
        }
        private async void EditCategory(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (bs.Current != null)
            {
                var row = (DataRowView)bs.Current;
                var category = new Category()
                {
                    Id = (int)row[0],
                    Name = row[1].ToString() ?? ""
                };
                var dlg = new CategoryDetailDialog();
                dlg.Tag = category;
            restart:
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.Tag is null) return;
                    category = (Category)dlg.Tag;
                    if (await repository.CategoryNameExist(category.Name, category.Id))
                    {
                        MessageBox.Show("Nama kategori '" + category.Name + "' sudah terdaftar di database, silakan menggunakan nama lain", "Nama kategori", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto restart;
                    }
                    await repository.UpdateAsync(category);
                    bs.DataSource = await repository.GetDataTableAsync(0);
                }
            }
        }

        private async void DeleteCategory(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                var row = (DataRowView)bs.Current;
                var id = (int)row[0];
                if (MessageBox.Show("Anda yakin akan menghapus kategori?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (await repository.DeleteAsync(id))
                    {
                        bs.DataSource = await repository.GetDataTableAsync(0);
                    }
                }
            }
        }
    }
}
