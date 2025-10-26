using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IServices._IBase
{
    public interface IBasePasswordMappingServices
    {
        Task<List<Account>> HelperPasswordMappingAsync(List<Account> accounts, List<string> newPasswords, CancellationToken cancellationToken = default);
    }
}