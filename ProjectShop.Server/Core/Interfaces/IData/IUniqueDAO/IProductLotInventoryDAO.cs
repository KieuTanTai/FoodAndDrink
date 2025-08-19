namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotInventoryDAO<TEntity, TKey> : IGetDataByDateTimeAsync<TEntity>, IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByInventoryQuantityAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByProductLotIdAsync(uint productLotId);
        Task<IEnumerable<TEntity>> GetByInventoryIdAsync(uint inventoryId);
    }
}
