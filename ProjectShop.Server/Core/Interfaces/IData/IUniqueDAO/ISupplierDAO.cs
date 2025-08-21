namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISupplierDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByNameAsync(string name);
        Task<TEntity?> GetByPhoneAsync(string phone);
        Task<IEnumerable<TEntity>> GetByRelativePhoneAsync(string phone, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByPhonesAsync(IEnumerable<string> phones, int? maxGetCount = null);
        Task<TEntity?> GetByEmailAsync(string email);
        Task<IEnumerable<TEntity>> GetByRelativeEmailAsync(string email, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByEmailsAsync(IEnumerable<string> emails, int? maxGetCount = null);
        Task<TEntity?> GetByCompanyLocationIdAsync(uint locationId);
        Task<IEnumerable<TEntity>> GetAllByCompanyLocationIdAsync(uint locationId, int? maxGetCount = null);
        Task<TEntity?> GetByStoreLocationIdAsync(uint locationId);
        Task<IEnumerable<TEntity>> GetAllByStoreLocationIdAsync(uint locationId, int? maxGetCount = null);
    }
}
