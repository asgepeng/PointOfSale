using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class Purchase
    {
        public int Id { get; set; } = 0;
        public string Invoice { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public int Supplier { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal Tax { get; set; } = 0;
        public decimal Cost { get; set; } = 0;
        public CostCollection Costs { get; } = new CostCollection();
        public decimal PaidAmount { get; set; }
        public List<PurchaseItem> Items { get; } = new List<PurchaseItem>();
        public (decimal bruto, decimal subtotal, decimal netto) Calculate()
        {
            decimal bruto = 0, subtotal = 0, netto = 0, discount = 0;
            foreach (var item in this.Items)
            {
                bruto += item.TotalProductPrice;
            }
            subtotal = bruto - Discount;
            netto = subtotal + discount + Cost;
            return (bruto, subtotal, netto);
        }
        public bool ItemExists(int id, decimal qty)
        {
            foreach (var item in Items)
            {
                if (item.ProductId == id)
                {
                    item.Quantity += qty;
                    return true;
                }
            }
            return false;
        }
    }

    public class PurchaseItem
    {
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public string ProductUnit { get; set; } = "";
        public string ProductSKU { get; set; } = "";
        public decimal Price { get; set; } = 0;
        public decimal Quantity { get; set; } = 0;
        public decimal TotalProductPrice => Price * Quantity;
        public decimal OldStock { get; set; } = 0;
        public decimal OldBasicPrice { get; set; } = 0;
        public string Action => "Hapus";
    }

    public class PurchaseManager
    {
        private readonly Database db;
        public PurchaseManager(Database _db) { db = _db; }
        public async Task<Purchase>CreateAsync(Purchase purchase)
        {
            (_, _, decimal netto) = purchase.Calculate();
            var totalPaid = purchase.PaidAmount > netto ? netto : purchase.PaidAmount;
            var ap = totalPaid < netto ? netto - totalPaid : 0;
            var costPerItem = (decimal)0;
            if ((purchase.Cost - purchase.Discount) > 0)
            {
                var totalItems = (decimal)0;
                foreach (var item in purchase.Items)
                {
                    totalItems += item.Quantity;
                }
                costPerItem = (purchase.Cost - purchase.Discount) / totalItems;
            }
            purchase.Invoice = await db.GenerateTransactionIdAsync("PMB");
            var sb = new StringBuilder();
            sb.Append($@"INSERT INTO purchases ([invoice], [date], [supplier], [discount], [tax], [cost], [ambp], [tp], [ap], [author], [datecreated])
VALUES (@invoice, @date, @supplier, @discount, @tax, @cost, @ambp, 0, @ap, @author, GETDATE());
DECLARE @purchaseID INT = (SELECT SCOPE_IDENTITY());
WITH pitems AS 
(
	SELECT 
		@purchaseID AS id,
		1 AS [type],
		T.product,
		T.qty AS credit,
		T.price,
		p.stock AS oldstock,
		p.stock + T.qty AS newstock,
		p.basicprice AS oldcogs,
		CASE 
			WHEN p.stock < 1 THEN T.price + {costPerItem}
			ELSE ROUND(((p.stock * p.basicprice) + ((T.price + {costPerItem}) * T.qty)) / (p.stock + T.qty), 0) 
		END AS newcogs
	FROM 
		(VALUES ");
            for (int i = 0; i < purchase.Items.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("(").Append(purchase.Items[i].ProductId).Append(",").Append(purchase.Items[i].Price).Append(",").Append(purchase.Items[i].Quantity).Append(")");
            }
            sb.Append($@") AS T (product, price, qty)
	INNER JOIN products AS p ON T.product = p.id
)
INSERT INTO stockflows 
(transactionId, [type], [product], [credit], [price], [oldstock], [newstock], oldcogs, newcogs)
SELECT id, [type], product, credit, price, oldstock, newstock, oldcogs, newcogs
FROM pitems; 
WITH pitems AS 
(
	SELECT 
		T.product,
		p.stock + T.qty AS newstock,
		CASE 
			WHEN p.stock < 1 THEN T.price + {costPerItem}
			ELSE ROUND(((p.stock * p.basicprice) + ((T.price + {costPerItem}) * T.qty)) / (p.stock + T.qty), 0) 
		END AS newcogs
	FROM 
		(VALUES ");
            for (int i = 0; i < purchase.Items.Count; i++)
            {
                if (i > 0) sb.Append(",");
                sb.Append("(").Append(purchase.Items[i].ProductId).Append(",").Append(purchase.Items[i].Price).Append(",").Append(purchase.Items[i].Quantity).Append(")");
            }
            sb.Append(@") AS T (product, price, qty)
	INNER JOIN products AS p ON T.product = p.id
)
UPDATE p
SET 
	p.stock = t.newstock,
	p.basicprice = t.newcogs
FROM pitems AS t
INNER JOIN products AS p ON t.product = p.id;");
            if (purchase.Costs.Count > 0)
            {
                sb.Append("INSERT INTO costs ([transactionId], [transactionType], [notes], [amount]) VALUES ");
                var rows = new string[purchase.Costs.Count];
                for (int i = 0; i < purchase.Costs.Count; i++)
                {
                    var costName = SqlHelpers.FromString(purchase.Costs[i].Name);
                    var costValue = purchase.Costs[i].Amount;
                    rows[i] = $"(@purchaseID, 1, {costName}, {costValue})";
                }
                sb.AppendLine(string.Join(",", rows));
            }
            sb.Append("SELECT @purchaseID as newID");
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@date", purchase.Date),
                new SqlParameter("@invoice", purchase.Invoice),
                new SqlParameter("@supplier", purchase.Supplier),
                new SqlParameter("@discount", purchase.Discount),
                new SqlParameter("@tax", purchase.Tax),
                new SqlParameter("@cost", purchase.Cost),
                new SqlParameter("@ambp", netto),
                new SqlParameter("@ap", netto),
                new SqlParameter("@author", My.Application.User != null ? My.Application.User.Id : 0)
            };
            purchase.Id = await db.ExecuteScalarIntegerAsync(sb.ToString(), parameters);
            if (purchase.Id > 0 && purchase.PaidAmount > 0)
            {
                await db.CreatePaymentAsync(TransactionType.Purchase, purchase.Id, purchase.PaidAmount);
            }
            return purchase;
        }
        public async Task<PurchaseItem> GetPurchaseItemAsync(string sku, int qty)
        {
            var commandText = $@"SELECT id, [name], [sku], {qty} AS qty, [unit], [basicprice]
FROM products WITH (NOLOCK)
WHERE (deleted = 0) AND ([sku] = @sku);";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@sku", sku)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        var item = new PurchaseItem()
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            ProductSKU = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            ProductUnit = reader.GetString(4),
                            Price = reader.GetDecimal(5)
                        };
                        return item;
                    }
                }
            }
            int.TryParse(sku, out int id);
            if (id == 0) return null;

            commandText = $@"SELECT id, [name], [sku], {qty} AS qty, [unit], [basicprice]
FROM products WITH (NOLOCK)
WHERE (deleted = 0) AND ([id] = @id);";

            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        var item = new PurchaseItem()
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            ProductSKU = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            ProductUnit = reader.GetString(4),
                            Price = reader.GetDecimal(5)
                        };
                        return item;
                    }
                }
            }

            return null;
        }
        public async Task<PurchaseItem> GetPurchaseItemAsync(int id, int qty)
        {
            if (id == 0) return null;
            var commandText = $@"SELECT id, [name], [sku], {qty} AS qty, [unit], [basicprice]
FROM products WITH (NOLOCK)
WHERE (deleted = 0) AND ([id] = @id);";

            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        var item = new PurchaseItem()
                        {
                            ProductId = reader.GetInt32(0),
                            ProductName = reader.GetString(1),
                            ProductSKU = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            ProductUnit = reader.GetString(4),
                            Price = reader.GetDecimal(5)
                        };
                        return item;
                    }
                }
            }
            return null;
        }
    }
}
