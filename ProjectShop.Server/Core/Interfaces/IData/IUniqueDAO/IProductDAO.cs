namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetByEnumAsync<TEntity>, IGetRelativeAsync<TEntity>, IGetDataByDateTimeAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCategoryIdAsync(uint categoryId);
        Task<IEnumerable<TEntity>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds);
        Task<IEnumerable<TEntity>> GetBySupplierIdAsync(uint supplierId);
        Task<IEnumerable<TEntity>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds);
        Task<TEntity?> GetByProductNameAsync(string productName);
        Task<IEnumerable<TEntity>> GetByRatingAgeAsync(string ratingAge);
        Task<IEnumerable<TEntity>> GetByNetWeightAsync<TCompareType>(decimal netWeight, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdatedDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByLastUpdateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdateMonthAndYearAsync(int year, int month);

    }
}
