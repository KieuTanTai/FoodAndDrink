namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByInventoryId(uint inventoryId);
        Task<IEnumerable<TEntity>> GetByProductBarcode(string barcode);
        Task<IEnumerable<TEntity>> GetByMonthAndYearAddedAsync(int year, int month);
        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedAsync(int year, int month);
        Task<IEnumerable<TEntity>> GetByYearAddedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeRangeAddedAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByDateTimeRangeLastUpdatedAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByDateTimeAddedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeLastUpdatedAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;

    }
}
