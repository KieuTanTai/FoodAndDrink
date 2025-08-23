using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountService<TEntity, TBoolean> where TEntity : class where TBoolean : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TBoolean? options = null, int? maxGetCount = null);
        Task<ServiceResult<TEntity>> GetByUserNameAsync(string userName, TBoolean? options = null);
        Task<ServiceResult<TEntity>> GetByAccountIdAsync(uint accountId, TBoolean? options = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TBoolean? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TBoolean? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TBoolean? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TBoolean? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TBoolean? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TBoolean? options = null, int? maxGetCount = null);
    }
}
