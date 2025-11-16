using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public partial class ProductRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<Product>(context, maxReturnRecordsRule, defaultPageSizeRule), IProductRepository
    {
        #region Query by ProductBarcode (PK)

        public async Task<Product?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(productEntity => productEntity.ProductBarcode == barcode, cancellationToken);

        public async Task<IEnumerable<Product>> GetByBarcodesAsync(IEnumerable<string> barcodes, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;
            List<string> barcodesList = [.. barcodes];

            return await _dbSet
                .Where(productEntity => barcodesList.Contains(productEntity.ProductBarcode) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by ProductName

        public async Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(productEntity => productEntity.ProductName == productName, cancellationToken);

        public async Task<IEnumerable<Product>> GetManyByNamesAsync(IEnumerable<string> productNames, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;
            List<string> namesList = productNames.ToList();

            return await _dbSet
                .Where(productEntity => namesList.Contains(productEntity.ProductName) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.ProductName.Contains(searchTerm) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Supplier

        public async Task<IEnumerable<Product>> GetBySupplierIdAsync(uint supplierId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.SupplierId == supplierId && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;
            List<uint> supplierIdsList = supplierIds.ToList();

            return await _dbSet
                .Where(productEntity => supplierIdsList.Contains(productEntity.SupplierId) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Country

        public async Task<IEnumerable<Product>> GetByCountryIdAsync(uint countryId, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.CountryId == countryId && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetByCountryIdsAsync(IEnumerable<uint> countryIds, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;
            List<uint> countryIdsList = [.. countryIds];

            return await _dbSet
                .Where(productEntity => countryIdsList.Contains(productEntity.CountryId) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by ProductType

        public async Task<IEnumerable<Product>> GetByTypeAsync(string productType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.ProductType == productType && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by ProductStatus

        public async Task<IEnumerable<Product>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.ProductStatus == status && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Price Range

        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;

            return await _dbSet
                .Where(productEntity => productEntity.ProductBasePrice >= minPrice && productEntity.ProductBasePrice <= maxPrice && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by ProductAddedDate

        public Task<IEnumerable<Product>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, productEntity => productEntity.ProductAddedDate, false, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query by ProductLastUpdatedDate

        public Task<IEnumerable<Product>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => GetByDateTimeRangeAsync(startDate, endDate, productEntity => productEntity.ProductLastUpdatedDate, false, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        // Product uses string barcode as PK
        public async Task<Product?> GetNavigationByIdAsync(string id, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Product> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(productEntity => productEntity.ProductBarcode == id, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetNavigationByIdsAsync(IEnumerable<string> ids, ProductNavigationOptions options, uint? fromRecord,
            uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            string cursorBarcode = fromRecord?.ToString() ?? string.Empty;
            List<string> barcodesList = [.. ids];

            IQueryable<Product> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(productEntity => barcodesList.Contains(productEntity.ProductBarcode) && string.Compare(productEntity.ProductBarcode, cursorBarcode) > 0)
                .OrderBy(productEntity => productEntity.ProductBarcode)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Explicit Load Methods

        #region Single Entity Explicit Load

        public async Task<Product> ExplicitLoadAsync(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            await ExplicitLoadOneToOneAsync(entity, options, cancellationToken);
            await ExplicitLoadOneToManyAsync(entity, options, cancellationToken);
            return entity;
        }

        #region Single Entity - One-to-One Relationships

        private async Task ExplicitLoadOneToOneAsync(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            await LoadCountryIfRequested(entity, options, cancellationToken);
            await LoadSupplierIfRequested(entity, options, cancellationToken);
            await LoadProductDrinkIfRequested(entity, options, cancellationToken);
            await LoadProductFruitIfRequested(entity, options, cancellationToken);
            await LoadProductMeatIfRequested(entity, options, cancellationToken);
            await LoadProductSnackIfRequested(entity, options, cancellationToken);
            await LoadProductVegetableIfRequested(entity, options, cancellationToken);
        }

        private async Task LoadCountryIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetCountry)
                await _context.Entry(entity).Reference(productEntity => productEntity.Country).LoadAsync(cancellationToken);
        }

        private async Task LoadSupplierIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetSupplier)
                await _context.Entry(entity).Reference(productEntity => productEntity.Supplier).LoadAsync(cancellationToken);
        }

        private async Task LoadProductDrinkIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductDrink)
                await _context.Entry(entity).Reference(productEntity => productEntity.ProductDrink).LoadAsync(cancellationToken);
        }

        private async Task LoadProductFruitIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductFruit)
                await _context.Entry(entity).Reference(productEntity => productEntity.ProductFruit).LoadAsync(cancellationToken);
        }

        private async Task LoadProductMeatIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductMeat)
                await _context.Entry(entity).Reference(productEntity => productEntity.ProductMeat).LoadAsync(cancellationToken);
        }

        private async Task LoadProductSnackIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductSnack)
                await _context.Entry(entity).Reference(productEntity => productEntity.ProductSnack).LoadAsync(cancellationToken);
        }

        private async Task LoadProductVegetableIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductVegetable)
                await _context.Entry(entity).Reference(productEntity => productEntity.ProductVegetable).LoadAsync(cancellationToken);
        }

        #endregion

        #region Single Entity - One-to-Many Relationships

        private async Task ExplicitLoadOneToManyAsync(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            await LoadDetailCartsIfRequested(entity, options, cancellationToken);
            await LoadDetailInventoriesIfRequested(entity, options, cancellationToken);
            await LoadDetailInventoryMovementsIfRequested(entity, options, cancellationToken);
            await LoadDetailInvoicesIfRequested(entity, options, cancellationToken);
            await LoadDetailSaleEventsIfRequested(entity, options, cancellationToken);
            await LoadDisposeProductsIfRequested(entity, options, cancellationToken);
            await LoadProductCategoriesIfRequested(entity, options, cancellationToken);
            await LoadProductImagesIfRequested(entity, options, cancellationToken);
        }

        private async Task LoadDetailCartsIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDetailCarts)
                await _context.Entry(entity).Collection(productEntity => productEntity.DetailCarts).LoadAsync(cancellationToken);
        }

        private async Task LoadDetailInventoriesIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDetailInventories)
                await _context.Entry(entity).Collection(productEntity => productEntity.DetailInventories).LoadAsync(cancellationToken);
        }

        private async Task LoadDetailInventoryMovementsIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDetailInventoryMovements)
                await _context.Entry(entity).Collection(productEntity => productEntity.DetailInventoryMovements).LoadAsync(cancellationToken);
        }

        private async Task LoadDetailInvoicesIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDetailInvoices)
                await _context.Entry(entity).Collection(productEntity => productEntity.DetailInvoices).LoadAsync(cancellationToken);
        }

        private async Task LoadDetailSaleEventsIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDetailSaleEvents)
                await _context.Entry(entity).Collection(productEntity => productEntity.DetailSaleEvents).LoadAsync(cancellationToken);
        }

        private async Task LoadDisposeProductsIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetDisposeProducts)
                await _context.Entry(entity).Collection(productEntity => productEntity.DisposeProducts).LoadAsync(cancellationToken);
        }

        private async Task LoadProductCategoriesIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductCategories)
                await _context.Entry(entity).Collection(productEntity => productEntity.ProductCategories).LoadAsync(cancellationToken);
        }

        private async Task LoadProductImagesIfRequested(Product entity, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetProductImages)
                await _context.Entry(entity).Collection(productEntity => productEntity.ProductImages).LoadAsync(cancellationToken);
        }

        #endregion

        #endregion

        #region Bulk Entities Explicit Load

        public async Task<IEnumerable<Product>> ExplicitLoadAsync(IEnumerable<Product> entities, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            List<string> productBarcodes = [.. entities.Select(productEntity => productEntity.ProductBarcode).Distinct()];

            (Dictionary<string, Country>? countriesDict, Dictionary<string, Supplier>? suppliersDict, Dictionary<string, ProductDrink>? productDrinksDict, Dictionary<string, ProductFruit>? productFruitsDict,
                Dictionary<string, ProductMeat>? productMeatsDict, Dictionary<string, ProductSnack>? productSnacksDict, Dictionary<string, ProductVegetable>? productVegetablesDict) =
                await ExplicitLoadOneToOneAsync(entities, productBarcodes, options, cancellationToken);

            (ILookup<string, DetailCart>? detailCartsLookup, ILookup<string, DetailInventory>? detailInventoriesLookup, ILookup<string, DetailInventoryMovement>? detailInventoryMovementsLookup, ILookup<string, DetailInvoice>? detailInvoicesLookup,
                ILookup<string, DetailSaleEvent>? detailSaleEventsLookup, ILookup<string, DisposeProduct>? disposeProductsLookup, ILookup<string, ProductCategory>? productCategoriesLookup, ILookup<string, ProductImage>? productImagesLookup) =
                await ExplicitLoadOneToManyAsync(productBarcodes, options, cancellationToken);

            MappingOneToOneToProducts(entities, countriesDict, suppliersDict, productDrinksDict, productFruitsDict, productMeatsDict, productSnacksDict, productVegetablesDict);
            MappingOneToManyToProducts(entities, detailCartsLookup, detailInventoriesLookup, detailInventoryMovementsLookup, detailInvoicesLookup, detailSaleEventsLookup, disposeProductsLookup, productCategoriesLookup, productImagesLookup);

            return entities;
        }

        #region Bulk Load - One-to-One Relationships

        private async Task<(Dictionary<string, Country>?, Dictionary<string, Supplier>?, Dictionary<string, ProductDrink>?, Dictionary<string, ProductFruit>?,
            Dictionary<string, ProductMeat>?, Dictionary<string, ProductSnack>?, Dictionary<string, ProductVegetable>?)>
            ExplicitLoadOneToOneAsync(IEnumerable<Product> entities, List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            Dictionary<string, Country>? countriesDict = await LoadCountriesDictionaryIfRequested(entities, options, cancellationToken);
            Dictionary<string, Supplier>? suppliersDict = await LoadSuppliersDictionaryIfRequested(entities, options, cancellationToken);
            Dictionary<string, ProductDrink>? productDrinksDict = await LoadProductDrinksDictionaryIfRequested(productBarcodes, options, cancellationToken);
            Dictionary<string, ProductFruit>? productFruitsDict = await LoadProductFruitsDictionaryIfRequested(productBarcodes, options, cancellationToken);
            Dictionary<string, ProductMeat>? productMeatsDict = await LoadProductMeatsDictionaryIfRequested(productBarcodes, options, cancellationToken);
            Dictionary<string, ProductSnack>? productSnacksDict = await LoadProductSnacksDictionaryIfRequested(productBarcodes, options, cancellationToken);
            Dictionary<string, ProductVegetable>? productVegetablesDict = await LoadProductVegetablesDictionaryIfRequested(productBarcodes, options, cancellationToken);

            return (countriesDict, suppliersDict, productDrinksDict, productFruitsDict, productMeatsDict, productSnacksDict, productVegetablesDict);
        }

        private async Task<Dictionary<string, Country>?> LoadCountriesDictionaryIfRequested(IEnumerable<Product> entities, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetCountry)
                return null;

            List<uint> countryIds = entities.Select(productItem => productItem.CountryId).Distinct().ToList();
            List<Country> countries = await _context.Countries
                .Where(countryEntity => countryIds.Contains(countryEntity.CountryId))
                .ToListAsync(cancellationToken);
            return entities.ToDictionary(productItem => productItem.ProductBarcode, productItem => countries.First(countryEntity => countryEntity.CountryId == productItem.CountryId));
        }

        private async Task<Dictionary<string, Supplier>?> LoadSuppliersDictionaryIfRequested(IEnumerable<Product> entities, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetSupplier)
                return null;

            List<uint> supplierIds = entities.Select(productItem => productItem.SupplierId).Distinct().ToList();
            List<Supplier> suppliers = await _context.Suppliers
                .Where(supplierEntity => supplierIds.Contains(supplierEntity.SupplierId))
                .ToListAsync(cancellationToken);
            return entities.ToDictionary(productItem => productItem.ProductBarcode, productItem => suppliers.First(supplierEntity => supplierEntity.SupplierId == productItem.SupplierId));
        }

        private async Task<Dictionary<string, ProductDrink>?> LoadProductDrinksDictionaryIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductDrink)
                return null;

            List<ProductDrink> productDrinks = await _context.ProductDrinks
                .Where(productDrinkEntity => productBarcodes.Contains(productDrinkEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productDrinks.ToDictionary(productDrinkEntity => productDrinkEntity.ProductBarcode);
        }

        private async Task<Dictionary<string, ProductFruit>?> LoadProductFruitsDictionaryIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductFruit)
                return null;

            List<ProductFruit> productFruits = await _context.ProductFruits
                .Where(productFruitEntity => productBarcodes.Contains(productFruitEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productFruits.ToDictionary(productFruitEntity => productFruitEntity.ProductBarcode);
        }

        private async Task<Dictionary<string, ProductMeat>?> LoadProductMeatsDictionaryIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductMeat)
                return null;

            List<ProductMeat> productMeats = await _context.ProductMeats
                .Where(productMeatEntity => productBarcodes.Contains(productMeatEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productMeats.ToDictionary(productMeatEntity => productMeatEntity.ProductBarcode);
        }

        private async Task<Dictionary<string, ProductSnack>?> LoadProductSnacksDictionaryIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductSnack)
                return null;

            List<ProductSnack> productSnacks = await _context.ProductSnacks
                .Where(productSnackEntity => productBarcodes.Contains(productSnackEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productSnacks.ToDictionary(productSnackEntity => productSnackEntity.ProductBarcode);
        }

        private async Task<Dictionary<string, ProductVegetable>?> LoadProductVegetablesDictionaryIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductVegetable)
                return null;

            List<ProductVegetable> productVegetables = await _context.ProductVegetables
                .Where(productVegetableEntity => productBarcodes.Contains(productVegetableEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productVegetables.ToDictionary(productVegetableEntity => productVegetableEntity.ProductBarcode);
        }

        #endregion

        #region Bulk Load - One-to-Many Relationships

        private async Task<(ILookup<string, DetailCart>?, ILookup<string, DetailInventory>?, ILookup<string, DetailInventoryMovement>?, ILookup<string, DetailInvoice>?,
            ILookup<string, DetailSaleEvent>?, ILookup<string, DisposeProduct>?, ILookup<string, ProductCategory>?, ILookup<string, ProductImage>?)>
            ExplicitLoadOneToManyAsync(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            ILookup<string, DetailCart>? detailCartsLookup = await LoadDetailCartsLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, DetailInventory>? detailInventoriesLookup = await LoadDetailInventoriesLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, DetailInventoryMovement>? detailInventoryMovementsLookup = await LoadDetailInventoryMovementsLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, DetailInvoice>? detailInvoicesLookup = await LoadDetailInvoicesLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, DetailSaleEvent>? detailSaleEventsLookup = await LoadDetailSaleEventsLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, DisposeProduct>? disposeProductsLookup = await LoadDisposeProductsLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, ProductCategory>? productCategoriesLookup = await LoadProductCategoriesLookupIfRequested(productBarcodes, options, cancellationToken);
            ILookup<string, ProductImage>? productImagesLookup = await LoadProductImagesLookupIfRequested(productBarcodes, options, cancellationToken);

            return (detailCartsLookup, detailInventoriesLookup, detailInventoryMovementsLookup, detailInvoicesLookup, detailSaleEventsLookup, disposeProductsLookup, productCategoriesLookup, productImagesLookup);
        }

        private async Task<ILookup<string, DetailCart>?> LoadDetailCartsLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDetailCarts)
                return null;

            List<DetailCart> detailCarts = await _context.DetailCarts
                .Where(detailCartEntity => productBarcodes.Contains(detailCartEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return detailCarts.ToLookup(detailCartEntity => detailCartEntity.ProductBarcode);
        }

        private async Task<ILookup<string, DetailInventory>?> LoadDetailInventoriesLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDetailInventories)
                return null;

            List<DetailInventory> detailInventories = await _context.DetailInventories
                .Where(detailInventoryEntity => productBarcodes.Contains(detailInventoryEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return detailInventories.ToLookup(detailInventoryEntity => detailInventoryEntity.ProductBarcode);
        }

        private async Task<ILookup<string, DetailInventoryMovement>?> LoadDetailInventoryMovementsLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDetailInventoryMovements)
                return null;

            List<DetailInventoryMovement> detailInventoryMovements = await _context.DetailInventoryMovements
                .Where(detailInventoryMovementEntity => productBarcodes.Contains(detailInventoryMovementEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return detailInventoryMovements.ToLookup(detailInventoryMovementEntity => detailInventoryMovementEntity.ProductBarcode);
        }

        private async Task<ILookup<string, DetailInvoice>?> LoadDetailInvoicesLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDetailInvoices)
                return null;

            List<DetailInvoice> detailInvoices = await _context.DetailInvoices
                .Where(detailInvoiceEntity => productBarcodes.Contains(detailInvoiceEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return detailInvoices.ToLookup(detailInvoiceEntity => detailInvoiceEntity.ProductBarcode);
        }

        private async Task<ILookup<string, DetailSaleEvent>?> LoadDetailSaleEventsLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDetailSaleEvents)
                return null;

            List<DetailSaleEvent> detailSaleEvents = await _context.DetailSaleEvents
                .Where(detailSaleEventEntity => productBarcodes.Contains(detailSaleEventEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return detailSaleEvents.ToLookup(detailSaleEventEntity => detailSaleEventEntity.ProductBarcode);
        }

        private async Task<ILookup<string, DisposeProduct>?> LoadDisposeProductsLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetDisposeProducts)
                return null;

            List<DisposeProduct> disposeProducts = await _context.DisposeProducts
                .Where(disposeProductEntity => productBarcodes.Contains(disposeProductEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return disposeProducts.ToLookup(disposeProductEntity => disposeProductEntity.ProductBarcode);
        }

        private async Task<ILookup<string, ProductCategory>?> LoadProductCategoriesLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductCategories)
                return null;

            List<ProductCategory> productCategories = await _context.ProductCategories
                .Where(productCategoryEntity => productBarcodes.Contains(productCategoryEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productCategories.ToLookup(productCategoryEntity => productCategoryEntity.ProductBarcode);
        }

        private async Task<ILookup<string, ProductImage>?> LoadProductImagesLookupIfRequested(List<string> productBarcodes, ProductNavigationOptions options, CancellationToken cancellationToken)
        {
            if (!options.IsGetProductImages)
                return null;

            List<ProductImage> productImages = await _context.ProductImages
                .Where(productImageEntity => productBarcodes.Contains(productImageEntity.ProductBarcode))
                .ToListAsync(cancellationToken);
            return productImages.ToLookup(productImageEntity => productImageEntity.ProductBarcode);
        }

        #endregion

        #endregion

        #endregion

        #region Apply Navigation Options Methods

        private static IQueryable<Product> ApplyNavigationOptions(IQueryable<Product> query, ProductNavigationOptions options)
        {
            query = ApplyOneToOneNavigationOptions(query, options);
            query = ApplyOneToManyNavigationOptions(query, options);
            return query;
        }

        #region Apply Navigation - One-to-One Relationships

        private static IQueryable<Product> ApplyOneToOneNavigationOptions(IQueryable<Product> query, ProductNavigationOptions options)
        {
            query = IncludeCountryIfRequested(query, options);
            query = IncludeSupplierIfRequested(query, options);
            query = IncludeProductDrinkIfRequested(query, options);
            query = IncludeProductFruitIfRequested(query, options);
            query = IncludeProductMeatIfRequested(query, options);
            query = IncludeProductSnackIfRequested(query, options);
            query = IncludeProductVegetableIfRequested(query, options);
            return query;
        }

        private static IQueryable<Product> IncludeCountryIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetCountry)
                query = query.Include(productEntity => productEntity.Country);
            return query;
        }

        private static IQueryable<Product> IncludeSupplierIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetSupplier)
                query = query.Include(productEntity => productEntity.Supplier);
            return query;
        }

        private static IQueryable<Product> IncludeProductDrinkIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductDrink)
                query = query.Include(productEntity => productEntity.ProductDrink);
            return query;
        }

        private static IQueryable<Product> IncludeProductFruitIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductFruit)
                query = query.Include(productEntity => productEntity.ProductFruit);
            return query;
        }

        private static IQueryable<Product> IncludeProductMeatIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductMeat)
                query = query.Include(productEntity => productEntity.ProductMeat);
            return query;
        }

        private static IQueryable<Product> IncludeProductSnackIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductSnack)
                query = query.Include(productEntity => productEntity.ProductSnack);
            return query;
        }

        private static IQueryable<Product> IncludeProductVegetableIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductVegetable)
                query = query.Include(productEntity => productEntity.ProductVegetable);
            return query;
        }

        #endregion

        #region Apply Navigation - One-to-Many Relationships

        private static IQueryable<Product> ApplyOneToManyNavigationOptions(IQueryable<Product> query, ProductNavigationOptions options)
        {
            query = IncludeDetailCartsIfRequested(query, options);
            query = IncludeDetailInventoriesIfRequested(query, options);
            query = IncludeDetailInventoryMovementsIfRequested(query, options);
            query = IncludeDetailInvoicesIfRequested(query, options);
            query = IncludeDetailSaleEventsIfRequested(query, options);
            query = IncludeDisposeProductsIfRequested(query, options);
            query = IncludeProductCategoriesIfRequested(query, options);
            query = IncludeProductImagesIfRequested(query, options);
            return query;
        }

        private static IQueryable<Product> IncludeDetailCartsIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDetailCarts)
                query = query.Include(productEntity => productEntity.DetailCarts);
            return query;
        }

        private static IQueryable<Product> IncludeDetailInventoriesIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDetailInventories)
                query = query.Include(productEntity => productEntity.DetailInventories);
            return query;
        }

        private static IQueryable<Product> IncludeDetailInventoryMovementsIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDetailInventoryMovements)
                query = query.Include(productEntity => productEntity.DetailInventoryMovements);
            return query;
        }

        private static IQueryable<Product> IncludeDetailInvoicesIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDetailInvoices)
                query = query.Include(productEntity => productEntity.DetailInvoices);
            return query;
        }

        private static IQueryable<Product> IncludeDetailSaleEventsIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDetailSaleEvents)
                query = query.Include(productEntity => productEntity.DetailSaleEvents);
            return query;
        }

        private static IQueryable<Product> IncludeDisposeProductsIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetDisposeProducts)
                query = query.Include(productEntity => productEntity.DisposeProducts);
            return query;
        }

        private static IQueryable<Product> IncludeProductCategoriesIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductCategories)
                query = query.Include(productEntity => productEntity.ProductCategories);
            return query;
        }

        private static IQueryable<Product> IncludeProductImagesIfRequested(IQueryable<Product> query, ProductNavigationOptions options)
        {
            if (options.IsGetProductImages)
                query = query.Include(productEntity => productEntity.ProductImages);
            return query;
        }

        #endregion

        #endregion

        #region Mapping Methods

        #region Mapping - One-to-One Relationships

        private static void MappingOneToOneToProducts(IEnumerable<Product> products,
            Dictionary<string, Country>? countriesDict, Dictionary<string, Supplier>? suppliersDict,
            Dictionary<string, ProductDrink>? productDrinksDict, Dictionary<string, ProductFruit>? productFruitsDict,
            Dictionary<string, ProductMeat>? productMeatsDict, Dictionary<string, ProductSnack>? productSnacksDict,
            Dictionary<string, ProductVegetable>? productVegetablesDict)
        {
            if (products == null || !products.Any())
                return;

            foreach (Product product in products)
            {
                MapCountryToProduct(product, countriesDict);
                MapSupplierToProduct(product, suppliersDict);
                MapProductDrinkToProduct(product, productDrinksDict);
                MapProductFruitToProduct(product, productFruitsDict);
                MapProductMeatToProduct(product, productMeatsDict);
                MapProductSnackToProduct(product, productSnacksDict);
                MapProductVegetableToProduct(product, productVegetablesDict);
            }
        }

        private static void MapCountryToProduct(Product product, Dictionary<string, Country>? countriesDict)
        {
            if (countriesDict != null && countriesDict.TryGetValue(product.ProductBarcode, out Country? country))
                product.Country = country;
        }

        private static void MapSupplierToProduct(Product product, Dictionary<string, Supplier>? suppliersDict)
        {
            if (suppliersDict != null && suppliersDict.TryGetValue(product.ProductBarcode, out Supplier? supplier))
                product.Supplier = supplier;
        }

        private static void MapProductDrinkToProduct(Product product, Dictionary<string, ProductDrink>? productDrinksDict)
        {
            if (productDrinksDict != null && productDrinksDict.TryGetValue(product.ProductBarcode, out ProductDrink? productDrink))
                product.ProductDrink = productDrink;
        }

        private static void MapProductFruitToProduct(Product product, Dictionary<string, ProductFruit>? productFruitsDict)
        {
            if (productFruitsDict != null && productFruitsDict.TryGetValue(product.ProductBarcode, out ProductFruit? productFruit))
                product.ProductFruit = productFruit;
        }

        private static void MapProductMeatToProduct(Product product, Dictionary<string, ProductMeat>? productMeatsDict)
        {
            if (productMeatsDict != null && productMeatsDict.TryGetValue(product.ProductBarcode, out ProductMeat? productMeat))
                product.ProductMeat = productMeat;
        }

        private static void MapProductSnackToProduct(Product product, Dictionary<string, ProductSnack>? productSnacksDict)
        {
            if (productSnacksDict != null && productSnacksDict.TryGetValue(product.ProductBarcode, out ProductSnack? productSnack))
                product.ProductSnack = productSnack;
        }

        private static void MapProductVegetableToProduct(Product product, Dictionary<string, ProductVegetable>? productVegetablesDict)
        {
            if (productVegetablesDict != null && productVegetablesDict.TryGetValue(product.ProductBarcode, out ProductVegetable? productVegetable))
                product.ProductVegetable = productVegetable;
        }

        #endregion

        #region Mapping - One-to-Many Relationships

        private static void MappingOneToManyToProducts(IEnumerable<Product> products,
            ILookup<string, DetailCart>? detailCartsLookup, ILookup<string, DetailInventory>? detailInventoriesLookup,
            ILookup<string, DetailInventoryMovement>? detailInventoryMovementsLookup, ILookup<string, DetailInvoice>? detailInvoicesLookup,
            ILookup<string, DetailSaleEvent>? detailSaleEventsLookup, ILookup<string, DisposeProduct>? disposeProductsLookup,
            ILookup<string, ProductCategory>? productCategoriesLookup, ILookup<string, ProductImage>? productImagesLookup)
        {
            if (products == null || !products.Any())
                return;

            foreach (Product product in products)
            {
                MapDetailCartsToProduct(product, detailCartsLookup);
                MapDetailInventoriesToProduct(product, detailInventoriesLookup);
                MapDetailInventoryMovementsToProduct(product, detailInventoryMovementsLookup);
                MapDetailInvoicesToProduct(product, detailInvoicesLookup);
                MapDetailSaleEventsToProduct(product, detailSaleEventsLookup);
                MapDisposeProductsToProduct(product, disposeProductsLookup);
                MapProductCategoriesToProduct(product, productCategoriesLookup);
                MapProductImagesToProduct(product, productImagesLookup);
            }
        }

        private static void MapDetailCartsToProduct(Product product, ILookup<string, DetailCart>? detailCartsLookup)
        {
            if (detailCartsLookup != null)
                product.DetailCarts = [.. detailCartsLookup[product.ProductBarcode]];
        }

        private static void MapDetailInventoriesToProduct(Product product, ILookup<string, DetailInventory>? detailInventoriesLookup)
        {
            if (detailInventoriesLookup != null)
                product.DetailInventories = [.. detailInventoriesLookup[product.ProductBarcode]];
        }

        private static void MapDetailInventoryMovementsToProduct(Product product, ILookup<string, DetailInventoryMovement>? detailInventoryMovementsLookup)
        {
            if (detailInventoryMovementsLookup != null)
                product.DetailInventoryMovements = [.. detailInventoryMovementsLookup[product.ProductBarcode]];
        }

        private static void MapDetailInvoicesToProduct(Product product, ILookup<string, DetailInvoice>? detailInvoicesLookup)
        {
            if (detailInvoicesLookup != null)
                product.DetailInvoices = [.. detailInvoicesLookup[product.ProductBarcode]];
        }

        private static void MapDetailSaleEventsToProduct(Product product, ILookup<string, DetailSaleEvent>? detailSaleEventsLookup)
        {
            if (detailSaleEventsLookup != null)
                product.DetailSaleEvents = [.. detailSaleEventsLookup[product.ProductBarcode]];
        }

        private static void MapDisposeProductsToProduct(Product product, ILookup<string, DisposeProduct>? disposeProductsLookup)
        {
            if (disposeProductsLookup != null)
                product.DisposeProducts = [.. disposeProductsLookup[product.ProductBarcode]];
        }

        private static void MapProductCategoriesToProduct(Product product, ILookup<string, ProductCategory>? productCategoriesLookup)
        {
            if (productCategoriesLookup != null)
                product.ProductCategories = [.. productCategoriesLookup[product.ProductBarcode]];
        }

        private static void MapProductImagesToProduct(Product product, ILookup<string, ProductImage>? productImagesLookup)
        {
            if (productImagesLookup != null)
                product.ProductImages = [.. productImagesLookup[product.ProductBarcode]];
        }

        #endregion

        #endregion
    }
}
