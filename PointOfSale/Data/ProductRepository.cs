using System.Data.SqlClient;
using PointOfSale.Models;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Data
{
    public enum RepositoryType
    {
        Contact = 0,
        Product = 1,
        ProductCategory = 2,
        Purchase = 3,
        Sales = 4,
        AccountPayable = 5,
        AccountReceivable = 6
    }
    public interface IRepository
    {
        Task<DataTable> GetDataTableAsync(int id);
        Task<object> GetByIdAsync(int id);
        Task<object> CreateAsync(object model); 
        Task<object> UpdateAsync(object model);
        Task<bool> DeleteAsync(int id);
        DataTableColumnInfo[] GetColumns();
    }

    public class DataTableColumnInfo
    {
        public string HeaderText { get; }
        public string DataProperty { get; }
        public int ColumnWidth { get; } = 100;
        public DataGridViewContentAlignment Alignment { get; } = DataGridViewContentAlignment.MiddleLeft;
        public string Format { get; } = "";
        public DataTableColumnInfo(string header, string columnName, int columnWidth, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft, string format = "")
        {
            this.HeaderText = header;
            this.DataProperty = columnName;
            this.ColumnWidth = columnWidth;
            this.Alignment = alignment;
            this.Format = format;
        }
    }
    public class ProductRepository : IRepository
    {
        private readonly Database db;
        public ProductRepository(Database _db) { db = _db; }
        public async Task<object> CreateAsync(object model)
        {
            var product = (Product)model;
            var commandText = @"INSERT INTO products ([name], [description], [sku], [category], [unit], [basicprice], [price], [stock], [supplier], [images], [author], [datecreated])
VALUES (@name, @description, @sku, @category, @unit, @basicprice, @price, @stock, @supplier, @images, @author, @datecreated);
SELECT SCOPE_IDENTITY();";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", product.Name),
                new SqlParameter("@description", product.Description),
                new SqlParameter("@sku", product.Sku),
                new SqlParameter("@category", product.Category),
                new SqlParameter("@unit", product.Unit),
                new SqlParameter("@basicprice", product.BasicPrice),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@stock", product.Stock),
                new SqlParameter("@supplier", product.Supplier),
                new SqlParameter("@images", product.Images.Count > 0 ? string.Join(",",product.Images) : ""),
                new SqlParameter("@author", My.Application.User != null ? My.Application.User.Id : 0),
                new SqlParameter("@datecreated", DateTime.Now)
            };
            product.Id = await db.ExecuteScalarIntegerAsync(commandText, parameters);
            return product;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var commandText = "UPDATE products SET deleted = 1 WHERE id = @id";
            return await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id));
        }
        public async Task<object> GetByIdAsync(int id)
        {
            var commandText = @"SELECT id, [name], [sku], [description],[category], [unit], price, basicprice
FROM products WITH (NOLOCK)
WHERE id = @id AND deleted = 0";
            var view = new ProductView();
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        view.Product.Id = reader.GetInt32(0);
                        view.Product.Sku = reader.GetString(2);
                        view.Product.Name = reader.GetString(1);
                        view.Product.Description = reader.GetString(3);
                        view.Product.Category = reader.GetInt32(4);
                        view.Product.Unit = reader.GetString(5);
                        view.Product.BasicPrice = reader.GetDecimal(7);
                        view.Product.Price = reader.GetDecimal(6);
                    }
                    reader.Close();
                    reader.Dispose();    
                }
            }
            commandText = "SELECT id, [name] FROM categories WITH (NOLOCK) ORDER BY [name];";
            using (var reader = await db.ExecuteReaderAsync(commandText))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        view.Categories.Add(new Category()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }

            commandText = "SELECT DISTINCT unit FROM products WHERE deleted = 0";
            using (var reader = await db.ExecuteReaderAsync(commandText))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        view.Units.Add(reader.GetString(0));
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }
            return view;
        }

        public async Task<DataTable> GetDataTableAsync(int id)
        {
            var commandText = @"SELECT p.id, p.[name], c.[name] AS category, p.sku, p.stock, p.unit, p.price, a.[name] AS author, p.datecreated, m.[name] AS modifier, p.datemodified
FROM products AS p
INNER JOIN categories AS c ON p.category = c.id
INNER JOIN users AS a ON p.author = a.id
LEFT JOIN users AS m ON p.modifier = m.id
WHERE deleted = 0";
            return await db.ExecuteDataTableAsync(commandText);
        }

        public async Task<object> UpdateAsync(object model)
        {
            var product = (Product)model;
            var commandText = @"UPDATE products
SET sku = @sku,
[name] = @name,
[description] = @description,
[category] = @category,
[unit] = @unit,
[price] = @price,
[modifier] = @modifier,
[datemodified] = GETDATE()
WHERE id = @id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@sku", product.Sku),
                new SqlParameter("@name", product.Name),
                new SqlParameter("@description", product.Description),
                new SqlParameter("@category", product.Category),
                new SqlParameter("@unit", product.Unit),
                new SqlParameter("@price", product.Price),
                new SqlParameter("@id", product.Id),
                new SqlParameter("@modifier", My.Application.User != null ? My.Application.User.Id : 0)
            };
            return await db.ExecuteNonQueryAsync(commandText, parameters);
        }
        public DataTableColumnInfo[] GetColumns()
        {
            return new DataTableColumnInfo[]
            {
                new DataTableColumnInfo("Kode", "id", 100, DataGridViewContentAlignment.MiddleCenter, "0000000"),
                new DataTableColumnInfo("Nama Barang", "name", 250),
                new DataTableColumnInfo("Kategori", "category", 200),
                new DataTableColumnInfo("Stok", "stock", 60, DataGridViewContentAlignment.MiddleRight, "N0"),
                new DataTableColumnInfo("Satuan", "unit", 100),
                new DataTableColumnInfo("Harga", "price", 100, DataGridViewContentAlignment.MiddleRight, "N2"),
                new DataTableColumnInfo("Dibuat oleh", "author", 150),
                new DataTableColumnInfo("Terakhir Diubah", "datemodified", 120,DataGridViewContentAlignment.MiddleRight, "dd/MM/yyyy HH:mm"),
                new DataTableColumnInfo("Diubah oleh", "modifier", 150)
            };
        }
    }
}
