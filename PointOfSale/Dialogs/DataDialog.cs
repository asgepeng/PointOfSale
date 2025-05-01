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
    public partial class DataDialog : Form
    {
        private readonly Database db;
        public DataDialog(Database _db)
        {
            InitializeComponent();
            grid.DataSource = bs;
            this.db = _db;
            grid.AutoGenerateColumns = false;
        }
        public string TextFilter { get; set; } = "";
        public void AddColumn(string headerText, string dataProperty, int width, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft, string format = "")
        {
            var index = grid.Columns.Add("col_" + grid.Columns.Count.ToString(), headerText);
            grid.Columns[index].DataPropertyName = dataProperty;
            grid.Columns[index].Width = width;
            grid.Columns[index].DefaultCellStyle.Alignment = alignment;
            grid.Columns[index].DefaultCellStyle.Format = format;
        }
        private ColumnFilterCollection filters = new ColumnFilterCollection();
        public string CommandText { get; set; } = "";
        private async Task LoadData()
        {
            var table = await db.ExecuteDataTableAsync(CommandText);
            if (filters.Count == 0)
            {
                foreach (DataColumn col in table.Columns)
                {
                    if (col.DataType.Equals(typeof(string)))
                    {
                        filters.Add(col.ColumnName);
                    }
                }
            }
            bs.DataSource = table;
        }

        private void tbFilterTextChanged(object sender, EventArgs e)
        {
            if (tbFilter.Text.Trim() == "")
            {
                bs.RemoveFilter();
                return;
            }
            filters.ApplyFilter(bs, tbFilter.Text);
        }

        private async void OnDialogLoad(object sender, EventArgs e)
        {
            await LoadData();
            if (TextFilter.Length > 0) tbFilter.Text = TextFilter;
            tbFilter.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedID >= 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        public int SelectedID
        {
            get
            {
                if (bs.Current != null)
                {
                    var view = (DataRowView)bs.Current;
                    if (view[0] != null)
                    {
                        return (int)view[0];
                    }
                }
                return -1;
            }
        }
        public string GetString(int columnIndex)
        {
            if (bs.Current != null)
            {
                var view = (DataRowView)bs.Current;
                if (view[columnIndex] != null)
                {
                    return view[columnIndex]?.ToString() ?? string.Empty;
                }
            }
            return "";
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            FormatHelpers.FilterOnlyAlphaNumericValue(e);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                bs.MovePrevious();
            }
            else if (e.KeyData == Keys.Down)
            {
                bs.MoveNext();
            }
            else if (e.KeyData == Keys.Enter)
            {
                btnOK.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOK.PerformClick();
        }
    }
}
