using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Models
{
    public class UserAccessSetting
    {
        public int Id { get; set; } = 0;
        public CRUDSetting DataProduct { get; set; } = new CRUDSetting();
        public CRUDSetting DataContact { get; set; } = new CRUDSetting();
        public CRUDSetting DataUser { get; set; }= new CRUDSetting();
        public CRUDSetting DataPurchase { get; set; } = new CRUDSetting();
        public CRUDSetting DataSales { get; set; } = new CRUDSetting();
        public static async Task<bool> AllowToCreateAccountPayable(Database db)
        {
            var commandText = "SELECT createap FROM usersettings WHERE [user] = @id";
            var result = false;
            using (var reader = await db.ExecuteReaderAsync(commandText, new System.Data.SqlClient.SqlParameter("@id", My.Application.User != null ? My.Application.User.Id : 0)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        result = reader.GetBoolean(0);
                    }
                }
            }
            return true;
        }
    }

    public class CRUDSetting
    {
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
    }
}
