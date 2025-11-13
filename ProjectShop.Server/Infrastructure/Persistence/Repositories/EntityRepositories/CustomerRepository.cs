using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories
{
    public class CustomerRepository(IFoodAndDrinkShopDbContext context, IMaxGetRecord maxGetRecord) : Repository<Customer>(context, maxGetRecord), ICustomerRepository
    {
        // ICustomerRepository specific methods
        public async Task<Customer?> GetByPersonIdAsync(uint personId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(customer => customer.PersonId == personId, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetByPersonIdsAsync(IEnumerable<uint> personIds, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(customer => personIds.Contains(customer.PersonId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetByLoyaltyPointsRangeAsync(decimal minPoints, decimal maxPoints, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(customer => customer.CustomerLoyaltyPoints >= minPoints && customer.CustomerLoyaltyPoints <= maxPoints)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetTopByLoyaltyPointsAsync(int topCount, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderByDescending(customer => customer.CustomerLoyaltyPoints)
                .Take(topCount)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdWithNavigationAsync(uint customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(customer => customer.Person)
                    .ThenInclude(person => person.Account)
                .Include(customer => customer.Carts)
                    .ThenInclude(cart => cart.DetailCarts)
                .Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerCity)
                .Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerDistrict)
                .Include(customer => customer.CustomerAddresses)
                    .ThenInclude(address => address.CustomerWard)
                .Include(customer => customer.Invoices)
                    .ThenInclude(invoice => invoice.Employee)
                .Include(customer => customer.Invoices)
                    .ThenInclude(invoice => invoice.DetailInvoices)
                .Include(customer => customer.UserPaymentMethods)
                    .ThenInclude(paymentMethod => paymentMethod.Bank)
                .FirstOrDefaultAsync(customer => customer.CustomerId == customerId, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetAllWithNavigationAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Include(customer => customer.Person)
                    .ThenInclude(person => person.Account)
                .Include(customer => customer.Carts)
                .Include(customer => customer.CustomerAddresses)
                .Include(customer => customer.Invoices)
                .Include(customer => customer.UserPaymentMethods)
                .ToListAsync(cancellationToken);
        }

        // IBaseExplicitLoadRepository methods
        public async Task<Customer?> GetNavigationByIdAsync(uint id, CustomerNavigationOptions options, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.AsNoTracking();
            query = ApplyNavigationOptions(query, options);
            return await query.FirstOrDefaultAsync(customer => customer.CustomerId == id, cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetNavigationByIdsAsync(IEnumerable<uint> ids, CustomerNavigationOptions options, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers
                .AsNoTracking()
                .Where(customer => ids.Contains(customer.CustomerId));

            query = ApplyNavigationOptions(query, options);

            return await query
                .Skip((int)fromRecord!)
                .Take((int)pageSize!)
                .ToListAsync(cancellationToken);
        }

        public async Task<Customer> ExplicitLoadAsync(Customer entity, CustomerNavigationOptions options, CancellationToken cancellationToken = default)
        {
            if (options.IsGetPerson)
            {
                await _context.Entry(entity).Reference(customer => customer.Person).LoadAsync(cancellationToken);
            }

            if (options.IsGetCarts)
            {
                await _context.Entry(entity).Collection(customer => customer.Carts).LoadAsync(cancellationToken);
            }

            if (options.IsGetCustomerAddresses)
            {
                await _context.Entry(entity).Collection(customer => customer.CustomerAddresses).LoadAsync(cancellationToken);
            }

            if (options.IsGetInvoices)
            {
                await _context.Entry(entity).Collection(customer => customer.Invoices).LoadAsync(cancellationToken);
            }

            if (options.IsGetUserPaymentMethods)
            {
                await _context.Entry(entity).Collection(customer => customer.UserPaymentMethods).LoadAsync(cancellationToken);
            }

            return entity;
        }
        public async Task<IEnumerable<Customer>> ExplicitLoadAsync(IEnumerable<Customer> entities, CustomerNavigationOptions options, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default)
        {
            var customerList = entities
                .Skip((int)fromRecord!)
                .Take((int)pageSize!)
                .ToList();

            foreach (var customer in customerList)
            {
                await ExplicitLoadAsync(customer, options, cancellationToken);
            }

            return customerList;
        }

        // IBaseGetByDateTime methods
        public async Task<IEnumerable<Customer>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .Where(customer => customer.CustomerRegistrationDate >= startDate && customer.CustomerRegistrationDate <= endDate)
                .Skip((int)fromRecord!)
                .Take((int)pageSize!)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetByYearAsync(int year, ECompareType eCompareType, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.AsNoTracking();

            query = eCompareType switch
            {
                ECompareType.EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year == year),
                ECompareType.GREATER_THAN => query.Where(customer => customer.CustomerRegistrationDate.Year > year),
                ECompareType.LESS_THAN => query.Where(customer => customer.CustomerRegistrationDate.Year < year),
                ECompareType.GREATER_THAN_OR_EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year >= year),
                ECompareType.LESS_THAN_OR_EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year <= year),
                _ => query
            };

            return await query
                .Skip((int)fromRecord!)
                .Take((int)pageSize!)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Customer>> GetByMonthAndYearAsync(int month, int year, ECompareType eCompareType, uint? fromRecord = 0, uint? pageSize = 10, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.AsNoTracking();

            query = eCompareType switch
            {
                ECompareType.EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year == year && customer.CustomerRegistrationDate.Month == month),
                ECompareType.GREATER_THAN => query.Where(customer => customer.CustomerRegistrationDate.Year > year || (customer.CustomerRegistrationDate.Year == year && customer.CustomerRegistrationDate.Month > month)),
                ECompareType.LESS_THAN => query.Where(customer => customer.CustomerRegistrationDate.Year < year || (customer.CustomerRegistrationDate.Year == year && customer.CustomerRegistrationDate.Month < month)),
                ECompareType.GREATER_THAN_OR_EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year > year || (customer.CustomerRegistrationDate.Year == year && customer.CustomerRegistrationDate.Month >= month)),
                ECompareType.LESS_THAN_OR_EQUAL => query.Where(customer => customer.CustomerRegistrationDate.Year < year || (customer.CustomerRegistrationDate.Year == year && customer.CustomerRegistrationDate.Month <= month)),
                _ => query
            };

            return await query
                .Skip((int)fromRecord!)
                .Take((int)pageSize!)
                .ToListAsync(cancellationToken);
        }

        // Helper method
        private static IQueryable<Customer> ApplyNavigationOptions(IQueryable<Customer> query, CustomerNavigationOptions? options)
        {
            if (options == null)
                return query;

            if (options.IsGetPerson)
            {
                query = query.Include(customer => customer.Person)
                    .ThenInclude(person => person.Account);
            }

            if (options.IsGetCarts)
            {
                query = query.Include(customer => customer.Carts)
                    .ThenInclude(cart => cart.DetailCarts)
                    .ThenInclude(detailCart => detailCart.ProductBarcodeNavigation);
            }

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
    }
}
