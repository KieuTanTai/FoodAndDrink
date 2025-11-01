using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    public interface IBaseExplicitLoadRepository<TEntity, TOptions>
        where TEntity : class
        where TOptions : class
    {
        // Query with navigation properties
        Task<TEntity?> GetNavigationByIdAsync(uint id, TOptions options, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetNavigationByIdsAsync(IEnumerable<uint> ids, TOptions options, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
        Task<TEntity> ExplicitLoadAsync(TEntity entity, TOptions options, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> ExplicitLoadAsync(IEnumerable<TEntity> entities, TOptions options, uint? fromRecord = 0, uint? pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}