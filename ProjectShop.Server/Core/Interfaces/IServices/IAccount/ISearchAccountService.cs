namespace ProjectShop.Server.Core.Interfaces.IServices.IAccount
{
    public interface ISearchAccountService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByUserNameAsync(string userName);
        Task<T> GetByAccountIdAsync(int accountId);
        Task<IEnumerable<T>> GetByCreatedDateMonthAndYearAsync(int year, int month);
        Task<IEnumerable<T>> GetByCreatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByCreatedDateTimeRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByCreatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedDateMonthAndYearAsync(int year, int month);
        Task<IEnumerable<T>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedDateTimeRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByLastUpdatedDateTimeAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByStatusAsync(bool status);
    }
}
