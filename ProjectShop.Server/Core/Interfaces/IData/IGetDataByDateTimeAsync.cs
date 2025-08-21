namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
        Task<IEnumerable<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount = null) where TEnum : Enum;
    }
}
