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
    public partial class CostDialog : Form
    {
        public CostCollection Costs { get; } = new CostCollection();
        public CostDialog()
        {
            InitializeComponent();
        }

        private void CostDialogLoad(object sender, EventArgs e)
        {
            costBindingSource.DataSource = Costs;
        }

        private void CreateNewCost(object sender, EventArgs e)
        {
            costBindingSource.Add(new Cost());
            grid.CurrentCell = grid.Rows[grid.Rows.Count - 1].Cells[0];
            grid.BeginEdit(true);
        }
        private void CloseWithOK(object sender, EventArgs e)
        {
            foreach (var cost in this.Costs)
            {
                if (cost.Name.Trim() == "" || cost.Amount == 0)
                {
                    MessageBox.Show("Keterangan dan nilai biaya tidak boleh kosong", "Data kosong", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RemoveCurrent(object sender, EventArgs e)
        {
            if (costBindingSource.Current != null)
            {
                costBindingSource.RemoveCurrent();
            }
        }

        private void CalculateTotalCost(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = Costs.GetTotal().ToString("N0");
        }
    }
}
