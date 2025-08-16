using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class UserPaymentMethodDAO : BaseDAO<UserPaymentMethodModel>, IUserPaymentMethodDAO<UserPaymentMethodModel>
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

        public async Task<IEnumerable<UserPaymentMethodModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
            => await GetByInputAsync(tEnum.ToString(), "payment_method_type");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetAllByLastFourDigitAsync(string lastFourDigit)
            => await GetByInputAsync(lastFourDigit, "payment_method_last_four_digit");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByBankIdAsync(uint bankId)
            => await GetByInputAsync(bankId.ToString(), "bank_id");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByBankIdsAsync(IEnumerable<uint> bankIds)
            => await GetByInputsAsync(bankIds.Select(bankId => bankId.ToString()), "bank_id");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByCustomerIdAsync(uint customerId)
            => await GetByInputAsync(customerId.ToString(), "customer_id");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds)
            => await GetByInputsAsync(customerIds.Select(customerId => customerId.ToString()), "customer_id");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<UserPaymentMethodModel?> GetByDisplayNameAsync(string displayName)
            => await GetSingleDataAsync(displayName, "payment_method_display_name");

        public async Task<UserPaymentMethodModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
            => await GetSingleDataAsync(tEnum.ToString(), "payment_method_type");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryMonthAsync<TCompareType>(int month, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDecimalAsync(month, ct, "payment_method_expiry_month");
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryYearAndMonthAsync(int year, int month)
        {
            try
            {
                string query = $@"
                    SELECT * FROM {TableName}
                    WHERE payment_method_expiry_year = @Year AND payment_method_expiry_month = @Month;";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> results = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Year = year, Month = month });
                if (results == null || !results.Any())
                    return Enumerable.Empty<UserPaymentMethodModel>();
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting payment methods by expiry year and month: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_expiry_year", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<UserPaymentMethodModel?> GetByLastFourDigitAsync(string lastFourDigit)
            => await GetSingleDataAsync(lastFourDigit, "payment_method_last_four_digit");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByRelativeDisplayNameAsync(string displayName)
            => await GetByLikeStringAsync(displayName, "payment_method_display_name");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByStatusAsync(bool status)
            => await GetByInputAsync(GetTinyIntString(status), "payment_method_status");

        public async Task<UserPaymentMethodModel?> GetByTokenAsync(string token)
            => await GetSingleDataAsync(token, "TokenColumnName");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }
    }
}
