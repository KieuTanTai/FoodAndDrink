using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        // Query operations
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllWithOffsetAsync(uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}