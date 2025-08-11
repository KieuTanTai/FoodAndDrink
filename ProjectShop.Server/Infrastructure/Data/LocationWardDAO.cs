using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationWardDAO : BaseDAO<LocationWardModel>, IGetRelativeAsync<LocationWardModel>, IGetByStatusAsync<LocationWardModel>
    {
        public LocationWardDAO(
                    IDbConnectionFactory connectionFactory,
                    IColumnService colService,
                    IStringConverter converter,
                    IStringChecker checker)
                    : base(connectionFactory, colService, converter, checker, "location_ward", "location_ward_id", string.Empty)
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

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input";
        }

        public async Task<List<LocationWardModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("location_ward_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationWardModel> wards = await connection.QueryAsync<LocationWardModel>(query, new { Input = status });
                return wards.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving location wards by status: {ex.Message}", ex);
            }
        }

        public async Task<List<LocationWardModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationWardModel> wards = await connection.QueryAsync<LocationWardModel>(query, new { Input = $"%{input}%" });
                return wards.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving relative location wards: {ex.Message}", ex);
            }
        }
    }
}
