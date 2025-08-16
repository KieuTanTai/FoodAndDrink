using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotInventoryDAO<T, TKey> : IGetDataByDateTimeAsync<T>, IGetByKeysAsync<T, TKey> where T : class where TKey : struct
    {
        Task<IEnumerable<T>> GetByInventoryQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByProductLotIdAsync(uint productLotId);
        Task<IEnumerable<T>> GetByInventoryIdAsync(uint inventoryId);
    }
}
