namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IPersonDAO<T> : IGetByStatusAsync<T>, IGetRelativeAsync<T>, IGetDataByDateTimeAsync<T> where T : class
    {
        Task<T?> GetByAccountIdAsync(uint accountId);
        Task<T?> GetByPhoneAsync(string phone);
        Task<IEnumerable<T>> GetByPhonesAsync(IEnumerable<string> phones);
        Task<IEnumerable<T>> GetByGenderAsync(bool isMale);
        Task<IEnumerable<T>> GetByGendersAsync(IEnumerable<bool> isMales);
        Task<T?> GetByEmailAsync(string email);
        Task<IEnumerable<T>> GetByEmailsAsync(IEnumerable<string> emails);
        Task<T?> GetByNameAsync(string name);
        Task<IEnumerable<T>> GetByNamesAsync(IEnumerable<string> names);
    }
}
