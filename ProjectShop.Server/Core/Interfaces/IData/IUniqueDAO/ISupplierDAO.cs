using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISupplierDAO<T> : IGetRelativeAsync<T>, IGetByStatusAsync<T>, IGetDataByDateTimeAsync<T> where T : class
    {
        Task<T?> GetByNameAsync(string name);
        Task<T?> GetByPhoneAsync(string phone);
        Task<IEnumerable<T>> GetByRelativePhoneAsync(string phone);
        Task<IEnumerable<T>> GetByPhonesAsync(IEnumerable<string> phones);
        Task<T?> GetByEmailAsync(string email);
        Task<IEnumerable<T>> GetByRelativeEmailAsync(string email);
        Task<IEnumerable<T>> GetByEmailsAsync(IEnumerable<string> emails);
        Task<T?> GetByCompanyLocationIdAsync(uint locationId);
        Task<IEnumerable<T>> GetAllByCompanyLocationIdAsync(uint locationId);
        Task<T?> GetByStoreLocationIdAsync(uint locationId);
        Task<IEnumerable<T>> GetAllByStoreLocationIdAsync(uint locationId);
    }
}
