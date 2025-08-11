namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByDateTimeAsync<T> where T : class
    {
        Task<List<T>> GetAllByMonthAndYearAsync(int year, int month, string colName);
        Task<List<T>> GetAllByYearAsync(int year, string colName);
        Task<List<T>> GetAllByDateTimeRangeAsync(DateTime startDate, DateTime endDate, string colName);
        Task<List<T>> GetAllByDateTimeAsync(DateTime dateTime, string colName);
    }
}
