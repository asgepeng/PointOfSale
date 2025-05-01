using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Models;
using System.Windows.Forms;

namespace PointOfSale.Data
{
    public class CategoryRepository : IRepository
    {
        private readonly IDatabase db;
        public CategoryRepository(IDatabase _db) { db = _db; }
        public async Task<object> CreateAsync(object model)
        {
            var category = (Category)model;
            var commandText = "INSERT INTO categories ([name]) VALUES (@name); SELECT SCOPE_IDENTITY()";
            var parameter = new SqlParameter("@name", category.Name);
            category.Id = await db.ExecuteScalarIntegerAsync(commandText, parameter);
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await db.ExecuteNonQueryAsync("DELETE FROM categories WHERE id=@id", new SqlParameter("@id", id));
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var category = new Category();
            var commandText = "SELECT id, [name] FROM categories WHERE (id=@id)";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        category.Id = reader.GetInt32(0);
                        category.Name = reader.GetString(1);
                    }
                }
            }
            return category;
        }

        public DataTableColumnInfo[] GetColumns()
        {
            return new DataTableColumnInfo[]
            {
                new DataTableColumnInfo("Kode", "id", 80, DataGridViewContentAlignment.MiddleCenter, "000000"),
                new DataTableColumnInfo("Description", "name", 200)
            };
        }

        public async Task<DataTable> GetDataTableAsync(int id)
        {
            return await db.ExecuteDataTableAsync("SELECT id, [name] FROM categories ORDER BY id");
        }

        public async Task<object> UpdateAsync(object model)
        {
            var category = (Category)model;
            var commandText = "UPDATE categories SET [name] = @name WHERE id=@id";
            await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@name", category.Name), new SqlParameter("id", category.Id));
            return category;
        }
        public async Task<bool> CategoryNameExist(string name, int id = 0)
        {
            var commandText = "SELECT 1 FROM categories WHERE [name] LIKE @name AND NOT [id] = @id";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@name", name), new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    return await reader.ReadAsync();
                }
            }
            return false;
        }
    }
}
