using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class UserPaymentMethodDAO : BaseDAO<UserPaymentMethodModel>, IGetAllByIdAsync<UserPaymentMethodModel>, IGetByStatusAsync<UserPaymentMethodModel>, IGetDataByDateTimeAsync<UserPaymentMethodModel>, IGetByEnumAsync<UserPaymentMethodModel>
    {
        public UserPaymentMethodDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "user_payment_method", "user_payment_method_id", string.Empty)
        {
        }
        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (payment_method_type, bank_id, customer_id, payment_method_added_date, payment_method_last_updated_date, payment_method_status, 
                                                payment_method_display_name, payment_method_last_four_digit, payment_method_expiry_year, payment_method_expiry_month, payment_method_token) 
                      VALUES (@PaymentMethodType, @BankId, @CustomerId, @PaymentMethodAddedDate, @PaymentMethodLastUpdatedDate, @PaymentMethodStatus,
                              @PaymentMethodDisplayName, @PaymentMethodLastFourDigit, @PaymentMethodExpiryYear, @PaymentMethodExpiryMonth, @PaymentMethodToken); 
                      SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET payment_method_status = @PaymentMethodStatus
                      WHERE {ColumnIdName} = @{colIdName};";
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

        public async Task<List<UserPaymentMethodModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("payment_method_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Input = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by status: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "payment_method_added_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Year = year, Month = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by month and year: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByYearAsync(int year, string colName = "payment_method_added_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by year: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "payment_method_added_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by date time: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByDateTimeRangeAsync(DateTime firstTime, DateTime secondTime, string colName = "payment_method_added_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { FirstTime = firstTime, SecondTime = secondTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by date time range: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByEnumAsync<TEnum>(TEnum enumValue, string colName = "payment_method_type") where TEnum : Enum
        {
            try
            {
                if (enumValue is not EPaymentMethodType)
                    throw new ArgumentException($"Enum type {typeof(TEnum).Name} is not a valid payment method type.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Input = enumValue });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by enum: {ex.Message}", ex);
            }
        }

        public async Task<UserPaymentMethodModel> GetByEnumAsync<TEnum>(TEnum enumValue, string colName = "payment_method_type") where TEnum : Enum
        {
            try
            {
                if (enumValue is not EPaymentMethodType)
                    throw new ArgumentException($"Enum type {typeof(TEnum).Name} is not a valid payment method type.");
                string query = GetDataQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                UserPaymentMethodModel? result = await connection.QueryFirstOrDefaultAsync<UserPaymentMethodModel>(query, new { Input = enumValue });
                if (result == null)
                    throw new KeyNotFoundException($"No User Payment Method found for enum value: {enumValue}");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Method by enum: {ex.Message}", ex);
            }
        }

        public async Task<List<UserPaymentMethodModel>> GetAllByIdAsync(string id, string colIdName = "customer_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> result = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving User Payment Methods by ID: {ex.Message}", ex);
            }
        }
    }
}
