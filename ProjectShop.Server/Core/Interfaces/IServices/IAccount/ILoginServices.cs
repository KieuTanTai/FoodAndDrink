using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ILoginServices
    {
        // Task<ServiceResult<Account>> HandleLoginAsync(string username, string password,
        //     AccountNavigationOptions? options = null, CancellationToken cancellationToken = default);

        Task<ServiceResult<Account>> HandleGetAuthLoginAsync(string username, string password, bool isGetRole = true, bool isGetPermission = false, CancellationToken cancellationToken = default);
    }
}
