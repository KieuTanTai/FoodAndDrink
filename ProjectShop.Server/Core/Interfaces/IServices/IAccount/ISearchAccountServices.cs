using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountServices
    {
        Task<ServiceResults<Account>> GetAllWithOffsetAsync(uint? fromRecord = 0, uint? pageSize = 10, AccountNavigationOptions? options = null,
            CancellationToken cancellationToken = default);
        Task<ServiceResult<Account>> GetByUserNameAsync(string userName, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResult<Account>> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByCreatedYearAsync(int year, ECompareType compareType, uint? fromRecord = 0, uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByCreatedDateMonthAndYearAsync(int year, int month, ECompareType compareType, uint? fromRecord = 0, uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByLastUpdatedYearAsync(int year, ECompareType compareType, uint? fromRecord = 0, uint? pageSize = 10,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, ECompareType compareType, uint? fromRecord = 0,
            uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0,
            uint? pageSize = 10, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByStatusAsync(bool status, uint? fromRecord = 0, uint? pageSize = 10, AccountNavigationOptions? options = null,
            CancellationToken cancellationToken = default);
    }
}
