namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IDAO<TModel> : IDbOperationAsync<TModel>, IQueryOperationsAsync, IExecuteOperationsAsync, IUpdateAsync<TModel>
        where TModel : class
    {
    }
}
