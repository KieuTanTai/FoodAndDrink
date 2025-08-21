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
        protected readonly IDAO<ProductLotModel> _baseProductLotDAO;
        protected readonly IDAO<ProductImageModel> _baseProductImageDAO;
        protected readonly IDAO<DetailSaleEventModel> _baseDetailSaleEventDAO;
        protected readonly IDAO<ProductCategoriesModel> _baseProductCategoriesDAO;
        protected readonly IDAO<DetailInvoiceModel> _baseDetailInvoiceDAO;
        protected readonly IProductDAO<ProductModel> _productDAO;
        protected readonly ISupplierDAO<SupplierModel> _supplierDAO;
        protected readonly IDetailCartDAO<DetailCartModel> _detailCartDAO;
        protected readonly IProductLotDAO<ProductLotModel> _productLotDAO;
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
            IDAO<ProductLotModel> baseProductLotDAO,
            IProductLotDAO<ProductLotModel> productLotDAO,
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
            _baseProductLotDAO = baseProductLotDAO ?? throw new ArgumentNullException(nameof(baseProductLotDAO));
            _productLotDAO = productLotDAO ?? throw new ArgumentNullException(nameof(productLotDAO));
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
            if (options.IsGetProductLots)
                product.ProductLots = await TryLoadProductLotsAsync(product.ProductBarcode);
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
            if (options.IsGetSupplier)
            {

            }

            if (options.IsGetProductCategories)
            {

            }

            if (options.IsGetDetailCarts)
            {

            }

            if (options.IsGetProductLots)
            {

            }

            if (options.IsGetProductImages)
            {

            }

            if (options.IsGetDetailSaleEvents)
            {

            }

            if (options.IsGetDetailInvoices)
            {

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

        private async Task<ICollection<ProductLotModel>> TryLoadProductLotsAsync(string productBarcode)
        {
            try
            {
                return (await _productLotDAO.GetByProductBarcodeAsync(productBarcode)).ToList();
            }
            catch (Exception)
            {
                return new List<ProductLotModel>(); // Hoặc throw exception nếu cần
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

        private async Task<Dictionary<uint, SupplierModel>> TryLoadSuppliersAsync(IEnumerable<uint> supplierIds)
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

        //private async Task<Dictionary<string, ICollection<ProductCategoriesModel>>> TryLoadProductCategoriesAsyncs(IEnumerable<string> productBarcodes)
        //{
        //    try
        //    {
        //        var categories = await _productCategoriesDAO.Get(productBarcodes);
        //        return categories.GroupBy(category => category.ProductBarcode)
        //                         .ToDictionary(group => group.Key, group => group.ToList());
        //    }
        //    catch (Exception)
        //    {
        //        return new Dictionary<string, ICollection<ProductCategoriesModel>>(); // Hoặc throw exception nếu cần
        //    }

        //}
    }
}
