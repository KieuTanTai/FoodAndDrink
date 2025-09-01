using Microsoft.AspNetCore.Mvc;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.IProduct;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductServicesController : ControllerBase
    {
        private readonly ILogger<ProductServicesController> _logger;
        private readonly IUpdateProductServices _updateProductServices;
        private readonly IAddProductServices<ProductModel> _addProductServices;
        private readonly ISearchProductServices<ProductModel, ProductNavigationOptions> _searchProductServices;

        public ProductServicesController(ILogger<ProductServicesController> logger, 
            IUpdateProductServices updateProductServices, 
            IAddProductServices<ProductModel> addProductServices, 
            ISearchProductServices<ProductModel, ProductNavigationOptions> searchProductServices)
        {
            _logger = logger;
            _addProductServices = addProductServices;
            _updateProductServices = updateProductServices;
            _searchProductServices = searchProductServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetAllAsync(options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products.");
                return StatusCode(500, "An error occurred while getting all products.");
            }
        }

        [HttpGet("all-by-unit")]
        public async Task<IActionResult> GetAllByEnumAsync([FromQuery] EProductUnit unit, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetAllByEnumAsync(unit, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products by unit.");
                return StatusCode(500, "An error occurred while getting all products by unit.");
            }
        }

        [HttpGet("by-category-id/{categoryId}")]
        public async Task<IActionResult> GetByCategoryIdAsync([FromRoute] uint categoryId, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByCategoryIdAsync(categoryId, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting products by categoryId {categoryId}.");
                return StatusCode(500, "An error occurred while getting products by category id.");
            }
        }

        [HttpPost("by-category-ids")]
        public async Task<IActionResult> GetByCategoryIdsAsync([FromBody] IEnumerable<uint> categoryIds, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByCategoryIdsAsync(categoryIds, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by category ids.");
                return StatusCode(500, "An error occurred while getting products by category ids.");
            }
        }

        [HttpGet("by-unit")]
        public async Task<IActionResult> GetByEnumAsync([FromQuery] EProductUnit unit, [FromQuery] ProductNavigationOptions? options)
        {
            try
            {
                var result = await _searchProductServices.GetByEnumAsync(unit, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by unit.");
                return StatusCode(500, "An error occurred while getting product by unit.");
            }
        }

        [HttpGet("by-input-price")]
        public async Task<IActionResult> GetByInputPriceAsync([FromQuery] decimal price, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByInputPriceAsync(price, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by input price.");
                return StatusCode(500, "An error occurred while getting products by input price.");
            }
        }

        [HttpGet("by-range-price")]
        public async Task<IActionResult> GetByRangePriceAsync([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByRangePriceAsync(minPrice, maxPrice, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by price range.");
                return StatusCode(500, "An error occurred while getting products by price range.");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByLikeStringAsync([FromQuery] string input, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByLikeStringAsync(input, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products.");
                return StatusCode(500, "An error occurred while searching products.");
            }
        }

        [HttpGet("by-net-weight")]
        public async Task<IActionResult> GetByNetWeightAsync([FromQuery] decimal netWeight, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByNetWeightAsync(netWeight, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by net weight.");
                return StatusCode(500, "An error occurred while getting products by net weight.");
            }
        }

        [HttpGet("by-product-name")]
        public async Task<IActionResult> GetByProductNameAsync([FromQuery] string productName, [FromQuery] ProductNavigationOptions? options)
        {
            try
            {
                var result = await _searchProductServices.GetByProductNameAsync(productName, options);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product by name.");
                return StatusCode(500, "An error occurred while getting product by name.");
            }
        }

        [HttpGet("by-rating-age")]
        public async Task<IActionResult> GetByRatingAgeAsync([FromQuery] string ratingAge, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByRatingAgeAsync(ratingAge, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by rating age.");
                return StatusCode(500, "An error occurred while getting products by rating age.");
            }
        }

        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatusAsync([FromQuery] bool status, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByStatusAsync(status, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by status.");
                return StatusCode(500, "An error occurred while getting products by status.");
            }
        }

        [HttpGet("by-supplier-id/{supplierId}")]
        public async Task<IActionResult> GetBySupplierIdAsync([FromRoute] uint supplierId, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetBySupplierIdAsync(supplierId, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting products by supplierId {supplierId}.");
                return StatusCode(500, "An error occurred while getting products by supplier id.");
            }
        }

        [HttpPost("by-supplier-ids")]
        public async Task<IActionResult> GetBySupplierIdsAsync([FromBody] IEnumerable<uint> supplierIds, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetBySupplierIdsAsync(supplierIds, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by supplier ids.");
                return StatusCode(500, "An error occurred while getting products by supplier ids.");
            }
        }

        [HttpGet("by-last-updated-date")]
        public async Task<IActionResult> GetByLastUpdatedDateAsync([FromQuery] DateTime lastUpdatedDate, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByLastUpdatedDateAsync(lastUpdatedDate, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by last updated date.");
                return StatusCode(500, "An error occurred while getting products by last updated date.");
            }
        }

        [HttpGet("by-last-updated-date-range")]
        public async Task<IActionResult> GetByLastUpdatedDateAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByLastUpdatedDateAsync(startDate, endDate, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by last updated date range.");
                return StatusCode(500, "An error occurred while getting products by last updated date range.");
            }
        }

        [HttpGet("by-last-updated-month-year")]
        public async Task<IActionResult> GetByLastUpdatedDateAsync([FromQuery] int year, [FromQuery] int month, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByLastUpdatedDateAsync(year, month, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by last updated month and year.");
                return StatusCode(500, "An error occurred while getting products by last updated month and year.");
            }
        }

        [HttpGet("by-last-updated-year")]
        public async Task<IActionResult> GetByLastUpdatedDateAsync([FromQuery] int year, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByLastUpdatedDateAsync(year, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by last updated year.");
                return StatusCode(500, "An error occurred while getting products by last updated year.");
            }
        }

        [HttpGet("by-created-date")]
        public async Task<IActionResult> GetByDateTimeAsync([FromQuery] DateTime dateTime, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByDateTimeAsync(dateTime, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by created date.");
                return StatusCode(500, "An error occurred while getting products by created date.");
            }
        }

        [HttpGet("by-created-date-range")]
        public async Task<IActionResult> GetByDateTimeRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByDateTimeRangeAsync(startDate, endDate, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by created date range.");
                return StatusCode(500, "An error occurred while getting products by created date range.");
            }
        }

        [HttpGet("by-created-month-year")]
        public async Task<IActionResult> GetByMonthAndYearAsync([FromQuery] int year, [FromQuery] int month, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByMonthAndYearAsync(year, month, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by created month and year.");
                return StatusCode(500, "An error occurred while getting products by created month and year.");
            }
        }

        [HttpGet("by-created-year")]
        public async Task<IActionResult> GetByYearAsync([FromQuery] int year, [FromQuery] ECompareType compareType, [FromQuery] ProductNavigationOptions? options, [FromQuery] int? maxGetCount)
        {
            try
            {
                var result = await _searchProductServices.GetByYearAsync(year, compareType, options, maxGetCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products by created year.");
                return StatusCode(500, "An error occurred while getting products by created year.");
            }
        }

        // Thêm 1 sản phẩm
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductModel product)
        {
            try
            {
                var result = await _addProductServices.AddProductAsync(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddProductAsync endpoint.");
                return StatusCode(500, "An unexpected error occurred while adding the product.");
            }
        }

        // Thêm nhiều sản phẩm
        [HttpPost("batch")]
        public async Task<IActionResult> AddProductsAsync([FromBody] IEnumerable<ProductModel> products)
        {
            try
            {
                var result = await _addProductServices.AddProductsAsync(products);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in AddProductsAsync endpoint.");
                return StatusCode(500, "An unexpected error occurred while adding the products.");
            }
        }

        // --- Update base price ---
        [HttpPut("{barcode}/base-price")]
        public async Task<IActionResult> UpdateProductBasePriceAsync([FromRoute] string barcode, [FromQuery] decimal basePrice)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductBasePriceAsync(barcode, basePrice);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating base price for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating product base price.");
            }
        }

        [HttpPut("base-price/batch")]
        public async Task<IActionResult> UpdateProductBasePricesAsync([FromBody] ProductUpdateDecimalBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductBasePricesAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product base prices (batch).");
                return StatusCode(500, "An error occurred while updating product base prices.");
            }
        }

        // --- Update name ---
        [HttpPut("{barcode}/name")]
        public async Task<IActionResult> UpdateProductNameAsync([FromRoute] string barcode, [FromQuery] string productName)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductNameAsync(barcode, productName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating product name for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating product name.");
            }
        }

        [HttpPut("name/batch")]
        public async Task<IActionResult> UpdateProductNamesAsync([FromBody] ProductUpdateStringBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductNamesAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product names (batch).");
                return StatusCode(500, "An error occurred while updating product names.");
            }
        }

        // --- Update net weight ---
        [HttpPut("{barcode}/net-weight")]
        public async Task<IActionResult> UpdateProductNetWeightAsync([FromRoute] string barcode, [FromQuery] decimal netWeight)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductNetWeightAsync(barcode, netWeight);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating net weight for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating net weight.");
            }
        }

        [HttpPut("net-weight/batch")]
        public async Task<IActionResult> UpdateProductNetWeightsAsync([FromBody] ProductUpdateDecimalBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductNetWeightsAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating net weights (batch).");
                return StatusCode(500, "An error occurred while updating net weights.");
            }
        }

        // --- Update rating age ---
        [HttpPut("{barcode}/rating-age")]
        public async Task<IActionResult> UpdateProductRatingAgeAsync([FromRoute] string barcode, [FromQuery] string ratingAge)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductRatingAgeAsync(barcode, ratingAge);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating rating age for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating rating age.");
            }
        }

        [HttpPut("rating-age/batch")]
        public async Task<IActionResult> UpdateProductRatingAgesAsync([FromBody] ProductUpdateStringBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductRatingAgesAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating rating ages (batch).");
                return StatusCode(500, "An error occurred while updating rating ages.");
            }
        }

        // --- Update status ---
        [HttpPut("{barcode}/status")]
        public async Task<IActionResult> UpdateProductStatusAsync([FromRoute] string barcode, [FromQuery] bool status)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductStatusAsync(barcode, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating status.");
            }
        }

        [HttpPut("status/batch")]
        public async Task<IActionResult> UpdateProductStatusesAsync([FromBody] ProductUpdateBoolBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductStatusesAsync(request.Barcodes, request.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating statuses (batch).");
                return StatusCode(500, "An error occurred while updating product statuses.");
            }
        }

        // --- Update unit ---
        [HttpPut("{barcode}/unit")]
        public async Task<IActionResult> UpdateProductUnitAsync([FromRoute] string barcode, [FromQuery] EProductUnit productUnit)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductUnitAsync(barcode, productUnit);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating unit for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating unit.");
            }
        }

        [HttpPut("unit/batch")]
        public async Task<IActionResult> UpdateProductUnitsAsync([FromBody] ProductUpdateEnumBatchRequest<EProductUnit> request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductUnitsAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating units (batch).");
                return StatusCode(500, "An error occurred while updating product units.");
            }
        }

        // --- Update weight range ---
        [HttpPut("{barcode}/weight-range")]
        public async Task<IActionResult> UpdateProductWeightRangeAsync([FromRoute] string barcode, [FromQuery] string weightRange)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductWeightRangeAsync(barcode, weightRange);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating weight range for barcode {barcode}.");
                return StatusCode(500, "An error occurred while updating weight range.");
            }
        }

        [HttpPut("weight-range/batch")]
        public async Task<IActionResult> UpdateProductWeightRangesAsync([FromBody] ProductUpdateStringBatchRequest request)
        {
            try
            {
                var result = await _updateProductServices.UpdateProductWeightRangesAsync(request.Barcodes, request.Values);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating weight ranges (batch).");
                return StatusCode(500, "An error occurred while updating product weight ranges.");
            }
        }
    }
}
