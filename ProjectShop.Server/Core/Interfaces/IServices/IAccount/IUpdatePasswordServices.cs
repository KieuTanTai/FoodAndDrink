using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.FrontEndRequestsForAccount;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdatePasswordServices
    {
        Task<JsonLogEntry> UpdatePasswordAsync(string userName, string newPassword, HttpContext httpContext, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts, HttpContext httpContext,
             CancellationToken cancellationToken = default);
    }
}
