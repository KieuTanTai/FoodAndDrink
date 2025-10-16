using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    public interface IInventoryMovementRepository : IRepository<InventoryMovement>
    {
        Task<IEnumerable<InventoryMovement>> GetByInventoryIdAsync(uint inventoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<InventoryMovement>> GetByMovementTypeAsync(string movementType, CancellationToken cancellationToken = default);
        Task<IEnumerable<InventoryMovement>> GetByMovementDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<InventoryMovement?> GetByIdWithDetailsAsync(uint movementId, CancellationToken cancellationToken = default);
    }
}
