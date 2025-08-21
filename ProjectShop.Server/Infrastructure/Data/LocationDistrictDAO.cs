using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationDistrictDAO : BaseDAO<LocationDistrictModel>, IGetRelativeAsync<LocationDistrictModel>, IGetByStatusAsync<LocationDistrictModel>
    {
        public LocationDistrictDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "location_district", "location_district_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_district_name) 
                      VALUES (@LocationDistrictName); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET location_district_name = @LocationDistrictName 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<LocationDistrictModel>> GetByLikeStringAsync(string input, int? maxGetCount)
            => await GetByLikeStringAsync(input, "location_district_name", maxGetCount);

        public async Task<IEnumerable<LocationDistrictModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "location_district_status", maxGetCount);
    }
}
