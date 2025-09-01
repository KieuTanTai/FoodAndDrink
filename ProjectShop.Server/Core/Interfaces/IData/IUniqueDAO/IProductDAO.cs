namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetByEnumAsync<TEntity>, IGetRelativeAsync<TEntity>, IGetDataByDateTimeAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByProductNameAsync(string productName);
        Task<IEnumerable<TEntity>> GetByProductNamesAsync(IEnumerable<string> productNames, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetBySupplierIdAsync(uint supplierId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByRatingAgeAsync(string ratingAge, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByNetWeightAsync<TCompareType>(decimal netWeight, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdatedDate, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedDateAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount = null);

    }
}
