namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IAccountDAO<TEntity> : IGetByStatusAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByUserNameAsync(string userName);
        Task<TEntity?> GetByUserNameAndPasswordAsync(string userName, string password);
        Task<IEnumerable<TEntity>> GetByUserNameAsync(IEnumerable<string> userNames, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByCreatedDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;

        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
