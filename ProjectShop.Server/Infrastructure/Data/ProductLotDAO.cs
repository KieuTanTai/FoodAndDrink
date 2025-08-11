using Dapper;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class ProductLotDAO : BaseNoneUpdateDAO<ProductLotModel>, IGetAllByIdAsync<ProductLotModel>
    {
        public ProductLotDAO(
        IDbConnectionFactory connectionFactory,
        IColumnService colService,
        IStringConverter converter,
        IStringChecker checker)
        : base(connectionFactory, colService, converter, checker, "product_lot", "product_lot_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (inventory_id, product_lot_create_date) 
                      VALUES (@InventoryId, @ProductLotCreateDate); SELECT LAST_INSERT_ID();";
        }

        public async Task<List<ProductLotModel>> GetAllByIdAsync(string id, string colIdName = "inventory_id")
        {
            try
            {
                string query = GetDataQuery(colIdName);
                using IDbConnection connection = ConnectionFactory.CreateConnection();
                IEnumerable<ProductLotModel> productLots = await connection.QueryAsync<ProductLotModel>(query, new { Input = id });
                return productLots.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving ProductLots by {colIdName}: {ex.Message}", ex);
            }
        }
    }
}
