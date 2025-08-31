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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "detail_inventory_movement", "detail_inventory_movement_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (inventory_movement_id, product_barcode, product_lot_id, detail_inventory_movement_quantity) 
                      VALUES (@InventoryMovementId, @ProductBarcode, ProductLotId, @DetailInventoryMovementQuantity); SELECT LAST_INSERT_ID();";
        }
        // Implement other methods as needed

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByMovementIdAsync(uint movementId, int? maxGetCount)
            => await GetByInputAsync(movementId.ToString(), "inventory_movement_id", maxGetCount);

        public Task<IEnumerable<DetailInventoryMovementModel>> GetByProductBarcodeAsync(string barcode, int? maxGetCount)
            => GetByInputAsync(barcode, "product_barcode", maxGetCount);

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByProductLotIdAsync(uint productLotId, int? maxGetCount)
            => await GetByInputAsync(productLotId.ToString(), "product_lot_id", maxGetCount);

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByProductLotIdsAsync(IEnumerable<uint> productLotIds, int? maxGetCount)
            => await GetByInputsAsync(productLotIds.Select(id => id.ToString()), "product_lot_id", maxGetCount);

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByMovementIdsAsync(IEnumerable<uint> movementIds, int? maxGetCount)
            => await GetByInputsAsync(movementIds.Select(id => id.ToString()), "inventory_movement_id", maxGetCount);

        public async Task<IEnumerable<DetailInventoryMovementModel>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount)
            => await GetByInputsAsync(barcodes, "product_barcode", maxGetCount);
    }
}
