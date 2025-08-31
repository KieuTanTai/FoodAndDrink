using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IForgotPasswordService
    {
        Task<JsonLogEntry> UpdatePasswordAsync(string username, string newPassword);
        Task<IEnumerable<JsonLogEntry>> UpdatePasswordAsync(List<string> usernames, List<string> newPasswords);
    }
}
