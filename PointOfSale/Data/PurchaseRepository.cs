using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Data
{
    public class PurchaseRepository : IRepository
    {
        private readonly IDatabase db;
        public PurchaseRepository(IDatabase _db) { db = _db; }
        public Task<object> CreateAsync(object model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public DataTableColumnInfo[] GetColumns()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetDataTableAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateAsync(object model)
        {
            throw new NotImplementedException();
        }
    }
}
