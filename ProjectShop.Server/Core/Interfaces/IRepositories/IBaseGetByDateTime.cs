using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseGetByDateTime<TEntity> where TEntity : class
    {
        // Query by TEntityDate
        Task<IEnumerable<TEntity>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByYearAsync(int year, ECompareType eCompareType, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByMonthAndYearAsync(int month, int year, ECompareType eCompareType, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}