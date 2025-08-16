using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IRoleDAO<T> : IGetRelativeAsync<T>, IGetByStatusAsync<T>, IGetDataByDateTimeAsync<T> where T : class
    {
        Task<T?> GetByRoleNameAsync(string roleName);
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum; 
        Task<IEnumerable<T>> GetByLastUpdatedMonthAndYearAsync(int month, int year);
        Task<IEnumerable<T>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate);
    }
}
