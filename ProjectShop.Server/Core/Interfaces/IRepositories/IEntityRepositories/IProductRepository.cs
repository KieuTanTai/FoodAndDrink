using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Product repository interface with specific query methods
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        // Query by ProductBarcode (PK)
        Task<Product?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByBarcodesAsync(IEnumerable<string> barcodes, CancellationToken cancellationToken = default);

        // Query by ProductName
        Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByNamesAsync(IEnumerable<string> productNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by Supplier
        Task<IEnumerable<Product>> GetBySupplierIdAsync(uint supplierId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, CancellationToken cancellationToken = default);

        // Query by Country
        Task<IEnumerable<Product>> GetByCountryIdAsync(uint countryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByCountryIdsAsync(IEnumerable<uint> countryIds, CancellationToken cancellationToken = default);

        // Query by ProductType
        Task<IEnumerable<Product>> GetByTypeAsync(string productType, CancellationToken cancellationToken = default);

        // Query by ProductStatus
        Task<IEnumerable<Product>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query by Price Range
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken = default);

        // Query by dates
        Task<IEnumerable<Product>> GetByAddedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Product?> GetByBarcodeWithNavigationAsync(string barcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);
    }
}
