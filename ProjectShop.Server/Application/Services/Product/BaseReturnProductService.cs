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
    public class BaseReturnProductService : IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions>
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
                await LoadSupplierAsync(product, logEntries, methodCall);

            if (options.IsGetProductCategories)
                await LoadProductCategoriesAsync(product, logEntries, methodCall);

            if (options.IsGetDetailCarts)
                await LoadDetailCartsAsync(product, logEntries, methodCall);

            if (options.IsGetDetailProductLots)
                await LoadDetailProductLotsAsync(product, logEntries, methodCall);

            if (options.IsGetProductImages)
                await LoadProductImagesAsync(product, logEntries, methodCall);

            if (options.IsGetDetailSaleEvents)
                await LoadDetailSaleEventsAsync(product, logEntries, methodCall);

            if (options.IsGetDetailInvoices)
                await LoadDetailInvoicesAsync(product, logEntries, methodCall);

            if (options.IsGetDisposeProducts)
                await LoadDisposeProductsAsync(product, logEntries, methodCall);

            if (options.IsGetDetailInventories)
                await LoadDetailInventoriesAsync(product, logEntries, methodCall);

            if (options.IsGetDetailInventoryMovements)
                await LoadDetailInventoryMovementsAsync(product, logEntries, methodCall);

            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for product.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResult(product, logEntries);
        }

        // --- MULTIPLE PRODUCTS ---
        public async Task<ServiceResults<ProductModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<ProductModel> products, ProductNavigationOptions options, [CallerMemberName] string? methodCall = null)
        {
            var productList = products.ToList();
            List<JsonLogEntry> logEntries = [];

            if (options.IsGetSupplier)
                await LoadSuppliersAsync(productList, logEntries, methodCall);

            if (options.IsGetProductCategories)
                await LoadProductCategoriesAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailCarts)
                await LoadDetailCartsAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailProductLots)
                await LoadDetailProductLotsAsync(productList, logEntries, methodCall);

            if (options.IsGetProductImages)
                await LoadProductImagesAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailSaleEvents)
                await LoadDetailSaleEventsAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailInvoices)
                await LoadDetailInvoicesAsync(productList, logEntries, methodCall);

            if (options.IsGetDisposeProducts)
                await LoadDisposeProductsAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailInventories)
                await LoadDetailInventoriesAsync(productList, logEntries, methodCall);

            if (options.IsGetDetailInventoryMovements)
                await LoadDetailInventoryMovementsAsync(productList, logEntries, methodCall);

            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for products.", methodCall: methodCall));
            return _serviceResultFactory.CreateServiceResults(productList, logEntries);
        }

        // --- PRIVATE METHODS FOR SINGLE PRODUCT ---

        private async Task<ServiceResult<SupplierModel>> TryLoadSupplierAsync(uint supplierId, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                SupplierModel? supplier = await _baseSupplierDAO.GetSingleDataAsync(supplierId.ToString());
                return supplier != null
                    ? _serviceResultFactory.CreateServiceResult<SupplierModel>($"Loaded supplier with ID: {supplierId}", supplier, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResult<SupplierModel>($"No supplier found with ID: {supplierId}.", new SupplierModel(), false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResult<SupplierModel>($"Error loading supplier with ID: {supplierId}.", new SupplierModel(), false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<ProductCategoriesModel>> TryLoadProductCategoriesAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var categories = await _productCategoriesDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return categories.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {productBarcode}", categories, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailCartModel>> TryLoadDetailCartsAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailCarts = await _detailCartDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailCarts.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {productBarcode}", detailCarts, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailProductLotModel>> TryLoadDetailProductLotsAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailProductLots = await _detailProductLotDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailProductLots.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {productBarcode}", detailProductLots, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<ProductImageModel>> TryLoadProductImagesAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var productImages = await _productImageDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return productImages.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product images for barcode: {productBarcode}", productImages, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailSaleEventModel>> TryLoadDetailSaleEventsAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailSaleEvents = await _detailSaleEventDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailSaleEvents.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Loaded detail sale events for barcode: {productBarcode}", detailSaleEvents, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailInvoiceModel>> TryLoadDetailInvoicesAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailInvoices = await _detailInvoiceDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInvoices.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoices for barcode: {productBarcode}", detailInvoices, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DisposeProductModel>> TryLoadDisposeProductsAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var disposeProducts = await _disposeProductDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return disposeProducts.Any()
                    ? _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Loaded dispose products for barcode: {productBarcode}", disposeProducts, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"No dispose products found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Error loading dispose products for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailInventoryModel>> TryLoadDetailInventoriesAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailInventories = await _detailInventoryDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInventories.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Loaded detail inventories for barcode: {productBarcode}", detailInventories, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"No detail inventories found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Error loading detail inventories for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        private async Task<ServiceResults<DetailInventoryMovementModel>> TryLoadDetailInventoryMovementsAsync(string productBarcode, [CallerMemberName] string? methodCall = null)
        {
            try
            {
                var detailInventoryMovements = await _detailInventoryMovementDAO.GetByProductBarcodeAsync(productBarcode) ?? [];
                return detailInventoryMovements.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Loaded detail inventory movements for barcode: {productBarcode}", detailInventoryMovements, true, methodCall: methodCall)
                    : _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"No detail inventory movements found for barcode: {productBarcode}.", [], false, methodCall: methodCall);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Error loading detail inventory movements for barcode: {productBarcode}.", [], false, ex, methodCall: methodCall);
            }
        }

        // --- PRIVATE METHODS FOR MULTIPLE PRODUCTS ---

        private async Task<IDictionary<uint, ServiceResult<SupplierModel>>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds, [CallerMemberName] string? methodCall = null)
        {
            uint firstId = supplierIds.FirstOrDefault();
            try
            {
                IEnumerable<SupplierModel> suppliers = await _baseSupplierDAO.GetByInputsAsync(supplierIds.Select(id => id.ToString()));
                if (suppliers == null || !suppliers.Any())
                    return new Dictionary<uint, ServiceResult<SupplierModel>>
                    {
                        [firstId] = _serviceResultFactory.CreateServiceResult<SupplierModel>($"No suppliers found for the provided IDs.", new SupplierModel(), false, methodCall: methodCall)
                    };
                return suppliers.ToDictionary(supplier => supplier.SupplierId, supplier => _serviceResultFactory.CreateServiceResult<SupplierModel>($"Loaded supplier: {supplier.SupplierId}", supplier, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<uint, ServiceResult<SupplierModel>>
                {
                    [firstId] = _serviceResultFactory.CreateServiceResult<SupplierModel>($"Error loading suppliers for the provided IDs.", new SupplierModel(), false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<ProductCategoriesModel>>> TryLoadProductCategoriesAsyncs(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var categories = (await _productCategoriesDAO.GetByProductBarcodesAsync(productBarcodes));
                if (categories == null || !categories.Any())
                    return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return categories.GroupBy(category => category.ProductBarcode)
                                 .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcodes: {string.Join(", ", productBarcodes)}.", [], false, ex, methodCall: methodCall)
                };
            }

        }

        private async Task<IDictionary<string, ServiceResults<DetailCartModel>>> TryLoadDetailCartsAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailCarts = (await _detailCartDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailCarts == null || !detailCarts.Any())
                    return new Dictionary<string, ServiceResults<DetailCartModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailCarts.GroupBy(cart => cart.ProductBarcode)
                                  .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<DetailCartModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcodes: {string.Join(", ", productBarcodes)}.", [], false, ex, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailProductLotModel>>> TryLoadDetailProductLotsAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailProductLots = (await _detailProductLotDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailProductLots == null || !detailProductLots.Any())
                    return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailProductLots.GroupBy(lot => lot.ProductBarcode)
                                        .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<ProductImageModel>>> TryLoadProductImagesAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var productImages = (await _productImageDAO.GetByProductBarcodesAsync(productBarcodes));
                if (productImages == null || !productImages.Any())
                    return new Dictionary<string, ServiceResults<ProductImageModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return productImages.GroupBy(image => image.ProductBarcode)
                                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product image for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<ProductImageModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailSaleEventModel>>> TryLoadDetailSaleEventsAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailSaleEvents = (await _detailSaleEventDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailSaleEvents == null || !detailSaleEvents.Any())
                    return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailSaleEvents.GroupBy(eventModel => eventModel.ProductBarcode)
                                       .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults($"Loaded detail sale events for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInvoiceModel>>> TryLoadDetailInvoicesAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInvoices = (await _detailInvoiceDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInvoices == null || !detailInvoices.Any())
                    return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailInvoices.GroupBy(invoice => invoice.ProductBarcode)
                                     .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoice for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DisposeProductModel>>> TryLoadDisposeProductsAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var disposeProducts = (await _disposeProductDAO.GetByProductBarcodesAsync(productBarcodes));
                if (disposeProducts == null || !disposeProducts.Any())
                    return new Dictionary<string, ServiceResults<DisposeProductModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"No dispose products found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return disposeProducts.GroupBy(dp => dp.ProductBarcode)
                                      .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Loaded dispose products for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DisposeProductModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DisposeProductModel>($"Error loading dispose products for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInventoryModel>>> TryLoadDetailInventoriesAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInventories = (await _detailInventoryDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInventories == null || !detailInventories.Any())
                    return new Dictionary<string, ServiceResults<DetailInventoryModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"No detail inventories found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailInventories.GroupBy(di => di.ProductBarcode)
                                       .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Loaded detail inventories for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInventoryModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryModel>($"Error loading detail inventories for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInventoryMovementModel>>> TryLoadDetailInventoryMovementsAsync(IEnumerable<string> productBarcodes, [CallerMemberName] string? methodCall = null)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInventoryMovements = (await _detailInventoryMovementDAO.GetByProductBarcodesAsync(productBarcodes));
                if (detailInventoryMovements == null || !detailInventoryMovements.Any())
                    return new Dictionary<string, ServiceResults<DetailInventoryMovementModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"No detail inventory movements found for the provided barcodes.", [], false, methodCall: methodCall)
                    };
                return detailInventoryMovements.GroupBy(dim => dim.ProductBarcode)
                                              .ToDictionary(group => group.Key.ToString(), group => _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Loaded detail inventory movements for barcode: {group.Key}", group, true, methodCall: methodCall));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInventoryMovementModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInventoryMovementModel>($"Error loading detail inventory movements for barcodes: {string.Join(", ", productBarcodes)}.", [], false, methodCall: methodCall)
                };
            }
        }

        // --- DRY METHODS FOR SINGLE PRODUCT ---
        private async Task LoadSupplierAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var result = await TryLoadSupplierAsync(product.SupplierId, methodCall);
            logEntries.AddRange(result.LogEntries!);
            product.Supplier = result.Data!;
        }

        private async Task LoadProductCategoriesAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadProductCategoriesAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.ProductCategories = (ICollection<ProductCategoriesModel>)results.Data!;
        }

        private async Task LoadDetailCartsAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailCartsAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailCarts = (ICollection<DetailCartModel>)results.Data!;
        }

        private async Task LoadDetailProductLotsAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailProductLotsAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailProductLot = (ICollection<DetailProductLotModel>)results.Data!;
        }

        private async Task LoadProductImagesAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadProductImagesAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.ProductImages = (ICollection<ProductImageModel>)results.Data!;
        }

        private async Task LoadDetailSaleEventsAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailSaleEventsAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)results.Data!;
        }

        private async Task LoadDetailInvoicesAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailInvoicesAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInvoices = (ICollection<DetailInvoiceModel>)results.Data!;
        }

        private async Task LoadDisposeProductsAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDisposeProductsAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DisposeProducts = (ICollection<DisposeProductModel>)results.Data!;
        }

        private async Task LoadDetailInventoriesAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailInventoriesAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInventories = (ICollection<DetailInventoryModel>)results.Data!;
        }

        private async Task LoadDetailInventoryMovementsAsync(ProductModel product, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var results = await TryLoadDetailInventoryMovementsAsync(product.ProductBarcode, methodCall);
            logEntries.AddRange(results.LogEntries!);
            product.DetailInventoryMovements = (ICollection<DetailInventoryMovementModel>)results.Data!;
        }

        // --- DRY METHODS FOR MULTIPLE PRODUCTS ---
        private async Task LoadSuppliersAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var supplierIds = products.Select(p => p.SupplierId).Distinct();
            var suppliers = await TryLoadSuppliersAsync(supplierIds, methodCall);
            foreach (var product in products)
            {
                suppliers.TryGetValue(product.SupplierId, out var serviceResult);
                logEntries.AddRange(serviceResult!.LogEntries!);
                if (serviceResult.Data!.SupplierId == 0)
                    break;
                product.Supplier = serviceResult.Data!;
            }
        }

        private async Task LoadProductCategoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var productCategories = await TryLoadProductCategoriesAsyncs(productBarcodes, methodCall);
            foreach (var product in products)
            {
                productCategories.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.ProductCategories = (ICollection<ProductCategoriesModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailCartsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailCarts = await TryLoadDetailCartsAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailCarts.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailCarts = (ICollection<DetailCartModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailProductLotsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailProductLots = await TryLoadDetailProductLotsAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailProductLots.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailProductLot = (ICollection<DetailProductLotModel>)serviceResults.Data!;
            }
        }

        private async Task LoadProductImagesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var productImages = await TryLoadProductImagesAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                productImages.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.ProductImages = (ICollection<ProductImageModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailSaleEventsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailSaleEvents = await TryLoadDetailSaleEventsAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailSaleEvents.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailInvoicesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInvoices = await TryLoadDetailInvoicesAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailInvoices.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailInvoices = (ICollection<DetailInvoiceModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDisposeProductsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var disposeProducts = await TryLoadDisposeProductsAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                disposeProducts.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DisposeProducts = (ICollection<DisposeProductModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailInventoriesAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInventories = await TryLoadDetailInventoriesAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailInventories.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailInventories = (ICollection<DetailInventoryModel>)serviceResults.Data!;
            }
        }

        private async Task LoadDetailInventoryMovementsAsync(List<ProductModel> products, List<JsonLogEntry> logEntries, [CallerMemberName] string? methodCall = null)
        {
            var productBarcodes = products.Select(p => p.ProductBarcode).Distinct();
            var detailInventoryMovements = await TryLoadDetailInventoryMovementsAsync(productBarcodes, methodCall);
            foreach (var product in products)
            {
                detailInventoryMovements.TryGetValue(product.ProductBarcode, out var serviceResults);
                logEntries.AddRange(serviceResults!.LogEntries!);
                if (!serviceResults.Data!.Any())
                    break;
                product.DetailInventoryMovements = (ICollection<DetailInventoryMovementModel>)serviceResults.Data!;
            }
        }
    }
}
