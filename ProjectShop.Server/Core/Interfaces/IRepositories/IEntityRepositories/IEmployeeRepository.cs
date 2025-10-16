using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Employee repository interface with specific query methods
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Query by PersonId
        Task<Employee?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default);

        // Query by Position
        Task<IEnumerable<Employee>> GetByPositionAsync(string position, CancellationToken cancellationToken = default);

        // Query by Salary
        Task<IEnumerable<Employee>> GetBySalaryRangeAsync(decimal minSalary, decimal maxSalary, CancellationToken cancellationToken = default);

        // Query by HireDate
        Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetByHireYearAsync(int year, CancellationToken cancellationToken = default);

        // Query by Status
        Task<IEnumerable<Employee>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default);

        // Query with navigation properties
        Task<Employee?> GetByIdWithNavigationAsync(uint employeeId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default);

        // Business queries
        Task<IEnumerable<Employee>> GetActiveEmployeesAsync(CancellationToken cancellationToken = default);
    }
}
