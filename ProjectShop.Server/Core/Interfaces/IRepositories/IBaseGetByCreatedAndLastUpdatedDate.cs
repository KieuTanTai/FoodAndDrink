using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseGetByCreatedAndLastUpdatedDate<TEntity> where TEntity : class
    {
        // Query by TEntityCreatedDate
        Task<IEnumerable<TEntity>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByCreatedYearAsync(int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByCreatedMonthAndYearAsync(int month, int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);

        // Query by TEntityLastUpdatedDate
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync(int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year, ECompareType eCompareType,
            CancellationToken cancellationToken = default);
    }
}