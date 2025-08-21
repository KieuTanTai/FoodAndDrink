namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IPersonDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetRelativeAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByAccountIdAsync(uint accountId);
        Task<IEnumerable<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, int? maxGetCount = null);
        Task<TEntity?> GetByPhoneAsync(string phone);
        Task<IEnumerable<TEntity>> GetByPhonesAsync(IEnumerable<string> phones, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByGenderAsync(bool isMale, int? maxGetCount = null);
        Task<TEntity?> GetByEmailAsync(string email);
        Task<IEnumerable<TEntity>> GetByEmailsAsync(IEnumerable<string> emails, int? maxGetCount = null);
        Task<TEntity?> GetByNameAsync(string name);
        Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<string> names, int? maxGetCount = null);
    }
}
