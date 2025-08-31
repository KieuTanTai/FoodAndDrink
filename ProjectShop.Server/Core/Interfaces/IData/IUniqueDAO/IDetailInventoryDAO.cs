namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailInventoryDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByInventoryIdAsync(uint inventoryId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByInventoryIdsAsync(IEnumerable<uint> inventoryIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;

    }
}
