using ProjectShop.Server.Core.ValueObjects.Results.ServiceResult;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountServices
    {
        Task<ServiceResults<Account>> GetAllWithOffsetAsync(uint? fromRecord = 0, uint? pageSize = null, AccountNavigationOptions? options = null,
            CancellationToken cancellationToken = default);
        Task<ServiceResult<Account>> GetByUserNameAsync(string userName, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResult<Account>> GetByAccountIdAsync(uint accountId, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = null,
            AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0,
            uint? pageSize = null, AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> GetByStatusAsync(bool status, uint? fromRecord = 0, uint? pageSize = null, AccountNavigationOptions? options = null,
            CancellationToken cancellationToken = default);
    }
}
