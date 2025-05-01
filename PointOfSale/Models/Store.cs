using System.Data.SqlClient;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.Common;

namespace PointOfSale.Models
{
    public class Store
    {
        private Store(DbDataReader reader)
        {
            Name = reader.GetString(0);
            Phone = reader.GetString(1);
            Email = reader.GetString(2);

            Address = new Address();
            Address.Streetline = reader.GetString(3);
            Address.Village = reader.GetInt64(4);
            Address.District = reader.GetInt32(5);
            Address.City = reader.GetInt32(6);
            Address.Province = reader.GetInt32(7);

            FooterText = reader.GetString(8);
            if (!reader.IsDBNull(9))
            {
                Logo = Image.FromStream(reader.GetStream(9));
            }
        }
        public string Name { get; } = "";
        public string Phone { get; } = "";
        public string Email { get; } = "";
        public Address Address { get; }
        public string FooterText { get; } = "";
        public Image Logo { get; }
        public static async Task<Store> SetDefault(Database db, string name, string phone, string email, string streetline, Int64 village, string footerText = "", Image logo = null)
        {
            var store = await Store.GetDefault(db);
            if (store != null) return store;

            var commandText = @"INSERT INTO dbo.stores ([name], [phone], [email], [address], [village], footer, logo)
VALUES (@name, @phone, @email, @address, @village, @footerText, @logo)";
            object image = DBNull.Value;
            if (logo != null)
            {
                image = FormatHelpers.ImageToByteArray(logo);
            }
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone),
                new SqlParameter("@email", email),
                new SqlParameter("@address", streetline),
                new SqlParameter("@village", village),
                new SqlParameter("@footerText", footerText),
                new SqlParameter("@logo", System.Data.SqlDbType.Image)
            };
            parameters[6].Value = image;
            await db.ExecuteNonQueryAsync(commandText, parameters);
            return await GetDefault(db);
        }
        public static async Task<bool> UpdateDefault(Database db, string name, string phone, string email, string streetline, Int64 village, string footerText = "", Image logo = null)
        {
            var commandText = @"UPDATE dbo.stores
SET [name] = @name, [phone] = @phone, [email] = @email, [address] = @address, [village] = @village, footer = @footerText, logo = @logo
WHERE id = 1";
            object bytes = DBNull.Value;
            if (logo != null)
            {
                bytes = FormatHelpers.ImageToByteArray(logo);
            }
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@phone", phone),
                new SqlParameter("@email", email),
                new SqlParameter("@address", streetline),
                new SqlParameter("@village", village),
                new SqlParameter("@footerText", footerText),
                new SqlParameter("@logo", System.Data.SqlDbType.Image)
            };
            parameters[6].Value = bytes;
            return await db.ExecuteNonQueryAsync(commandText, parameters);
        }
        public static async Task<Store> GetDefault(Database db)
        {
            var commandText = @"SELECT s.[name], s.[phone], s.[email], s.[address], s.village, v.district, d.city, c.province, s.footer, s.logo
FROM stores AS s
INNER JOIN villages AS v ON s.village = v.id
INNER JOIN districts AS d ON v.district = d.id
INNER JOIN cities AS c ON d.city = c.id
INNER JOIN provinces AS p ON c.province = p.id
WHERE s.id = 1";
            using (var reader = await db.ExecuteReaderAsync(commandText))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        var store = new Store(reader);
                        reader.Close();
                        reader.Dispose();
                        return store;
                    }
                    else
                    {
                        reader.Close();
                        reader.Dispose();
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

    }
    public class Province
    {
        public string Name { get; set; } = "";
        public int Id { get; set; } = 0;
        public override string ToString()
        {
            return Name;
        }
    }
    public class City
    {
        public string Name { get; set; } = "";
        public int Id { get; set; } = 0;
        public override string ToString()
        {
            return Name;
        }
    }
    public class District
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }
    public class Village
    {
        public Int64 Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public override string ToString()
        {
            return Name;
        }
    }
}
