using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDisposeProductDAO<T> : IGetDataByDateTimeAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByLocationIdAsync(uint locationId);
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string barcode);
        Task<IEnumerable<T>> GetByEmployeeIdAsync(uint employeeId);
        Task<IEnumerable<T>> GetByReasonIdAsync(uint reasonId);
        Task<IEnumerable<T>> GetByQuantityIdAsync<TCompareType>(int quantity, TCompareType compareType) where TCompareType : Enum;

    }
}
