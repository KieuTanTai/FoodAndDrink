using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByStatusAsync<T> where T : class
    {
        Task<List<T>> GetAllByStatusAsync(bool status);
    }
}
