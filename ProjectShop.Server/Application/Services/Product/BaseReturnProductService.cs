using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;

namespace ProjectShop.Server.Application.Services.Product
{
    public abstract class BaseReturnProductService : BaseGetByTimeService<ProductModel, ProductNavigationOptions>
    {
        protected readonly IDAO<ProductModel> _baseDAO;
        protected readonly IDAO<SupplierModel> _baseSupplierDAO;
        protected readonly IDAO<DetailCartModel> _baseDetailCartDAO;
        protected readonly IDAO<DetailProductLotModel> _baseDetailProductLotDAO;
        protected readonly IDAO<ProductImageModel> _baseProductImageDAO;
        protected readonly IDAO<DetailSaleEventModel> _baseDetailSaleEventDAO;
        protected readonly IDAO<ProductCategoriesModel> _baseProductCategoriesDAO;
        protected readonly IDAO<DetailInvoiceModel> _baseDetailInvoiceDAO;
        protected readonly IProductDAO<ProductModel> _productDAO;
        protected readonly ISupplierDAO<SupplierModel> _supplierDAO;
        protected readonly IDetailCartDAO<DetailCartModel> _detailCartDAO;
        protected readonly IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> _detailProductLotDAO;
        protected readonly IProductImageDAO<ProductImageModel> _productImageDAO;
        protected readonly IDetailSaleEventDAO<DetailSaleEventModel> _detailSaleEventDAO;
        protected readonly IDetailInvoiceDAO<DetailInvoiceModel> _detailInvoiceDAO;
        protected readonly IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> _productCategoriesDAO;

        public BaseReturnProductService(
            IDAO<ProductModel> baseDAO,
            IProductDAO<ProductModel> productDAO,
            IDAO<SupplierModel> baseSupplierDAO,
            ISupplierDAO<SupplierModel> supplierDAO,
            IDAO<DetailCartModel> baseDetailCartDAO,
            IDetailCartDAO<DetailCartModel> detailCartDAO,
            IDAO<DetailProductLotModel> baseDetailProductLotDAO,
            IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey> detailProductLotDAO,
            IDAO<ProductImageModel> baseProductImageDAO,
            IProductImageDAO<ProductImageModel> productImageDAO,
            IDAO<DetailSaleEventModel> baseDetailSaleEventDAO,
            IDetailSaleEventDAO<DetailSaleEventModel> detailSaleEventDAO,
            IDAO<ProductCategoriesModel> baseProductCategoriesDAO,
            IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey> productCategoriesDAO,
            IDAO<DetailInvoiceModel> baseDetailInvoiceDAO,
            IDetailInvoiceDAO<DetailInvoiceModel> detailInvoiceDAO)
        {
            _baseDAO = baseDAO ?? throw new ArgumentNullException(nameof(baseDAO));
            _productDAO = productDAO ?? throw new ArgumentNullException(nameof(productDAO));
            _baseSupplierDAO = baseSupplierDAO ?? throw new ArgumentNullException(nameof(baseSupplierDAO));
            _supplierDAO = supplierDAO ?? throw new ArgumentNullException(nameof(supplierDAO));
            _baseDetailCartDAO = baseDetailCartDAO ?? throw new ArgumentNullException(nameof(baseDetailCartDAO));
            _detailCartDAO = detailCartDAO ?? throw new ArgumentNullException(nameof(detailCartDAO));
            _baseDetailProductLotDAO = baseDetailProductLotDAO ?? throw new ArgumentNullException(nameof(baseDetailProductLotDAO));
            _detailProductLotDAO = detailProductLotDAO ?? throw new ArgumentNullException(nameof(detailProductLotDAO));
            _baseProductImageDAO = baseProductImageDAO ?? throw new ArgumentNullException(nameof(baseProductImageDAO));
            _productImageDAO = productImageDAO ?? throw new ArgumentNullException(nameof(productImageDAO));
            _baseDetailSaleEventDAO = baseDetailSaleEventDAO ?? throw new ArgumentNullException(nameof(baseDetailSaleEventDAO));
            _detailSaleEventDAO = detailSaleEventDAO ?? throw new ArgumentNullException(nameof(detailSaleEventDAO));
            _baseProductCategoriesDAO = baseProductCategoriesDAO ?? throw new ArgumentNullException(nameof(baseProductCategoriesDAO));
            _productCategoriesDAO = productCategoriesDAO ?? throw new ArgumentNullException(nameof(productCategoriesDAO));
            _baseDetailInvoiceDAO = baseDetailInvoiceDAO ?? throw new ArgumentNullException(nameof(baseDetailInvoiceDAO));
            _detailInvoiceDAO = detailInvoiceDAO ?? throw new ArgumentNullException(nameof(detailInvoiceDAO));
        }

