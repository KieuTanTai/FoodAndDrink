using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISaleEventImageDAO<T> : IGetDataByDateTimeAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetBySaleEventIdAsync(uint saleEventId);
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdated, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedMonthAndYearAsync(int month, int year);
        Task<IEnumerable<T>> GetByLastUpdatedDateTimeRangeAsync(DateTime start, DateTime end);
    }
}
