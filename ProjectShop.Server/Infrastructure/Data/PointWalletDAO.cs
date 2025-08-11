using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class PointWalletDAO : BaseDAO<PointWalletModel>, IGetDataByDateTimeAsync<PointWalletModel>,
        IGetByRangePriceAsync<PointWalletModel>

    {
        public PointWalletDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "point_wallet", "point_wallet_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (customer_id, balance_point, last_updated_balance_date) 
                      VALUES (@CustomerId, @BalancePoint, @LastUpdatedBalanceDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET balance_point = @BalancePoint, 
                          last_updated_balance_date = @LastUpdatedBalanceDate
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }
        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE Year({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        private string GetByRangeDateTime(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        public async Task<List<PointWalletModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "last_updated_balance_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<PointWalletModel> pointWallets = await connection.QueryAsync<PointWalletModel>(query, new { Input = dateTime });
                return pointWallets.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving point wallets by date time: {ex.Message}", ex);
            }
        }

        public async Task<List<PointWalletModel>> GetAllByYearAsync(int year, string colName = "last_updated_balance_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<PointWalletModel> pointWallets = await connection.QueryAsync<PointWalletModel>(query, new { Input = year });
                return pointWallets.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving point wallets by year: {ex.Message}", ex);
            }
        }

        public async Task<List<PointWalletModel>> GetAllByMonthAndYearAsync(int month, int year, string colName = "last_updated_balance_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<PointWalletModel> pointWallets = await connection.QueryAsync<PointWalletModel>(query, new { FirstTime = year, SecondTime = month });
                return pointWallets.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving point wallets by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<PointWalletModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "last_updated_balance_date")
        {
            try
            {
                string query = GetByRangeDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<PointWalletModel> pointWallets = await connection.QueryAsync<PointWalletModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return pointWallets.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving point wallets by date range: {ex.Message}", ex);
            }
        }

        public async Task<List<PointWalletModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, string colName = "balance_point")
        {
            try
            {
                string query = $"SELECT * FROM {TableName} WHERE {colName} BETWEEN @MinPrice AND @MaxPrice";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<PointWalletModel> pointWallets = await connection.QueryAsync<PointWalletModel>(query, new { MinPrice = minPrice, MaxPrice = maxPrice });
                return pointWallets.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving point wallets by range price: {ex.Message}", ex);
            }
        }
    }
}
