using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationCityDAO : BaseDAO<LocationCityModel>, IGetRelativeAsync<LocationCityModel>, IGetByStatusAsync<LocationCityModel>
    {
        public LocationCityDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "location_city", "location_city_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_city_name) 
                      VALUES (@LocationCityName); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET location_city_name = @LocationCityName 
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<LocationCityModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "location_city_name");

        public async Task<IEnumerable<LocationCityModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "location_city_status");
    }
}
