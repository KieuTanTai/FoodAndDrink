namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface ISaleEventDAO<T> : IGetRelativeAsync<T>, IGetByStatusAsync<T> where T : class
    {
        Task<T?> GetByNameAsync(string name);
        Task<T?> GetByDiscountCodeAsync(string discountCode);
        Task<IEnumerable<T>> GetByRelativeDiscountCodeAsync(string discountCode);
        Task<IEnumerable<T>> GetByTextAsync(string text);
        Task<IEnumerable<T>> GetByStartDateAsync<TCompareType>(DateTime startDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByEndDateAsync<TCompareType>(DateTime endDate, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByStartDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByEndDateYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByStartDateMonthAndYearAsync(int month, int year);
        Task<IEnumerable<T>> GetByEndDateMonthAndYearAsync(int month, int year);
        Task<IEnumerable<T>> GetByStartDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByEndDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByStartAndEndDateRangeAsync(DateTime startDate, DateTime endDate);

    }
}
