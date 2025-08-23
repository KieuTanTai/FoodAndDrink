using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdateAccountService
    {
        Task<JsonLogEntry> UpdateAccountStatusAsync(uint accountId, bool status);
        Task<JsonLogEntry> UpdateAccountStatusByUserNameAsync(string userName, bool status);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status);

        Task<JsonLogEntry> UpdateAccountPasswordAsync(uint accountId, string newPassword);
        Task<JsonLogEntry> UpdateAccountPasswordByUserNameAsync(string userName, string newPassword);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountPasswordByUserNamesAsync(IEnumerable<string> userNames, IEnumerable<string> newPasswords);
        Task<IEnumerable<JsonLogEntry>> UpdateAccountPasswordAsync(IEnumerable<uint> accountIds, IEnumerable<string> newPasswords);
    }
}
