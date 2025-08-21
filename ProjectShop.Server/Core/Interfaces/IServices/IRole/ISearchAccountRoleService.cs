namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchAccountRoleService<TEntity, TOption, TKey> where TEntity : class where TOption : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync(TOption? options = null, int? maxGetCount = null);
        Task<TEntity?> GetByKeysAsync(TKey keys, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByListKeysAsync(IEnumerable<TKey> listKeys, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByAccountIdAsync(uint accountId, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByRoleIdAsync(uint roleId, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByAddedDateMonthAndYearAsync(int year, int month, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null, int? maxGetCount = null) where TCompareType : Enum;
    }
}
