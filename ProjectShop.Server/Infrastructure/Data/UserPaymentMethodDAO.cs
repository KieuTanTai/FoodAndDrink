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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "user_payment_method", "user_payment_method_id", string.Empty)
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

        public async Task<IEnumerable<UserPaymentMethodModel>> GetAllByEnumAsync<TEnum>(TEnum tEnum, int? maxGetCount) where TEnum : Enum
            => await GetByInputAsync(tEnum.ToString(), "payment_method_type", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetAllByLastFourDigitAsync(string lastFourDigit, int? maxGetCount)
            => await GetByInputAsync(lastFourDigit, "payment_method_last_four_digit", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByBankIdAsync(uint bankId, int? maxGetCount)
            => await GetByInputAsync(bankId.ToString(), "bank_id", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByBankIdsAsync(IEnumerable<uint> bankIds, int? maxGetCount)
            => await GetByInputsAsync(bankIds.Select(bankId => bankId.ToString()), "bank_id", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByCustomerIdAsync(uint customerId, int? maxGetCount)
            => await GetByInputAsync(customerId.ToString(), "customer_id", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, int? maxGetCount)
            => await GetByInputsAsync(customerIds.Select(customerId => customerId.ToString()), "customer_id", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.DATE_TIME, ct, dateTime, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate), maxGetCount);

        public async Task<UserPaymentMethodModel?> GetByDisplayNameAsync(string displayName)
            => await GetSingleDataAsync(displayName, "payment_method_display_name");

        public async Task<UserPaymentMethodModel?> GetByEnumAsync<TEnum>(TEnum tEnum) where TEnum : Enum
            => await GetSingleDataAsync(tEnum.ToString(), "payment_method_type");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryMonthAsync<TCompareType>(int month, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDecimalAsync(month, ct, "payment_method_expiry_month", maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryYearAndMonthAsync(int year, int month, int? maxGetCount)
        {
            string query = $@"";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxGetCount), "Max get count must be greater than zero.");
            else if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $@"SELECT * FROM {TableName} 
                           WHERE payment_method_expiry_year = @Year AND payment_method_expiry_month = @Month 
                           LIMIT @MaxGetCount";
            else
                query = $@"
                    SELECT * FROM {TableName}
                    WHERE payment_method_expiry_year = @Year AND payment_method_expiry_month = @Month";

            try
            {
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<UserPaymentMethodModel> results = await connection.QueryAsync<UserPaymentMethodModel>(query, new { Year = year, Month = month, MaxGetCount = maxGetCount});
                Logger.LogInfo<IEnumerable<UserPaymentMethodModel>, UserPaymentMethodDAO>($"Retrieved payment methods by expiry year {year} and month {month} successfully.");
                return results;
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<UserPaymentMethodModel>, UserPaymentMethodDAO>($"Error getting payment methods by expiry year {year} and month {month}: {ex.Message}", ex);
                throw new Exception($"Error getting payment methods by expiry year and month: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryYearAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_expiry_year", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<UserPaymentMethodModel?> GetByLastFourDigitAsync(string lastFourDigit)
            => await GetSingleDataAsync(lastFourDigit, "payment_method_last_four_digit");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByRelativeDisplayNameAsync(string displayName, int? maxGetCount)
            => await GetByLikeStringAsync(displayName, "payment_method_display_name", maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "payment_method_status", maxGetCount);

        public async Task<UserPaymentMethodModel?> GetByTokenAsync(string token)
            => await GetSingleDataAsync(token, "TokenColumnName");

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("payment_method_added_date", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByExpiryMonthAndYearAsync(int year, int month, int? maxGetCount = null)
            => await GetByDateTimeAsync("payment_method_expiry_year", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GEtByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount = null)
            => await GetByDateTimeAsync("payment_method_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type", nameof(compareType));
            return await GetByDateTimeAsync("payment_method_last_updated_date", EQueryTimeType.YEAR, type, year, maxGetCount);   
        }

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null)
            => await GetByDateTimeAsync("payment_method_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate), maxGetCount);

        public async Task<IEnumerable<UserPaymentMethodModel>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type", nameof(compareType));
            return await GetByDateTimeAsync("payment_method_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }
    }
}
