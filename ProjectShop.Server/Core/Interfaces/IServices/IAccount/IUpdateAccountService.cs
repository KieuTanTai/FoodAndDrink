namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdateAccountService
    {
        Task<int> UpdateAccountStatusAsync(uint accountId, bool status);
        Task<int> UpdateAccountStatusByUserNameAsync(string userName, bool status);
        Task<int> UpdateAccountStatusByUserNamesAsync(IEnumerable<string> userNames, bool status);
        Task<int> UpdateAccountStatusAsync(IEnumerable<uint> accountIds, bool status);

        Task<int> UpdateAccountPasswordAsync(uint accountId, string newPassword);
        Task<int> UpdateAccountPasswordByUserNameAsync(string userName, string newPassword);
        Task<int> UpdateAccountPasswordByUserNamesAsync(IEnumerable<string> userNames, IEnumerable<string> newPasswords);
        Task<int> UpdateAccountPasswordAsync(IEnumerable<uint> accountIds, IEnumerable<string> newPasswords);
    }
}
