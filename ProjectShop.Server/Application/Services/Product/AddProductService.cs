using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Product
{
    public class AddProductService : IAddProductServices<ProductModel>
    {
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly IBaseHelperServices<ProductModel> _helper;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<AddProductService> _serviceResultFactory;

        public AddProductService(IDAO<ProductModel> baseDAO, IProductDAO<ProductModel> productDAO,
            IBaseHelperServices<ProductModel> helper,
            ILogService logger,
            IServiceResultFactory<AddProductService> serviceResultFactory)
        {
            _baseDAO = baseDAO;
            _productDAO = productDAO;
            _helper = helper;
            _logger = logger;
            _serviceResultFactory = serviceResultFactory;
        }

        public async Task<ServiceResult<ProductModel>> AddProductAsync(ProductModel entity)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                if (await _helper.IsExistObject(entity.ProductName, _productDAO.GetByProductNameAsync))
                    return _serviceResultFactory.CreateServiceResult("Product with the same name already exists.", entity, false);
                int affectedRows = await _baseDAO.InsertAsync(entity);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, AddProductService>($"Failed to insert the product for barcode: {entity.ProductBarcode}."));
                    return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<ProductModel, AddProductService>($"Product inserted successfully for barcode: {entity.ProductBarcode}.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, true);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, AddProductService>("An error occurred while inserting the product.", ex));
                return _serviceResultFactory.CreateServiceResult(entity, logEntries, false);
            }
        }

        public async Task<ServiceResults<ProductModel>> AddProductsAsync(IEnumerable<ProductModel> entities)
        {
            List<JsonLogEntry> logEntries = [];
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, (entity)
                    => entity.ProductName, _productDAO.GetByProductNamesAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (serviceResults == null || serviceResults.Data == null || !serviceResults.Data.Any())
                {
                    logEntries = serviceResults?.LogEntries?.ToList() ?? [];
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, AddProductService>("No valid products to insert after filtering."));
                    return _serviceResultFactory.CreateServiceResults<ProductModel>([], logEntries, false);
                }
                // Insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data);
                if (affectedRows == 0)
                {
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, AddProductService>("Failed to insert the products.", affectedRows: affectedRows));
                    return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, false);
                }
                logEntries.Add(_logger.JsonLogInfo<ProductModel, AddProductService>($"Products inserted successfully.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResults(serviceResults.Data, logEntries, true);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, AddProductService>("An error occurred while inserting the products.", ex));
                return _serviceResultFactory.CreateServiceResults(entities, logEntries, false);
            }
        }
    }
}
