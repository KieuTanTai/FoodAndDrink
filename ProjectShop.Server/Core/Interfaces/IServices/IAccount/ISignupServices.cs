using ProjectShop.Server.Core.ValueObjects.Results.ServiceResult;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISignupServices
    {
        Task<ServiceResult<Account>> AddAccountAsync(Account entity, CancellationToken cancellationToken = default);
        Task<ServiceResults<Account>> AddAccountsAsync(IEnumerable<Account> entities, HttpContext httpContext, CancellationToken cancellationToken = default);
    }
}
