using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Application.Services.Product
{
    public class BaseReturnProductService : IBaseGetNavigationPropertyServices<ProductModel, ProductNavigationOptions>
    {
        private readonly ILogService _logger;
        private readonly IDAO<SupplierModel> _baseSupplierDAO;
        private readonly IDetailCartDAO<DetailCartModel> _detailCartDAO;
        private readonly IProductImageDAO<ProductImageModel> _productImageDAO;
        private readonly IDetailInvoiceDAO<DetailInvoiceModel> _detailInvoiceDAO;
        private readonly IDisposeProductDAO<DisposeProductModel> _disposeProductDAO;
        private readonly IDetailSaleEventDAO<DetailSaleEventModel> _detailSaleEventDAO;
        private readonly IDetailInventoryDAO<DetailInventoryModel> _detailInventoryDAO;
        private readonly IServiceResultFactory<BaseReturnProductService> _serviceResultFactory;
        private readonly IBaseHelperReturnTEntityService<BaseReturnProductService> _baseHelperReturnTEntityService;
        private readonly IDetailInventoryMovementDAO<DetailInventoryMovementModel> _detailInventoryMovementDAO;
        private readonly IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> _detailProductLotDAO;
        private readonly IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> _productCategoriesDAO;

        public BaseReturnProductService(IDAO<SupplierModel> baseSupplierDAO, IDetailCartDAO<DetailCartModel> detailCartDAO,
            IDisposeProductDAO<DisposeProductModel> disposeProductDAO,
            IDetailInventoryDAO<DetailInventoryModel> detailInventoryDAO,
            IBaseHelperReturnTEntityService<BaseReturnProductService> baseHelperReturnTEntityService,
            IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> detailProductLotDAO, IProductImageDAO<ProductImageModel> productImageDAO, IDetailSaleEventDAO<DetailSaleEventModel> detailSaleEventDAO,
            IDetailInvoiceDAO<DetailInvoiceModel> detailInvoiceDAO, IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> productCategoriesDAO,
            ILogService logger, IServiceResultFactory<BaseReturnProductService> serviceResultFactory,
            IDetailInventoryMovementDAO<DetailInventoryMovementModel> detailInventoryMovementDAO)
        {
            _logger = logger;
            _detailCartDAO = detailCartDAO;
            _baseSupplierDAO = baseSupplierDAO;
            _productImageDAO = productImageDAO;
            _detailInvoiceDAO = detailInvoiceDAO;
            _disposeProductDAO = disposeProductDAO;
            _detailSaleEventDAO = detailSaleEventDAO;
            _detailInventoryDAO = detailInventoryDAO;
            _detailProductLotDAO = detailProductLotDAO;
            _serviceResultFactory = serviceResultFactory;
            _productCategoriesDAO = productCategoriesDAO;
            _detailInventoryMovementDAO = detailInventoryMovementDAO;
            _baseHelperReturnTEntityService = baseHelperReturnTEntityService;
        }

        // --- SINGLE PRODUCT ---
        public async Task<ServiceResult<ProductModel>> GetNavigationPropertyByOptionsAsync(ProductModel product, ProductNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetSupplier)
            {
                await LoadSupplierAsync(product, logEntries);
                isSuccess = ValidateSupplierLoaded(product, isSuccess);
            }

            if (options.IsGetProductCategories)
            {
                await LoadProductCategoriesAsync(product, logEntries);
                isSuccess = ValidateProductCategoriesLoaded(product, isSuccess);
            }

            if (options.IsGetDetailCarts)
            {
                await LoadDetailCartsAsync(product, logEntries);
                isSuccess = ValidateDetailCartsLoaded(product, isSuccess);
            }

            if (options.IsGetDetailProductLots)
            {
                await LoadDetailProductLotsAsync(product, logEntries);
                isSuccess = ValidateDetailProductLotsLoaded(product, isSuccess);
            }

            if (options.IsGetProductImages)
            {
                await LoadProductImagesAsync(product, logEntries);
                isSuccess = ValidateProductImagesLoaded(product, isSuccess);
            }

            if (options.IsGetDetailSaleEvents)
            {
                await LoadDetailSaleEventsAsync(product, logEntries);
                isSuccess = ValidateDetailSaleEventsLoaded(product, isSuccess);
            }

            if (options.IsGetDetailInvoices)
            {
                await LoadDetailInvoicesAsync(product, logEntries);
                isSuccess = ValidateDetailInvoicesLoaded(product, isSuccess);
            }

            if (options.IsGetDisposeProducts)
            {
                await LoadDisposeProductsAsync(product, logEntries);
                isSuccess = ValidateDisposeProductsLoaded(product, isSuccess);
            }

            if (options.IsGetDetailInventories)
            {
                await LoadDetailInventoriesAsync(product, logEntries);
                isSuccess = ValidateDetailInventoriesLoaded(product, isSuccess);
            }

            if (options.IsGetDetailInventoryMovements)
            {
                await LoadDetailInventoryMovementsAsync(product, logEntries);
                isSuccess = ValidateDetailInventoryMovementsLoaded(product, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "product");
            return _serviceResultFactory.CreateServiceResult(product, logEntries, isSuccess);
        }

        // --- MULTIPLE PRODUCTS ---
        public async Task<ServiceResults<ProductModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<ProductModel> products, ProductNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var productList = products.ToList();
            List<JsonLogEntry> logEntries = [];
            bool isSuccess = true;

            if (options.IsGetSupplier)
            {
                await LoadSuppliersAsync(productList, logEntries);
                isSuccess = ValidateSuppliersLoaded(productList, isSuccess);
            }

            if (options.IsGetProductCategories)
            {
                await LoadProductCategoriesAsync(productList, logEntries);
                isSuccess = ValidateProductCategoriesLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailCarts)
            {
                await LoadDetailCartsAsync(productList, logEntries);
                isSuccess = ValidateDetailCartsLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailProductLots)
            {
                await LoadDetailProductLotsAsync(productList, logEntries);
                isSuccess = ValidateDetailProductLotsLoaded(productList, isSuccess);
            }

            if (options.IsGetProductImages)
            {
                await LoadProductImagesAsync(productList, logEntries);
                isSuccess = ValidateProductImagesLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailSaleEvents)
            {
                await LoadDetailSaleEventsAsync(productList, logEntries);
                isSuccess = ValidateDetailSaleEventsLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailInvoices)
            {
                await LoadDetailInvoicesAsync(productList, logEntries);
                isSuccess = ValidateDetailInvoicesLoaded(productList, isSuccess);
            }

            if (options.IsGetDisposeProducts)
            {
                await LoadDisposeProductsAsync(productList, logEntries);
                isSuccess = ValidateDisposeProductsLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailInventories)
            {
                await LoadDetailInventoriesAsync(productList, logEntries);
                isSuccess = ValidateDetailInventoriesLoaded(productList, isSuccess);
            }

            if (options.IsGetDetailInventoryMovements)
            {
                await LoadDetailInventoryMovementsAsync(productList, logEntries);
                isSuccess = ValidateDetailInventoryMovementsLoaded(productList, isSuccess);
            }

            AddFinalLogEntry(logEntries, isSuccess, methodCall, "products");
            return _serviceResultFactory.CreateServiceResults(productList, logEntries, isSuccess);
        }

        // --- PRIVATE METHODS FOR SINGLE PRODUCT ---

        private async Task<ServiceResult<SupplierModel>> TryLoadSupplierAsync(uint supplierId)
            => await _baseHelperReturnTEntityService.TryLoadEntityAsync(supplierId,
                (id) => _baseSupplierDAO.GetSingleDataAsync(id.ToString()), () => new SupplierModel(), nameof(TryLoadSupplierAsync));

        private async Task<ServiceResults<ProductCategoriesModel>> TryLoadProductCategoriesAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _productCategoriesDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadProductCategoriesAsync));

        private async Task<ServiceResults<DetailCartModel>> TryLoadDetailCartsAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailCartDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailCartsAsync));

        private async Task<ServiceResults<DetailProductLotModel>> TryLoadDetailProductLotsAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailProductLotDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailProductLotsAsync));

        private async Task<ServiceResults<ProductImageModel>> TryLoadProductImagesAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _productImageDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadProductImagesAsync));

        private async Task<ServiceResults<DetailSaleEventModel>> TryLoadDetailSaleEventsAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailSaleEventDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailSaleEventsAsync));

        private async Task<ServiceResults<DetailInvoiceModel>> TryLoadDetailInvoicesAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailInvoiceDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailInvoicesAsync));

        private async Task<ServiceResults<DisposeProductModel>> TryLoadDisposeProductsAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _disposeProductDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDisposeProductsAsync));

        private async Task<ServiceResults<DetailInventoryModel>> TryLoadDetailInventoriesAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailInventoryDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailInventoriesAsync));

        private async Task<ServiceResults<DetailInventoryMovementModel>> TryLoadDetailInventoryMovementsAsync(string productBarcode)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(
                productBarcode, (barcode) => _detailInventoryMovementDAO.GetByProductBarcodeAsync(barcode), nameof(TryLoadDetailInventoryMovementsAsync));

        // --- PRIVATE METHODS FOR MULTIPLE PRODUCTS ---

        private async Task<IDictionary<uint, ServiceResult<SupplierModel>>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds)
            => await _baseHelperReturnTEntityService.TryLoadEntitiesAsync(supplierIds,
                (ids) => _baseSupplierDAO.GetByInputsAsync(ids.Select(id => id.ToString())),
                () => new SupplierModel(), (entity) => entity.SupplierId, nameof(TryLoadSuppliersAsync));

        private async Task<IDictionary<string, ServiceResults<ProductCategoriesModel>>> TryLoadProductCategoriesAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _productCategoriesDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadProductCategoriesAsync));

        private async Task<IDictionary<string, ServiceResults<DetailCartModel>>> TryLoadDetailCartsAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailCartDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailCartsAsync));

        private async Task<IDictionary<string, ServiceResults<DetailProductLotModel>>> TryLoadDetailProductLotsAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailProductLotDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailProductLotsAsync));

        private async Task<IDictionary<string, ServiceResults<ProductImageModel>>> TryLoadProductImagesAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _productImageDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadProductImagesAsync));

        private async Task<IDictionary<string, ServiceResults<DetailSaleEventModel>>> TryLoadDetailSaleEventsAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailSaleEventDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailSaleEventsAsync));

        private async Task<IDictionary<string, ServiceResults<DetailInvoiceModel>>> TryLoadDetailInvoicesAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailInvoiceDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailInvoicesAsync));

        private async Task<IDictionary<string, ServiceResults<DisposeProductModel>>> TryLoadDisposeProductsAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _disposeProductDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDisposeProductsAsync));

        private async Task<IDictionary<string, ServiceResults<DetailInventoryModel>>> TryLoadDetailInventoriesAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailInventoryDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailInventoriesAsync));

        private async Task<IDictionary<string, ServiceResults<DetailInventoryMovementModel>>> TryLoadDetailInventoryMovementsAsync(IEnumerable<string> productBarcodes)
            => await _baseHelperReturnTEntityService.TryLoadICollectionEntitiesAsync(productBarcodes,
                (barcodes) => _detailInventoryMovementDAO.GetByProductBarcodesAsync(barcodes),
                (entity) => entity.ProductBarcode, nameof(TryLoadDetailInventoryMovementsAsync));

        // --- DRY METHODS FOR SINGLE PRODUCT ---
        private async Task LoadSupplierAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntityAsync(supplier => product.Supplier = supplier,
                () => product.SupplierId, TryLoadSupplierAsync, logEntries, nameof(LoadSupplierAsync));

        private async Task LoadProductCategoriesAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(categories => product.ProductCategories = [.. categories],
                () => product.ProductBarcode, TryLoadProductCategoriesAsync, logEntries, nameof(LoadProductCategoriesAsync));

        private async Task LoadDetailCartsAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(carts => product.DetailCarts = [.. carts],
                () => product.ProductBarcode, TryLoadDetailCartsAsync, logEntries, nameof(LoadDetailCartsAsync));

        private async Task LoadDetailProductLotsAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(lots => product.DetailProductLot = [.. lots],
                () => product.ProductBarcode, TryLoadDetailProductLotsAsync, logEntries, nameof(LoadDetailProductLotsAsync));

        private async Task LoadProductImagesAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(images => product.ProductImages = [.. images],
                () => product.ProductBarcode, TryLoadProductImagesAsync, logEntries, nameof(LoadProductImagesAsync));

        private async Task LoadDetailSaleEventsAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(events => product.DetailSaleEvents = [.. events],
                () => product.ProductBarcode, TryLoadDetailSaleEventsAsync, logEntries, nameof(LoadDetailSaleEventsAsync));

        private async Task LoadDetailInvoicesAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(invoices => product.DetailInvoices = [.. invoices],
                () => product.ProductBarcode, TryLoadDetailInvoicesAsync, logEntries, nameof(LoadDetailInvoicesAsync));

        private async Task LoadDisposeProductsAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(disposes => product.DisposeProducts = [.. disposes],
                () => product.ProductBarcode, TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));

        private async Task LoadDetailInventoriesAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(inventories => product.DetailInventories = [.. inventories],
                () => product.ProductBarcode, TryLoadDetailInventoriesAsync, logEntries, nameof(LoadDetailInventoriesAsync));

        private async Task LoadDetailInventoryMovementsAsync(ProductModel product, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(movements => product.DetailInventoryMovements = [.. movements],
                () => product.ProductBarcode, TryLoadDetailInventoryMovementsAsync, logEntries, nameof(LoadDetailInventoryMovementsAsync));

        // --- DRY METHODS FOR MULTIPLE PRODUCTS ---
        private async Task LoadSuppliersAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadEntitiesAsync(products,
                (product, supplier) => product.Supplier = supplier,
                (product) => product.SupplierId,
                TryLoadSuppliersAsync, logEntries, nameof(LoadSuppliersAsync));

        private async Task LoadProductCategoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, categories) => product.ProductCategories = [.. categories],
                (product) => product.ProductBarcode,
                TryLoadProductCategoriesAsync, logEntries, nameof(LoadProductCategoriesAsync));

        private async Task LoadDetailCartsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, carts) => product.DetailCarts = [.. carts],
                (product) => product.ProductBarcode,
                TryLoadDetailCartsAsync, logEntries, nameof(LoadDetailCartsAsync));

        private async Task LoadDetailProductLotsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, lots) => product.DetailProductLot = [.. lots],
                (product) => product.ProductBarcode,
                TryLoadDetailProductLotsAsync, logEntries, nameof(LoadDetailProductLotsAsync));

        private async Task LoadProductImagesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, images) => product.ProductImages = [.. images],
                (product) => product.ProductBarcode,
                TryLoadProductImagesAsync, logEntries, nameof(LoadProductImagesAsync));

        private async Task LoadDetailSaleEventsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, events) => product.DetailSaleEvents = [.. events],
                (product) => product.ProductBarcode,
                TryLoadDetailSaleEventsAsync, logEntries, nameof(LoadDetailSaleEventsAsync));

        private async Task LoadDetailInvoicesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, invoices) => product.DetailInvoices = [.. invoices],
                (product) => product.ProductBarcode,
                TryLoadDetailInvoicesAsync, logEntries, nameof(LoadDetailInvoicesAsync));

        private async Task LoadDisposeProductsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, disposes) => product.DisposeProducts = [.. disposes],
                (product) => product.ProductBarcode,
                TryLoadDisposeProductsAsync, logEntries, nameof(LoadDisposeProductsAsync));

        private async Task LoadDetailInventoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, inventories) => product.DetailInventories = [.. inventories],
                (product) => product.ProductBarcode,
                TryLoadDetailInventoriesAsync, logEntries, nameof(LoadDetailInventoriesAsync));

        private async Task LoadDetailInventoryMovementsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
            => await _baseHelperReturnTEntityService.LoadICollectionEntitiesAsync(products,
                (product, movements) => product.DetailInventoryMovements = [.. movements],
                (product) => product.ProductBarcode,
                TryLoadDetailInventoryMovementsAsync, logEntries, nameof(LoadDetailInventoryMovementsAsync));

        // Validation helper methods for single entity
        private static bool ValidateSupplierLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.Supplier != null && product.Supplier.SupplierId != 0;

        private static bool ValidateProductCategoriesLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.ProductCategories != null && product.ProductCategories.Count > 0;

        private static bool ValidateDetailCartsLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailCarts != null && product.DetailCarts.Count > 0;

        private static bool ValidateDetailProductLotsLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailProductLot != null && product.DetailProductLot.Count > 0;

        private static bool ValidateProductImagesLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.ProductImages != null && product.ProductImages.Count > 0;

        private static bool ValidateDetailSaleEventsLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailSaleEvents != null && product.DetailSaleEvents.Count > 0;

        private static bool ValidateDetailInvoicesLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailInvoices != null && product.DetailInvoices.Count > 0;

        private static bool ValidateDisposeProductsLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DisposeProducts != null && product.DisposeProducts.Count > 0;

        private static bool ValidateDetailInventoriesLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailInventories != null && product.DetailInventories.Count > 0;

        private static bool ValidateDetailInventoryMovementsLoaded(ProductModel product, bool currentSuccess)
            => currentSuccess && product.DetailInventoryMovements != null && product.DetailInventoryMovements.Count > 0;

        // Validation helper methods for multiple entities
        private static bool ValidateSuppliersLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.Supplier == null || product.Supplier.SupplierId == 0);

        private static bool ValidateProductCategoriesLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.ProductCategories == null || product.ProductCategories.Count == 0);

        private static bool ValidateDetailCartsLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailCarts == null || product.DetailCarts.Count == 0);

        private static bool ValidateDetailProductLotsLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailProductLot == null || product.DetailProductLot.Count == 0);

        private static bool ValidateProductImagesLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.ProductImages == null || product.ProductImages.Count == 0);

        private static bool ValidateDetailSaleEventsLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailSaleEvents == null || product.DetailSaleEvents.Count == 0);

        private static bool ValidateDetailInvoicesLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailInvoices == null || product.DetailInvoices.Count == 0);

        private static bool ValidateDisposeProductsLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DisposeProducts == null || product.DisposeProducts.Count == 0);

        private static bool ValidateDetailInventoriesLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailInventories == null || product.DetailInventories.Count == 0);

        private static bool ValidateDetailInventoryMovementsLoaded(IEnumerable<ProductModel> products, bool currentSuccess)
            => currentSuccess && !products.Any(product => product.DetailInventoryMovements == null || product.DetailInventoryMovements.Count == 0);

        // Final log entry helper
        private void AddFinalLogEntry(List<JsonLogEntry> logEntries, bool isSuccess, string? methodCall, string entityName)
        {
            if (!isSuccess)
                logEntries.Add(_logger.JsonLogWarning<ProductModel, BaseReturnProductService>($"One or more navigation properties could not be loaded for {entityName}.", methodCall: methodCall));
            else
                logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Successfully retrieved {entityName} with navigation properties.", methodCall: methodCall));
        }
    }
}
