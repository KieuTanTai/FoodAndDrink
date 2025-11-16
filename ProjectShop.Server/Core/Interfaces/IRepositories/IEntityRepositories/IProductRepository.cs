using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Product repository interface with specific query methods
    /// </summary>
    public interface IProductRepository : IRepository<Product>, IBaseExplicitLoadRepository<Product, string, ProductNavigationOptions>, IBaseGetByCreatedAndLastUpdatedDate<Product>
    {
        // Query by ProductBarcode (PK)
        Task<Product?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByBarcodesAsync(IEnumerable<string> barcodes, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by ProductName
        Task<Product?> GetByNameAsync(string productName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetManyByNamesAsync(IEnumerable<string> productNames, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> SearchByNameContainsAsync(string searchTerm, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Supplier
        Task<IEnumerable<Product>> GetBySupplierIdAsync(uint supplierId, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetBySupplierIdsAsync(IEnumerable<uint> supplierIds, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Country
        Task<IEnumerable<Product>> GetByCountryIdAsync(uint countryId, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetByCountryIdsAsync(IEnumerable<uint> countryIds, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by ProductType
        Task<IEnumerable<Product>> GetByTypeAsync(string productType, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by ProductStatus
        Task<IEnumerable<Product>> GetByStatusAsync(bool? status, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);

        // Query by Price Range
        Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice, uint? fromRecord = 0, uint? pageSize = null, CancellationToken cancellationToken = default);
    }
}
