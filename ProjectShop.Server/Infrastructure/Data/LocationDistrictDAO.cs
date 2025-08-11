using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationDistrictDAO : BaseDAO<LocationDistrictModel>, IGetRelativeAsync<LocationDistrictModel>,
                    IGetByStatusAsync<LocationDistrictModel>
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

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input;";
        }

        public async Task<List<LocationDistrictModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is wrapped in wildcards
                IEnumerable<LocationDistrictModel> locationDistricts = await connection.QueryAsync<LocationDistrictModel>(query, new { Input = input });
                return locationDistricts.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(LocationDistrictDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }

        public async Task<List<LocationDistrictModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("location_district_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationDistrictModel> locationDistricts = await connection.QueryAsync<LocationDistrictModel>(query, new { Input = status });
                return locationDistricts.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(LocationDistrictDAO)}.{nameof(GetAllByStatusAsync)}: {ex.Message}", ex);
            }
        }
    }
}
