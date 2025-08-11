using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class RoleOfUserDAO : BaseDAO<RolesOfUserModel>, IGetDataByDateTimeAsync<RolesOfUserModel>, IGetByKeysAsync<RolesOfUserModel, RolesOfUserKey>, IDeleteByKeysAsync<RolesOfUserKey>
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
            CheckColumnName(colName);
            return $@"DELETE FROM {TableName} WHERE {colName} = @Input";
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

        private string GetByYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @Input";
        }

        private string GetByMonthAndYear(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        public async Task<RolesOfUserModel> GetByKeysAsync(RolesOfUserKey keys)
        {
            try
            {
                string query = GetByKeysQuery();
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                RolesOfUserModel? result = await connection.QueryFirstOrDefaultAsync<RolesOfUserModel>(query, keys);
                if (result == null)
                    throw new KeyNotFoundException($"No RolesOfUserModel found for AccountId: {keys.AccountId} and RoleId: {keys.RoleId}");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModel by keys: {ex.Message}", ex);
            }
        }

        public async Task<List<RolesOfUserModel>> GetByListKeysAsync(IEnumerable<RolesOfUserKey> listKeys)
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

        public async Task<int> DeleteBySingleKeyAsync(string key, string colName = "id")
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

        public async Task<List<RolesOfUserModel>> GetAllByDateTimeAsync(DateTime dateTime, string colName = "added_date")
        {
            try
            {
                string query = GetByDateTime(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, new { Input = dateTime });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModels by date time: {ex.Message}", ex);
            }
        }

        public async Task<List<RolesOfUserModel>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName = "added_date")
        {
            try
            {
                string query = GetByDateTimeRange(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, new { FirstTime = startDate, SecondTime = endDate });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModels by date time range: {ex.Message}", ex);
            }
        }

        public async Task<List<RolesOfUserModel>> GetAllByYearAsync(int year, string colName = "added_date")
        {
            try
            {
                string query = GetByYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, new { Input = year });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModels by year: {ex.Message}", ex);
            }
        }

        public async Task<List<RolesOfUserModel>> GetAllByMonthAndYearAsync(int year, int month, string colName = "added_date")
        {
            try
            {
                string query = GetByMonthAndYear(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<RolesOfUserModel> result = await connection.QueryAsync<RolesOfUserModel>(query, new { FirstTime = year, SecondTime = month });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving RolesOfUserModels by month and year: {ex.Message}", ex);
            }
        }
    }
}
