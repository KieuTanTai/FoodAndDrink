namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface IForgotPasswordService
    {
        Task<int> UpdatePasswordAsync(string username, string newPassword);
        Task<int> UpdatePasswordAsync(List<string> usernames, List<string> newPasswords);
    }
}
