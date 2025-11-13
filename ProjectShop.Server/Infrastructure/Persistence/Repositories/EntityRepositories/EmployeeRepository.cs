using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class EmployeeRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Employee>(context, maxGetRecord), IEmployeeRepository
    {
        // Query by PersonId
        public async Task<Employee?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(employee => employee.PersonId == personId, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(employee => personIds.Contains(employee.PersonId))
                .ToListAsync(cancellationToken);
        }

        // Query by Position (Note: Employee entity doesn't have Position field, so this may need adjustment)
        public async Task<IEnumerable<Employee>> GetByPositionAsync(string position, CancellationToken cancellationToken = default)
        {
            // TODO: If Employee needs a Position field, add it to the entity
            // For now, returning empty list as Position field doesn't exist
            return await Task.FromResult(Enumerable.Empty<Employee>());
        }

        // Query by Salary
        public async Task<IEnumerable<Employee>> GetBySalaryRangeAsync(decimal minSalary, decimal maxSalary, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(employee => employee.EmployeeSalary >= minSalary && employee.EmployeeSalary <= maxSalary)
                .ToListAsync(cancellationToken);
        }

        // Query by HireDate
        public async Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(employee => employee.EmployeeHireDate >= startDate && employee.EmployeeHireDate <= endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetByHireYearAsync(int year, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Where(employee => employee.EmployeeHireDate.Year == year)
                .ToListAsync(cancellationToken);
        }

        // Query by Status (Note: Employee doesn't have Status field, but Person does)
        public async Task<IEnumerable<Employee>> GetByStatusAsync(bool? status, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(employee => employee.Person)
                .Where(employee => employee.Person.PersonStatus == status)
                .ToListAsync(cancellationToken);
        }

        // Query with navigation properties
        public async Task<Employee?> GetByIdWithNavigationAsync(uint employeeId, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(employee => employee.Person)
                    .ThenInclude(person => person.Account)
                .Include(employee => employee.Location)
                    .ThenInclude(location => location.LocationCity)
                .Include(employee => employee.Location)
                    .ThenInclude(location => location.LocationDistrict)
                .Include(employee => employee.Location)
                    .ThenInclude(location => location.LocationWard)
                .Include(employee => employee.EmployeeCity)
                .Include(employee => employee.EmployeeDistrict)
                .Include(employee => employee.EmployeeWard)
                .Include(employee => employee.DisposeProducts)
                .Include(employee => employee.Invoices)
                    .ThenInclude(invoice => invoice.Customer)
                .FirstOrDefaultAsync(employee => employee.EmployeeId == employeeId, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(employee => employee.Person)
                    .ThenInclude(person => person.Account)
                .Include(employee => employee.Location)
                .Include(employee => employee.EmployeeCity)
                .Include(employee => employee.EmployeeDistrict)
                .Include(employee => employee.EmployeeWard)
                .Include(employee => employee.DisposeProducts)
                .Include(employee => employee.Invoices)
                .ToListAsync(cancellationToken);
        }

        // Business queries
        public async Task<IEnumerable<Employee>> GetActiveEmployeesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .AsNoTracking()
                .Include(employee => employee.Person)
                .Where(employee => employee.Person.PersonStatus == true)
                .ToListAsync(cancellationToken);
        }
    }
}
