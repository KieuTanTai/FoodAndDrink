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
    public class RoleOfUserDAO : BaseDAO<RolesOfUserModel>, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey>
    {
        public RoleOfUserDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "roles_of_user", "id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (account_id, role_id, added_date) 
                      VALUES (@AccountId, @RoleId, @AddedDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET role_id = @RoleId
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetByKeysQuery()
        {
            return $@"SELECT * FROM {TableName} 
                      WHERE account_id = @AccountId AND role_id = @RoleId";
        }

        private string GetDeleteByKeysQuery()
        {
            return $@"DELETE FROM {TableName} 
                      WHERE account_id = @AccountId AND role_id = @RoleId";
        }

        private string GetDeleteByIdQuery(string colName)
        {
            return $@"DELETE FROM {TableName} WHERE {colName} = @Input";
        }


        public async Task<RolesOfUserModel> GetByKeysAsync(RolesOfUserKey keys) => await GetSingleByTwoIdAsync("account_id", "role_id", keys.AccountId, keys.RoleId);

        public async Task<IEnumerable<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys)
        {
            try
            {
                string query = GetByKeysQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, listKeys);
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModels by list of keys: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByKeysAsync(RolesOfUserKey keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, keys);
                if (rowsAffected == 0)
                    throw new KeyNotFoundException($"No RolesOfUserModel found for AccountId: {keys.AccountId} and RoleId: {keys.RoleId}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting RolesOfUserModel by keys: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdAsync(uint accountId)
    => await GetByInputAsync(accountId.ToString(), "account_id");

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdAsync(uint roleId)
            => await GetByInputAsync(roleId.ToString(), "role_id");

        public async Task<IEnumerable<RolesOfUserModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<RolesOfUserModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("added_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<RolesOfUserModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("added_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // Các method delete giữ nguyên cho bạn tự sửa
        public async Task<int> DeleteByAccountIdAsync(uint accountId) => await DeleteByColumnNameAsync(accountId.ToString(), "account_id");
        public async Task<int> DeleteByRoleIdAsync(uint roleId) => await DeleteByColumnNameAsync(roleId.ToString(), "role_id");

        // DRY:
        private async Task<int> DeleteByColumnNameAsync(string key, string colName)
        {
            try
            {
                string query = GetDeleteByIdQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, new { Input = key });
                if (rowsAffected == 0)
                    throw new KeyNotFoundException($"No RolesOfUserModel found with {colName}: {key}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting RolesOfUserModel by single key: {ex.Message}", ex);
            }
        }
    }
}
