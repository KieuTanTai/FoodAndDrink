using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;

namespace ProjectShop.Server.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork(
        IFoodAndDrinkShopDbContext context,
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
        IUserPaymentMethodRepository userPaymentMethodRepository) : IUnitOfWork
    {
        private readonly IFoodAndDrinkShopDbContext _context = context;

        // Main Entity Repositories
        public IAccountRepository Accounts { get; } = accountRepository;
        public IProductRepository Products { get; } = productRepository;
        public ICustomerRepository Customers { get; } = customerRepository;
        public IInvoiceRepository Invoices { get; } = invoiceRepository;
        public IPersonRepository People { get; } = personRepository;
        public IInventoryRepository Inventories { get; } = inventoryRepository;
        public IRoleRepository Roles { get; } = roleRepository;
        public IPermissionRepository Permissions { get; } = permissionRepository;
        public ISupplierRepository Suppliers { get; } = supplierRepository;
        public ICategoryRepository Categories { get; } = categoryRepository;
        public IEmployeeRepository Employees { get; } = employeeRepository;

        // Junction Table Repositories
        public IAccountRoleRepository AccountRoles { get; } = accountRoleRepository;
        public IAccountAdditionalPermissionRepository AccountAdditionalPermissions { get; } = accountAdditionalPermissionRepository;
        public IRolePermissionRepository RolePermissions { get; } = rolePermissionRepository;
        public IProductCategoryRepository ProductCategories { get; } = productCategoryRepository;

        // Lookup Table Repositories
        public IBankRepository Banks { get; } = bankRepository;
        public ICountryRepository Countries { get; } = countryRepository;
        public IDisposeReasonRepository DisposeReasons { get; } = disposeReasonRepository;
        public ILocationTypeRepository LocationTypes { get; } = locationTypeRepository;

        // Location Hierarchy Repositories
        public ILocationRepository Locations { get; } = locationRepository;
        public ILocationCityRepository LocationCities { get; } = locationCityRepository;
        public ILocationDistrictRepository LocationDistricts { get; } = locationDistrictRepository;
        public ILocationWardRepository LocationWards { get; } = locationWardRepository;

        // Product Type Repositories
        public IProductDrinkRepository ProductDrinks { get; } = productDrinkRepository;
        public IProductFruitRepository ProductFruits { get; } = productFruitRepository;
        public IProductMeatRepository ProductMeats { get; } = productMeatRepository;
        public IProductSnackRepository ProductSnacks { get; } = productSnackRepository;
        public IProductVegetableRepository ProductVegetables { get; } = productVegetableRepository;

        // Product Related Repositories
        public IProductImageRepository ProductImages { get; } = productImageRepository;
        public IProductLotRepository ProductLots { get; } = productLotRepository;
        public IDetailProductLotRepository DetailProductLots { get; } = detailProductLotRepository;

        // Sale & Event Repositories
        public ISaleEventRepository SaleEvents { get; } = saleEventRepository;
        public ISaleEventImageRepository SaleEventImages { get; } = saleEventImageRepository;
        public IDetailSaleEventRepository DetailSaleEvents { get; } = detailSaleEventRepository;

        // Cart & Invoice Detail Repositories
        public ICartRepository Carts { get; } = cartRepository;
        public IDetailCartRepository DetailCarts { get; } = detailCartRepository;
        public IDetailInvoiceRepository DetailInvoices { get; } = detailInvoiceRepository;

        // Inventory Management Repositories
        public IDetailInventoryRepository DetailInventories { get; } = detailInventoryRepository;
        public IInventoryMovementRepository InventoryMovements { get; } = inventoryMovementRepository;
        public IDetailInventoryMovementRepository DetailInventoryMovements { get; } = detailInventoryMovementRepository;

        // Dispose & User Detail Repositories
        public IDisposeProductRepository DisposeProducts { get; } = disposeProductRepository;
        public ICustomerAddressRepository CustomerAddresses { get; } = customerAddressRepository;
        public IUserPaymentMethodRepository UserPaymentMethods { get; } = userPaymentMethodRepository;

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
