namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByRoleNameAsync(string roleName);
        Task<IEnumerable<TEntity>> GetByRoleNamesAsync(IEnumerable<string> roleNames);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate);
    }
}
