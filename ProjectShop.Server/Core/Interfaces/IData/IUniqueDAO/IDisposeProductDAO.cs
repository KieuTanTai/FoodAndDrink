namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDisposeProductDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByLocationIdAsync(uint locationId, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByEmployeeIdAsync(uint employeeId, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByReasonIdAsync(uint reasonId, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByQuantityIdAsync<TCompareType>(int quantity, TCompareType compareType, int? maxGetCount) where TCompareType : Enum;

    }
}
