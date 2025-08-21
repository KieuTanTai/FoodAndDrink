namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByRoleNameAsync(string roleName);
        Task<IEnumerable<TEntity>> GetByRoleNamesAsync(IEnumerable<string> roleNames, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType, int? maxGetCount) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year, int? maxGetCount);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount);
    }
}
