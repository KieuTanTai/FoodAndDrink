namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByStatusAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByStatusAsync(bool status);
    }
}
