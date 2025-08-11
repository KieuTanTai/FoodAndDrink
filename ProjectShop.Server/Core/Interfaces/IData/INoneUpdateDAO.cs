namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface INoneUpdateDAO<TModel> : IDbOperationAsync<TModel> where TModel : class
    {

    }
}
