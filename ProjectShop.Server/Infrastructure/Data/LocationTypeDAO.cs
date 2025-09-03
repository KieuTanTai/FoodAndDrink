using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationTypeDAO : BaseDAO<LocationTypeModel>, IBaseLocationDAO<LocationTypeModel>
    {
        public LocationTypeDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "location_type", "location_type_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_type_name, location_type_status) 
                      VALUES (@LocationTypeName, @LocationTypeStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET location_type_name = @LocationTypeName, location_type_status = @LocationTypeStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<LocationTypeModel>> GetByLikeStringAsync(string input, int? maxCountName)
            => await GetByLikeStringAsync(input, "location_type_name", maxCountName);

        public async Task<IEnumerable<LocationTypeModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "location_type_status", maxGetCount);
    }
}
