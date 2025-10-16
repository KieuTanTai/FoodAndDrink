using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Category repository interface with specific query methods
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        // Query by CategoryName
        Task<Category?> GetByNameAsync(string categoryName, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetByNamesAsync(IEnumerable<string> categoryNames, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> SearchByNameAsync(string searchTerm, CancellationToken cancellationToken = default);

        // Query by ParentCategoryId
        Task<IEnumerable<Category>> GetByParentCategoryIdAsync(uint? parentCategoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetRootCategoriesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetChildCategoriesAsync(uint parentCategoryId, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Category>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Category?> GetByIdWithHierarchyAsync(uint categoryId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Category>> GetCategoryTreeAsync(CancellationToken cancellationToken = default);
    }
}
