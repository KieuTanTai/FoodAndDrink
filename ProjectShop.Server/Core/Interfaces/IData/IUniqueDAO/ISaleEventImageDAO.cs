namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISaleEventImageDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetBySaleEventIdAsync(uint saleEventId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime lastUpdated, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateTimeRangeAsync(DateTime start, DateTime end, int? maxGetCount = null);
    }
}
