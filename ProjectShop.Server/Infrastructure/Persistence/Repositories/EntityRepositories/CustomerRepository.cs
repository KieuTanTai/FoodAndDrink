using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class CustomerRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord)
        : Repository<Customer>(context, maxGetRecord), ICustomerRepository
    {
        #region Query by PersonId

        public async Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken)
            => await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(customer => customer.PersonId == personId, cancellationToken);

        public async Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .AsNoTracking()
                .Where(customer => personIds.Contains(customer.PersonId) && customer.CustomerId > cursor)
                .OrderBy(customer => customer.CustomerId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by LoyaltyPoints

        public async Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            return await _dbSet
                .AsNoTracking()
                .Where(customer => customer.CustomerLoyaltyPoints >= minPoints
                    && customer.CustomerLoyaltyPoints <= maxPoints
                    && customer.CustomerId > cursor)
                .OrderBy(customer => customer.CustomerId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);

            return await _dbSet
                .AsNoTracking()
                .OrderByDescending(customer => customer.CustomerLoyaltyPoints)
                .Take(Math.Min(topCount, (int)pageSize))
                .ToListAsync(cancellationToken);
        }

        #endregion

        #region Query by CustomerRegistrationDate

        public async Task<IEnumerable<Customer>> GetByRegistrationDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
            => await GetByDateTimeRangeAsync(startDate, endDate, customer => customer.CustomerRegistrationDate, false, fromRecord, pageSize, cancellationToken);

        #endregion

        #region Query with Navigation Properties

        public async Task<Customer?> GetNavigationByIdAsync(uint id, CustomerNavigationOptions options, CancellationToken cancellationToken)
        {
            IQueryable<Customer> query = _dbSet.AsNoTracking();
            query = ApplyNavigationOptions(query, options);

            return await query.FirstOrDefaultAsync(customer => customer.CustomerId == id, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetNavigationByIdsAsync(IEnumerable<uint> ids, CustomerNavigationOptions options, uint? fromRecord, uint? pageSize, CancellationToken cancellationToken)
        {
            pageSize = ValidateAndNormalizePageSize(pageSize);
            var cursor = fromRecord ?? 0;

            IQueryable<Customer> query = _dbSet.AsNoTracking();
            query = ApplyNavigationOptions(query, options);

            return await query
                .Where(customer => ids.Contains(customer.CustomerId) && customer.CustomerId > cursor)
                .OrderBy(customer => customer.CustomerId)
                .Take((int)pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer> ExplicitLoadAsync(Customer entity, CustomerNavigationOptions options, CancellationToken cancellationToken)
        {
            if (options.IsGetPerson)
                await _context.Entry(entity).Reference(customer => customer.Person).LoadAsync(cancellationToken);
            if (options.IsGetCarts)
                await _context.Entry(entity).Collection(customer => customer.Carts).LoadAsync(cancellationToken);
            if (options.IsGetCustomerAddresses)
                await _context.Entry(entity).Collection(customer => customer.CustomerAddresses).LoadAsync(cancellationToken);
            if (options.IsGetInvoices)
                await _context.Entry(entity).Collection(customer => customer.Invoices).LoadAsync(cancellationToken);
            if (options.IsGetUserPaymentMethods)
                await _context.Entry(entity).Collection(customer => customer.UserPaymentMethods).LoadAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<Customer>> ExplicitLoadAsync(IEnumerable<Customer> entities, CustomerNavigationOptions options, CancellationToken cancellationToken)
        {
            List<Person> persons = [];
            List<Cart> carts = [];
            List<CustomerAddress> customerAddresses = [];
            List<Invoice> invoices = [];
            List<UserPaymentMethod> userPaymentMethods = [];
            var entitiesIdSet = entities.Select(entity => entity.CustomerId).Distinct().ToList();

            if (options.IsGetPerson)
                persons = await _context.People.Where(person => entitiesIdSet.Contains(person.PersonId)).ToListAsync(cancellationToken);
            if (options.IsGetCarts)
                carts = await _context.Carts.Where(cart => entitiesIdSet.Contains(cart.CustomerId)).ToListAsync(cancellationToken);
            if (options.IsGetCustomerAddresses)
                customerAddresses = await _context.CustomerAddresses.Where(address => entitiesIdSet.Contains(address.CustomerId)).ToListAsync(cancellationToken);
            if (options.IsGetInvoices)
                invoices = await _context.Invoices.Where(invoice => entitiesIdSet.Contains(invoice.CustomerId)).ToListAsync(cancellationToken);
            if (options.IsGetUserPaymentMethods)
                userPaymentMethods = await _context.UserPaymentMethods.Where(method => entitiesIdSet.Contains(method.CustomerId)).ToListAsync(cancellationToken);
            return MappingToCustomers(entities, persons, carts, customerAddresses, invoices, userPaymentMethods);
        }

        #endregion

        #region Helper Methods for Apply Navigation Options

        private static IQueryable<Customer> ApplyNavigationOptions(IQueryable<Customer> query, CustomerNavigationOptions? options)
        {
            if (options == null)
                return query;

            if (options.IsGetPerson)
                query = query.Include(customer => customer.Person)
                    .ThenInclude(person => person.Account);

            if (options.IsGetCarts)
                query = query.Include(customer => customer.Carts)
                    .ThenInclude(cart => cart.DetailCarts);

            if (options.IsGetCustomerAddresses)
            {
                query = query.Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerCity);
                query = query.Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerDistrict);
                query = query.Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerWard);
            }

            if (options.IsGetInvoices)
            {
                query = query.Include(customer => customer.Invoices)
                    .ThenInclude(invoice => invoice.Employee);
                query = query.Include(customer => customer.Invoices)
                    .ThenInclude(invoice => invoice.DetailInvoices);
                query = query.Include(customer => customer.Invoices)
                    .ThenInclude(invoice => invoice.PaymentMethod);
            }

            if (options.IsGetUserPaymentMethods)
            {
                query = query.Include(customer => customer.UserPaymentMethods)
                    .ThenInclude(paymentMethod => paymentMethod.Bank);
            }

            return query;
        }

        private static IEnumerable<Customer> MappingToCustomers(IEnumerable<Customer> customers,
            List<Person> persons, List<Cart> carts, List<CustomerAddress> customerAddresses,
            List<Invoice> invoices, List<UserPaymentMethod> userPaymentMethods)
        {
            if (customers == null || !customers.Any())
                return [];

            Dictionary<uint, Person> personsDict = [];
            Dictionary<uint, Cart> cartsDict = [];
            ILookup<uint, CustomerAddress> customerAddressesLookup = Enumerable.Empty<CustomerAddress>().ToLookup(key => default(uint));
            ILookup<uint, Invoice> invoicesLookup = Enumerable.Empty<Invoice>().ToLookup(key => default(uint));
            ILookup<uint, UserPaymentMethod> userPaymentMethodsLookup = Enumerable.Empty<UserPaymentMethod>().ToLookup(key => default(uint));

            if (persons != null && persons.Count > 0)
                personsDict = persons.ToDictionary(person => person.PersonId);
            if (carts != null && carts.Count > 0)
                cartsDict = carts.ToDictionary(cart => cart.CustomerId);
            if (customerAddresses != null && customerAddresses.Count > 0)
                customerAddressesLookup = customerAddresses.ToLookup(address => address.CustomerId);
            if (invoices != null && invoices.Count > 0)
                invoicesLookup = invoices.ToLookup(invoice => invoice.CustomerId);
            if (userPaymentMethods != null && userPaymentMethods.Count > 0)
                userPaymentMethodsLookup = userPaymentMethods.ToLookup(method => method.CustomerId);

            // mapping
            foreach (Customer customer in customers)
            {
                if (personsDict.TryGetValue(customer.PersonId, out var person))
                    customer.Person = person ?? new();
                if (cartsDict.TryGetValue(customer.CustomerId, out var cart))
                    customer.Carts = [cart];
                customer.CustomerAddresses = [.. customerAddressesLookup[customer.CustomerId]];
                customer.Invoices = [.. invoicesLookup[customer.CustomerId]];
                customer.UserPaymentMethods = [.. userPaymentMethodsLookup[customer.CustomerId]];
            }
            return customers;
        }
        #endregion
    }
}
