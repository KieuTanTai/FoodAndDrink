using MySql.Data.MySqlClient;
using System;
using System.Data;
using ProjectShop.Server.Core.Interfaces.IData;

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

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        //public void ExecuteQuery(string query) { }

    }
}
