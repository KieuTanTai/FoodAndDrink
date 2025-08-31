using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Product
{
    public class SearchProductService : ISearchProductService<ProductModel, ProductNavigationOptions>
    {
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly IBaseHelperService<ProductModel> _helper;
        private readonly IServiceGetSingle<ProductModel, ProductNavigationOptions, SearchProductService> _getSingleService;
        private readonly IServiceGetMultiple<ProductModel, ProductNavigationOptions, SearchProductService> _getMultipleService;
        private readonly IBaseGetByTimeService<ProductModel, ProductNavigationOptions> _byTimeService;

        public SearchProductService(IDAO<ProductModel> baseDAO, IProductDAO<ProductModel> productDAO,
            IBaseHelperService<ProductModel> helper,
            IBaseGetByTimeService<ProductModel, ProductNavigationOptions> byTimeService,
            IServiceGetSingle<ProductModel, ProductNavigationOptions, SearchProductService> getSingleService,
            IServiceGetMultiple<ProductModel, ProductNavigationOptions, SearchProductService> getMultipleService)
        {
            _helper = helper;
            _baseDAO = baseDAO;
            _productDAO = productDAO;
            _byTimeService = byTimeService;
            _getSingleService = getSingleService;
            _getMultipleService = getMultipleService;
        }

        public async Task<ServiceResults<ProductModel>> GetAllAsync(ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(_baseDAO.GetAllAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetAllByEnumAsync(EProductUnit unit, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(unit, _productDAO.GetAllByEnumAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByCategoryIdAsync(uint categoryId, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(categoryId, _productDAO.GetByCategoryIdAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(categoryIds, _productDAO.GetByCategoryIdsAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<ProductModel>> GetByEnumAsync(EProductUnit unit, ProductNavigationOptions? options = null)
            => await _getSingleService.GetAsync(unit, _productDAO.GetByEnumAsync, options);

        public async Task<ServiceResults<ProductModel>> GetByInputPriceAsync(decimal price, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByValueAsync(price, (price, maxGet) => _productDAO.GetByInputPriceAsync(price, compareType, maxGet), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByRangeAsync(minPrice, maxPrice, _productDAO.GetByRangePriceAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByLikeStringAsync(string input, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(input, _productDAO.GetByLikeStringAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByNetWeightAsync(decimal netWeight, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByValueAsync(netWeight, (weight, maxGet) => _productDAO.GetByNetWeightAsync(weight, compareType, maxGet), options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResult<ProductModel>> GetByProductNameAsync(string productName, ProductNavigationOptions? options = null)
            => await _getSingleService.GetAsync(productName, _productDAO.GetByProductNameAsync, options);

        public async Task<ServiceResults<ProductModel>> GetByRatingAgeAsync(string ratingAge, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(ratingAge, _productDAO.GetByRatingAgeAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByStatusAsync(bool status, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(status, _productDAO.GetByStatusAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetBySupplierIdAsync(uint supplierId, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(supplierId, _productDAO.GetBySupplierIdAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetManyAsync(supplierIds, _productDAO.GetBySupplierIdsAsync, options, _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(DateTime lastUpdatedDate, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _productDAO.GetByLastUpdatedDateAsync(lastUpdatedDate, type, maxGet),
                compareType, options, "Error retrieving products by last updated date.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _productDAO.GetByLastUpdatedDateRangeAsync(startDate, endDate, maxGet),
                options, "Error retrieving products by last updated date range.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(int year, int month, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByMonthAndYearGenericAsync((year, month, maxGet) => _productDAO.GetByMonthAndYearLastUpdatedDateAsync(year, month, maxGet),
                year, month, options, "Error retrieving products by last updated month and year.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(int year, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _productDAO.GetByYearLastUpdatedDateAsync(year, type, maxGet),
                compareType, options, "Error retrieving products by last updated year.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByDateTimeAsync(DateTime dateTime, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _productDAO.GetByDateTimeAsync(dateTime, type, maxGet),
                compareType, options, "Error retrieving products by created date.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _productDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGet),
                options, "Error retrieving products by created date range.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByMonthAndYearAsync(int year, int month, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByMonthAndYearGenericAsync((year, month, maxGet) => _productDAO.GetByMonthAndYearAsync(year, month, maxGet),
                year, month, options, "Error retrieving products by created month and year.", _helper.GetValidMaxRecord(maxGetCount));

        public async Task<ServiceResults<ProductModel>> GetByYearAsync(int year, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((type, maxGet) => _productDAO.GetByYearAsync(year, type, maxGet),
                compareType, options, "Error retrieving products by created year.", _helper.GetValidMaxRecord(maxGetCount));
    }
}
