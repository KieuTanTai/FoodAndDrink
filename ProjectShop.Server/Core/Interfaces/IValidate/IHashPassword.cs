namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface IHashPassword
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> ComparePasswords(string hashedPassword, string password);
        Task<bool> IsPasswordHashed(string password);
        Task<bool> IsPasswordValidAsync(string password);
    }
}
