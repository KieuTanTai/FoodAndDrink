using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Application.Services.Product
{
    public class UpdateProductService : IUpdateProductService
    {
        private readonly ILogService _logger;
        private readonly IDAO<ProductModel> _baseDAO;
        private readonly IProductDAO<ProductModel> _productDAO;
        private readonly IBaseHelperService<ProductModel> _helper;

        public UpdateProductService(ILogService logger, IDAO<ProductModel> baseDAO, IProductDAO<ProductModel> productDAO, IBaseHelperService<ProductModel> helper)
        {
            _logger = logger;
            _baseDAO = baseDAO;
            _productDAO = productDAO;
            _helper = helper;
        }

        public async Task<JsonLogEntry> UpdateProductBasePriceAsync(string productBarcode, decimal basePrice)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductBasePricesAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> basePrices)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductNameAsync(string productBarcode, string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductNamesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newProductNames)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductNetWeightAsync(string productBarcode, decimal netWeight)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductNetWeightsAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> netWeights)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductRatingAgeAsync(string productBarcode, string ratingAge)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductRatingAgesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> ratingAges)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductStatusAsync(string productBarcode, bool status)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductStatusesAsync(IEnumerable<string> productBarcodes, bool status)
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductUnitAsync<TEnum>(string productBarcode, TEnum productUnit) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductUnitsAsync<TEnum>(IEnumerable<string> productBarcodes, IEnumerable<TEnum> productUnits) where TEnum : Enum
        {
            throw new NotImplementedException();
        }

        public async Task<JsonLogEntry> UpdateProductWeightRangeAsync(string productBarcode, string weightRange)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JsonLogEntry>> UpdateProductWeightRangesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newWeightRanges)
        {
            throw new NotImplementedException();
        }

        private async Task<JsonLogEntry> UpdateProductFieldAsync<T>(string input, T newValue, Func<string, Task<ProductModel?>> daoFunc, Action<ProductModel, T> updateAction, string fieldName)
        {
            try
            {
                ProductModel? product = await daoFunc(input);
                if (product == null)
                    return _logger.JsonLogWarning<ProductModel, UpdateProductService>($"Product with barcode {input} does not exist.");
                updateAction(product, newValue);
                int affectedRows = await _baseDAO.UpdateAsync(product);
                if (affectedRows == 0)
                    return _logger.JsonLogWarning<ProductModel, UpdateProductService>($"Failed to update the {fieldName} for product with barcode {input}.");
                return _logger.JsonLogInfo<ProductModel, UpdateProductService>($"Successfully updated the {fieldName} for product with barcode {input}.", affectedRows: affectedRows);
            }
            catch (Exception ex)
            {
                return _logger.JsonLogError<ProductModel, UpdateProductService>($"An error occurred while updating the {fieldName} for product with barcode {input}.", ex);
            }
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateProductFieldAsync<T>(IEnumerable<string> inputs, IEnumerable<T> newValues, Func<IEnumerable<string>, int?, Task<IEnumerable<ProductModel>>> daoFunc, Action<ProductModel, T> updateAction, string fieldName)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            try
            {
                var products = await daoFunc(inputs, null);
                if (products == null || !products.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, UpdateProductService>($"No products found for barcodes: {string.Join(", ", inputs)}."));
                    return logEntries;
                }
                var productDict = products.ToDictionary(p => p.ProductBarcode, p => p);
                var inputList = inputs.ToList();
                var newValueList = newValues.ToList();
                for (int i = 0; i < inputList.Count; i++)
                {
                    string input = inputList[i];
                    T newValue = newValueList[i];
                    if (productDict.TryGetValue(input, out var product))
                    {
                        updateAction(product, newValue);
                        int affectedRows = await _baseDAO.UpdateAsync(product);
                        if (affectedRows == 0)
                        {
                            logEntries.Add(_logger.JsonLogWarning<ProductModel, UpdateProductService>($"Failed to update the {fieldName} for product with barcode {input}."));
                        }
                        else
                        {
                            logEntries.Add(_logger.JsonLogInfo<ProductModel, UpdateProductService>($"Successfully updated the {fieldName} for product with barcode {input}.", affectedRows: affectedRows));
                        }
                    }
                    else
                    {
                        logEntries.Add(_logger.JsonLogWarning<ProductModel, UpdateProductService>($"Product with barcode {input} does not exist."));
                    }
                }
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, UpdateProductService>($"An error occurred while updating the {fieldName} for products.", ex));
            }
            return logEntries;
        }

        private async Task<IEnumerable<JsonLogEntry>> UpdateProductFieldAsync<T>(IEnumerable<string> inputs, T newValue, Func<IEnumerable<string>, int?, Task<IEnumerable<ProductModel>>> daoFunc, Action<ProductModel, T> updateAction, string fieldName)
        {
            List<JsonLogEntry> logEntries = new List<JsonLogEntry>();
            try
            {
                var products = await daoFunc(inputs, null);
                if (products == null || !products.Any())
                {
                    logEntries.Add(_logger.JsonLogWarning<ProductModel, UpdateProductService>($"No products found for barcodes: {string.Join(", ", inputs)}."));
                    return logEntries;
                }
                foreach (var product in products)
                {
                    updateAction(product, newValue);
                    int affectedRows = await _baseDAO.UpdateAsync(product);
                    if (affectedRows == 0)
                    {
                        logEntries.Add(_logger.JsonLogWarning<ProductModel, UpdateProductService>($"Failed to update the {fieldName} for product with barcode {product.ProductBarcode}."));
                    }
                    else
                    {
                        logEntries.Add(_logger.JsonLogInfo<ProductModel, UpdateProductService>($"Successfully updated the {fieldName} for product with barcode {product.ProductBarcode}.", affectedRows: affectedRows));
                    }
                }
            }
            catch (Exception ex)
            {
                logEntries.Add(_logger.JsonLogError<ProductModel, UpdateProductService>($"An error occurred while updating the {fieldName} for products.", ex));
            }
            return logEntries;
        }
    }
}
