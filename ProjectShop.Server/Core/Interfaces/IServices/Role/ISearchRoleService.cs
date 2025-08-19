namespace ProjectShop.Server.Core.Interfaces.IServices.Role
{
    public interface ISearchRoleService<TEntity, TOption> where TEntity : class where TOption : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(int? maxGetCount = null, TOption? options = null);
        Task<TEntity> GetByRoleNameAsync(string roleName, TOption? options = null);
        Task<IEnumerable<TEntity>> GetRelativeByRoleName(string roleName, int? maxGetCount = 0, TOption? options = null);
        Task<TEntity> GetByRoleIdAsync(uint roleId, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateMonthAndYearAsync(int year, int month, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, TOption? options = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType, TOption? options = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByStatusAsync(bool status, int? maxGetCount = null, TOption? options = null);
    }
}
