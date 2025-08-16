using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class HashPaswordService : IHashPassword
    {
        // Hash password dùng BCrypt (async)
        public Task<string> HashPasswordAsync(string password)
        {
            // BCrypt sẽ tự sinh salt và hash password
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }

        // So sánh password với hashedPassword (trả về 1 nếu đúng, 0 nếu sai)
        public Task<bool> ComparePasswords(string hashedPassword, string password)
        {
            bool match = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return Task.FromResult(match);
        }

        public Task<bool> IsPasswordHashed(string password)
        {
            return Task.FromResult(password.Length == 60 && password.StartsWith("$2a$"));
        }

        // Kiểm tra password hợp lệ (ví dụ: tối thiểu 6 ký tự, có số, ký tự đặc biệt...)
        public Task<bool> IsPasswordValidAsync(string password)
        {
            // Ví dụ chính sách:
            bool isValid = !string.IsNullOrWhiteSpace(password)
                && password.Length > 8
                && password.Any(char.IsUpper)        // Có chữ hoa
                && password.Any(char.IsLower)        // Có chữ thường
                && password.Any(char.IsDigit)        // Có số
                && password.Any(ch => !char.IsLetterOrDigit(ch)); // Có kí tự đặc biệt
            return Task.FromResult(isValid);
        }
    }
}
