using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class TransactionDAO : BaseNoneUpdateDAO<TransactionModel>, IGetAllByIdAsync<TransactionModel>, IGetDataByDateTimeAsync<TransactionModel>, IGetByStatusAsync<TransactionModel>
    {
        public TransactionDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "transaction", "transaction_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (point_wallet_id, invoice_id, transaction_date, transaction_type, transaction_current_balance, transaction_status) 
                      VALUES (@PointWalletId, @InvoiceId, @TransactionDate, @TransactionType, @TransactionCurrentBalance, @TransactionStatus); SELECT LAST_INSERT_ID();";
        }

        private string GetByStatusQuery()
        {
            return $@"SELECT * FROM {TableName} 
                      WHERE transaction_status = @TransactionStatus";
        }

        private string GetByDateTime(string colName)
        {
            CheckColumnName(colName);
            return $@"SELECT * FROM {TableName} 
                      WHERE {colName} = DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        private string GetByDateTimeRange(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Year AND MONTH({colName}) = @Month";
        }

        public async Task<List<TransactionModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetByStatusQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { TransactionStatus = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<TransactionModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "transaction_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { Year = year, Month = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<TransactionModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "transaction_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by date time range: {ex.Message}", ex);
            }
        }

        public async Task<List<TransactionModel>> GetAllByDateTimeAsync(DateTime input, string colName = "transaction_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { Input = input });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by date time: {ex.Message}", ex);
            }
        }

        public async Task<List<TransactionModel>> GetAllByYearAsync(int year, string colName = "transaction_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<TransactionModel>> GetAllByIdAsync(string input, string colName = "transaction_date")
        {
            try
            {
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<TransactionModel> result = await connection.QueryAsync<TransactionModel>(query, new { Input = $"%{input}%" });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TransactionModels by relative input: {ex.Message}", ex);
            }
        }
    }
}
