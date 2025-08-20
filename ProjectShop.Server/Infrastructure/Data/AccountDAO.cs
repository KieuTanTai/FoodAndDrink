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

        public async Task<AccountModel?> GetByUserNameAndPasswordAsync(string userName, string password)
        {
            try
            {
                string query = $@"SELECT * FROM {TableName} 
                             WHERE user_name = @UserName AND password = @Password";
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                AccountModel? result = await connection.QueryFirstOrDefaultAsync<AccountModel>(query, new { UserName = userName, Password = password });
                if (result == null)
                    throw new InvalidOperationException($"Account with username {userName} and provided password not found.");
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving account with username {userName}.", ex);
            }
        }

        public async Task<AccountModel?> GetByUserNameAsync(string userName) => await GetSingleDataAsync(userName, "user_name");

        public async Task<IEnumerable<AccountModel>> GetByUserNameAsync(IEnumerable<string> userNames) => await GetByInputsAsync(userNames, "user_name");
        
        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "account_status");
        
        public async Task<IEnumerable<AccountModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "account_status", maxGetCount);

        // ---- Public API cho Created Date ----
        public async Task<IEnumerable<AccountModel>> GetByCreatedDateAsync(int year, int month)
            => await GetByDateTimeAsync("account_created_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_created_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("account_created_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<AccountModel>> GetByCreatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_created_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }

        // ---- Public API cho Last Updated Date ----
        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateAsync(int year, int month)
            => await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<AccountModel>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("account_last_updated_date", EQueryTimeType.DATE_TIME, type, dateTime);
        }
    }
}
