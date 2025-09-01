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
        private readonly IDAO<SupplierModel> _baseSupplierDAO;
        private readonly IDetailCartDAO<DetailCartModel> _detailCartDAO;
        private readonly IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> _detailProductLotDAO;
        private readonly IProductImageDAO<ProductImageModel> _productImageDAO;
        private readonly IDetailSaleEventDAO<DetailSaleEventModel> _detailSaleEventDAO;
        private readonly IDetailInvoiceDAO<DetailInvoiceModel> _detailInvoiceDAO;
        private readonly IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> _productCategoriesDAO;
        private readonly IDisposeProductDAO<DisposeProductModel> _disposeProductDAO;
        private readonly IDetailInventoryDAO<DetailInventoryModel> _detailInventoryDAO;
        private readonly IDetailInventoryMovementDAO<DetailInventoryMovementModel> _detailInventoryMovementDAO;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseReturnProductService> _serviceResultFactory;

        public BaseReturnProductService(IDAO<SupplierModel> baseSupplierDAO, IDetailCartDAO<DetailCartModel> detailCartDAO,
            IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> detailProductLotDAO, IProductImageDAO<ProductImageModel> productImageDAO, IDetailSaleEventDAO<DetailSaleEventModel> detailSaleEventDAO,
            IDetailInvoiceDAO<DetailInvoiceModel> detailInvoiceDAO, IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> productCategoriesDAO,
            ILogService logger, IServiceResultFactory<BaseReturnProductService> serviceResultFactory,
            IDisposeProductDAO<DisposeProductModel> disposeProductDAO,
            IDetailInventoryDAO<DetailInventoryModel> detailInventoryDAO,
            IDetailInventoryMovementDAO<DetailInventoryMovementModel> detailInventoryMovementDAO)
        {
            _logger = logger;
            _baseSupplierDAO = baseSupplierDAO;
            _detailCartDAO = detailCartDAO;
            _detailProductLotDAO = detailProductLotDAO;
            _productImageDAO = productImageDAO;
            _detailSaleEventDAO = detailSaleEventDAO;
            _detailInvoiceDAO = detailInvoiceDAO;
            _productCategoriesDAO = productCategoriesDAO;
            _serviceResultFactory = serviceResultFactory;
            _disposeProductDAO = disposeProductDAO;
            _detailInventoryDAO = detailInventoryDAO;
            _detailInventoryMovementDAO = detailInventoryMovementDAO;
        }

        // --- SINGLE PRODUCT ---
        public async Task<ServiceResult<ProductModel>> GetNavigationPropertyByOptionsAsync(ProductModel product, ProductNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            List<JsonLogEntry> logEntries = [];

            if (options.IsGetSupplier)
                await LoadSupplierAsync(product, logEntries);

            if (options.IsGetProductCategories)
                await LoadProductCategoriesAsync(product, logEntries);

            if (options.IsGetDetailCarts)
                await LoadDetailCartsAsync(product, logEntries);

            if (options.IsGetDetailProductLots)
                await LoadDetailProductLotsAsync(product, logEntries);

            if (options.IsGetProductImages)
                await LoadProductImagesAsync(product, logEntries);

            if (options.IsGetDetailSaleEvents)
                await LoadDetailSaleEventsAsync(product, logEntries);

            if (options.IsGetDetailInvoices)
                await LoadDetailInvoicesAsync(product, logEntries);

            if (options.IsGetDisposeProducts)
                await LoadDisposeProductsAsync(product, logEntries);

            if (options.IsGetDetailInventories)
                await LoadDetailInventoriesAsync(product, logEntries);

            if (options.IsGetDetailInventoryMovements)
                await LoadDetailInventoryMovementsAsync(product, logEntries);

            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for product.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult(product, logEntries);
        }

        // --- MULTIPLE PRODUCTS ---
        public async Task<ServiceResults<ProductModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<ProductModel> products, ProductNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var productList = products.ToList();
            List<JsonLogEntry> logEntries = [];

            if (options.IsGetSupplier)
                await LoadSuppliersAsync(productList, logEntries);

            if (options.IsGetProductCategories)
                await LoadProductCategoriesAsync(productList, logEntries);

            if (options.IsGetDetailCarts)
                await LoadDetailCartsAsync(productList, logEntries);

            if (options.IsGetDetailProductLots)
                await LoadDetailProductLotsAsync(productList, logEntries);

            if (options.IsGetProductImages)
                await LoadProductImagesAsync(productList, logEntries);

            if (options.IsGetDetailSaleEvents)
                await LoadDetailSaleEventsAsync(productList, logEntries);

            if (options.IsGetDetailInvoices)
                await LoadDetailInvoicesAsync(productList, logEntries);

            if (options.IsGetDisposeProducts)
                await LoadDisposeProductsAsync(productList, logEntries);

            if (options.IsGetDetailInventories)
                await LoadDetailInventoriesAsync(productList, logEntries);

            if (options.IsGetDetailInventoryMovements)
                await LoadDetailInventoryMovementsAsync(productList, logEntries);

            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for products.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(productList, logEntries);
        }

        // --- PRIVATE METHODS FOR SINGLE PRODUCT ---

        private async Task<ServiceResult<SupplierModel>> TryLoadSupplierAsync(uint supplierId)
        {
            try
            {
                SupplierModel? supplier = await _baseSupplierDAO.GetSingleDataAsync(supplierId.ToString());
                return supplier != null
                    ? _serviceResultFactory.CreateServiceResult<SupplierModel>($"Loaded supplier with ID: {supplierId}", supplier, true)
                    : _serviceResultFactory.CreateServiceResult<SupplierModel>($"No supplier found with ID: {supplierId}.", new SupplierModel(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<SupplierModel>($"Error loading supplier with ID: {supplierId}.", new SupplierModel(), false, ex);
            }
        }

        private async Task<ServiceResults<ProductCategoriesModel>> TryLoadProductCategoriesAsync(string productBarcode)
        {
            try
            {
                var categories = await _productCategoriesDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return categories.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {productBarcode}", categories, true)
                    : _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailCartModel>> TryLoadDetailCartsAsync(string productBarcode)
        {
            try
            {
                var detailCarts = await _detailCartDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailCarts.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {productBarcode}", detailCarts, true)
                    : _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailProductLotModel>> TryLoadDetailProductLotsAsync(string productBarcode)
        {
            try
            {
                var detailProductLots = await _detailProductLotDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailProductLots.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {productBarcode}", detailProductLots, true)
                    : _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<ProductImageModel>> TryLoadProductImagesAsync(string productBarcode)
        {
            try
            {
                var productImages = await _productImageDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return productImages.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product images for barcode: {productBarcode}", productImages, true)
                    : _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailSaleEventModel>> TryLoadDetailSaleEventsAsync(string productBarcode)
        {
            try
            {
                var detailSaleEvents = await _detailSaleEventDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailSaleEvents.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Loaded detail sale events for barcode: {productBarcode}", detailSaleEvents, true)
                    : _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailInvoiceModel>> TryLoadDetailInvoicesAsync(string productBarcode)
        {
            try
            {
                var detailInvoices = await _detailInvoiceDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInvoices.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoices for barcode: {productBarcode}", detailInvoices, true)
                    : _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DisposeProductModel>> TryLoadDisposeProductsAsync(string productBarcode)
        {
            try
            {
                var disposeProducts = await _disposeProductDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return disposeProducts.Any()
                    ? _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Loaded dispose products for barcode: {productBarcode}", disposeProducts, true)
                    : _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"No dispose products found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Error loading dispose products for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailInventoryModel>> TryLoadDetailInventoriesAsync(string productBarcode)
        {
            try
            {
                var detailInventories = await _detailInventoryDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInventories.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Loaded detail inventories for barcode: {productBarcode}", detailInventories, true)
                    : _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"No detail inventories found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Error loading detail inventories for barcode: {productBarcode}.", [], false, ex);
            }
        }

        private async Task<ServiceResults<DetailInventoryMovementModel>> TryLoadDetailInventoryMovementsAsync(string productBarcode)
        {
            try
            {
                var detailInventoryMovements = await _detailInventoryMovementDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInventoryMovements.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Loaded detail inventory movements for barcode: {productBarcode}", detailInventoryMovements, true)
                    : _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"No detail inventory movements found for barcode: {productBarcode}.", [], false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Error loading detail inventory movements for barcode: {productBarcode}.", [], false, ex);
            }
        }

        // --- PRIVATE METHODS FOR MULTIPLE PRODUCTS ---

        private async Task<IDictionary<uint, ServiceResult<SupplierModel>>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds)
        {
            uint firstId = supplierIds.FirstOrDefault();
            try
            {
                IEnumerable<SupplierModel> suppliers = await _baseSupplierDAO.GetByInputsAsync(supplierIds.Select(id => id.ToString()));
                if (suppliers == null || !suppliers.Any())
                    return new Dictionary<uint, ServiceResult<SupplierModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<SupplierModel>($"No suppliers found for the provided IDs.", new SupplierModel(), false)
                    };
                return suppliers.ToDictionary(supplier => supplier.SupplierId, supplier => _serviceResultFactory.CreateServiceResult<SupplierModel>($"Loaded supplier: {supplier.SupplierId}", supplier, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<SupplierModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<SupplierModel>($"Error loading suppliers for the provided IDs.", new SupplierModel(), false, ex)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<ProductCategoriesModel>>> TryLoadProductCategoriesAsyncs(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var categories = (await _productCategoriesDAO.GetByProductBarcodesAsync(productBarcodes));
                if (categories == null || !categories.Any())
                    return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for the provided barcodes.", [], false)
                    };
                return categories.GroupBy(category => category.ProductBarcode)
                                 .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {group.Key}", group, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcodes: {string.Join(", ", productBarcodes)}.", [], false, ex)
                };
            }

        }

        private async Task<IDictionary<string, ServiceResults<DetailCartModel>>> TryLoadDetailCartsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailCarts = (await _detailCartDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailCarts == null || !detailCarts.Any())
                    return new Dictionary<string, ServiceResults<DetailCartModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for the provided barcodes.", [], false)
                    };
                return detailCarts.GroupBy(cart => cart.ProductBarcode)
                                  .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {group.Key}", group, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<DetailCartModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcodes: {string.Join(", ", productBarcodes)}.", [], false, ex)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailProductLotModel>>> TryLoadDetailProductLotsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailProductLots = (await _detailProductLotDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailProductLots == null || !detailProductLots.Any())
                    return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for the provided barcodes.", [], false)
                    };
                return detailProductLots.GroupBy(lot => lot.ProductBarcode)
                                        .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<ProductImageModel>>> TryLoadProductImagesAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var productImages = (await _productImageDAO.GetByProductBarcodesAsync(productBarcodes));
                if (productImages == null || !productImages.Any())
                    return new Dictionary<string, ServiceResults<ProductImageModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for the provided barcodes.", [], false)
                    };
                return productImages.GroupBy(image => image.ProductBarcode)
                                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product image for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<ProductImageModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailSaleEventModel>>> TryLoadDetailSaleEventsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailSaleEvents = (await _detailSaleEventDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailSaleEvents == null || !detailSaleEvents.Any())
                    return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for the provided barcodes.", [], false)
                    };
                return detailSaleEvents.GroupBy(eventModel => eventModel.ProductBarcode)
                                       .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults($"Loaded detail sale events for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInvoiceModel>>> TryLoadDetailInvoicesAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInvoices = (await _detailInvoiceDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInvoices == null || !detailInvoices.Any())
                    return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for the provided barcodes.", [], false)
                    };
                return detailInvoices.GroupBy(invoice => invoice.ProductBarcode)
                                     .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoice for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DisposeProductModel>>> TryLoadDisposeProductsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var disposeProducts = (await _disposeProductDAO.GetByProductBarcodesAsync(productBarcodes));
                if (disposeProducts == null || !disposeProducts.Any())
                    return new Dictionary<string, ServiceResults<DisposeProductModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"No dispose products found for the provided barcodes.", [], false)
                    };
                return disposeProducts.GroupBy(dp => dp.ProductBarcode)
                                      .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Loaded dispose products for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DisposeProductModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Error loading dispose products for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInventoryModel>>> TryLoadDetailInventoriesAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInventories = (await _detailInventoryDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInventories == null || !detailInventories.Any())
                    return new Dictionary<string, ServiceResults<DetailInventoryModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"No detail inventories found for the provided barcodes.", [], false)
                    };
                return detailInventories.GroupBy(di => di.ProductBarcode)
                                       .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Loaded detail inventories for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInventoryModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Error loading detail inventories for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInventoryMovementModel>>> TryLoadDetailInventoryMovementsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInventoryMovements = (await _detailInventoryMovementDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInventoryMovements == null || !detailInventoryMovements.Any())
                    return new Dictionary<string, ServiceResults<DetailInventoryMovementModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"No detail inventory movements found for the provided barcodes.", [], false)
                    };
                return detailInventoryMovements.GroupBy(dim => dim.ProductBarcode)
                                              .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Loaded detail inventory movements for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInventoryMovementModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Error loading detail inventory movements for barcodes: {string.Join(", ", productBarcodes)}.", [], false)
                };
            }
        }

        // --- DRY METHODS FOR SINGLE PRODUCT ---
        private async Task LoadSupplierAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var result = await TryLoadSupplierAsync(product.SupplierId);
            logEntries.AddRange(result.LogEntries!);
            product.Supplier = result.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded supplier for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadProductCategoriesAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadProductCategoriesAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.ProductCategories = (ICollection<ProductCategoriesModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded product categories for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailCartsAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailCartsAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailCarts = (ICollection<DetailCartModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail carts for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailProductLotsAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailProductLotsAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailProductLot = (ICollection<DetailProductLotModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail product lots for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadProductImagesAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadProductImagesAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.ProductImages = (ICollection<ProductImageModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded product images for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailSaleEventsAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailSaleEventsAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail sale events for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailInvoicesAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailInvoicesAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInvoices = (ICollection<DetailInvoiceModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail invoices for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDisposeProductsAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDisposeProductsAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DisposeProducts = (ICollection<DisposeProductModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded dispose products for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailInventoriesAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailInventoriesAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInventories = (ICollection<DetailInventoryModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail inventories for product with barcode: {product.ProductBarcode}"));
        }

        private async Task LoadDetailInventoryMovementsAsync(ProductModel product, List<JsonLogEntry> logEntries)
        {
            var results = await TryLoadDetailInventoryMovementsAsync(product.ProductBarcode);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInventoryMovements = (ICollection<DetailInventoryMovementModel>)results.Data!;
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>($"Loaded detail inventory movements for product with barcode: {product.ProductBarcode}"));
        }

        // --- DRY METHODS FOR MULTIPLE PRODUCTS ---
        private async Task LoadSuppliersAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var supplierIds = products.Select(p => p.SupplierId).Distinct();
            var suppliers = await TryLoadSuppliersAsync(supplierIds);
            foreach (var product in products)
            {
                if (!suppliers.TryGetValue(product.SupplierId, out var serviceResult) || serviceResult == null || serviceResult.Data == null)
                {
                    product.Supplier = new();
                    continue;
                }
                logEntries.AddRange(serviceResult.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.Supplier = serviceResult.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded suppliers for products."));
        }

        private async Task LoadProductCategoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var productCategories = await TryLoadProductCategoriesAsyncs(productBarcodes);
            foreach (var product in products)
            {
                if (!productCategories.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.ProductCategories = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.ProductCategories = (ICollection<ProductCategoriesModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded product categories for products."));
        }

        private async Task LoadDetailCartsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailCarts = await TryLoadDetailCartsAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailCarts.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailCarts = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailCarts = (ICollection<DetailCartModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail carts for products."));
        }

        private async Task LoadDetailProductLotsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailProductLots = await TryLoadDetailProductLotsAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailProductLots.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailProductLot = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailProductLot = (ICollection<DetailProductLotModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail product lots for products."));
        }

        private async Task LoadProductImagesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var productImages = await TryLoadProductImagesAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!productImages.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.ProductImages = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.ProductImages = (ICollection<ProductImageModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded product images for products."));
        }

        private async Task LoadDetailSaleEventsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailSaleEvents = await TryLoadDetailSaleEventsAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailSaleEvents.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailSaleEvents = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail sale events for products."));
        }

        private async Task LoadDetailInvoicesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInvoices = await TryLoadDetailInvoicesAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailInvoices.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailInvoices = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailInvoices = (ICollection<DetailInvoiceModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail invoices for products."));
        }

        private async Task LoadDisposeProductsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var disposeProducts = await TryLoadDisposeProductsAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!disposeProducts.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DisposeProducts = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DisposeProducts = (ICollection<DisposeProductModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded dispose products for products."));
        }

        private async Task LoadDetailInventoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInventories = await TryLoadDetailInventoriesAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailInventories.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailInventories = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailInventories = (ICollection<DetailInventoryModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail inventories for products."));
        }

        private async Task LoadDetailInventoryMovementsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInventoryMovements = await TryLoadDetailInventoryMovementsAsync(productBarcodes);
            foreach (var product in products)
            {
                if (!detailInventoryMovements.TryGetValue(product.ProductBarcode, out var serviceResults) || serviceResults == null || serviceResults.Data == null)
                {
                    product.DetailInventoryMovements = [];
                    continue;
                }
                logEntries.AddRange(serviceResults.LogEntries ?? Enumerable.Empty<JsonLogEntry>());
                product.DetailInventoryMovements = (ICollection<DetailInventoryMovementModel>)serviceResults.Data;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Loaded detail inventory movements for products."));
        }
    }
}
