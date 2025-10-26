using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.PlatformRules;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdatePasswordServices
    {
        Task<JsonLogEntry> UpdatePasswordAsync(string userName, string newPassword, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<FrontEndUpdatePasswordAccount> frontEndUpdatePasswordAccounts, CancellationToken cancellationToken = default);
    }
}
