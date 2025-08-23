using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Product
{
    public class BaseReturnProductService : IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions>
    {
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly IDAO<SupplierModel> _baseSupplierDAO;
        private readonly IDAO<DetailCartModel> _baseDetailCartDAO;
        private readonly INoneUpdateDAO<DetailProductLotModel> _baseDetailProductLotDAO;
        private readonly IDAO<ProductImageModel> _baseProductImageDAO;
        private readonly INoneUpdateDAO<DetailSaleEventModel> _baseDetailSaleEventDAO;
        private readonly IDAO<ProductCategoriesModel> _baseProductCategoriesDAO;
        private readonly INoneUpdateDAO<DetailInvoiceModel> _baseDetailInvoiceDAO;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly ISupplierDAO<SupplierModel> _supplierDAO;
        private readonly IDetailCartDAO<DetailCartModel> _detailCartDAO;
        private readonly IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> _detailProductLotDAO;
        private readonly IProductImageDAO<ProductImageModel> _productImageDAO;
        private readonly IDetailSaleEventDAO<DetailSaleEventModel> _detailSaleEventDAO;
        private readonly IDetailInvoiceDAO<DetailInvoiceModel> _detailInvoiceDAO;
        private readonly IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> _productCategoriesDAO;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<BaseReturnProductService> _serviceResultFactory;

        public BaseReturnProductService(IDAO<ProductModel> baseDAO, IDAO<SupplierModel> baseSupplierDAO, IDAO<DetailCartModel> baseDetailCartDAO, INoneUpdateDAO<DetailProductLotModel> baseDetailProductLotDAO,
            IDAO<ProductImageModel> baseProductImageDAO, INoneUpdateDAO<DetailSaleEventModel> baseDetailSaleEventDAO, IDAO<ProductCategoriesModel> baseProductCategoriesDAO,
            INoneUpdateDAO<DetailInvoiceModel> baseDetailInvoiceDAO, IProductDAO<ProductModel> productDAO, ISupplierDAO<SupplierModel> supplierDAO, IDetailCartDAO<DetailCartModel> detailCartDAO,
            IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> detailProductLotDAO, IProductImageDAO<ProductImageModel> productImageDAO, IDetailSaleEventDAO<DetailSaleEventModel> detailSaleEventDAO,
            IDetailInvoiceDAO<DetailInvoiceModel> detailInvoiceDAO, IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> productCategoriesDAO,
            ILogService logger, IServiceResultFactory<BaseReturnProductService> serviceResultFactory)
        {
            _baseDAO = baseDAO;
            _baseSupplierDAO = baseSupplierDAO;
            _baseDetailCartDAO = baseDetailCartDAO;
            _baseDetailProductLotDAO = baseDetailProductLotDAO;
            _baseProductImageDAO = baseProductImageDAO;
            _baseDetailSaleEventDAO = baseDetailSaleEventDAO;
            _baseProductCategoriesDAO = baseProductCategoriesDAO;
            _baseDetailInvoiceDAO = baseDetailInvoiceDAO;
            _productDAO = productDAO;
            _supplierDAO = supplierDAO;
            _detailCartDAO = detailCartDAO;
            _detailProductLotDAO = detailProductLotDAO;
            _productImageDAO = productImageDAO;
            _detailSaleEventDAO = detailSaleEventDAO;
            _detailInvoiceDAO = detailInvoiceDAO;
            _productCategoriesDAO = productCategoriesDAO;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<ProductModel>> GetNavigationPropertyByOptionsAsync(ProductModel product, ProductNavigationOptions options)
        {
            List<JsonLogEntry> logEntries = new();
            if (options.IsGetSupplier)
            {
                ServiceResult<SupplierModel> result = await TryLoadSupplierAsync(product.SupplierId);
                logEntries.AddRange(result.LogEntries!);
                product.Supplier = result.Data!;
            }

            if (options.IsGetProductCategories)
            {
                ServiceResults<ProductCategoriesModel> results = await TryLoadProductCategoriesAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.ProductCategories = (ICollection<ProductCategoriesModel>)results.Data!;

            }

            if (options.IsGetDetailCarts)
            {
                ServiceResults<DetailCartModel> results = await TryLoadDetailCartsAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.DetailCarts = (ICollection<DetailCartModel>)results.Data!;
            }

            if (options.IsGetDetailProductLots)
            {
                ServiceResults<DetailProductLotModel> results = await TryLoadDetailProductLotsAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.DetailProductLot = (ICollection<DetailProductLotModel>)results.Data!;
            }

            if (options.IsGetProductImages)
            {
                ServiceResults<ProductImageModel> results = await TryLoadProductImagesAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.ProductImages = (ICollection<ProductImageModel>)results.Data!;
            }

            if (options.IsGetDetailSaleEvents)
            {
                ServiceResults<DetailSaleEventModel> results = await TryLoadDetailSaleEventsAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)results.Data!;
            }

            if (options.IsGetDetailInvoices)
            {
                ServiceResults<DetailInvoiceModel> results = await TryLoadDetailInvoicesAsync(product.ProductBarcode);
                logEntries.AddRange(results.LogEntries!);
                product.DetailInvoices = (ICollection<DetailInvoiceModel>)results.Data!;
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for product."));
            return _serviceResultFactory.CreateServiceResult<ProductModel>(product, logEntries);
        }

        public async Task<ServiceResults<ProductModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<ProductModel> products, ProductNavigationOptions options)
        {
            var productList = products.ToList();
            var productBarcodes = productList.Select(p => p.ProductBarcode).Distinct();
            List<JsonLogEntry> logEntries = new();
            if (options.IsGetSupplier)
            {
                var supplierIds = productList.Select(p => p.SupplierId).Distinct();
                var suppliers = await TryLoadSuppliersAsync(supplierIds);
                foreach (var product in productList)
                {
                    suppliers.TryGetValue(product.SupplierId, out var serviceResult);
                    logEntries.AddRange(serviceResult!.LogEntries!);
                    if (serviceResult.Data!.SupplierId == 0)
                        break;
                    product.Supplier = serviceResult.Data!;
                }
            }

            if (options.IsGetProductCategories)
            {
                var productCategories = await TryLoadProductCategoriesAsyncs(productBarcodes);
                foreach (var product in productList)
                {
                    productCategories.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.ProductCategories = (ICollection<ProductCategoriesModel>)serviceResults.Data!;
                }
            }

            if (options.IsGetDetailCarts)
            {
                var detailCarts = await TryLoadDetailCartsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailCarts.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.DetailCarts = (ICollection<DetailCartModel>)serviceResults.Data!;
                }
            }

            if (options.IsGetDetailProductLots)
            {
                var detailProductLots = await TryLoadDetailProductLotsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailProductLots.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.DetailProductLot = (ICollection<DetailProductLotModel>)serviceResults.Data!;
                }
            }

            if (options.IsGetProductImages)
            {
                var productImages = await TryLoadProductImagesAsync(productBarcodes);
                foreach (var product in productList)
                {
                    productImages.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.ProductImages = (ICollection<ProductImageModel>)serviceResults.Data!;
                }
            }

            if (options.IsGetDetailSaleEvents)
            {
                var detailSaleEvents = await TryLoadDetailSaleEventsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailSaleEvents.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.DetailSaleEvents = (ICollection<DetailSaleEventModel>)serviceResults.Data!;
                }
            }

            if (options.IsGetDetailInvoices)
            {
                var detailInvoices = await TryLoadDetailInvoicesAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailInvoices.TryGetValue(product.ProductBarcode, out var serviceResults);
                    logEntries.AddRange(serviceResults!.LogEntries!);
                    if (!serviceResults.Data!.Any())
                        break;
                    product.DetailInvoices = (ICollection<DetailInvoiceModel>)serviceResults.Data!;
                }
            }
            logEntries.Add(_logger.JsonLogInfo<ProductModel, BaseReturnProductService>("Completed loading navigation properties for products."));
            return _serviceResultFactory.CreateServiceResults<ProductModel>(productList, logEntries);
        }

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
                var categories = await _productCategoriesDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<ProductCategoriesModel>();
                return categories.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {productBarcode}", categories, true)
                    : _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for barcode: {productBarcode}.", new List<ProductCategoriesModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcode: {productBarcode}.", new List<ProductCategoriesModel>(), false, ex);
            }
        }

        private async Task<ServiceResults<DetailCartModel>> TryLoadDetailCartsAsync(string productBarcode)
        {
            try
            {
                var detailCarts = await _detailCartDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<DetailCartModel>();
                return detailCarts.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {productBarcode}", detailCarts, true)
                    : _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for barcode: {productBarcode}.", new List<DetailCartModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcode: {productBarcode}.", new List<DetailCartModel>(), false, ex);
            }
        }

        private async Task<ServiceResults<DetailProductLotModel>> TryLoadDetailProductLotsAsync(string productBarcode)
        {
            try
            {
                var detailProductLots = await _detailProductLotDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<DetailProductLotModel>();
                return detailProductLots.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {productBarcode}", detailProductLots, true)
                    : _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for barcode: {productBarcode}.", new List<DetailProductLotModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcode: {productBarcode}.", new List<DetailProductLotModel>(), false, ex);
            }
        }

        private async Task<ServiceResults<ProductImageModel>> TryLoadProductImagesAsync(string productBarcode)
        {
            try
            {
                var productImages = await _productImageDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<ProductImageModel>();
                return productImages.Any()
                    ? _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product images for barcode: {productBarcode}", productImages, true)
                    : _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for barcode: {productBarcode}.", new List<ProductImageModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcode: {productBarcode}.", new List<ProductImageModel>(), false, ex);
            }
        }

        private async Task<ServiceResults<DetailSaleEventModel>> TryLoadDetailSaleEventsAsync(string productBarcode)
        {
            try
            {
                var detailSaleEvents = await _detailSaleEventDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<DetailSaleEventModel>();
                return detailSaleEvents.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Loaded detail sale events for barcode: {productBarcode}", detailSaleEvents, true)
                    : _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for barcode: {productBarcode}.", new List<DetailSaleEventModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcode: {productBarcode}.", new List<DetailSaleEventModel>(), false, ex);
            }
        }

        private async Task<ServiceResults<DetailInvoiceModel>> TryLoadDetailInvoicesAsync(string productBarcode)
        {
            try
            {
                var detailInvoices = await _detailInvoiceDAO.GetByProductBarcodeAsync(productBarcode) ?? Enumerable.Empty<DetailInvoiceModel>();
                return detailInvoices.Any()
                    ? _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoices for barcode: {productBarcode}", detailInvoices, true)
                    : _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for barcode: {productBarcode}.", new List<DetailInvoiceModel>(), false);
            }
            catch (Exception ex)
            {
                return _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcode: {productBarcode}.", new List<DetailInvoiceModel>(), false, ex);
            }
        }

        private async Task<IDictionary<uint, ServiceResult<SupplierModel>>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds)
        {
            uint firstId = supplierIds.FirstOrDefault();
            try
            {
                IEnumerable<SupplierModel> suppliers = await _baseSupplierDAO.GetByInputsAsync(supplierIds.Select(id => id.ToString())) ?? Enumerable.Empty<SupplierModel>();
                if (!suppliers.Any())
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
                var categories = (await _productCategoriesDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<ProductCategoriesModel>();
                if (!categories.Any())
                    return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"No product categories found for the provided barcodes.", new List<ProductCategoriesModel>(), false)
                    };
                return categories.GroupBy(category => category.ProductBarcode)
                                 .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Loaded product categories for barcode: {group.Key}", group, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<ProductCategoriesModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductCategoriesModel>($"Error loading product categories for barcodes: {string.Join(", ", productBarcodes)}.", new List<ProductCategoriesModel>(), false, ex)
                };
            }

        }

        private async Task<IDictionary<string, ServiceResults<DetailCartModel>>> TryLoadDetailCartsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailCarts = (await _detailCartDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailCartModel>();
                if (!detailCarts.Any())
                    return new Dictionary<string, ServiceResults<DetailCartModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"No detail carts found for the provided barcodes.", new List<DetailCartModel>(), false)
                    };
                return detailCarts.GroupBy(cart => cart.ProductBarcode)
                                  .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Loaded detail carts for barcode: {group.Key}", group, true));
            }
            catch (Exception ex)
            {
                return new Dictionary<string, ServiceResults<DetailCartModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailCartModel>($"Error loading detail carts for barcodes: {string.Join(", ", productBarcodes)}.", new List<DetailCartModel>(), false, ex)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailProductLotModel>>> TryLoadDetailProductLotsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailProductLots = (await _detailProductLotDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailProductLotModel>();
                if (!detailProductLots.Any())
                    return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"No detail product lots found for the provided barcodes.", new List<DetailProductLotModel>(), false)
                    };
                return detailProductLots.GroupBy(lot => lot.ProductBarcode)
                                        .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Loaded detail product lots for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailProductLotModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailProductLotModel>($"Error loading detail product lots for barcodes: {string.Join(", ", productBarcodes)}.", new List<DetailProductLotModel>(), false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<ProductImageModel>>> TryLoadProductImagesAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var productImages = (await _productImageDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<ProductImageModel>();
                if (!productImages.Any())
                    return new Dictionary<string, ServiceResults<ProductImageModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"No product images found for the provided barcodes.", new List<ProductImageModel>(), false)
                    };
                return productImages.GroupBy(image => image.ProductBarcode)
                                    .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Loaded product image for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<ProductImageModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<ProductImageModel>($"Error loading product images for barcodes: {string.Join(", ", productBarcodes)}.", new List<ProductImageModel>(), false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailSaleEventModel>>> TryLoadDetailSaleEventsAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailSaleEvents = (await _detailSaleEventDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailSaleEventModel>();
                if (!detailSaleEvents.Any())
                    return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"No detail sale events found for the provided barcodes.", new List<DetailSaleEventModel>(), false)
                    };
                return detailSaleEvents.GroupBy(eventModel => eventModel.ProductBarcode)
                                       .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults($"Loaded detail sale events for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailSaleEventModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailSaleEventModel>($"Error loading detail sale events for barcodes: {string.Join(", ", productBarcodes)}.", new List<DetailSaleEventModel>(), false)
                };
            }
        }

        private async Task<IDictionary<string, ServiceResults<DetailInvoiceModel>>> TryLoadDetailInvoicesAsync(IEnumerable<string> productBarcodes)
        {
            string firstBarcode = productBarcodes.FirstOrDefault() ?? string.Empty;
            try
            {
                var detailInvoices = (await _detailInvoiceDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailInvoiceModel>();
                if (!detailInvoices.Any())
                    return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                    {
                        [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"No detail invoices found for the provided barcodes.", new List<DetailInvoiceModel>(), false)
                    };
                return detailInvoices.GroupBy(invoice => invoice.ProductBarcode)
                                     .ToDictionary(group => group.Key, group => _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Loaded detail invoice for barcode: {group.Key}", group, true));
            }
            catch (Exception)
            {
                return new Dictionary<string, ServiceResults<DetailInvoiceModel>>
                {
                    [firstBarcode] = _serviceResultFactory.CreateServiceResults<DetailInvoiceModel>($"Error loading detail invoices for barcodes: {string.Join(", ", productBarcodes)}.", new List<DetailInvoiceModel>(), false)
                };
            }
        }
    }
}
