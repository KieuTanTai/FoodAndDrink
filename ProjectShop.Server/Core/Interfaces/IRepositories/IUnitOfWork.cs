using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;

namespace ProjectShop.Server.Core.Interfaces.IRepositories
{
    /// <summary>
    /// Unit of Work pattern interface
    /// Manages database transactions and repository instances
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // Main Entity Repositories
        IAccountRepository Accounts { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IInvoiceRepository Invoices { get; }
        IPersonRepository People { get; }
        IInventoryRepository Inventories { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        ISupplierRepository Suppliers { get; }
        ICategoryRepository Categories { get; }
        IEmployeeRepository Employees { get; }

        // Junction Table Repositories
        IAccountRoleRepository AccountRoles { get; }
        IAccountAdditionalPermissionRepository AccountAdditionalPermissions { get; }
        IRolePermissionRepository RolePermissions { get; }
        IProductCategoryRepository ProductCategories { get; }

        // Lookup Table Repositories
        IBankRepository Banks { get; }
        ICountryRepository Countries { get; }
        IDisposeReasonRepository DisposeReasons { get; }
        ILocationTypeRepository LocationTypes { get; }

        // Location Hierarchy Repositories
        ILocationRepository Locations { get; }
        ILocationCityRepository LocationCities { get; }
        ILocationDistrictRepository LocationDistricts { get; }
        ILocationWardRepository LocationWards { get; }

        // Product Type Repositories
        IProductDrinkRepository ProductDrinks { get; }
        IProductFruitRepository ProductFruits { get; }
        IProductMeatRepository ProductMeats { get; }
        IProductSnackRepository ProductSnacks { get; }
        IProductVegetableRepository ProductVegetables { get; }

        // Product Related Repositories
        IProductImageRepository ProductImages { get; }
        IProductLotRepository ProductLots { get; }
        IDetailProductLotRepository DetailProductLots { get; }

        // Sale & Event Repositories
        ISaleEventRepository SaleEvents { get; }
        ISaleEventImageRepository SaleEventImages { get; }
        IDetailSaleEventRepository DetailSaleEvents { get; }

        // Cart & Invoice Detail Repositories
        ICartRepository Carts { get; }
        IDetailCartRepository DetailCarts { get; }
        IDetailInvoiceRepository DetailInvoices { get; }

        // Inventory Management Repositories
        IDetailInventoryRepository DetailInventories { get; }
        IInventoryMovementRepository InventoryMovements { get; }
        IDetailInventoryMovementRepository DetailInventoryMovements { get; }

        // Dispose & User Detail Repositories
        IDisposeProductRepository DisposeProducts { get; }
        ICustomerAddressRepository CustomerAddresses { get; }
        IUserPaymentMethodRepository UserPaymentMethods { get; }

        // Transaction management
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
