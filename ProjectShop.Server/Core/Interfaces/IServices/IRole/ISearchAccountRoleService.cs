using ProjectShop.Server.Core.ObjectValue;

namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchAccountRoleService<TEntity, TOption, TKey> where TEntity : class where TOption : class where TKey : struct
    {
        Task<ServiceResults<TEntity>> GetAllAsync(TOption? options = null, int? maxGetCount = null);
        Task<ServiceResult<TEntity>> GetByKeysAsync(TKey keys, TOption? options = null);
        Task<ServiceResults<TEntity>> GetByListKeysAsync(IEnumerable<TKey> listKeys, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByAccountIdAsync(uint accountId, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByRoleIdAsync(uint roleId, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByAddedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<ServiceResults<TEntity>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null);
        Task<ServiceResults<TEntity>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
    }
}
