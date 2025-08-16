namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByDateTimeAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByMonthAndYearAsync(int year, int month);
        Task<IEnumerable<T>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<T>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<T>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
    }
}
