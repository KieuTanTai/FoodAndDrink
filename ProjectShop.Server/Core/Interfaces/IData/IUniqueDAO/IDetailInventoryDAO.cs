namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryDAO<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByInventoryId(uint inventoryId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcode(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMonthAndYearAddedAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearAddedAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeRangeAddedAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDateTimeRangeLastUpdatedAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDateTimeAddedAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeLastUpdatedAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;

    }
}
