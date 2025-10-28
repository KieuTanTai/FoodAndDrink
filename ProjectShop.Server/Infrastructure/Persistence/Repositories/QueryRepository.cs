using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Constants;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
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

        // Helper methods for get DateTime methods
        protected async Task<IEnumerable<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate,
           Func<TEntity, DateTime> dateTimeConditions, CancellationToken cancellationToken = default)
        {
            if (endDate > DateTime.Now)
                endDate = DateTime.Now;
            if (startDate > endDate)
                throw new ArgumentException("Start date must be less than or equal to end date.");
            if (startDate == endDate)
                return await GetByTimeAsync(entity => dateTimeConditions(entity).Date == startDate.Date, cancellationToken);
            return await GetByTimeAsync(entity => dateTimeConditions(entity) >= startDate && dateTimeConditions(entity) <= endDate.AddDays(1), cancellationToken);
        }

        protected async Task<Func<TEntity, bool>> GetCompareConditions(int year, ECompareType compareType,
            Func<TEntity, DateTime> dateTimeConditions)
            => compareType switch
            {
                ECompareType.EQUAL => entity => dateTimeConditions(entity).Year == year,
                ECompareType.LESS_THAN => entity => dateTimeConditions(entity).Year < year,
                ECompareType.LESS_THAN_OR_EQUAL => entity => dateTimeConditions(entity).Year <= year,
                ECompareType.GREATER_THAN => entity => dateTimeConditions(entity).Year > year,
                ECompareType.GREATER_THAN_OR_EQUAL => entity => dateTimeConditions(entity).Year >= year,
                _ => throw new InvalidOperationException($"Invalid compare type: {compareType}")
            };

        protected async Task<Func<TEntity, bool>> GetCompareConditions(int month, int year, ECompareType compareType,
            Func<TEntity, DateTime> dateTimeConditions)
            => compareType switch
            {
                ECompareType.EQUAL => entity => dateTimeConditions(entity).Year == year && dateTimeConditions(entity).Month == month,
                ECompareType.LESS_THAN => entity => dateTimeConditions(entity).Year < year
                    || (dateTimeConditions(entity).Year == year && dateTimeConditions(entity).Month < month),
                ECompareType.LESS_THAN_OR_EQUAL => entity => dateTimeConditions(entity).Year < year
                    || (dateTimeConditions(entity).Year == year && dateTimeConditions(entity).Month <= month),
                ECompareType.GREATER_THAN => entity => dateTimeConditions(entity).Year > year
                    || (dateTimeConditions(entity).Year == year && dateTimeConditions(entity).Month > month),
                ECompareType.GREATER_THAN_OR_EQUAL => entity => dateTimeConditions(entity).Year > year
                    || (dateTimeConditions(entity).Year == year && dateTimeConditions(entity).Month >= month),
                _ => throw new InvalidOperationException($"Invalid compare type: {compareType}")
            };

        #endregion
    }
}