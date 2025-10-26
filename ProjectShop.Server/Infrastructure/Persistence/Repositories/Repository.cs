using System.Globalization;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class Repository<TEntity>(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord, string? primaryKeyName = null) 
    : QueryRepository<TEntity>(context, maxGetRecord, primaryKeyName), IRepository<TEntity> where TEntity : class
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

        public virtual async Task<int> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.UpdateRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, bool isSoftDelete = false, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete)
                return await SoftDeleteAsync(entity, cancellationToken);

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities, bool isSoftDelete = false,
            Action<TEntity>? updateFieldFunc = null, CancellationToken cancellationToken = default)
        {
            if (isSoftDelete && updateFieldFunc != null)
                return await SoftDeleteRangeAsync(entities, updateFieldFunc, cancellationToken);

            _dbSet.RemoveRange(entities);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Custom Delete Operations
        private async Task<int> SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<int> SoftDeleteRangeAsync(IEnumerable<TEntity> entities, Action<TEntity> updateFieldAction, CancellationToken cancellationToken = default)
        {
            var entityList = entities.ToList();
            foreach (var entity in entityList)
                updateFieldAction(entity);
            _dbSet.UpdateRange(entityList);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
