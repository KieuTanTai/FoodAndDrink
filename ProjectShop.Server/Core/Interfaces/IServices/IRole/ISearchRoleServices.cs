using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchRoleServices<TEntity, TOption> where TEntity : class where TOption : class
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOption? options = null, int? maxGetCount = null);
        Task<ServiceResult<TEntity>> GetByRoleNameAsync(string roleName, TOption? options = null);
        Task<ServiceResults<TEntity>> GetRelativeByRoleName(string roleName, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResult<TEntity>> GetByRoleIdAsync(uint roleId, TOption? options = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByStatusAsync(bool status, TOption? options = null, int? maxGetCount = null);
    }
}
