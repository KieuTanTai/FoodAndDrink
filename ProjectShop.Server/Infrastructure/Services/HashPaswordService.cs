using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class HashPaswordService : IHashPassword
    {
        // Hash password dùng BCrypt (async), truyền workFactor = 4 cho tốc độ hash nhanh hơn
        public Task<string> HashPasswordAsync(string password)
        {
            // BCrypt sẽ tự sinh salt (16 bytes, 22 ký tự base64), hash nhanh với workFactor 4
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password, workFactor: 4));
        }

        // So sánh password với hashedPassword (trả về true nếu đúng, false nếu sai)
        public Task<bool> ComparePasswords(string hashedPassword, string password)
        {
            bool match = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return Task.FromResult(match);
        }

        public Task<bool> IsPasswordHashed(string password)
        {
            // BCrypt chuẩn: 60 ký tự, bắt đầu bằng $2a$
            return Task.FromResult(password.Length == 60 && password.StartsWith("$2a$"));
        }

        // Kiểm tra password hợp lệ (ví dụ: tối thiểu 8 ký tự, có số, ký tự đặc biệt, chữ hoa/thường)
        public Task<bool> IsPasswordValidAsync(string password)
        {
            bool isValid = !string.IsNullOrWhiteSpace(password)
                && password.Length > 8
                && password.Any(char.IsUpper)        // Có chữ hoa
                && password.Any(char.IsLower)        // Có chữ thường
                && password.Any(char.IsDigit)        // Có số
                && password.Any(ch => !char.IsLetterOrDigit(ch)); // Có ký tự đặc biệt
            return Task.FromResult(isValid);
        }
    }
}