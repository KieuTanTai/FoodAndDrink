using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class HashPasswordServices: IHashPassword
    {
        public async Task<string> HashPasswordAsync(string password, CancellationToken cancellationToken)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 4);
        }

        public async Task<bool> ComparePasswordsAsync(string hashedPassword, string password, CancellationToken cancellationToken = default)
        {
            bool match = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return match;
        }

        public async Task<bool> IsPasswordHashedAsync(string password, CancellationToken cancellationToken = default)
        {
            return password.Length == 60 && password.StartsWith("$2a$");
        }

        public async Task<bool> IsPasswordValidAsync(string password, CancellationToken cancellationToken = default)
        {
            bool isValid = !string.IsNullOrWhiteSpace(password)
                && password.Length > 8
                && password.Any(char.IsUpper)        // Có chữ hoa
                && password.Any(char.IsLower)        // Có chữ thường
                && password.Any(char.IsDigit)        // Có số
                && password.Any(ch => !char.IsLetterOrDigit(ch)); // Có ký tự đặc biệt
            return isValid;
        }
    }
}