using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryObjectDesignPatternExample
{
    public class DALContext
    {
        readonly static SqlConnection _connection;
        readonly static object _object = new();
        static DALContext()
        {
            lock (_object)
                _connection = new("Server=localhost, 1433;Database=QueryObjectDesignPatternDB;User ID=SA;Password=***");
        }
        async Task<SqlConnection> GetOpenConnectionAsync()
        {
            if (_connection.State == ConnectionState.Closed)
                await _connection.OpenAsync();

            return _connection;
        }
        public async Task CloseConnectionAsync()
        {
            if (_connection.State == ConnectionState.Open)
                await _connection.CloseAsync();
        }
        public async Task<SqlDataReader> ExecuteQueryAsync(string query)
        {
            using SqlCommand command = new(query, await GetOpenConnectionAsync());
            SqlDataReader dr = await command.ExecuteReaderAsync();
            return dr;
        }
    }
}
