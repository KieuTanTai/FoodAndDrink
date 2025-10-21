using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDBContext _context;

        // Main Entity Repositories
        public IAccountRepository Accounts { get; }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }
        public IInvoiceRepository Invoices { get; }
        public IPersonRepository People { get; }
        public IInventoryRepository Inventories { get; }
        public IRoleRepository Roles { get; }
        public IPermissionRepository Permissions { get; }
        public ISupplierRepository Suppliers { get; }
        public ICategoryRepository Categories { get; }
        public IEmployeeRepository Employees { get; }

        // Junction Table Repositories
        public IAccountRoleRepository AccountRoles { get; }
        public IAccountAdditionalPermissionRepository AccountAdditionalPermissions { get; }
        public IRolePermissionRepository RolePermissions { get; }
        public IProductCategoryRepository ProductCategories { get; }

        // Lookup Table Repositories
        public IBankRepository Banks { get; }
        public ICountryRepository Countries { get; }
        public IDisposeReasonRepository DisposeReasons { get; }
        public ILocationTypeRepository LocationTypes { get; }

        // Location Hierarchy Repositories
        public ILocationRepository Locations { get; }
        public ILocationCityRepository LocationCities { get; }
        public ILocationDistrictRepository LocationDistricts { get; }
        public ILocationWardRepository LocationWards { get; }

        // Product Type Repositories
        public IProductDrinkRepository ProductDrinks { get; }
        public IProductFruitRepository ProductFruits { get; }
        public IProductMeatRepository ProductMeats { get; }
        public IProductSnackRepository ProductSnacks { get; }
        public IProductVegetableRepository ProductVegetables { get; }

        // Product Related Repositories
        public IProductImageRepository ProductImages { get; }
        public IProductLotRepository ProductLots { get; }
        public IDetailProductLotRepository DetailProductLots { get; }

        // Sale & Event Repositories
        public ISaleEventRepository SaleEvents { get; }
        public ISaleEventImageRepository SaleEventImages { get; }
        public IDetailSaleEventRepository DetailSaleEvents { get; }

        // Cart & Invoice Detail Repositories
        public ICartRepository Carts { get; }
        public IDetailCartRepository DetailCarts { get; }
        public IDetailInvoiceRepository DetailInvoices { get; }

        // Inventory Management Repositories
        public IDetailInventoryRepository DetailInventories { get; }
        public IInventoryMovementRepository InventoryMovements { get; }
        public IDetailInventoryMovementRepository DetailInventoryMovements { get; }

        // Dispose & User Detail Repositories
        public IDisposeProductRepository DisposeProducts { get; }
        public ICustomerAddressRepository CustomerAddresses { get; }
        public IUserPaymentMethodRepository UserPaymentMethods { get; }

        public UnitOfWork(
            IDBContext context,
            IAccountRepository accountRepository,
            IProductRepository productRepository,
            ICustomerRepository customerRepository,
            IInvoiceRepository invoiceRepository,
            IPersonRepository personRepository,
            IInventoryRepository inventoryRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            ISupplierRepository supplierRepository,
            ICategoryRepository categoryRepository,
            IEmployeeRepository employeeRepository,
            IAccountRoleRepository accountRoleRepository,
            IAccountAdditionalPermissionRepository accountAdditionalPermissionRepository,
            IRolePermissionRepository rolePermissionRepository,
            IProductCategoryRepository productCategoryRepository,
            IBankRepository bankRepository,
            ICountryRepository countryRepository,
            IDisposeReasonRepository disposeReasonRepository,
            ILocationTypeRepository locationTypeRepository,
            ILocationRepository locationRepository,
            ILocationCityRepository locationCityRepository,
            ILocationDistrictRepository locationDistrictRepository,
            ILocationWardRepository locationWardRepository,
            IProductDrinkRepository productDrinkRepository,
            IProductFruitRepository productFruitRepository,
            IProductMeatRepository productMeatRepository,
            IProductSnackRepository productSnackRepository,
            IProductVegetableRepository productVegetableRepository,
            IProductImageRepository productImageRepository,
            IProductLotRepository productLotRepository,
            IDetailProductLotRepository detailProductLotRepository,
            ISaleEventRepository saleEventRepository,
            ISaleEventImageRepository saleEventImageRepository,
            IDetailSaleEventRepository detailSaleEventRepository,
            ICartRepository cartRepository,
            IDetailCartRepository detailCartRepository,
            IDetailInvoiceRepository detailInvoiceRepository,
            IDetailInventoryRepository detailInventoryRepository,
            IInventoryMovementRepository inventoryMovementRepository,
            IDetailInventoryMovementRepository detailInventoryMovementRepository,
            IDisposeProductRepository disposeProductRepository,
            ICustomerAddressRepository customerAddressRepository,
            IUserPaymentMethodRepository userPaymentMethodRepository)
        {
            _context = context;

            // Main Entity Repositories
            Accounts = accountRepository;
            Products = productRepository;
            Customers = customerRepository;
            Invoices = invoiceRepository;
            People = personRepository;
            Inventories = inventoryRepository;
            Roles = roleRepository;
            Permissions = permissionRepository;
            Suppliers = supplierRepository;
            Categories = categoryRepository;
            Employees = employeeRepository;

            // Junction Table Repositories
            AccountRoles = accountRoleRepository;
            AccountAdditionalPermissions = accountAdditionalPermissionRepository;
            RolePermissions = rolePermissionRepository;
            ProductCategories = productCategoryRepository;

            // Lookup Table Repositories
            Banks = bankRepository;
            Countries = countryRepository;
            DisposeReasons = disposeReasonRepository;
            LocationTypes = locationTypeRepository;

            // Location Hierarchy Repositories
            Locations = locationRepository;
            LocationCities = locationCityRepository;
            LocationDistricts = locationDistrictRepository;
            LocationWards = locationWardRepository;

            // Product Type Repositories
            ProductDrinks = productDrinkRepository;
            ProductFruits = productFruitRepository;
            ProductMeats = productMeatRepository;
            ProductSnacks = productSnackRepository;
            ProductVegetables = productVegetableRepository;

            // Product Related Repositories
            ProductImages = productImageRepository;
            ProductLots = productLotRepository;
            DetailProductLots = detailProductLotRepository;

            // Sale & Event Repositories
            SaleEvents = saleEventRepository;
            SaleEventImages = saleEventImageRepository;
            DetailSaleEvents = detailSaleEventRepository;

            // Cart & Invoice Detail Repositories
            Carts = cartRepository;
            DetailCarts = detailCartRepository;
            DetailInvoices = detailInvoiceRepository;

            // Inventory Management Repositories
            DetailInventories = detailInventoryRepository;
            InventoryMovements = inventoryMovementRepository;
            DetailInventoryMovements = detailInventoryMovementRepository;

            // Dispose & User Detail Repositories
            DisposeProducts = disposeProductRepository;
            CustomerAddresses = customerAddressRepository;
            UserPaymentMethods = userPaymentMethodRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.RollbackTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
