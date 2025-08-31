namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDisposeProductDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByLocationIdAsync(uint locationId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> barcodes, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEmployeeIdAsync(uint employeeId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEmployeeIdsAsync(IEnumerable<uint> employeeIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByReasonIdAsync(uint reasonId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByReasonIdsAsync(IEnumerable<uint> reasonIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByQuantityIdAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;

    }
}
