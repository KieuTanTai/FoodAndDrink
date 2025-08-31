using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountService<TEntity, TOptions> where TEntity : class where TOptions : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResult<TEntity>> GetByUserNameAsync(string userName, TOptions? options = null);
        Task<ServiceResult<TEntity>> GetByAccountIdAsync(uint accountId, TOptions? options = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, TOptions? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOptions? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, TOptions? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOptions? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOptions? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TOptions? options = null, int? maxGetCount = null);
    }
}
