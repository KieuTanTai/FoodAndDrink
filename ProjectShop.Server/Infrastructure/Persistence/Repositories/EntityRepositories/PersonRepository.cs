using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using Microsoft.EntityFrameworkCore;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class PersonRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Person>(context, maxGetRecord),
        IPersonRepository
    {
        #region Query by foreign id

        public async Task<Person?> GetByAccountIdAsync(uint accountId, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(person => person.AccountId == accountId, cancellationToken);

        public async Task<IEnumerable<Person>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, CancellationToken cancellationToken)
            => await _dbSet.Where(person => accountIds.Contains(person.AccountId)).ToListAsync(cancellationToken);

        public async Task<Person?> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(person => person.PersonEmail == email, cancellationToken);

        public async Task<IEnumerable<Person>> GetByEmailsAsync(IEnumerable<string> emails, CancellationToken cancellationToken)
            => await _dbSet.Where(person => emails.Contains(person.PersonEmail)).ToListAsync(cancellationToken);

        public async Task<Person?> GetByPhoneAsync(string phone, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(person => person.PersonPhone == phone, cancellationToken);

        public async Task<IEnumerable<Person>> GetByPhonesAsync(IEnumerable<string> phones, CancellationToken cancellationToken)
            => await _dbSet.Where(person => phones.Contains(person.PersonPhone)).ToListAsync(cancellationToken);

        public async Task<IEnumerable<Person>> SearchByNameAsync(string searchTerm, uint fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            return await _dbSet
                .Where(person => person.PersonName.Contains(searchTerm))
                .Skip((int)fromRecord)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Person?> GetByFullNameAsync(string name, CancellationToken cancellationToken)
            => await _dbSet.FirstOrDefaultAsync(person => person.PersonName == name, cancellationToken);

        public async Task<IEnumerable<Person>> GetByGenderAsync(bool isMale, uint fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            return await _dbSet
                .Where(person => person.PersonGender == isMale)
                .Skip((int)fromRecord)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Person>> GetByStatusAsync(bool? status, uint fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            return await _dbSet
                .Where(person => person.PersonStatus == status)
                .Skip((int)fromRecord)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by PersonCreatedDate

        public async Task<IEnumerable<Person>> GetByCreatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, person => person.PersonCreatedDate, fromRecord, pageSize, cancellationToken);

        public async Task<IEnumerable<Person>> GetByCreatedYearAsync(int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Person, bool> predicate = await GetCompareConditions(year, eCompareType, person => person.PersonCreatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        public async Task<IEnumerable<Person>> GetByCreatedMonthAndYearAsync(int month, int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Person, bool> predicate = await GetCompareConditions(month, year, eCompareType, person => person.PersonCreatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        #endregion

        #region Query by PersonLastUpdatedDate

        public async Task<IEnumerable<Person>> GetByLastUpdatedDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, person => person.PersonLastUpdatedDate, fromRecord, pageSize, cancellationToken);

        public async Task<IEnumerable<Person>> GetByLastUpdatedYearAsync(int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Person, bool> predicate = await GetCompareConditions(year, eCompareType, person => person.PersonLastUpdatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        public async Task<IEnumerable<Person>> GetByLastUpdatedMonthAndYearAsync(int month, int year, ECompareType eCompareType, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            Func<Person, bool> predicate = await GetCompareConditions(month, year, eCompareType, person => person.PersonLastUpdatedDate);
            return await GetByTimeAsync(predicate, fromRecord, pageSize, cancellationToken);
        }

        #endregion

        #region Query with Navigation Properties

        public async Task<Person?> GetByIdWithNavigationAsync(uint personId, CancellationToken cancellationToken)
        {
            var options = new PersonNavigationOptions
            {
                IsGetAccount = true,
                IsGetCustomer = true,
                IsGetEmployee = true
            };
            return await GetNavigationByIdAsync(personId, options, cancellationToken);
        }

        public async Task<Person?> GetNavigationByIdAsync(uint id, PersonNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Person> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(person => person.PersonId == id, cancellationToken);
        }

        public async Task<IEnumerable<Person>> GetNavigationByIdsAsync(IEnumerable<uint> ids, PersonNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            if (pageSize == null || pageSize == 0 || pageSize > _maxGetReturn)
                pageSize = _maxGetReturn;
            IQueryable<Person> query = _dbSet.AsQueryable();
            query = ApplyNavigationOptions(query, options);
            return await query
                .Where(person => ids.Contains(person.PersonId))
                .Skip((int)(fromRecord ?? 0))
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Person> ExplicitLoadAsync(Person entity, PersonNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetAccount)
                await _context.Entry(entity).Reference(person => person.Account).LoadAsync(cancellationToken);
            if (options.IsGetCustomer)
                await _context.Entry(entity).Reference(person => person.Customer).LoadAsync(cancellationToken);
            if (options.IsGetEmployee)
                await _context.Entry(entity).Reference(person => person.Employee).LoadAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<Person>> ExplicitLoadAsync(IEnumerable<Person> entities, PersonNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            List<Account> accounts = [];
            List<Customer> customers = [];
            List<Employee> employees = [];
            var personIds = entities.Select(person => person.PersonId).ToList();

            if (options.IsGetAccount)
            {
                var accountIds = entities.Select(person => person.AccountId).ToList();
                accounts = await _context.Accounts.Where(account => accountIds.Contains(account.AccountId)).ToListAsync(cancellationToken);
            }
            if (options.IsGetCustomer)
                customers = await _context.Customers.Where(customer => personIds.Contains(customer.PersonId)).ToListAsync(cancellationToken);
            if (options.IsGetEmployee)
                employees = await _context.Employees.Where(employee => personIds.Contains(employee.PersonId)).ToListAsync(cancellationToken);

            return MappingToPersons(entities, accounts, customers, employees);
        }

        #endregion

        #region Helper Methods for Mapping and Apply Navigation Options

        private static IQueryable<Person> ApplyNavigationOptions(IQueryable<Person> query, PersonNavigationOptions options)
        {
            if (options.IsGetAccount)
                query = query.Include(person => person.Account);
            if (options.IsGetCustomer)
                query = query.Include(person => person.Customer);
            if (options.IsGetEmployee)
                query = query.Include(person => person.Employee);

            return query;
        }

        private static IEnumerable<Person> MappingToPersons(IEnumerable<Person> persons, List<Account> accounts,
            List<Customer> customers, List<Employee> employees)
        {
            if (persons == null || !persons.Any())
                return [];

            Dictionary<uint, Account> accountsDict = [];
            Dictionary<uint, Customer> customersDict = [];
            Dictionary<uint, Employee> employeesDict = [];

            if (accounts != null && accounts.Count > 0)
                accountsDict = accounts.ToDictionary(account => account.AccountId);
            if (customers != null && customers.Count > 0)
                customersDict = customers.ToDictionary(customer => customer.PersonId);
            if (employees != null && employees.Count > 0)
                employeesDict = employees.ToDictionary(employee => employee.PersonId);

            // mapping
            foreach (Person person in persons)
            {
                if (accountsDict.TryGetValue(person.AccountId, out var account))
                    person.Account = account ?? new();
                if (customersDict.TryGetValue(person.PersonId, out var customer))
                    person.Customer = customer;
                if (employeesDict.TryGetValue(person.PersonId, out var employee))
                    person.Employee = employee;
            }
            return persons;
        }

        #endregion
    }
}
