using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IDetailInventoryMovementRepository : IRepository<DetailInventoryMovement>
    {
        Task<IEnumerable<DetailInventoryMovement>> GetByInventoryMovementIdAsync(uint inventoryMovementId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInventoryMovement>> GetByProductLotIdAsync(uint productLotId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DetailInventoryMovement>> GetByProductBarcodeAsync(string productBarcode, CancellationToken cancellationToken = default);
    }
}
