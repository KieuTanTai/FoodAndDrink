using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationTypeDAO : BaseDAO<LocationTypeModel>, IGetRelativeAsync<LocationTypeModel>,
                    IGetByStatusAsync<LocationTypeModel>
    {
        public LocationTypeDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "location_type", "location_type_id", string.Empty)
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
        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input;";
        }
        public async Task<List<LocationTypeModel>> GetRelativeAsync(string input, string colName)
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains('%'))
                    input = $"%{input}%"; // Ensure input is wrapped in wildcards
                IEnumerable<LocationTypeModel> locationTypes = await connection.QueryAsync<LocationTypeModel>(query, new { Input = input });
                return locationTypes.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in {nameof(LocationTypeDAO)}.{nameof(GetRelativeAsync)}: {ex.Message}", ex);
            }
        }
        public async Task<List<LocationTypeModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("location_type_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<LocationTypeModel> locationTypes = await connection.QueryAsync<LocationTypeModel>(query, new { Input = status });
                return locationTypes.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving LocationTypeModels by status: {ex.Message}", ex);
            }
        }
    }
}
