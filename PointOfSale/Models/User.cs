using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace PointOfSale.Models
{
    internal class User
    {
        internal int Id { get; set; } = 0;
        internal string Name { get; set; } = "";
        private User(DbDataReader reader)
        {
            Id = reader.GetInt32(0);
            Name = reader.GetString(1);
        }
        internal static User Create(DbDataReader reader)
        {
            return new User(reader);
        }
    }

    public class LoginManager
    {
        private readonly Database db;
        internal LoginManager(Database _db)
        {
            db = _db;
        }
        internal async Task<bool> SignInAsync(string username, string password)
        {
            var commandText = "SELECT [id], [name] FROM [users] WHERE [login] = @username AND [password] = HASHBYTES('SHA2_256', CAST(@password AS VARCHAR))";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@username", username), new SqlParameter("@password", password)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        My.Application.User = User.Create(reader);
                    }
                }
            }
            return My.Application.User != null;
        }
        internal async Task<bool> IsValidPassword(string password)
        {
            if (My.Application.User is null) return false;

            var commandText = "SELECT 1 FROM [users] WHERE [id] = @id AND [password] = HASHBYTES('SHA2_256', CAST(@password AS VARCHAR))";
            return await db.HasRecords(commandText, new SqlParameter("@id", My.Application.User.Id), new SqlParameter("@password", password));
        }
        internal async Task<int> UpdatePasswordAsync(string oldPassword, string newPassword)
        {
            if (My.Application.User is null) return 2;

            var isValidOldPassword = await IsValidPassword(oldPassword);
            if (!isValidOldPassword) return 1;

            var commandText = "UPDATE [users] SET [password] = HASHBYTES('SHA2_256', CAST(@password AS VARCHAR)) WHERE [id]=@id";
            if (await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@password", newPassword), new SqlParameter("@id", My.Application.User.Id)))
            {
                return 0;
            }
            return 3;
        }
    }
}
