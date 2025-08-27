namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IAccountDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByUserNameAsync(string userName);
        Task<TEntity?> GetByUserNameAndPasswordAsync(string userName, string password);
        Task<IEnumerable<TEntity>> GetByUserNameAsync(IEnumerable<string> userNames, int? maxGetCount = null);
        //Task<IEnumerable<TEntity>> GetByMonthAndYearCreatedDateAsync(int year, int month, int? maxGetCount = null);
        //Task<IEnumerable<TEntity>> GetByYearCreatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        //Task<IEnumerable<TEntity>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        //Task<IEnumerable<TEntity>> GetByCreatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;

        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
