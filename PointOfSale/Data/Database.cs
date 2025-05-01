using System.Data.SqlClient;
using PointOfSale.Models;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Windows.Forms;

namespace PointOfSale
{
    public interface IDatabase
    {
        bool ExecuteNonQuery(string commandText, params SqlParameter[] parameters);
        Task<bool> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters);
        DbDataReader ExecuteReader(string commandText, params SqlParameter[] parameters);
        Task<DbDataReader> ExecuteReaderAsync(string commandText, params SqlParameter[] parameters);
        object ExecuteScalar(string commandText, params SqlParameter[] parameters);
        Task<object> ExecuteScalarAsync(string commandText, params SqlParameter[] parameters);
        int ExecuteScalarInteger(string commandText, params SqlParameter[] parameters);
        Task<int> ExecuteScalarIntegerAsync(string commandText, params SqlParameter[] parameters);
        DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters);
        Task<DataTable> ExecuteDataTableAsync(string commandText, params SqlParameter[] parameters);
    }
    public class Database : IDatabase
    {
        private static string _connectionString = string.Empty;
        internal static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
        private (SqlConnection conn, SqlCommand cmd) CreateConnection(string commandText, params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(_connectionString);
            var cmd = new SqlCommand(commandText, conn);
            if (parameters != null) cmd.Parameters.AddRange(parameters);
            return (conn, cmd);
        }
        internal static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        #region SYNCRONOUS METHODS
        public bool ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    cmd.Dispose();
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    conn.Dispose();
                }
            }
        }
        public DbDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            try
            {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                cmd.Dispose();
                conn.Dispose();
                return null;
            }
        }
        public DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters)
        {
            var table = new DataTable();
            using (var reader = ExecuteReader(commandText, parameters))
            {
                if (reader != null)
                {
                    table.Load(reader);
                    reader.Close();
                    reader.Dispose();
                }
            }
            return table;
        }
        public object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    cmd.Dispose();
                    if (conn.State != ConnectionState.Closed) conn.Close();
                    conn.Dispose();
                }
            }
        }
        public int ExecuteScalarInteger(string commandText, params SqlParameter[] parameters)
        {
            var obj = ExecuteScalar(commandText, parameters);
            if (obj != null && obj is int)
            {
                return Convert.ToInt32(obj);
            }
            return -1;
        }
        #endregion SYNCRONOUS METHODS
        #region ASYNCRONOUS METHODS
        public async Task<string> GenerateTransactionIdAsync(string transactionType)
        {
            var id = DateTime.Today.ToString("yyMMdd");
            var commandText = $@"IF NOT EXISTS (SELECT 1 FROM autonumbers WITH (NOLOCK) WHERE [id] = {id})
BEGIN
    INSERT INTO autonumbers ([id]) VALUES ({id})
END
ELSE
BEGIN
    UPDATE autonumbers SET [{transactionType}] = [{transactionType}] + 1 WHERE ([id] = {id})
END
SELECT [{transactionType}] FROM autonumbers WITH (NOLOCK) WHERE [id] = {id};";
            var sequence = await ExecuteScalarIntegerAsync(commandText);
            return string.Concat(transactionType, "-", id, "-", sequence.ToString("0000"));
        }
        public async Task<bool> ExecuteNonQueryAsync(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                    conn.Dispose();
                }
            }                
        }
        public async Task<DbDataReader> ExecuteReaderAsync(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            try
            {
                conn.Open();
                return await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            }
            catch
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
                cmd.Dispose();
                conn.Dispose();
                return null;
            }
        }
        public async Task<object> ExecuteScalarAsync(string commandText, params SqlParameter[] parameters)
        {
            var (conn, cmd) = CreateConnection(commandText, parameters);
            using (conn) using (cmd)
            {
                try
                {
                    await conn.OpenAsync();
                    return await cmd.ExecuteScalarAsync();
                }
                catch
                {
                    return null;
                }
                finally
                {
                    cmd.Dispose();
                    if (conn.State != ConnectionState.Closed) conn.Dispose();
                    conn.Dispose();
                }
            }             
        }
        public async Task<int> ExecuteScalarIntegerAsync(string commandText, params SqlParameter[] parameters)
        {
            var result = await ExecuteScalarAsync(commandText, parameters);
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            else
            {
                return -1;
            }
        }
        public async Task<DataTable> ExecuteDataTableAsync(string commandText, params SqlParameter[] parameters)
        {
            var table = new DataTable();
            using (var reader = await ExecuteReaderAsync(commandText, parameters))
            {
                if (reader != null)
                {
                    table.Load(reader);
                    reader.Close();
                    reader.Dispose();
                }
            }
            return table;
        }
        public async Task<bool> CreatePaymentAsync(TransactionType type, int transactionID, decimal paidAmount)
        {
            decimal debt = type == TransactionType.Purchase || type == TransactionType.AccountPayable ? paidAmount : 0;
            decimal credit = type == TransactionType.Sales || type == TransactionType.AccountReceivable ? paidAmount : 0;
            var commandText = @"INSERT INTO cashflows ([transactionId], [type], [debt], [credit], [author], datecreated)
VALUES (@transactionId, @type, @debt, @credit, @author, @datecreated);";
            if (type == TransactionType.Purchase)
            {
                commandText += Environment.NewLine + @"DECLARE @totalPaid DEC(10,2) = (SELECT SUM(debt) AS tp FROM cashflows WHERE transactionId = @transactionId AND [type] = 1);
UPDATE purchases SET tp = @totalPaid, ap = ambp - @totalPaid
WHERE id = @transactionId";
            }
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@transactionId", transactionID),
                new SqlParameter("@type", (int)type),
                new SqlParameter("@debt", debt),
                new SqlParameter("@credit", credit),
                new SqlParameter("@author", My.Application.User != null ? My.Application.User.Id : 0),
                new SqlParameter("@datecreated", paidAmount)
            };
            return await ExecuteNonQueryAsync(commandText, parameters);
        }
        #endregion ASYNCRONOUS METHODS


        
        internal async Task<bool> HasRecords(string commandText, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    try
                    {
                        await conn.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            return reader.HasRows;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return false;
                    }
                    finally
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        #region STATIC METHODS
        internal static async Task CreateDatabaseIfNotExists()
        {
            string constring = "server=.\\SQLEXPRESS;Database=master;Integrated Security=True;TrustServerCertificate=true;";
            bool databaseExists = false;
            using (SqlConnection conn = new SqlConnection(constring))
            {
                await conn.OpenAsync();

                string sql = "SELECT [name] FROM sys.databases WHERE [name] = 'PointOfSale';";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            databaseExists = true;
                        }
                    }
                }
                if (!databaseExists)
                {
                    var newConn = await CreateDatabase(conn);
                    if (newConn != null)
                    {
                        SqlCommand newCommand = new SqlCommand(global::PointOfSale.Properties.Resources.DDL, newConn);
                        try
                        {
                            await newConn.OpenAsync();
                            await newCommand.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        newCommand.Dispose();
                        newConn.Close();
                        newConn.Dispose();
                    }
                }
                Database.SetConnectionString("Server=.\\SQLEXPRESS;database=PointOfSale;Integrated Security=True;TrustServerCertificate=True;");
            }
        }
        private static async Task<SqlConnection> CreateDatabase(SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand("CREATE DATABASE PointOfSale;", conn))
            {
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    return new SqlConnection("Server=.\\SQLEXPRESS;database=PointOfSale;Integrated Security=True;TrustServerCertificate=True;");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }
        }
        #endregion
    }

    public static class SqlHelpers
    {
        public static string FromString(string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }
        public static string FromDate(DateTime date)
        {
            return "'" + date.ToString("yyyy-MM-dd") + "'";
        }
        public static string FromDateTime(DateTime date)
        {
            return "'" + date.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        }
        public static string FromBoolean(bool value)
        {
            return value ? "1" : "0";
        }
    }
}
