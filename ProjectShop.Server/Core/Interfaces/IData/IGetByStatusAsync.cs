namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByStatusAsync<TEntity> where TEntity : class
    {
        //Task<IEnumerable<TEntity>> GetByStatusAsync(bool status);
        Task<IEnumerable<TEntity>> GetByStatusAsync(bool status, int? maxGetCount = null);
    }
}