        protected override async Task<ProductModel> GetNavigationPropertyByOptionsAsync(ProductModel product, ProductNavigationOptions? options)
        {
            if (options == null)
                return product;
            if (options.IsGetSupplier)
                product.Supplier = await TryLoadSupplierAsync(product.SupplierId);
            if (options.IsGetProductCategories)
                product.ProductCategories = await TryLoadProductCategoriesAsync(product.ProductBarcode);
            if (options.IsGetDetailCarts)
                product.DetailCarts = await TryLoadDetailCartsAsync(product.ProductBarcode);
            if (options.IsGetDetailProductLots)
                product.DetailProductLot = await TryLoadDetailProductLotsAsync(product.ProductBarcode);
            if (options.IsGetProductImages)
                product.ProductImages = await TryLoadProductImagesAsync(product.ProductBarcode);
            if (options.IsGetDetailSaleEvents)
                product.DetailSaleEvents = await TryLoadDetailSaleEventsAsync(product.ProductBarcode);
            if (options.IsGetDetailInvoices)
                product.DetailInvoices = await TryLoadDetailInvoicesAsync(product.ProductBarcode);
            return product;
        }

        protected override async Task<IEnumerable<ProductModel>> GetNavigationPropertyByOptionsAsync(IEnumerable<ProductModel> products, ProductNavigationOptions? options)
        {
            if (options == null)
                return products;
            var productList = products.ToList();
            var productBarcodes = productList.Select(p => p.ProductBarcode).Distinct();
            if (options.IsGetSupplier)
            {
                var supplierIds = productList.Select(p => p.SupplierId).Distinct();
                var suppliers = await TryLoadSuppliersAsync(supplierIds);
                foreach (var product in productList)
                {
                    suppliers.TryGetValue(product.SupplierId, out var supplier);
                    product.Supplier = supplier ?? new();
                }
            }

            if (options.IsGetProductCategories)
            {
                var productCategories = await TryLoadProductCategoriesAsyncs(productBarcodes);
                foreach (var product in productList)
                {
                    productCategories.TryGetValue(product.ProductBarcode, out var categories);
                    product.ProductCategories = categories ?? new List<ProductCategoriesModel>();
                }
            }

            if (options.IsGetDetailCarts)
            {
                var detailCarts = await TryLoadDetailCartsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailCarts.TryGetValue(product.ProductBarcode, out var carts);
                    product.DetailCarts = carts ?? new List<DetailCartModel>();
                }
            }

            if (options.IsGetDetailProductLots)
            {
                var detailProductLots = await TryLoadDetailProductLotsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailProductLots.TryGetValue(product.ProductBarcode, out var lots);
                    product.DetailProductLot = lots ?? new List<DetailProductLotModel>();
                }
            }

            if (options.IsGetProductImages)
            {
                var productImages = await TryLoadProductImagesAsync(productBarcodes);
                foreach (var product in productList)
                {
                    productImages.TryGetValue(product.ProductBarcode, out var images);
                    product.ProductImages = images ?? new List<ProductImageModel>();
                }   
            }

