namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IPersonDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetRelativeAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByAccountIdAsync(uint accountId);
        Task<TEntity?> GetByPhoneAsync(string phone);
        Task<IEnumerable<TEntity>> GetByPhonesAsync(IEnumerable<string> phones);
        Task<IEnumerable<TEntity>> GetByGenderAsync(bool isMale);
        Task<IEnumerable<TEntity>> GetByGendersAsync(IEnumerable<bool> isMales);
        Task<TEntity?> GetByEmailAsync(string email);
        Task<IEnumerable<TEntity>> GetByEmailsAsync(IEnumerable<string> emails);
        Task<TEntity?> GetByNameAsync(string name);
        Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<string> names);
    }
}
