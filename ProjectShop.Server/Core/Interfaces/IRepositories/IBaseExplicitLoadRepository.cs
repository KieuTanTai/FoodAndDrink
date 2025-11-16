using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseExplicitLoadRepository<TEntity, TId, TOptions>
        where TEntity : class
        where TId : notnull
        where TOptions : class
    {
        // Query with navigation properties
        Task<TEntity?> GetNavigationByIdAsync(TId id, TOptions options, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetNavigationByIdsAsync(IEnumerable<TId> ids, TOptions options, uint? fromRecord = 0, uint? pageSize = null,
            CancellationToken cancellationToken = default);
        Task<TEntity> ExplicitLoadAsync(TEntity entity, TOptions options, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ExplicitLoadAsync(IEnumerable<TEntity> entities, TOptions options, CancellationToken cancellationToken = default);
    }
}