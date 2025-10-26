using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IProduct
{
    public interface ISearchProductServices<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetAllByEnumAsync(EProductUnit unit, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByDateTimeAsync(DateTime dateTime, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResult<TEntity>> GetByEnumAsync(EProductUnit unit, TOptions? options = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByInputPriceAsync(decimal price, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByLikeStringAsync(string input, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByMonthAndYearAsync(int year, int month, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByNetWeightAsync(decimal netWeight, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResult<TEntity>> GetByProductNameAsync(string productName, TOptions? options = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByRatingAgeAsync(string ratingAge, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetBySupplierIdAsync(uint supplierId, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByYearAsync(int year, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(DateTime lastUpdatedDate, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(int year, ECompareType compareType, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

        Task<ServiceResults<TEntity>> GetByLastUpdatedDateAsync(int year, int month, TOptions? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);

    }
}
