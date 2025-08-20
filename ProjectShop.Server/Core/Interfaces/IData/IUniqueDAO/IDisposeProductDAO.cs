namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDisposeProductDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode);
        Task<IEnumerable<TEntity>> GetByEmployeeIdAsync(uint employeeId);
        Task<IEnumerable<TEntity>> GetByReasonIdAsync(uint reasonId);
        Task<IEnumerable<TEntity>> GetByQuantityIdAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;

    }
}
