using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationWardDAO : BaseDAO<LocationWardModel>, IBaseLocationDAO<LocationWardModel>
    {
        public LocationWardDAO(
                    IDbConnectionFactory connectionFactory,
                    IStringConverter converter,
                    ILogService logger)
                    : base(connectionFactory, converter, logger, "location_ward", "location_ward_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_ward_name, location_ward_status) 
                      VALUES (@LocationWardName, @LocationWardStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string locationWardIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET location_ward_name = @LocationWardName, location_ward_status = @LocationWardStatus 
                      WHERE {ColumnIdName} = @{locationWardIdName}";
        }


        public async Task<IEnumerable<LocationWardModel>> GetByLikeStringAsync(string input, int? maxGetCount)
            => await GetByInputAsync(input, "location_ward_name", maxGetCount);

        public async Task<IEnumerable<LocationWardModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "location_ward_status", maxGetCount);
    }
}
