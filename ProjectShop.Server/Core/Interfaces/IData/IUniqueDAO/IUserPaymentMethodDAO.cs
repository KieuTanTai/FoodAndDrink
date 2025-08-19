namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IUserPaymentMethodDAO<TEntity> : IGetByStatusAsync<TEntity>, IGetDataByDateTimeAsync<TEntity>, IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCustomerIdAsync(uint customerId);
        Task<IEnumerable<TEntity>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds);
        Task<IEnumerable<TEntity>> GetByBankIdAsync(uint bankId);
        Task<IEnumerable<TEntity>> GetByBankIdsAsync(IEnumerable<uint> bankIds);
        Task<TEntity?> GetByTokenAsync(string token);
        Task<TEntity?> GetByDisplayNameAsync(string displayName);
        Task<TEntity?> GetByLastFourDigitAsync(string lastFourDigit);
        Task<IEnumerable<TEntity>> GetByRelativeDisplayNameAsync(string displayName);
        Task<IEnumerable<TEntity>> GetAllByLastFourDigitAsync(string lastFourDigit);
        Task<IEnumerable<TEntity>> GetByExpiryYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByExpiryMonthAsync<TCompareType>(int month, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByExpiryYearAndMonthAsync(int year, int month);
    }
}
