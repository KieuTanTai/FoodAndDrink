namespace ProjectShop.Server.Core.Interfaces.IValidate
{
    public interface IHashPassword
    {
        Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken = default);
        Task<bool> ComparePasswordsAsync(string hashedPassword, string password, CancellationToken cancellationToken = default);
        Task<bool> IsPasswordHashedAsync(string password, CancellationToken cancellationToken = default);
        Task<bool> IsPasswordValidAsync(string password, CancellationToken cancellationToken = default);
    }
}
