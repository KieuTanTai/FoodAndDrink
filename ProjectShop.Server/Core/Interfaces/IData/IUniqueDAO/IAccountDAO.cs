using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IAccountDAO<T> : IGetByStatusAsync<T> where T : class
    {
        Task<T?> GetByUserNameAsync(string userName);
        Task<T?> GetByUserNameAndPasswordAsync(string userName, string password);
        Task<IEnumerable<T>> GetByUserNameAsync(IEnumerable<string> userNames);
        Task<IEnumerable<T>> GetByCreatedDateAsync(int year, int month);
        Task<IEnumerable<T>> GetByCreatedDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByCreatedDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByCreatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;

        Task<IEnumerable<T>> GetByLastUpdatedDateAsync(int year, int month);
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
    }
}
