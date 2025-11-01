using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdateAccountServices
    {
        Task<JsonLogEntry> UpdateAccountStatusAsync(uint accountId, bool status, HttpContext httpContext, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateAccountStatusByUserNameAsync(string userName, bool status, HttpContext httpContext, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status, HttpContext httpContext, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status, HttpContext httpContext, CancellationToken cancellationToken = default);
    }
}
