namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInventoryMovementDAO<TEntity> : IGetByEnumAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetBySourceLocationId(uint locationId);
        Task<TEntity?> GetByDestinationLocationId(uint locationId);
        Task<IEnumerable<TEntity>> GetBySourceLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDestinationLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount = null);
        Task<TEntity?> GetByInventoryIdAsync(uint inventoryId);
        Task<IEnumerable<TEntity>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByQuantityAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
    }
}
