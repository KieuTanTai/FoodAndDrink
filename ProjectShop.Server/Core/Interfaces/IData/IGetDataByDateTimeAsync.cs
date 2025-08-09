using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetDataByDateTimeAsync<T> where T : class
    {
        Task<List<T>?> GetAllByMonthAndYearAsync(int year, int month);
        Task<List<T>?> GetAllByYearAsync(int year);
        Task<List<T>?> GetAllByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<T>?> GetAllByDateAsync(DateTime date);
    }
}
