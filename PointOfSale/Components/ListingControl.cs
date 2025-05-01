using PointOfSale.Data;
using PointOfSale.Dialogs;
using PointOfSale.Models;
using PointOfSale.Panels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Components
{
    public partial class ListingControl : UserControl
    {
        private readonly BindingSource bs;
        private readonly Database _db;
        private ColumnFilterCollection filters = new ColumnFilterCollection();
        public ListingControl(Database db, RepositoryType type)
        {
            InitializeComponent();
            _db = db;
            bs = new BindingSource();
            grid.AutoGenerateColumns = false;
            grid.DataSource = bs;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false; 
            switch (type)
            {
                case RepositoryType.Contact:
                    this.Repository = new ContactRepository(db);
                    this.Text = "Kontak";
                    break;
                default:
                    this.Repository = new ProductRepository(db);
                    this.Text = "Produk";
                    break;
            }
            Dock = DockStyle.Fill;
        }
        public IRepository Repository { get; set; }

        private async void OnControlLoaded(object sender, EventArgs e)
        {
            var columns = Repository.GetColumns();
            var columnCount = 0;
            foreach (var column in columns)
            {
                var index = grid.Columns.Add("colIndex_" + columnCount.ToString(), column.HeaderText);
                grid.Columns[index].DataPropertyName = column.DataProperty;
                grid.Columns[index].Width = column.ColumnWidth;
                grid.Columns[index].DefaultCellStyle.Alignment = column.Alignment;
                grid.Columns[index].DefaultCellStyle.Format = column.Format;
                columnCount++;
            }
            grid.Columns[0].DefaultCellStyle.BackColor = Color.AliceBlue;
            var table = await Repository.GetDataTableAsync(0);
            GridHelpers.SetFilterSetting(table, filters);
            bs.DataSource = table;

            tbSearch.TextChanged += (tb, evt) =>
            {
                filters.ApplyFilter(bs, tbSearch.Text);
            };
        }
        private PanelBase DetailControl { get; set; }
        private async void AddNew(object sender, EventArgs e)
        {
            if (DetailControl is null && !Controls.Contains(DetailControl))
            {
                await SetDetailControl();
                SetViewMode(1);
            }
        }
        private void SetViewMode(int mode)
        {
            var listingMode = mode == 0;
            btnBack.Available = !listingMode;
            btnAdd.Available = listingMode;
            btnRemove.Available = listingMode;
            btnSave.Available = !listingMode;
            lblSearch.Available = listingMode;
            tbSearch.Available = listingMode;
        }
        private void BackToList(object sender, EventArgs e)
        {
            if (DetailControl != null && Controls.Contains(DetailControl))
            {
                Controls.Remove(DetailControl);
                DetailControl.Dispose();
                DetailControl = null;
                SetViewMode(0);
            }
        }

        private async void SaveData(object sender, EventArgs e)
        {
            if (DetailControl != null)
            {
                if (await DetailControl.Save(this.Repository))
                {
                    Controls.Remove(DetailControl);
                    DetailControl.Dispose();
                    DetailControl = null;

                    bs.DataSource = await Repository.GetDataTableAsync(0);
                    SetViewMode(0);
                }
            }
        }

        private async void DeleteCurrent(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                var row = (DataRowView)bs.Current;
                var id = (int)row[0];
                await Repository.DeleteAsync(id);
                bs.DataSource = await Repository.GetDataTableAsync(0);
            }
        }
        private async Task SetDetailControl(int id = 0)
        {
            if (Repository is ContactRepository) DetailControl = new ContactPanel(_db);
            if (Repository is ProductRepository) DetailControl = new ProductPanel();
            if (DetailControl != null)
            {
                DetailControl.Tag = await Repository.GetByIdAsync(id);
                DetailControl.Dock = DockStyle.Fill;
                Controls.Add(DetailControl);
                DetailControl.BringToFront();
            }
        }
        private async void OnGridDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            if (bs.Current != null && DetailControl is null)
            {
                var id = (int)((DataRowView)bs.Current)[0];
                var obj = await Repository.GetByIdAsync(id);
                await SetDetailControl(id);
                SetViewMode(1);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var dlg = new CategoryDialog(_db);
            dlg.ShowDialog();
        }

        private void FilterOnlyAlphabet(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyAlphaNumericValue(e);
        }
    }
}
