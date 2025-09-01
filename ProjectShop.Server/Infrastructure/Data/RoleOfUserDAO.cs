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
    public class RoleOfUserDAO : BaseNoneUpdateDAO<RolesOfUserModel>, IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey>
    {
        public RoleOfUserDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "roles_of_user", "id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (account_id, role_id, added_date) 
                      VALUES (@AccountId, @RoleId, @AddedDate); SELECT LAST_INSERT_ID();";
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

        public async Task<RolesOfUserModel?> GetByKeysAsync(RolesOfUserKey keys)
            => await GetSingleByTwoIdAsync("account_id", "role_id", keys.AccountId, keys.RoleId);

        public async Task<IEnumerable<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys, int? maxGetCount)
        {
            string query = "";
            if (maxGetCount.HasValue && maxGetCount.Value <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxGetCount), "maxGetCount must be greater than 0");
            if (maxGetCount.HasValue && maxGetCount.Value > 0)
                query = $@"{GetByKeysQuery()} LIMIT @MaxGetCount";
            else
                query = GetByKeysQuery();

            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.AddDynamicParams(listKeys);
                parameters.Add("MaxGetCount", maxGetCount);

                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, parameters);
                Logger.LogInfo<IEnumerable<RolesOfUserModel>, RoleOfUserDAO>($"Retrieved RolesOfUserModels by list of keys successfully.");
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError<IEnumerable<RolesOfUserModel>, RoleOfUserDAO>($"Error retrieving RolesOfUserModels by list of keys: {ex.Message}", ex);
                throw new Exception($"Error retrieving RolesOfUserModels by list of keys: {ex.Message}", ex);
            }
        }

        public async Task<int> DeleteByKeysAsync(RolesOfUserKey keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, keys);
                Logger.LogInfo<int, RoleOfUserDAO>($"Deleted RolesOfUserModel by keys successfully. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, RoleOfUserDAO>($"Error deleting RolesOfUserModel by keys: {ex.Message}", ex);
                throw new Exception($"Error deleting RolesOfUserModel by keys: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdAsync(uint accountId, int? maxGetCount)
            => await GetByInputAsync(accountId.ToString(), "account_id", maxGetCount);

        public async Task<IEnumerable<RolesOfUserModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, int? maxGetCount)
            => await GetByInputsAsync(accountIds.Select(id => id.ToString()), "account_id", maxGetCount);
        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdAsync(uint roleId, int? maxGetCount)
            => await GetByInputAsync(roleId.ToString(), "role_id", maxGetCount);

        public async Task<IEnumerable<RolesOfUserModel>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, int? maxGetCount)
            => await GetByInputsAsync(roleIds.Select(id => id.ToString()), "role_id", maxGetCount);

        public async Task<IEnumerable<RolesOfUserModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("added_date", EQueryTimeType.MONTH_AND_YEAR, (year, month), maxGetCount);

        public async Task<IEnumerable<RolesOfUserModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("added_date", EQueryTimeType.YEAR, ct, year, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<RolesOfUserModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("added_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate), maxGetCount);

        public async Task<IEnumerable<RolesOfUserModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("added_date", EQueryTimeType.DATE_TIME, ct, dateTime, maxGetCount);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // Các method delete giữ nguyên cho bạn tự sửa
        public async Task<int> DeleteByAccountIdAsync(uint accountId)
            => await DeleteByColumnNameAsync(accountId.ToString(), "account_id");

        public async Task<int> DeleteByAccountIdsAsync(IEnumerable<uint> accountIds)
            => await DeleteByColumnNameAsync(accountIds.Select(id => id.ToString()), "account_id");

        public async Task<int> DeleteByRoleIdAsync(uint roleId) => await DeleteByColumnNameAsync(roleId.ToString(), "role_id");

        public async Task<int> DeleteByRoleIdsAsync(IEnumerable<uint> roleIds)
            => await DeleteByColumnNameAsync(roleIds.Select(id => id.ToString()), "role_id");

        public async Task<int> DeleteByListKeysAsync(IEnumerable<RolesOfUserKey> keys) => await DeleteByKeysAsync(keys);

        // DRY:
        private async Task<int> DeleteByColumnNameAsync(string id, string colName)
        {
            try
            {
                string query = GetDeleteByIdQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, new { Input = id });
                Logger.LogInfo<int, RoleOfUserDAO>($"Deleted RolesOfUserModel by single id successfully. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, RoleOfUserDAO>($"Error deleting RolesOfUserModel by single id: {ex.Message}", ex);
                throw new Exception($"Error deleting RolesOfUserModel by single id: {ex.Message}", ex);
            }
        }

        private async Task<int> DeleteByColumnNameAsync(IEnumerable<string> ids, string colName)
        {
            try
            {
                string query = GetDeleteByIdQuery(colName);
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, new { Input = ids });
                Logger.LogInfo<int, RoleOfUserDAO>($"Deleted RolesOfUserModel by list of ids successfully. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, RoleOfUserDAO>($"Error deleting RolesOfUserModel by list of ids: {ex.Message}", ex);
                throw new Exception($"Error deleting RolesOfUserModel by list of ids: {ex.Message}", ex);
            }
        }

        private async Task<int> DeleteByKeysAsync(IEnumerable<RolesOfUserKey> keys)
        {
            try
            {
                string query = GetDeleteByKeysQuery();
                using IDbConnection connection = await ConnectionFactory.CreateConnection();
                int rowsAffected = await connection.ExecuteAsync(query, keys);
                Logger.LogInfo<int, RoleOfUserDAO>($"Deleted RolesOfUserModel by list of keys successfully. Rows affected: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Logger.LogError<int, RoleOfUserDAO>($"Error deleting RolesOfUserModel by list of keys: {ex.Message}", ex);
                throw new Exception($"Error deleting RolesOfUserModel by list of keys: {ex.Message}", ex);
            }
        }
    }
}
