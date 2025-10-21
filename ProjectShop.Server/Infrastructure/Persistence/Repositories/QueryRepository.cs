using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Constants;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class QueryRepository<TEntity>(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord, string? primaryKeyName = null) 
        : IQueryRepository<TEntity> where TEntity : class
    {
        protected readonly string _colIdName = primaryKeyName ?? typeof(TEntity).Name + EntityPrimaryKeyNames.IdSuffix;
        protected readonly uint _maxGetReturn = maxGetRecord.MaxGetRecord;
        protected readonly IFoodAndDrinkShopDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        #region Query Operations
        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default)
        {
            var idList = ids.ToList();
            return await _dbSet.Where(e => idList.Contains(EF.Property<object>(e, _colIdName))).Take((int)_maxGetReturn).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.Take((int)_maxGetReturn).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).Take((int)_maxGetReturn).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(predicate, cancellationToken);
        }

        #endregion

        #region Helper methods for generic repositories
        protected async Task<IEnumerable<TEntity>> GetByColumnAsync<TColumn>(string columnName, IEnumerable<TColumn> columnValues, CancellationToken cancellationToken = default)
        {
            var valueList = columnValues.ToList();
            return await _dbSet
                .Where(e => valueList.Contains(EF.Property<TColumn>(e, columnName))).Take((int)_maxGetReturn)
                .ToListAsync(cancellationToken);
        }

        protected async Task<IEnumerable<TEntity>> GetByTimeAsync(Func<TEntity, bool> compareConditions, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(e => compareConditions(e))
                .Take((int)_maxGetReturn)
                .ToListAsync(cancellationToken);
        }

        #endregion
    }
}