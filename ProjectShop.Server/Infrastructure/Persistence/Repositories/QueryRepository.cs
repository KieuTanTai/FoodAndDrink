using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Constants;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class QueryRepository<TEntity>(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord, string primaryKeyName = "")
        : IQueryRepository<TEntity> where TEntity : class
    {
        protected readonly string _colIdName = !string.IsNullOrEmpty(primaryKeyName)
            ? primaryKeyName
            : IsProductBarcodeEntity(typeof(TEntity))
                ? EntityPrimaryKeyNames.ProductBarcode
                : typeof(TEntity).Name + EntityPrimaryKeyNames.IdSuffix;

        protected readonly uint _maxGetReturn = maxGetRecord.MaxGetRecord;
        protected readonly IFoodAndDrinkShopDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        #region Query Operations
        public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync([id], cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<object> ids, CancellationToken cancellationToken = default)
        {
            var idList = ids.ToList();
            return await _dbSet.Where(e => idList.Contains(EF.Property<object>(e, _colIdName))).Take((int)_maxGetReturn).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithOffsetAsync(uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            fromRecord ??= 0;
            return await _dbSet.Skip((int)fromRecord).Take((int)pageSize).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            fromRecord ??= 0;
            return await _dbSet.Where(predicate).Skip((int)fromRecord).Take((int)pageSize).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.AnyAsync(predicate, cancellationToken);

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.CountAsync(predicate, cancellationToken);

        #endregion

        #region Helper methods for generic repositories
        protected async Task<IEnumerable<TEntity>> GetByColumnAsync<TColumn>(string columnName, IEnumerable<TColumn> columnValues,
            uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;

            var valueList = columnValues.ToList();
            return await _dbSet
                .Where(e => valueList.Contains(EF.Property<TColumn>(e, columnName)))
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        protected async Task<IEnumerable<TEntity>> GetByTimeAsync(Func<TEntity, bool> compareConditions, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;

            return await _dbSet
                .Where(e => compareConditions(e))
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        // Helper methods for get DateTime methods
        protected async Task<IEnumerable<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate,
           Func<TEntity, DateTime> dateTimeConditions, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (endDate > DateTime.Now)
                endDate = DateTime.Now;
            if (startDate > endDate)
                throw new ArgumentException("Start date must be less than or equal to end date.");
            if (startDate == endDate)
                return await GetByTimeAsync(entity => dateTimeConditions(entity).Date == startDate.Date, fromRecord, pageSize, cancellationToken);
            return await GetByTimeAsync(entity => dateTimeConditions(entity) >= startDate
                && dateTimeConditions(entity) <= endDate.AddDays(1), fromRecord, pageSize, cancellationToken);
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

        #region Private Helper Methods for Entity Types
        
        private static bool IsProductBarcodeEntity(Type entityType)
        {
            var barcodeEntities = new[]
            {
                "ProductDrink",
                "ProductFruit",
                "ProductMeat",
                "ProductSnack",
                "ProductVegetable"
            };
            return barcodeEntities.Contains(entityType.Name);
        }

        #endregion
    }
}