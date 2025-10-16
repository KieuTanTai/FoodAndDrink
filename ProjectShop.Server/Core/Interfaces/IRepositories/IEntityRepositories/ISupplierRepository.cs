using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Supplier repository interface with specific query methods
    /// </summary>
    public interface ISupplierRepository : IRepository<Supplier>
    {
        // Query by SupplierName
        Task<Supplier?> GetByNameAsync(string supplierName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Supplier>> GetByNamesAsync(IEnumerable<string> supplierNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Supplier>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Supplier>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Supplier?> GetByIdWithProductsAsync(uint supplierId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Supplier>> GetAllWithProductsAsync(CancellationToken cancellationToken = default);
    }
}
