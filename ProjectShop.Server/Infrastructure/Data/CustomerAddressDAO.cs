using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CustomerAddressDAO : BaseDAO<CustomerAddressModel>, ICustomerAddressDAO<CustomerAddressModel>
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

        public async Task<IEnumerable<CustomerAddressModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "customer_address_status");

        public async Task<IEnumerable<CustomerAddressModel>> GetByStreetLikeAsync(string streetLike) => await GetByLikeStringAsync(streetLike, "customer_street");

        public async Task<IEnumerable<CustomerAddressModel>> GetByCityIdAsync(uint cityId) => await GetByInputAsync(cityId.ToString(), "customer_city_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByCustomerIdAsync(string customerId) => await GetByInputAsync(customerId, "customer_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByDistrictIdAsync(uint districtId) => await GetByInputAsync(districtId.ToString(), "customer_district_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByWardIdAsync(uint wardId) => await GetByInputAsync(wardId.ToString(), "customer_ward_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByCityIdAndDistrictIdAsync(uint cityId, uint districtId) => await GetByColNamesAsync(cityId, districtId, "customer_city_id", "customer_district_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByCityIdAndWardIdAsync(uint cityId, uint wardId) => await GetByColNamesAsync(cityId, wardId, "customer_city_id", "customer_ward_id");

        public async Task<IEnumerable<CustomerAddressModel>> GetByDistrictIdAndWardIdAsync(uint districtId, uint wardId) => await GetByColNamesAsync(districtId, wardId, "customer_district_id", "customer_ward_id");

        // DRY
        private async Task<IEnumerable<CustomerAddressModel>> GetByColNamesAsync(uint firstInput, uint secondInput, string firstColName, string secondColName)
        {
            try
            {
                string query = $"SELECT * FROM {TableName} WHERE {firstColName} = @FirstInput AND {secondColName} = @SecondInput";
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<CustomerAddressModel> addresses = await connection.QueryAsync<CustomerAddressModel>(query, new { FirstInput = firstInput, SecondInput = secondInput });
                return addresses;
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
                throw new Exception($"Error retrieving customer addresses by column names: {ex.Message}", ex);
            }
        }
    }
}
