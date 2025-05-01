using System.Data.SqlClient;
using PointOfSale.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale.Data
{
    public class ContactRepository : IRepository
    {
        private readonly Database db;
        public ContactRepository(Database _db) {  db = _db; }
        public async Task<object> CreateAsync(object model)
        {
            var contact = (Contact)model;
            var commandText = @"INSERT INTO contacts (name, type, title, organization, author, datecreated)
VALUES (@name, @type, @title, @organization, @author, GETDATE());
SELECT SCOPE_IDENTITY() AS newID;";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@name", contact.Name),
                new SqlParameter("@type", contact.Type),
                new SqlParameter("@title", contact.Title),
                new SqlParameter("@organization", contact.Organization),
                new SqlParameter("@author", My.Application.User != null ? My.Application.User.Id : 0)
            };
            contact.Id = await db.ExecuteScalarIntegerAsync(commandText, parameters);
            if (contact.Id > 0)
            {
                var values = new List<string>();
                var parameterList = new List<SqlParameter>();

                for (int i = 0; i < contact.Addresses.Count; i++)
                {
                    commandText = @"INSERT INTO addresses (contact, streetline, village, district, city, province, isPrimary)
VALUES (@contact, @streetline, @village, @district, @city, @province, @isPrimary);";
                    var param2 = new SqlParameter[]
                    {
                        new SqlParameter("@contact", contact.Id),
                        new SqlParameter("@streetline", contact.Addresses[i].Streetline),
                        new SqlParameter("@village", contact.Addresses[i].Village),
                        new SqlParameter("@district", contact.Addresses[i].District),
                        new SqlParameter("@city", contact.Addresses[i].City),
                        new SqlParameter("@province", contact.Addresses[i].Province),
                        new SqlParameter("@isPrimary", contact.Addresses[i].IsPrimary)
                    };
                    await db.ExecuteNonQueryAsync(commandText, param2);
                }
            }
            return contact;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var commandText = @"UPDATE contacts SET deleted = 1 WHERE id = @id;";
            return await db.ExecuteNonQueryAsync(commandText, new SqlParameter("@id", id));
        }

        public async Task<object> GetByIdAsync(int id)
        {
            var contact = new Contact();
            var commandText = @"SELECT [id], [name], [type], [title], [organization] FROM contacts WIT (NOLOCK) WHERE [id] = @id;";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@id", id)))
            {
                if (reader != null)
                {
                    if (await reader.ReadAsync())
                    {
                        contact.Id = reader.GetInt32(0);
                        contact.Name = reader.GetString(1);
                        contact.Type = reader.GetInt32(2);
                        contact.Title = reader.GetString(3);
                        contact.Organization = reader.GetString(4);
                    }
                    reader.Close();
                    reader.Dispose();
                }
            }
            commandText = @"SELECT a.id, a.streetline, a.village, a.district, a.city, a.province, a.isPrimary, CONCAT(c.[name], ' - ', p.[name]) AS [description]
FROM addresses AS a WITH (NOLOCK)
INNER JOIN cities AS c ON a.city = c.id
INNER JOIN provinces AS p ON a.province = p.id
WHERE a.contact = @contact";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@contact", id)))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        var address = new Address()
                        {
                            Id = reader.GetInt32(0),
                            Streetline = reader.GetString(1),
                            Village = reader.GetInt64(2),
                            District = reader.GetInt32(3),
                            City = reader.GetInt32(4),
                            Province = reader.GetInt32(5),
                            Description = reader.GetString(7),
                            IsPrimary = reader.GetBoolean(6)
                        };
                        contact.Addresses.Add(address);
                    }
                }
            }

            commandText = "SELECT number, [type], isPrimary FROM phones WHERE contact = @contact;";
            using (var reader = await db.ExecuteReaderAsync(commandText, new SqlParameter("@contact", id)))
            {
                if (reader != null)
                {
                    while (await reader.ReadAsync())
                    {
                        contact.Phones.Add(new Phone()
                        {
                            Number = reader.GetString(0),
                            Type = reader.GetInt32(1)
                        });
                    }
                }
            }


            return contact;
        }

        public DataTableColumnInfo[] GetColumns()
        {
            return new DataTableColumnInfo[]
            {
                new DataTableColumnInfo("Kode", "id", 70, DataGridViewContentAlignment.MiddleCenter, "0000000"),
                new DataTableColumnInfo("Nama", "name", 200),
                new DataTableColumnInfo("Tipe", "type", 80),
                new DataTableColumnInfo("Alamat", "address", 500),
                new DataTableColumnInfo("Telepon", "phone", 120, DataGridViewContentAlignment.MiddleCenter),
                //new DataTableColumnInfo("Tanggal dibuat", "datecreated", 120, DataGridViewContentAlignment.MiddleRight, "dd/MM/yy HH:mm"),
        //      new DataTableColumnInfo("Dibuat oleh", "author", 150),
                new DataTableColumnInfo("Terakhir diubah", "datemodified", 120, DataGridViewContentAlignment.MiddleRight, "dd/MM/yy HH:mm"),
                new DataTableColumnInfo("Diubah oleh", "modifier", 150)
            };
        }

        public async Task<DataTable> GetDataTableAsync(int id)
        {
            var commandText = @"SELECT c.id, CASE WHEN c.title <> '' THEN CONCAT(c.[name], ', ', c.[title]) ELSE c.[name] END AS [name], CASE(c.[type]) WHEN 1 THEN 'Pelanggan' WHEN 2 THEN 'Supplier' ELSE '-' END AS [type], 
contactAddress.fullAddress AS [address], phone.number AS phone, c.datecreated, author.[name] AS author, CASE WHEN c.datemodified = '1900-01-01' THEN NULL ELSE c.datemodified END AS datemodified, modifier.[name] AS modifier
FROM contacts AS c WITH (NOLOCK)
LEFT JOIN (
	SELECT a.contact, CONCAT(a.streetline, ' ', v.[name], ', ', d.[name], ', ', c.[name], ', ', v.zipcode) AS fullAddress
	FROM addresses AS a WITH (NOLOCK)
	INNER JOIN villages AS v WITH (NOLOCK) ON a.village = v.id
	INNER JOIN districts AS d WITH (NOLOCK) ON a.district = d.id
	INNER JOIN cities AS c WITH (NOLOCK) ON a.city = c.id
	INNER JOIN provinces AS p ON a.province = p.id
	WHERE a.isPrimary = 1
) AS contactAddress ON c.id = contactAddress.contact
LEFT JOIN phones AS phone ON c.id = phone.contact AND phone.isPrimary = 1
LEFT JOIN phonetypes AS pt ON phone.[type] = pt.id
INNER JOIN [users] AS author ON c.author = author.id
LEFT JOIN [users] AS modifier ON c.modifier = modifier.id
WHERE c.deleted = 0";
            return await db.ExecuteDataTableAsync(commandText);
        }

        public async Task<object> UpdateAsync(object model)
        {
            var contact = (Contact)model;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"UPDATE contacts
SET [name] = @name,
[type] = @type,
organization = @organization,
title = @title,
modifier = @modifier,
datemodified = GETDATE()
WHERE [id] = @id;
DELETE FROM addresses WHERE contact = @id; ");
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", contact.Name),
                new SqlParameter("@type", contact.Type),
                new SqlParameter("@title", contact.Title),
                new SqlParameter("@organization", contact.Organization),
                new SqlParameter("@id", contact.Id),
                new SqlParameter("@modifier", My.Application.User != null ? My.Application.User.Id : 0)
            };
            if (contact.Addresses.Count > 0)
            {
                sb.Append(@"INSERT INTO addresses (contact, streetline, village, district, city, province, isPrimary) VALUES ");
                List<string> rows = new List<string>();
                foreach (var address in contact.Addresses)
                {
                    string[] values = new string[7];
                    values[0] = "@id";
                    values[1] = SqlHelpers.FromString(address.Streetline);
                    values[2] = address.Village.ToString();
                    values[3] = address.District.ToString();
                    values[4] = address.City.ToString();
                    values[5] = address.Province.ToString();
                    values[6] = SqlHelpers.FromBoolean(address.IsPrimary);
                    rows.Add("(" + string.Join(", ", values) + ")");
                }
                sb.Append(string.Join(", ", rows)).Append("; ");
            }
            sb.Append("DELETE FROM phones WHERE contact = @id; ");
            if (contact.Phones.Count > 0)
            {
                sb.Append("INSERT INTO phones (contact, number, type, isPrimary) VALUES ");
                List<string> rows = new List<string>();
                foreach (var phone in contact.Phones)
                {
                    string[] values = new string[4];
                    values[0] = "@id";
                    values[1] = SqlHelpers.FromString(phone.Number);
                    values[2] = phone.Type.ToString();
                    values[3] = SqlHelpers.FromBoolean(phone.IsPrimary);
                    rows.Add("(" + string.Join(", ", values) + ")");
                }
                sb.Append(string.Join(", ", rows)).Append("; ");
            }
            await db.ExecuteNonQueryAsync(sb.ToString(), parameters);
            return contact;
        }
    }
}
