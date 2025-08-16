namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IUpdateAccountStatusService
    {
        Task<int> UpdateAccountStatusAsync(string accountId, bool status);
        Task<int> UpdateAccountStatusAsync(IEnumerable<string> accountIds, bool status);
    }
}
