using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductCategoryRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<ProductCategory>(context, maxReturnRecordsRule, defaultPageSizeRule), IProductCategoryRepository
    {
        #region Query by ProductBarcode and CategoryId

        public async Task<ProductCategory?> GetByProductBarcodeAndCategoryIdAsync(string productBarcode, uint categoryId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(productCategoryEntity => productCategoryEntity.ProductBarcode == productBarcode && productCategoryEntity.CategoryId == categoryId, cancellationToken);

        public async Task<IEnumerable<ProductCategory>> GetByProductBarcodeAsync(string productBarcode, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            uint cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(productCategoryEntity => productCategoryEntity.ProductBarcode == productBarcode && productCategoryEntity.ProductCategoryId > cursor)
                .OrderBy(productCategoryEntity => productCategoryEntity.ProductCategoryId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(uint categoryId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            uint cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(productCategoryEntity => productCategoryEntity.CategoryId == categoryId && productCategoryEntity.ProductCategoryId > cursor)
                .OrderBy(productCategoryEntity => productCategoryEntity.ProductCategoryId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<ProductCategory?> GetNavigationByIdAsync(uint id, ProductCategoryNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<ProductCategory> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(productCategoryEntity => productCategoryEntity.ProductCategoryId == id, cancellationToken);
        }

        public async Task<IEnumerable<ProductCategory>> GetNavigationByIdsAsync(IEnumerable<uint> ids, ProductCategoryNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            uint cursor = fromRecord ?? 0;

            IQueryable<ProductCategory> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(productCategoryEntity => ids.Contains(productCategoryEntity.ProductCategoryId) && productCategoryEntity.ProductCategoryId > cursor)
                .OrderBy(productCategoryEntity => productCategoryEntity.ProductCategoryId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductCategory> ExplicitLoadAsync(ProductCategory entity, ProductCategoryNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProduct)
                await _context.Entry(entity).Reference(productCategoryEntity => productCategoryEntity.ProductBarcodeNavigation).LoadAsync(cancellationToken);
            if (options.IsGetCategory)
                await _context.Entry(entity).Reference(productCategoryEntity => productCategoryEntity.Category).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<ProductCategory>> ExplicitLoadAsync(IEnumerable<ProductCategory> entities, ProductCategoryNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Product> products = [];
            List<Category> categories = [];
            List<string> productBarcodes = entities.Select(productCategoryItem => productCategoryItem.ProductBarcode).Distinct().ToList();
            List<uint> categoryIds = entities.Select(productCategoryItem => productCategoryItem.CategoryId).Distinct().ToList();

            if (options.IsGetProduct)
                products = await _context.Products.Where(productEntity => productBarcodes.Contains(productEntity.ProductBarcode)).ToListAsync(cancellationToken);
            if (options.IsGetCategory)
                categories = await _context.Categories.Where(categoryEntity => categoryIds.Contains(categoryEntity.CategoryId)).ToListAsync(cancellationToken);

            return MappingToProductCategories(entities, products, categories);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<ProductCategory> ApplyNavigationOptions(IQueryable<ProductCategory> query, ProductCategoryNavigationOptions options)
        {
            if (options.IsGetProduct)
                query = query.Include(productCategoryEntity => productCategoryEntity.ProductBarcodeNavigation);
            if (options.IsGetCategory)
                query = query.Include(productCategoryEntity => productCategoryEntity.Category);

            return query;
        }

        private static IEnumerable<ProductCategory> MappingToProductCategories(IEnumerable<ProductCategory> productCategories,
            List<Product> products, List<Category> categories)
        {
            if (productCategories == null || !productCategories.Any())
                return [];

            Dictionary<string, Product> productsDict = [];
            Dictionary<uint, Category> categoriesDict = [];

            if (products != null && products.Count > 0)
                productsDict = products.ToDictionary(productEntity => productEntity.ProductBarcode);
            if (categories != null && categories.Count > 0)
                categoriesDict = categories.ToDictionary(categoryEntity => categoryEntity.CategoryId);

            foreach (ProductCategory pc in productCategories)
            {
                if (productsDict.TryGetValue(pc.ProductBarcode, out Product? product))
                    pc.ProductBarcodeNavigation = product ?? new();
                if (categoriesDict.TryGetValue(pc.CategoryId, out Category? category))
                    pc.Category = category ?? new();
            }

            return productCategories;
        }

        #endregion
    }
}
