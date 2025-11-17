using ProjectShop.Server.Core.Interfaces.IContext;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IPlatformRules;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.Interfaces.IRepositories;
using ProjectShop.Server.Core.Interfaces.IRepositories.IEntityRepositories;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Persistence.Repositories;
using ProjectShop.Server.Infrastructure.Persistence.Repositories.EntityRepositories;
using ProjectShop.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities.Context;
using ProjectShop.Server.Core.Enums;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IHashPassword, HashPasswordServices>();
            services.AddSingleton<IClock, SystemClockService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IClock>(provider => new FakeClockService { UtcNow = new DateTime(2030, 12, 31) });

            // Database Configuration
            var connectionString = GetConnectionString();
            var maxQueryRules = GetMaxQueryRules();
            var defaultPageSize = GetDefaultPageSize();

            // Register Platform Rules as Singletons
            services.AddSingleton<IMaxReturnRecordsRule>(provider => new MaxReturnRecordsRuleService { MaxRecords = maxQueryRules });
            services.AddSingleton<IDefaultPageSizeRule>(provider => new DefaultPageSizeRuleService { DefaultPageSize = defaultPageSize });
            services.AddSingleton<IDbConnectionFactory>(provider => new MySqlConnectionFactory(connectionString));

            // Add DbContext with connection string from configuration
            services.AddDbContext<FoodAndDrinkShopDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.Parse("12.0.2-mariadb")));

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

        private static string GetConnectionString()
            => AppConfigConnection.GetConnectionString() ?? throw new InvalidOperationException("Connection string is incorrect or empty. Please check configuration.");

        private static uint GetMaxQueryRules()
        {
            // Default to a safe value
            var maxQueryRules = ReadConfigRulesJson.Get(EPlatformRules.MAX_GET_RECORDS);
            if (maxQueryRules == 0)
                maxQueryRules = 200;
            return maxQueryRules;
        }

        private static uint GetDefaultPageSize()
        {
            // Default to a safe value
            var defaultPageSize = ReadConfigRulesJson.Get(EPlatformRules.DEFAULT_PAGE_SIZE);
            if (defaultPageSize == 0)
                defaultPageSize = 10;
            return defaultPageSize;
        }
    }
}
