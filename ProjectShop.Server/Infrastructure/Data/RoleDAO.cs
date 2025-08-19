using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class RoleDAO : BaseDAO<RoleModel>, IRoleDAO<RoleModel>
    {
        public RoleDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "role", "role_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (role_name, role_status, role_created_date, role_last_updated_date) 
                      VALUES (@RoleName, @RoleStatus, @RoleCreatedDate, @RoleUpdatedDate); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET role_name = @RoleName, role_status = @RoleStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<RoleModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("role_created_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<RoleModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("role_created_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<RoleModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("role_created_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<RoleModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("role_created_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- LastUpdatedDate -----------
        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("role_last_updated_date", EQueryTimeType.DATE_TIME, ct, dateTime);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("role_last_updated_date", EQueryTimeType.DATE_TIME_RANGE, (startDate, endDate));

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedMonthAndYearAsync(int month, int year)
            => await GetByDateTimeAsync("role_last_updated_date", EQueryTimeType.MONTH_AND_YEAR, (year, month));

        public async Task<IEnumerable<RoleModel>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum
        {
            if (compareType is ECompareType ct)
                return await GetByDateTimeAsync("role_last_updated_date", EQueryTimeType.YEAR, ct, year);
            throw new ArgumentException("Invalid compare type", nameof(compareType));
        }

        // ----------- LIKE String -----------
        public async Task<IEnumerable<RoleModel>> GetByLikeStringAsync(string input)
            => await GetByLikeStringAsync(input, "role_name");

        public async Task<IEnumerable<RoleModel>> GetByLikeStringAsync(string input, int maxGetCount)
            => await GetByLikeStringAsync(input, "role_name", maxGetCount);
        public async Task<RoleModel?> GetByRoleNameAsync(string roleName) => await GetSingleDataAsync(roleName, "role_name");
        // ----------- Status -----------
        public async Task<IEnumerable<RoleModel>> GetByStatusAsync(bool status)
            => await GetByInputAsync(GetTinyIntString(status), "role_status");

        public async Task<IEnumerable<RoleModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "role_status", maxGetCount);
    }
}
