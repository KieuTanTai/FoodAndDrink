namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryDAO<T> where T : class
    {
        Task<IEnumerable<T>> GetByInventoryId(uint inventoryId);
        Task<IEnumerable<T>> GetByProductBarcode(string barcode);
        Task<IEnumerable<T>> GetByMonthAndYearAddedAsync(int year, int month);
        Task<IEnumerable<T>> GetByMonthAndYearLastUpdatedAsync(int year, int month);
        Task<IEnumerable<T>> GetByYearAddedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByYearLastUpdatedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByDateTimeRangeAddedAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByDateTimeRangeLastUpdatedAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByDateTimeAddedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByDateTimeLastUpdatedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;

    }
}
