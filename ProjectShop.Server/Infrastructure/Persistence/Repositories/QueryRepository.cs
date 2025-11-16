using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Constants;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class QueryRepository<TEntity>(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule,
        IDefaultPageSizeRule defaultPageSizeRule, string primaryKeyName = "")
        : IQueryRepository<TEntity> where TEntity : class
    {
        protected readonly string _colIdName = !string.IsNullOrEmpty(primaryKeyName)
            ? primaryKeyName
            : IsProductBarcodeEntity(typeof(TEntity))
                ? EntityPrimaryKeyNames.ProductBarcode
                : typeof(TEntity).Name + EntityPrimaryKeyNames.IdSuffix;

        protected readonly uint _maxGetReturn = maxReturnRecordsRule.MaxRecords;
        protected readonly uint _defaultPageSize = defaultPageSizeRule.DefaultPageSize;
        protected readonly IFoodAndDrinkShopDbContext _context = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        #region Query Operations
        public virtual async Task<TEntity?> GetByIdAsync(uint id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync([id], cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> GetByIdsAsync(IEnumerable<uint> ids, CancellationToken cancellationToken = default)
        {
            var idList = ids.ToList();
            return await _dbSet.Where(entity => idList.Contains(EF.Property<uint>(entity, _colIdName))).Take((int)_maxGetReturn).ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllWithOffsetAsync(uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(entity => EF.Property<uint>(entity, _colIdName) > cursor)
                .OrderBy(entity => EF.Property<uint>(entity, _colIdName))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(predicate)
                .Where(entity => EF.Property<uint>(entity, _colIdName) > cursor)
                .OrderBy(entity => EF.Property<uint>(entity, _colIdName))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.AnyAsync(predicate, cancellationToken);

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await _dbSet.CountAsync(predicate, cancellationToken);

        #endregion

        #region Helper methods for generic repositories

        protected uint ValidateAndNormalizePageSize(uint? pageSize)
        {
            // If pageSize is null, use default page size from platform rules
            if (pageSize == null)
                return _defaultPageSize;

            // If pageSize is 0 or exceeds max, use max return records
            if (pageSize == 0)
                return _defaultPageSize;

            if (pageSize > _maxGetReturn)
                return _maxGetReturn;

            return pageSize.Value;
        }

        protected async Task<IEnumerable<TEntity>> GetByColumnAsync<TColumn>(string columnName, IEnumerable<TColumn> columnValues,
            uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;
            var valueList = columnValues.ToList();

            return await _dbSet
                .Where(entity => valueList.Contains(EF.Property<TColumn>(entity, columnName)))
                .Where(entity => EF.Property<uint>(entity, _colIdName) > cursor)
                .OrderBy(entity => EF.Property<uint>(entity, _colIdName))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        protected async Task<IEnumerable<TEntity>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate,
           Func<TEntity, DateTime> dateTimeConditions, bool isTracking = true, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default)
        {
            if (endDate > DateTime.Now)
                endDate = DateTime.Now;
            if (startDate > endDate)
                throw new ArgumentException("Start date must be less than or equal to end date.");
            if (startDate == endDate)
                return await GetByTimeAsync(entity => dateTimeConditions(entity).Date == startDate.Date, isTracking, fromRecord, pageSize, cancellationToken);
            return await GetByTimeAsync(entity => dateTimeConditions(entity) >= startDate
                && dateTimeConditions(entity) <= endDate.AddDays(1), isTracking, fromRecord, pageSize, cancellationToken);
        }

        private async Task<IEnumerable<TEntity>> GetByTimeAsync(Func<TEntity, bool> compareConditions, bool isTracking, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            if (isTracking)
            {
                return await Task.Run(() => _dbSet
                    .Where(entity => compareConditions(entity))
                    .Where(entity => EF.Property<uint>(entity, _colIdName) > cursor)
                    .OrderBy(entity => EF.Property<uint>(entity, _colIdName))
                    .Take((int)pageSize)
                    .ToList(), cancellationToken);
            }
            else
            {
                return await Task.Run(() => _dbSet
                    .AsNoTracking()
                    .Where(entity => compareConditions(entity))
                    .Where(entity => EF.Property<uint>(entity, _colIdName) > cursor)
                    .OrderBy(entity => EF.Property<uint>(entity, _colIdName))
                    .Take((int)pageSize)
                    .ToList(), cancellationToken);
            }
        }

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