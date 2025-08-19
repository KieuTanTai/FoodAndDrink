namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByMonthAndYearAsync(int year, int month);
        Task<IEnumerable<TEntity>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TEntity>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum;
    }
}
