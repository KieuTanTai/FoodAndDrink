using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchRoleServices<TEntity, TOption> where TEntity : class where TOption : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResult<TEntity>> GetByRoleNameAsync(string roleName, TOption? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetRelativeByRoleName(string roleName, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResult<TEntity>> GetByRoleIdAsync(uint roleId, TOption? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByCreatedYearAsync(int year, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime ECompareTypestartDate, DateTime endDate, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeAsync(DateTime dateTime, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByLastUpdatedYearAsync(int year, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeAsync(DateTime dateTime, ECompareType compareType, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TOption? options = null, int? maxGetCount = null, CancellationToken cancellationToken = default);
    }
}
