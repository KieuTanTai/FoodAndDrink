namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByRoleNameAsync(string roleName);
        Task<IEnumerable<TEntity>> GetByRoleNamesAsync(IEnumerable<string> roleNames, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType, int? maxGetCount) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedDateAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByMonthAndYearLastUpdatedDateAsync(int month, int year, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount);
    }
}
