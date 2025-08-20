namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchAccountRoleService<TEntity, TOption, TKey> where TEntity : class where TOption : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int? maxGetCount = null, TOption? options = null);
        Task<TEntity?> GetByKeysAsync(TKey keys, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByListKeysAsync(IEnumerable<TKey> listKeys, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByAccountIdAsync(uint accountId, int? maxGetCount = null, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByRoleIdAsync(uint roleId, int? maxGetCount = null, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByRoleIdsAsync(IEnumerable<uint> roleIds, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByAddedDateMonthAndYearAsync(int year, int month, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByAddedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByAddedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByAddedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
    }
}
