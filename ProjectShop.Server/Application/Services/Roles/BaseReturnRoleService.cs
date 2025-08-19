using TLGames.Application.Services;

namespace ProjectShop.Server.Application.Services.Roles
{
    public abstract class BaseReturnRoleService<TEntity> : ValidateService<TEntity> where TEntity : class
    {
    }
}
