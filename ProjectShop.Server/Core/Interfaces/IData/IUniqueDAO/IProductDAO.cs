using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductDAO<T> : IGetByStatusAsync<T>, IGetByEnumAsync<T>, IGetRelativeAsync<T>, IGetDataByDateTimeAsync<T>, IGetByRangePriceAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByCategoryIdAsync(uint categoryId);
        Task<IEnumerable<T>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds);
        Task<IEnumerable<T>> GetBySupplierIdAsync(uint supplierId);
        Task<IEnumerable<T>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds);
        Task<T?> GetByProductNameAsync(string productName);
        Task<IEnumerable<T>> GetByRatingAgeAsync(string ratingAge);
        Task<IEnumerable<T>> GetByNetWeightAsync<TCompareType>(decimal netWeight, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdatedDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByLastUpdateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdateMonthAndYearAsync(int year, int month);

    }
}
