using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchAccountRoleServices<TEntity, TOption, TKey> where TEntity : class where TOption : class where TKey : struct
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResult<TEntity>> GetByKeysAsync(TKey keys, TOption? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByListKeysAsync(IEnumerable<TKey> listKeys, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAccountIdAsync(uint accountId, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByRoleIdAsync(uint roleId, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAddedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAddedYearAsync(int year, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByAddedDateTimeAsync(DateTime dateTime, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
    }
}
