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
    public partial class UpdatePriceDialog : Form
    {
        private readonly Database db;
        public UpdatePriceDialog(Database _db)
        {
            InitializeComponent();
            db = _db;
        }
        private List<PriceSetItem> Items { get; } = new List<PriceSetItem>();

        private async void FormLoadHandler(object sender, EventArgs e)
        {
            if (Tag != null)
            {
                int id = (int)Tag;
                var commandText = @"SELECT p.id, p.[name], s.oldcogs, s.oldstock, s.price, p.unit, s.credit, s.newcogs, s.newstock, p.[price] As newprice
FROM stockflows AS s WITH (NOLOCK)
INNER JOIN products AS p WITH (NOLOCK) ON s.product = p.id AND p.deleted = 0
WHERE s.transactionId = @id AND s.[type] = 1";

                using (var reader = await db.ExecuteReaderAsync(commandText, new System.Data.SqlClient.SqlParameter("@id", id)))
                {
                    if (reader != null)
                    {
                        while (await reader.ReadAsync())
                        {
                            this.Items.Add(new PriceSetItem()
                            {
                                Id = reader.GetInt32(0),         // p.id
                                Name = reader.GetString(1),      // p.name
                                OldCogs = reader.GetDecimal(2),  // s.oldcogs
                                OldStock = reader.GetInt32(3),   // s.oldstock
                                PriceIn = reader.GetDecimal(4),  // s.price
                                Unit = reader.GetString(5),      // p.unit
                                QtyIn = reader.GetInt32(6),      // s.credit
                                NewCogs = reader.GetDecimal(7),  // s.newcogs
                                NewStock = reader.GetInt32(8),   // s.newstock
                                NewPrice = reader.GetDecimal(9)  // p.price AS newprice
                            });
                        }
                    }
                }
                this.priceSetItemBindingSource.DataSource = this.Items;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            foreach (var item in this.Items)
            {
                sb.Append($"UPDATE products SET price = {item.NewPrice} WHERE id = {item.Id}; ");
            }
            if (await db.ExecuteNonQueryAsync(sb.ToString()))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }

    public class PriceSetItem
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public decimal QtyIn { get; set; } = 0;
        public string Unit { get; set; } = "";
        public decimal OldCogs { get; set; }
        public decimal OldStock { get; set; }
        public decimal PriceIn { get; set; }
        public decimal NewCogs { get; set; }
        public decimal NewStock { get; set; }
        public decimal NewPrice { get; set; }
        public decimal NewMargin => NewPrice - NewCogs;
        public decimal NewPercentMargin => NewMargin / NewCogs;
    }
}
