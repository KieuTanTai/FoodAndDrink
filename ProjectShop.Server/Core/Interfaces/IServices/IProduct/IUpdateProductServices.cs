using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices.IProduct
{
    public interface IUpdateProductServices
    {
        Task<JsonLogEntry> UpdateProductStatusAsync(string productBarcode, bool status, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateProductNameAsync(string productBarcode, string productName, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductNamesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newProductNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductStatusesAsync(IEnumerable<string> productBarcodes, bool status, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateProductBasePriceAsync(string productBarcode, decimal basePrice, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductBasePricesAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> basePrices, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateProductNetWeightAsync(string productBarcode, decimal netWeight, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductNetWeightsAsync(IEnumerable<string> productBarcodes, IEnumerable<decimal> netWeights, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateProductWeightRangeAsync(string productBarcode, string weightRange, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductWeightRangesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> newWeightRanges, CancellationToken cancellationToken = default);
        Task<JsonLogEntry> UpdateProductUnitAsync<TEnum>(string productBarcode, TEnum productUnit, CancellationToken cancellationToken = default) where TEnum : Enum;
        Task<IEnumerable<JsonLogEntry>> UpdateProductUnitsAsync<TEnum>(IEnumerable<string> productBarcodes, IEnumerable<TEnum> productUnits, CancellationToken cancellationToken = default) where TEnum : Enum;
        Task<JsonLogEntry> UpdateProductRatingAgeAsync(string productBarcode, string ratingAge, CancellationToken cancellationToken = default);
        Task<IEnumerable<JsonLogEntry>> UpdateProductRatingAgesAsync(IEnumerable<string> productBarcodes, IEnumerable<string> ratingAges, CancellationToken cancellationToken = default);
    }
}
