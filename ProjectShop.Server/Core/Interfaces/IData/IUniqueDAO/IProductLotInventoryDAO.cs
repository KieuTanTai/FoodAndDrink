namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotInventoryDAO<TEntity, TKey> : IGetDataByDateTimeAsync<TEntity>, IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByInventoryQuantityAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByProductLotIdAsync(uint productLotId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByInventoryIdAsync(uint inventoryId, int? maxGetCount = null);
    }
}
