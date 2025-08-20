namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInventoryMovementDAO<TEntity> : IGetByEnumAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetBySourceLocationId(uint locationId);
        Task<TEntity?> GetByDestinationLocationId(uint locationId);
        Task<IEnumerable<TEntity>> GetBySourceLocationIdsAsync(IEnumerable<uint> locationIds);
        Task<IEnumerable<TEntity>> GetByDestinationLocationIdsAsync(IEnumerable<uint> locationIds);
        Task<TEntity?> GetByInventoryIdAsync(uint inventoryId);
        Task<IEnumerable<TEntity>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds);
        Task<IEnumerable<TEntity>> GetByQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;
    }
}
