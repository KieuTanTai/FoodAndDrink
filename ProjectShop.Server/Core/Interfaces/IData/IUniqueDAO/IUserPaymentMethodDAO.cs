namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IUserPaymentMethodDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity>, IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(uint customerId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByBankIdAsync(uint bankId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByBankIdsAsync(IEnumerable<uint> bankIds, int? maxGetCount = null);
        Task<TEntity?> GetByTokenAsync(string token);
        Task<TEntity?> GetByDisplayNameAsync(string displayName);
        Task<TEntity?> GetByLastFourDigitAsync(string lastFourDigit);
        Task<IEnumerable<TEntity>> GetByRelativeDisplayNameAsync(string displayName, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetAllByLastFourDigitAsync(string lastFourDigit, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByExpiryYearAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByExpiryMonthAsync<TCompareType>(int month, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByExpiryMonthAndYearAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GEtByMonthAndYearLastUpdatedDateAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearLastUpdatedDateAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
