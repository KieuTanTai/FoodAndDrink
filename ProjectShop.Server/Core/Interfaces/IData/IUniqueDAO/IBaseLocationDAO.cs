namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IBaseLocationDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity> where TEntity : class
    {

    }
}
