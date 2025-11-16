using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories
{
    /// <summary>
    /// Employee repository interface with specific query methods
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Query by PersonId
        Task<Employee?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetByPersonIdsAsync(IEnumerable<uint> personIds, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken);

        // Query by Salary
        Task<IEnumerable<Employee>> GetBySalaryRangeAsync(decimal minSalary, decimal maxSalary, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken);

        // Query by HireDate
        Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken);

        // Query by Status
        Task<IEnumerable<Employee>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken);

        // Query with Navigation Properties
        Task<Employee?> GetNavigationByIdAsync(uint id, EmployeeNavigationOptions options, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetNavigationByIdsAsync(IEnumerable<uint> ids, EmployeeNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken);
        Task<Employee> ExplicitLoadAsync(Employee entity, EmployeeNavigationOptions options, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> ExplicitLoadAsync(IEnumerable<Employee> entities, EmployeeNavigationOptions options, CancellationToken cancellationToken);
    }
}
