using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class DetailInventoryMovementDAO : BaseNoneUpdateDAO<DetailInventoryMovementModel>, IDetailInventoryMovementDAO<DetailInventoryMovementModel>
    {
        public DetailInventoryMovementDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "detail_inventory_movement", "detail_inventory_movement_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (inventory_movement_id, product_barcode, detail_inventory_movement_quantity) 
                      VALUES (@InventoryMovementId, @ProductBarcode, @DetailInventoryMovementQuantity); SELECT LAST_INSERT_ID();";
        }
        // Implement other methods as needed

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByMovementIdAsync(uint movementId, int? maxGetCount) 
            => await GetByInputAsync(movementId.ToString(), "inventory_movement_id", maxGetCount);

        public Task<IEnumerable<DetailInventoryMovementModel>> GetByProductBarcodeAsync(string barcode, int? maxGetCount) 
            => GetByInputAsync(barcode, "product_barcode", maxGetCount);
    }
}
