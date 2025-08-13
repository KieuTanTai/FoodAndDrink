using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CustomerAddressDAO : BaseDAO<CustomerAddressModel>, IGetRelativeAsync<CustomerAddressModel>, IGetByStatusAsync<CustomerAddressModel>
    {
        public CustomerAddressDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "customer_address", "customer_address_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (customer_city_id, customer_district_id, customer_ward_id, customer_id, customer_street, customer_address_number, customer_address_status) 
                      VALUES (@CustomerCityId, @CustomerDistrictId, @CustomerWardId, @CustomerId, @CustomerStreet, @CustomerAddressNumber, @CustomerAddressStatus); 
                      SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} 
                      SET customer_district_id = @CustomerDistrictId, 
                          customer_ward_id = @CustomerWardId, 
                          customer_street = @CustomerStreet, 
                          customer_address_number = @CustomerAddressNumber, 
                          customer_address_status = @CustomerAddressStatus
                      WHERE {ColumnIdName} = @{colIdName}";
        }

        private string GetRelativeQuery(string colName)
        {
            CheckColumnName(colName);
            return $"SELECT * FROM {TableName} WHERE {colName} LIKE @Input";
        }

        public async Task<List<CustomerAddressModel>> GetAllByStatusAsync(bool status)
        {
            try
            {
                string query = GetDataQuery("customer_address_status");
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerAddressModel> addresses = await connection.QueryAsync<CustomerAddressModel>(query, new { Input = status });
                return addresses.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customer addresses by status: {ex.Message}", ex);
            }
        }

        public async Task<List<CustomerAddressModel>> GetRelativeAsync(string input, string colName = "customer_street")
        {
            try
            {
                string query = GetRelativeQuery(colName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                if (!input.Contains("%"))
                    input = $"%{input}%"; // Ensure input is a relative search
                IEnumerable<CustomerAddressModel> addresses = await connection.QueryAsync<CustomerAddressModel>(query, new { Input = input });
                return addresses.AsList();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customer addresses relative to {colName}: {ex.Message}", ex);
            }
        }
    }
}
