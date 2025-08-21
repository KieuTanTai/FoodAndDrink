namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailProductLotDAO<TEntity, TKey> : IGetByKeysAsync<TEntity, TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByProductBarcode(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByInitialQuantityAsync<TEnum>(int initialQuantity, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByMFGDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMFGDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByMFGDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMFGDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;

        Task<IEnumerable<TEntity>> GetByEXPDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEXPDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByEXPDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEXPDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
