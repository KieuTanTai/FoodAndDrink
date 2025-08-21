namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountService<TEntity, TBoolean> where TEntity : class where TBoolean : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TBoolean? options = null, int? maxGetCount = null);
        Task<TEntity> GetByUserNameAsync(string userName, TBoolean? options = null);
        Task<TEntity> GetByAccountIdAsync(uint accountId, TBoolean? options = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TBoolean? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TBoolean? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TBoolean? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TBoolean? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByStatusAsync(bool status, TBoolean? options = null, int? maxGetCount = null);
    }
}
