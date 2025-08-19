namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailProductLotDAO<TEntity, TKey> : IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByProductBarcode(string barcode);
        Task<IEnumerable<TEntity>> GetByInitialQuantityAsync<TEnum>(int initialQuantity, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByMFGDateAsync(int year, int month);
        Task<IEnumerable<TEntity>> GetByMFGDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByMFGDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByMFGDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;

        Task<IEnumerable<TEntity>> GetByEXPDateAsync(int year, int month);
        Task<IEnumerable<TEntity>> GetByEXPDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByEXPDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByEXPDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
    }
}
