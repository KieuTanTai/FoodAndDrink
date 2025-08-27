using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.IProduct
{
    public interface ISearchProductService<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetAllByEnumAsync(EProductUnit unit, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByCategoryIdAsync(uint categoryId, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByDateTimeAsync(DateTime dateTime, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResult<TEntity>> GetByEnumAsync(EProductUnit unit, TOptions? options = null);

        Task<ServiceResults<TEntity>> GetByInputPriceAsync(decimal price, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByLikeStringAsync(string input, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByMonthAndYearAsync(int year, int month, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByNetWeightAsync(decimal netWeight, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResult<TEntity>> GetByProductNameAsync(string productName, TOptions? options = null);

        Task<ServiceResults<TEntity>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByRatingAgeAsync(string ratingAge, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetBySupplierIdAsync(uint supplierId, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByYearAsync(int year, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(DateTime lastUpdatedDate, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(int year, ECompareType compareType, TOptions? options = null, int? maxGetCount = null);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(int year, int month, TOptions? options = null, int? maxGetCount = null);

    }
}
