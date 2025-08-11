using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

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

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input;";
        }

        public async Task<List<LocationCityModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is wrapped in wildcards
                IEnumerable<LocationCityModel> locationCities = await connection.QueryAsync<LocationCityModel>(query, new { Input = input });
                return locationCities.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(LocationCityDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<LocationCityModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("location_city_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationCityModel> result = await connection.QueryAsync<LocationCityModel>(query, new { Input = status });
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving LocationCityModels by status: {ex.Message}", ex);
            }
        }
    }
}
