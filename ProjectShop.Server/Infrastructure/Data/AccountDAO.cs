using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class AccountDAO : BaseDAO<AccountModel>, IGetDataByDateTimeAsync<AccountModel>, IGetByStatusAsync<AccountModel>
    {
        public AccountDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "account", "account_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (user_name, password, account_created_date, account_last_updated_date, account_status) 
                      VALUES (@UserName, @Password, @AccountCreatedDate, @AccountLastUpdatedDate, @AccountStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET password = @Password, account_status = @AccountStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<List<AccountModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "account_created_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<AccountModel> accounts = await connection.QueryAsync<AccountModel>(query, new { FirstTime = year, SecondTime = month });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving accounts by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<AccountModel>> GetAllByYearAsync(int year, string colName = "account_created_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<AccountModel> accounts = await connection.QueryAsync<AccountModel>(query, new { Input = year });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving accounts by year: {ex.Message}", ex);
            }
        }

        public async Task<List<AccountModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "account_created_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<AccountModel> accounts = await connection.QueryAsync<AccountModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving accounts by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<AccountModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "account_created_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<AccountModel> accounts = await connection.QueryAsync<AccountModel>(query, new { Input = dateTime });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving accounts by date: {ex.Message}", ex);
            }
        }

        public async Task<List<AccountModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("account_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<AccountModel> accounts = await connection.QueryAsync<AccountModel>(query, new { Input = status });
                return accounts.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving accounts by status: {ex.Message}", ex);
            }
        }
    }
}
