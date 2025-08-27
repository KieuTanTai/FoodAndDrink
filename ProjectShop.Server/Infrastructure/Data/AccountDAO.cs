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
    public class AccountDAO : BaseDAO<AccountModel>, IAccountDAO<AccountModel>
    {
        public AccountDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "account", "account_id", string.Empty)
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

        public async Task<AccountModel?> GetByUserNameAndPasswordAsync(string userName, string password)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                             WHERE user_name = @UserName AND password = @Password";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                AccountModel? result = await connection.QueryFirstOrDefaultAsync<AccountModel>(query, new { UserName = userName, Password = password });
                Logger.LogInfo<AccountModel, AccountDAO>($"Retrieved account with username {userName} and provided password successfully.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError<AccountModel, AccountDAO>($"Error retrieving account with username {userName} and provided password.", ex);
                throw new InvalidOperationException($"An error occurred while retrieving account with username {userName}.", ex);
            }
        }

        public async Task<AccountModel?> GetByUserNameAsync(string userName)
            => await GetSingleDataAsync(userName, "user_name");

        public async Task<IEnumerable<AccountModel>> GetByUserNameAsync(IEnumerable<string> userNames, int? maxGetCount)
            => await GetByInputsAsync(userNames, "user_name", maxGetCount);

        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "account_status", maxGetCount);

        // ---- Public API cho Created Date ----
        public async Task<IEnumerable<AccountModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("account_created_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<AccountModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_created_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<AccountModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("account_created_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<AccountModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_created_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }

        // ---- Public API cho Last Updated Date ----
        public async Task<IEnumerable<AccountModel>> GetByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<AccountModel>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }
    }
}
