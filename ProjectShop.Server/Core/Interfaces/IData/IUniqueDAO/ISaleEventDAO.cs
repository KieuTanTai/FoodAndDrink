namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISaleEventDAO<TEntity> : IGetRelativeAsync<TEntity>, IGetByStatusAsync<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByNameAsync(string name);
        Task<TEntity?> GetByDiscountCodeAsync(string discountCode);
        Task<IEnumerable<TEntity>> GetByRelativeDiscountCodeAsync(string discountCode);
        Task<IEnumerable<TEntity>> GetByTextAsync(string text);
        Task<IEnumerable<TEntity>> GetByStartDateAsync<TCompareType>(DateTime startDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByEndDateAsync<TCompareType>(DateTime endDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByStartDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByEndDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByStartDateMonthAndYearAsync(int month, int year);
        Task<IEnumerable<TEntity>> GetByEndDateMonthAndYearAsync(int month, int year);
        Task<IEnumerable<TEntity>> GetByStartDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByEndDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByStartAndEndDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}
