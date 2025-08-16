using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IUserPaymentMethodDAO<T> : IGetByStatusAsync<T>, IGetDataByDateTimeAsync<T>, IGetByEnumAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByCustomerIdAsync(uint customerId);
        Task<IEnumerable<T>> GetByCustomerIdsAsync(IEnumerable<uint> customerIds);
        Task<IEnumerable<T>> GetByBankIdAsync(uint bankId);
        Task<IEnumerable<T>> GetByBankIdsAsync(IEnumerable<uint> bankIds);
        Task<T?> GetByTokenAsync(string token);
        Task<T?> GetByDisplayNameAsync(string displayName);
        Task<T?> GetByLastFourDigitAsync(string lastFourDigit);
        Task<IEnumerable<T>> GetByRelativeDisplayNameAsync(string displayName);
        Task<IEnumerable<T>> GetAllByLastFourDigitAsync(string lastFourDigit);
        Task<IEnumerable<T>> GetByExpiryYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByExpiryMonthAsync<TCompareType>(int month, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByExpiryYearAndMonthAsync(int year, int month);
    }
}
