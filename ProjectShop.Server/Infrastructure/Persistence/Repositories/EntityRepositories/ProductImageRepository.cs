using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class ProductImageRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<ProductImage>(context, maxReturnRecordsRule, defaultPageSizeRule), IProductImageRepository
    {
        #region Query by ProductBarcode

        public async Task<IEnumerable<ProductImage>> GetByProductBarcodeAsync(string productBarcode, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            uint cursor = fromRecord ?? 0;

            return await _dbSet
                .Where(productImageEntity => productImageEntity.ProductBarcode == productBarcode && productImageEntity.ProductImageId > cursor)
                .OrderBy(productImageEntity => productImageEntity.ProductImageId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by ProductImageCreatedDate

        public Task<IEnumerable<ProductImage>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, productImage => productImage.ProductImageCreatedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query by ProductImageLastUpdatedDate

        public Task<IEnumerable<ProductImage>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, productImageEntity => productImageEntity.ProductImageLastUpdatedDate, true, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<ProductImage?> GetNavigationByIdAsync(uint id, ProductImageNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<ProductImage> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(productImageEntity => productImageEntity.ProductImageId == id, cancellationToken);
        }

        public async Task<IEnumerable<ProductImage>> GetNavigationByIdsAsync(IEnumerable<uint> ids, ProductImageNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            uint cursor = fromRecord ?? 0;

            IQueryable<ProductImage> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(productImageEntity => ids.Contains(productImageEntity.ProductImageId) && productImageEntity.ProductImageId > cursor)
                .OrderBy(productImageEntity => productImageEntity.ProductImageId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<ProductImage> ExplicitLoadAsync(ProductImage entity, ProductImageNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductBarcodeNavigation)
                await _context.Entry(entity).Reference(productImageEntity => productImageEntity.ProductBarcodeNavigation).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<ProductImage>> ExplicitLoadAsync(IEnumerable<ProductImage> entities, ProductImageNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Product> products = [];
            List<string> productBarcodes = entities.Select(productImageItem => productImageItem.ProductBarcode).Distinct().ToList();

            if (options.IsGetProductBarcodeNavigation)
                products = await _context.Products.Where(productEntity => productBarcodes.Contains(productEntity.ProductBarcode)).ToListAsync(cancellationToken);

            return MappingToProductImages(entities, products);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<ProductImage> ApplyNavigationOptions(IQueryable<ProductImage> query, ProductImageNavigationOptions options)
        {
            if (options.IsGetProductBarcodeNavigation)
                query = query.Include(productImageEntity => productImageEntity.ProductBarcodeNavigation);

            return query;
        }

        private static IEnumerable<ProductImage> MappingToProductImages(IEnumerable<ProductImage> productImages, List<Product> products)
        {
            if (productImages == null || !productImages.Any())
                return [];

            Dictionary<string, Product> productsDict = [];

            if (products != null && products.Count > 0)
                productsDict = products.ToDictionary(productEntity => productEntity.ProductBarcode);

            foreach (ProductImage pi in productImages)
            {
                if (productsDict.TryGetValue(pi.ProductBarcode, out Product? product))
                    pi.ProductBarcodeNavigation = product ?? new();
            }

            return productImages;
        }

        #endregion
    }
}
