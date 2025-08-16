using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailProductLotDAO<T, TKey> : IGetByKeysAsync<T, TKey> where T : class where TKey : struct
    {
        Task<IEnumerable<T>> GetByProductBarcode(string barcode);
        Task<IEnumerable<T>> GetByInitialQuantityAsync<TEnum>(int initialQuantity, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByMFGDateAsync(int year, int month);
        Task<IEnumerable<T>> GetByMFGDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByMFGDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByMFGDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;

        Task<IEnumerable<T>> GetByEXPDateAsync(int year, int month);
        Task<IEnumerable<T>> GetByEXPDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByEXPDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByEXPDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
    }
}
