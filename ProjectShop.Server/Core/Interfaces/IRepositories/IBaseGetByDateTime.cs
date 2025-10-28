using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseGetByDateTime<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByYearAsync(int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByMonthAndYearAsync(int month, int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);
    }
}