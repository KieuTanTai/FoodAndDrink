using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseGetByCreatedAndLastUpdatedDate<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}