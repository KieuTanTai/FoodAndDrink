using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ICategoryDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetRelativeAsync<TEntity> where TEntity : class
    {

    }
}
