using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IProduct
{
    public interface IUpdateProductServices
    {
        Task<JsonLogEntry> UpdateProductStatusAsync(string productBarcode, bool status);
        Task<JsonLogEntry> UpdateProductNameAsync(string productBarcode, string productName);
        Task<IEnumerable<JsonLogEntry>> UpdateProductNamesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newProductNames);
        Task<IEnumerable<JsonLogEntry>> UpdateProductStatusesAsync(IEnumerable<string> productBarcodes, bool status);
        Task<JsonLogEntry> UpdateProductBasePriceAsync(string productBarcode, decimal basePrice);
        Task<IEnumerable<JsonLogEntry>> UpdateProductBasePricesAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> basePrices);
        Task<JsonLogEntry> UpdateProductNetWeightAsync(string productBarcode, decimal netWeight);
        Task<IEnumerable<JsonLogEntry>> UpdateProductNetWeightsAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> netWeights);
        Task<JsonLogEntry> UpdateProductWeightRangeAsync(string productBarcode, string weightRange);
        Task<IEnumerable<JsonLogEntry>> UpdateProductWeightRangesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newWeightRanges);
        Task<JsonLogEntry> UpdateProductUnitAsync<TEnum>(string productBarcode, TEnum productUnit) where TEnum : Enum;
        Task<IEnumerable<JsonLogEntry>> UpdateProductUnitsAsync<TEnum>(IEnumerable<string> productBarcodes, IEnumerable<TEnum> productUnits) where TEnum : Enum;
        Task<JsonLogEntry> UpdateProductRatingAgeAsync(string productBarcode, string ratingAge);
        Task<IEnumerable<JsonLogEntry>> UpdateProductRatingAgesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> ratingAges);
    }
}
