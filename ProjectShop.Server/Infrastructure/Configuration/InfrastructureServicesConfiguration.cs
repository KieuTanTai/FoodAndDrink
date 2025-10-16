using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Persistence.Repositories;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;
using ProjectShop.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities.Context;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringChecker, StringChecker>();
            services.AddSingleton<IStringConverter, StringConverter>();
            services.AddSingleton<IHashPassword, HashPasswordService>();
            services.AddSingleton<IClock, SystemClockService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IClock>(provider => new FakeClockService { UtcNow = new DateTime(2030, 12, 31) });
            services.AddSingleton<IMaxGetRecord>(provider => new MaxGetRecordService { MaxGetRecord = 500 });

            string connectionString = AppConfigConnection.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string is incorrect or empty. Please check configuration.");

            services.AddSingleton<IDbConnectionFactory>(provider => new MySqlConnectionFactory(connectionString));

            // Add DbContext with connection string from configuration
            services.AddDbContext<FoodAndDrinkShopDbContext>(options =>
                options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.0.2-mariadb")));

            // Register IDBContext interface for dependency injection
            // IDBContext extends IFoodAndDrinkShopDbContext, so both can be injected
            services.AddScoped<IDBContext>(sp => sp.GetRequiredService<FoodAndDrinkShopDbContext>());

            // Also register IFoodAndDrinkShopDbContext for those who prefer explicit naming
            services.AddScoped<IFoodAndDrinkShopDbContext>(sp => sp.GetRequiredService<FoodAndDrinkShopDbContext>());

            // Register Base Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Register Main Entity Repositories
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register Junction Table Repositories
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            services.AddScoped<IAccountAdditionalPermissionRepository, AccountAdditionalPermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            // Register Lookup Table Repositories
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IDisposeReasonRepository, DisposeReasonRepository>();
            services.AddScoped<ILocationTypeRepository, LocationTypeRepository>();

            // Register Location Hierarchy Repositories
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<ILocationCityRepository, LocationCityRepository>();
            services.AddScoped<ILocationDistrictRepository, LocationDistrictRepository>();
            services.AddScoped<ILocationWardRepository, LocationWardRepository>();

            // Register Product Type Repositories
            services.AddScoped<IProductDrinkRepository, ProductDrinkRepository>();
            services.AddScoped<IProductFruitRepository, ProductFruitRepository>();
            services.AddScoped<IProductMeatRepository, ProductMeatRepository>();
            services.AddScoped<IProductSnackRepository, ProductSnackRepository>();
            services.AddScoped<IProductVegetableRepository, ProductVegetableRepository>();

            // Register Product Related Repositories
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductLotRepository, ProductLotRepository>();
            services.AddScoped<IDetailProductLotRepository, DetailProductLotRepository>();

            // Register Sale & Event Repositories
            services.AddScoped<ISaleEventRepository, SaleEventRepository>();
            services.AddScoped<ISaleEventImageRepository, SaleEventImageRepository>();
            services.AddScoped<IDetailSaleEventRepository, DetailSaleEventRepository>();

            // Register Cart & Invoice Detail Repositories
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IDetailCartRepository, DetailCartRepository>();
            services.AddScoped<IDetailInvoiceRepository, DetailInvoiceRepository>();

            // Register Inventory Management Repositories
            services.AddScoped<IDetailInventoryRepository, DetailInventoryRepository>();
            services.AddScoped<IInventoryMovementRepository, InventoryMovementRepository>();
            services.AddScoped<IDetailInventoryMovementRepository, DetailInventoryMovementRepository>();

            // Register Dispose & User Detail Repositories
            services.AddScoped<IDisposeProductRepository, DisposeProductRepository>();
            services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
            services.AddScoped<IUserPaymentMethodRepository, UserPaymentMethodRepository>();

            // Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
