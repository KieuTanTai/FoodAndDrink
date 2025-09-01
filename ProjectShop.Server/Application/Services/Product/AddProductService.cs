using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Product
{
    public class AddProductService : IAddProductService<ProductModel>
    {
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly IBaseHelperService<ProductModel> _helper;
        private readonly ILogService _logger;
        private readonly IServiceResultFactory<AddProductService> _serviceResultFactory;

        public AddProductService(IDAO<ProductModel> baseDAO, IProductDAO<ProductModel> productDAO, 
            IBaseHelperService<ProductModel> helper, 
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
            List<JsonLogEntry> logEntries = new();
            try
            {
                if (await _helper.IsExistObject(entity.ProductName, _productDAO.GetByProductNameAsync))
                    return _serviceResultFactory.CreateServiceResult<ProductModel>("Product with the same name already exists.", entity, false);
                int affectedRows = await _baseDAO.InsertAsync(entity);
                if (affectedRows == 0)
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, AddProductService>($"Failed to insert the product for barcode: {entity.ProductBarcode}."));
                logEntries.Add(_logger.JsonLogInfo<ProductModel, AddProductService>($"Product inserted successfully for barcode: {entity.ProductBarcode}.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResult<ProductModel>(entity, logEntries);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, AddProductService>("An error occurred while inserting the product.", ex));
                return _serviceResultFactory.CreateServiceResult<ProductModel>(entity, logEntries);
            }
        }

        public async Task<ServiceResults<ProductModel>> AddProductsAsync(IEnumerable<ProductModel> entities)
        {
            List<JsonLogEntry> logEntries = new();
            try
            {
                var filteredEntities = await _helper.FilterValidEntities(entities, (entity)
                    => entity.ProductName, _productDAO.GetByProductNamesAsync);
                filteredEntities.TryGetValue(filteredEntities.Keys.FirstOrDefault(), out var serviceResults);
                if (!serviceResults!.Data!.Any())
                    return _serviceResultFactory.CreateServiceResults<ProductModel>("No valid products to add.", [], false);
                // Insert
                int affectedRows = await _baseDAO.InsertAsync(serviceResults.Data!);
                if (affectedRows == 0)
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, AddProductService>("Failed to insert the products."));
                logEntries.Add(_logger.JsonLogInfo<ProductModel, AddProductService>($"Products inserted successfully.", affectedRows: affectedRows));
                return _serviceResultFactory.CreateServiceResults<ProductModel>(serviceResults.Data!, logEntries);
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, AddProductService>("An error occurred while inserting the products.", ex));
                return _serviceResultFactory.CreateServiceResults<ProductModel>(entities, logEntries);
            }
        }
    }
}
