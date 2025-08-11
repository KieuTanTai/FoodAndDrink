namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetAllByIdAsync<T> where T : class
    {
        Task<List<T>> GetAllByIdAsync(string id, string colIdName);
    }
}