            if (options.IsGetDetailSaleEvents)
            {
                var detailSaleEvents = await TryLoadDetailSaleEventsAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailSaleEvents.TryGetValue(product.ProductBarcode, out var details);
                    product.DetailSaleEvents = details ?? new List<DetailSaleEventModel>();
                }
            }

            if (options.IsGetDetailInvoices)
            {
                var detailInvoices = await TryLoadDetailInvoicesAsync(productBarcodes);
                foreach (var product in productList)
                {
                    detailInvoices.TryGetValue(product.ProductBarcode, out var details);
                    product.DetailInvoices = details ?? new List<DetailInvoiceModel>();
                }
            }

            return productList;
        }

        private async Task<SupplierModel> TryLoadSupplierAsync(uint supplierId)
        {
            try
            {
                SupplierModel? supplier = await _baseSupplierDAO.GetSingleDataAsync(supplierId.ToString());
                return supplier ?? new SupplierModel(); // Hoặc throw exception nếu cần
            }
            catch (Exception)
            {
                return new SupplierModel(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<ProductCategoriesModel>> TryLoadProductCategoriesAsync(string productBarcode)
        {
            try
            {
                return (await _productCategoriesDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<ProductCategoriesModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<DetailCartModel>> TryLoadDetailCartsAsync(string productBarcode)
        {
            try
            {
                return (await _detailCartDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<DetailCartModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<DetailProductLotModel>> TryLoadDetailProductLotsAsync(string productBarcode)
        {
            try
            {
                return (await _detailProductLotDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<DetailProductLotModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<ProductImageModel>> TryLoadProductImagesAsync(string productBarcode)
        {
            try
            {
                return (await _productImageDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<ProductImageModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<DetailSaleEventModel>> TryLoadDetailSaleEventsAsync(string productBarcode)
        {
            try
            {
                return (await _detailSaleEventDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<DetailSaleEventModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<ICollection<DetailInvoiceModel>> TryLoadDetailInvoicesAsync(string productBarcode)
        {
            try
            {
                return (await _detailInvoiceDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<DetailInvoiceModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<IDictionary<uint, SupplierModel>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds)
        {
            try
            {
                return (await _baseSupplierDAO.GetByInputsAsync(supplierIds.Select(supplierId => supplierId.ToString())))
                    .ToDictionary(supplier => supplier.SupplierId, supplier => supplier) ?? new Dictionary<uint, SupplierModel>();
            }
            catch (Exception)
            {
                return new Dictionary<uint, SupplierModel>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<IDictionary<string, ICollection<ProductCategoriesModel>>> TryLoadProductCategoriesAsyncs(IEnumerable<string> productBarcodes)
        {
            try
            {
                var categories = (await _productCategoriesDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<ProductCategoriesModel>();
                return categories.GroupBy(category => category.ProductBarcode)
                                 .ToDictionary(group => group.Key, group => (ICollection<ProductCategoriesModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<ProductCategoriesModel>>(); // Hoặc throw exception nếu cần
            }

        }

        private async Task<IDictionary<string, ICollection<DetailCartModel>>> TryLoadDetailCartsAsync(IEnumerable<string> productBarcodes)
        {
            try
            {
                var detailCarts = (await _detailCartDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailCartModel>();
                return detailCarts.GroupBy(cart => cart.ProductBarcode)
                                  .ToDictionary(group => group.Key, group => (ICollection<DetailCartModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<DetailCartModel>>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<IDictionary<string, ICollection<DetailProductLotModel>>> TryLoadDetailProductLotsAsync(IEnumerable<string> productBarcodes)
        {
            try
            {
                var detailProductLots = (await _detailProductLotDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailProductLotModel>();
                return detailProductLots.GroupBy(lot => lot.ProductBarcode)
                                        .ToDictionary(group => group.Key, group => (ICollection<DetailProductLotModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<DetailProductLotModel>>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<IDictionary<string, ICollection<ProductImageModel>>> TryLoadProductImagesAsync(IEnumerable<string> productBarcodes)
        {
            try
            {
                var productImages = (await _productImageDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<ProductImageModel>();
                return productImages.GroupBy(image => image.ProductBarcode)
                                    .ToDictionary(group => group.Key, group => (ICollection<ProductImageModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<ProductImageModel>>(); // Hoặc throw exception nếu cần
            }
        }

        private async Task<IDictionary<string, ICollection<DetailSaleEventModel>>> TryLoadDetailSaleEventsAsync(IEnumerable<string> productBarcodes)
        {
            try
            {
                var detailSaleEvents = (await _detailSaleEventDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailSaleEventModel>();
                return detailSaleEvents.GroupBy(eventModel => eventModel.ProductBarcode)
                                       .ToDictionary(group => group.Key, group => (ICollection<DetailSaleEventModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<DetailSaleEventModel>>();
            }
        }

        private async Task<IDictionary<string, ICollection<DetailInvoiceModel>>> TryLoadDetailInvoicesAsync(IEnumerable<string> productBarcodes)
        {
            try
            {
                var detailInvoices = (await _detailInvoiceDAO.GetByProductBarcodesAsync(productBarcodes)) ?? Enumerable.Empty<DetailInvoiceModel>();
                return detailInvoices.GroupBy(invoice => invoice.ProductBarcode)
                                     .ToDictionary(group => group.Key, group => (ICollection<DetailInvoiceModel>)group.ToList());
            }
            catch (Exception)
            {
                return new Dictionary<string, ICollection<DetailInvoiceModel>>(); // Hoặc throw exception nếu cần
            }
        }
    }
}
