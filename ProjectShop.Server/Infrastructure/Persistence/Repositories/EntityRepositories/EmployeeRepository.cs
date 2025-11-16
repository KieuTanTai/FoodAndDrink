using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class EmployeeRepository(IFoodAndDrinkShopDbContext context, IMaxReturnRecordsRule maxReturnRecordsRule, IDefaultPageSizeRule defaultPageSizeRule)
        : Repository<Employee>(context, maxReturnRecordsRule, defaultPageSizeRule), IEmployeeRepository
    {
        #region Query by PersonId

        public async Task<Employee?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken)
            => await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(employee => employee.PersonId == personId, cancellationToken);

        public async Task<IEnumerable<Employee>> GetByPersonIdsAsync(IEnumerable<uint> personIds, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .AsNoTracking()
                .Where(employee => personIds.Contains(employee.PersonId) && employee.EmployeeId > cursor)
                .OrderBy(employee => employee.EmployeeId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by Salary

        public async Task<IEnumerable<Employee>> GetBySalaryRangeAsync(decimal minSalary, decimal maxSalary, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .AsNoTracking()
                .Where(employee => employee.EmployeeSalary >= minSalary
                    && employee.EmployeeSalary <= maxSalary
                    && employee.EmployeeId > cursor)
                .OrderBy(employee => employee.EmployeeId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by EmployeeHireDate

        public async Task<IEnumerable<Employee>> GetByHireDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, employee => employee.EmployeeHireDate, false, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query by Status

        public async Task<IEnumerable<Employee>> GetByStatusAsync(bool? status, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .AsNoTracking()
                .Include(employee => employee.Person)
                .Where(employee => employee.Person.PersonStatus == status && employee.EmployeeId > cursor)
                .OrderBy(employee => employee.EmployeeId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<Employee?> GetNavigationByIdAsync(uint id, EmployeeNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Employee> query = _dbSet.AsNoTracking();
            query = ApplyNavigationOptions(query, options);

            return await query.FirstOrDefaultAsync(employee => employee.EmployeeId == id, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetNavigationByIdsAsync(IEnumerable<uint> ids, EmployeeNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            IQueryable<Employee> query = _dbSet.AsNoTracking();
            query = ApplyNavigationOptions(query, options);

            return await query
                .Where(employee => ids.Contains(employee.EmployeeId) && employee.EmployeeId > cursor)
                .OrderBy(employee => employee.EmployeeId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee> ExplicitLoadAsync(Employee entity, EmployeeNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetPerson)
                await _context.Entry(entity).Reference(employee => employee.Person).LoadAsync(cancellationToken);
            if (options.IsGetEmployeeWorkLocation)
                await _context.Entry(entity).Reference(employee => employee.EmployeeWorkLocation).LoadAsync(cancellationToken);
            if (options.IsGetDisposeProducts)
                await _context.Entry(entity).Collection(employee => employee.DisposeProducts).LoadAsync(cancellationToken);
            if (options.IsGetInvoices)
                await _context.Entry(entity).Collection(employee => employee.Invoices).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<Employee>> ExplicitLoadAsync(IEnumerable<Employee> entities, EmployeeNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Person> persons = [];
            List<Location> workLocations = [];
            List<DisposeProduct> disposeProducts = [];
            List<Invoice> invoices = [];
            var employeeIds = entities.Select(entity => entity.EmployeeId).Distinct().ToList();
            var personIds = entities.Select(entity => entity.PersonId).Distinct().ToList();
            var workLocationIds = entities.Select(entity => entity.EmployeeWorkLocationId).Distinct().ToList();

            if (options.IsGetPerson)
                persons = await _context.People.Where(person => personIds.Contains(person.PersonId)).ToListAsync(cancellationToken);
            if (options.IsGetEmployeeWorkLocation)
                workLocations = await _context.Locations.Where(location => workLocationIds.Contains(location.LocationId)).ToListAsync(cancellationToken);
            if (options.IsGetDisposeProducts)
                disposeProducts = await _context.DisposeProducts.Where(dispose => employeeIds.Contains(dispose.DisposeByEmployeeId)).ToListAsync(cancellationToken);
            if (options.IsGetInvoices)
                invoices = await _context.Invoices.Where(invoice => employeeIds.Contains(invoice.EmployeeId)).ToListAsync(cancellationToken);

            return MappingToEmployees(entities, persons, workLocations, disposeProducts, invoices);
        }

        #endregion

        #region Helper Methods for Apply Navigation Options and Mapping

        private static IQueryable<Employee> ApplyNavigationOptions(IQueryable<Employee> query, EmployeeNavigationOptions? options)
        {
            if (options == null)
                return query;

            if (options.IsGetPerson)
                query = query.Include(employee => employee.Person);
            if (options.IsGetEmployeeWorkLocation)
                query = query.Include(employee => employee.EmployeeWorkLocation);
            if (options.IsGetDisposeProducts)
                query = query.Include(employee => employee.DisposeProducts);
            if (options.IsGetInvoices)
                query = query.Include(employee => employee.Invoices);

            return query;
        }

        private static IEnumerable<Employee> MappingToEmployees(IEnumerable<Employee> employees,
            List<Person> persons, List<Location> workLocations, List<DisposeProduct> disposeProducts,
            List<Invoice> invoices)
        {
            if (employees == null || !employees.Any())
                return [];

            Dictionary<uint, Person> personsDict = [];
            Dictionary<uint, Location> workLocationsDict = [];
            ILookup<uint, DisposeProduct> disposeProductsLookup = Enumerable.Empty<DisposeProduct>().ToLookup(key => default(uint));
            ILookup<uint, Invoice> invoicesLookup = Enumerable.Empty<Invoice>().ToLookup(key => default(uint));

            if (persons != null && persons.Count > 0)
                personsDict = persons.ToDictionary(person => person.PersonId);
            if (workLocations != null && workLocations.Count > 0)
                workLocationsDict = workLocations.ToDictionary(location => location.LocationId);
            if (disposeProducts != null && disposeProducts.Count > 0)
                disposeProductsLookup = disposeProducts.ToLookup(dispose => dispose.DisposeByEmployeeId);
            if (invoices != null && invoices.Count > 0)
                invoicesLookup = invoices.ToLookup(invoice => invoice.EmployeeId);

            // mapping
            foreach (Employee employee in employees)
            {
                if (personsDict.TryGetValue(employee.PersonId, out var person))
                    employee.Person = person ?? new();
                if (workLocationsDict.TryGetValue(employee.EmployeeWorkLocationId, out var workLocation))
                    employee.EmployeeWorkLocation = workLocation ?? new();
                employee.DisposeProducts = [.. disposeProductsLookup[employee.EmployeeId]];
                employee.Invoices = [.. invoicesLookup[employee.EmployeeId]];
            }
            return employees;
        }

        #endregion
    }
}
