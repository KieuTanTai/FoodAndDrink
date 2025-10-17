using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity>(IDBContext context) : QueryRepository<TEntity>(context), IRepository<TEntity> where TEntity : class
    {
        #region Command Operations

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var entityList = entities.ToList();
            await _dbSet.AddRangeAsync(entityList, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entityList;
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, bool isSoftDelete = false, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete)
            {
                await SoftDeleteAsync(entity, cancellationToken);
                return;
            }

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool isSoftDelete = false,
            Action<TEntity>? updateFieldFunc = null, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete && updateFieldFunc != null)
            {
                await SoftDeleteRangeAsync(entities, updateFieldFunc, cancellationToken);
                return;
            }

            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Custom Delete Operations
        private async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task SoftDeleteRangeAsync(IEnumerable<TEntity> entities, Action<TEntity> updateFieldFunc, CancellationToken cancellationToken = default)
        {
            var entityList = entities.ToList();
            foreach (var entity in entityList)
                updateFieldFunc(entity);
            _dbSet.UpdateRange(entityList);
            await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
