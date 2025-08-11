using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationDAO : BaseDAO<LocationModel>, IGetAllByIdAsync<LocationModel>, IGetByStatusAsync<LocationModel>,
                IGetRelativeAsync<LocationModel>
    {
        public LocationDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "location", "location_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_type_id, house_number, location_street, location_ward_id, location_district_id,
                     location_city_id, location_phone, location_email, location_name, location_status)
                    VALUES (@LocationTypeId, @HouseNumber, @LocationStreet, @LocationWardId,
                     @LocationDistrictId, @LocationCityId, @LocationPhone, @LocationEmail, @LocationName, @LocationStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} SET 
                location_type_id = @LocationTypeId,
                house_number = @HouseNumber,
                location_street = @LocationStreet,
                location_ward_id = @LocationWardId,
                location_district_id = @LocationDistrictId,
                location_city_id = @LocationCityId,
                location_phone = @LocationPhone,
                location_email = @LocationEmail,
                location_name = @LocationName,
                location_status = @LocationStatus
              WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input;";
        }

        public async Task<List<LocationModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is wrapped in wildcards
                IEnumerable<LocationModel> locations = await connection.QueryAsync<LocationModel>(query, new { Input = input });
                return locations.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(LocationDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<LocationModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("location_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationModel> result = await connection.QueryAsync<LocationModel>(query, new { Input = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving LocationModels by status: {ex.Message}", ex);
            }
        }

        public async Task<List<LocationModel>> GetAllByIdAsync(string id, string colIdName)
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationModel> result = await connection.QueryAsync<LocationModel>(query, new { Input = id });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving LocationModels by ID: {ex.Message}", ex);
            }
        }
    }
}
