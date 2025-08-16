using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IInventoryMovementDAO<T> : IGetByEnumAsync<T>, IGetDataByDateTimeAsync<T> where T : class
    {
        Task<T?> GetBySourceLocationId(uint locationId);
        Task<T?> GetByDestinationLocationId(uint locationId);
        Task<IEnumerable<T>> GetBySourceLocationIdsAsync(IEnumerable<uint> locationIds);
        Task<IEnumerable<T>> GetByDestinationLocationIdsAsync(IEnumerable<uint> locationIds);
        Task<T?> GetByInventoryIdAsync(uint inventoryId);
        Task<IEnumerable<T>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds);
        Task<IEnumerable<T>> GetByQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;
    }
}
