using MySql.Data.MySqlClient;
using ProjectShop.Server.Core.Interfaces.IData;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class MySqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString = string.Empty;

        private MySqlConnectionFactory()
        {
        }

        public MySqlConnectionFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("Invalid Connection String!");
            _connectionString = connectionString;
        }

        public async Task<IDbConnection> CreateConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        //public void ExecuteQuery(string query) { }

    }
}
