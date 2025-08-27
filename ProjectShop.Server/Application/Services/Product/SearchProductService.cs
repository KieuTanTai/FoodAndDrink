using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ObjectValue;
using ProjectShop.Server.Core.ObjectValue.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Application.Services.Product
{
    public class SearchProductService : ISearchProductService<ProductModel, ProductNavigationOptions>
    {
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly ILogService _logger;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly IBaseHelperService<ProductModel> _helper;
        private readonly IServiceResultFactory<SearchProductService> _serviceResultFactory;
        private readonly IServiceGetSingle<ProductModel, ProductNavigationOptions> _getSingleService;
        private readonly IServiceGetMultiple<ProductModel, ProductNavigationOptions> _getMultipleService;
        private readonly IBaseGetByTimeService<ProductModel, ProductNavigationOptions> _byTimeService;
        private readonly IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions> _navigationService;

        public SearchProductService(IDAO<ProductModel> baseDAO, IProductDAO<ProductModel> productDAO,
            ILogService logger,
            IBaseHelperService<ProductModel> helper,
            IServiceResultFactory<SearchProductService> serviceResultFactory,
            IBaseGetByTimeService<ProductModel, ProductNavigationOptions> byTimeService,
            IServiceGetSingle<ProductModel, ProductNavigationOptions> getSingleService,
            IServiceGetMultiple<ProductModel, ProductNavigationOptions> getMultipleService,
            IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions> navigationService)
        {
            _helper = helper;
            _logger = logger;
            _baseDAO = baseDAO;
            _productDAO = productDAO;
            _byTimeService = byTimeService;
            _navigationService = navigationService;
            _getSingleService = getSingleService;
            _getMultipleService = getMultipleService;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResults<ProductModel>> GetAllByEnumAsync(EProductUnit unit, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(unit, _productDAO.GetAllByEnumAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByCategoryIdAsync(uint categoryId, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(categoryId, _productDAO.GetByCategoryIdAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(categoryIds, _productDAO.GetByCategoryIdsAsync, options, maxGetCount);

        public async Task<ServiceResult<ProductModel>> GetByEnumAsync(EProductUnit unit, ProductNavigationOptions? options = null)
            => await _getSingleService.QueryAsync(unit, _productDAO.GetByEnumAsync, options);

        public async Task<ServiceResults<ProductModel>> GetByInputPriceAsync(decimal price, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByValueAsync(price, (price, maxGet) => _productDAO.GetByInputPriceAsync(price, compareType, maxGet), options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByRangePriceAsync(decimal minPrice, decimal maxPrice, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByRangeAsync(minPrice, maxPrice, _productDAO.GetByRangePriceAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByLikeStringAsync(string input, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(input, _productDAO.GetByLikeStringAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByNetWeightAsync(decimal netWeight, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.GetByValueAsync(netWeight, (weight, maxGet) => _productDAO.GetByNetWeightAsync(weight, compareType, maxGet), options, maxGetCount);

        public async Task<ServiceResult<ProductModel>> GetByProductNameAsync(string productName, ProductNavigationOptions? options = null)
            => await _getSingleService.QueryAsync(productName, _productDAO.GetByProductNameAsync, options);

        public async Task<ServiceResults<ProductModel>> GetByRatingAgeAsync(string ratingAge, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(ratingAge, _productDAO.GetByRatingAgeAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByStatusAsync(bool status, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(status, _productDAO.GetByStatusAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetBySupplierIdAsync(uint supplierId, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(supplierId, _productDAO.GetBySupplierIdAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _getMultipleService.QueryManyAsync(supplierIds, _productDAO.GetBySupplierIdsAsync, options, maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(DateTime lastUpdatedDate, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGet) => _productDAO.GetByLastUpdatedDateAsync(lastUpdatedDate, compareType, maxGet),
                compareType, options, "Error retrieving products by last updated date.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(DateTime startDate, DateTime endDate, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _productDAO.GetByLastUpdatedDateRangeAsync(startDate, endDate, maxGet),
                options, "Error retrieving products by last updated date range.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(int year, int month, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByMonthAndYearGenericAsync((year, month, maxGet) => _productDAO.GetByMonthAndYearLastUpdatedDateAsync(year, month, maxGet),
                year, month, options, "Error retrieving products by last updated month and year.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByLastUpdatedDateAsync(int year, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGet) => _productDAO.GetByYearLastUpdatedDateAsync(year, compareType, maxGet),
                compareType, options, "Error retrieving products by last updated year.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByDateTimeAsync(DateTime dateTime, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGet) => _productDAO.GetByDateTimeAsync(dateTime, compareType, maxGet),
                compareType, options, "Error retrieving products by created date.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeRangeGenericAsync((maxGet) => _productDAO.GetByDateTimeRangeAsync(startDate, endDate, maxGet),
                options, "Error retrieving products by created date range.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByMonthAndYearAsync(int year, int month, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByMonthAndYearGenericAsync((year, month, maxGet) => _productDAO.GetByMonthAndYearAsync(year, month, maxGet),
                year, month, options, "Error retrieving products by created month and year.", maxGetCount);

        public async Task<ServiceResults<ProductModel>> GetByYearAsync(int year, ECompareType compareType, ProductNavigationOptions? options = null, int? maxGetCount = null)
            => await _byTimeService.GetByDateTimeGenericAsync((compareType, maxGet) => _productDAO.GetByYearAsync(year, compareType, maxGet),
                compareType, options, "Error retrieving products by created year.", maxGetCount);
    }
}
