using System.Linq.Expressions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Base repository interface for generic CRUD operations with EF Core
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IQueryRepository<TEntity> where TEntity : class
    {
        // Command operations
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        // NOTE: for both case (hard delete - soft delete)
        Task<int> DeleteAsync(TEntity entity, bool isSoftDelete = true, CancellationToken cancellationToken = default);
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, bool isSoftDelete = true, Action<TEntity>? action = null, CancellationToken cancellationToken = default);
    }
}
