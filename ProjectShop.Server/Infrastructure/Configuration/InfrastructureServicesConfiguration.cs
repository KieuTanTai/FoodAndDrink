using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Data;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Persistence.Repositories;
using ProjectShop.Server.Infrastructure.Services;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringChecker, StringChecker>();
            services.AddSingleton<IStringConverter, StringConverter>();
            services.AddSingleton<IColumnService, ColumnService>();
            services.AddSingleton<IHashPassword, HashPaswordService>();
            services.AddSingleton<IClock, SystemClockService>();
            services.AddSingleton<IClock>(provider => new FakeClockService { UtcNow = new DateTime(2030, 12, 31) });
            services.AddSingleton<IMaxGetRecord>(provider => new MaxGetRecordService { MaxGetRecord = 500 });

            string connectionString = AppConfigConnection.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string is incorrect or empty. Please check configuration.");
            services.AddSingleton<IDbConnectionFactory>(provider => new MySqlConnectionFactory(connectionString));

            // Registering DAOs
            services.AddScoped<IDAO<AccountModel>, AccountDAO>();
            services.AddTransient<IDAO<BankModel>, BankDAO>();
            services.AddTransient<IDAO<CartModel>, CartDAO>();
            services.AddTransient<IDAO<CategoryModel>, CategoryDAO>();
            services.AddTransient<IDAO<CustomerAddressModel>, CustomerAddressDAO>();
            services.AddTransient<IDAO<CustomerModel>, CustomerDAO>();
            services.AddTransient<IDAO<DetailCartModel>, DetailCartDAO>();
            services.AddTransient<IDAO<DetailInventoryModel>, DetailInventoryDAO>();
            services.AddTransient<INoneUpdateDAO<DetailInventoryMovementModel>, DetailInventoryMovementDAO>();
            services.AddTransient<INoneUpdateDAO<DetailInvoiceModel>, DetailInvoiceDAO>();
            services.AddTransient<INoneUpdateDAO<DetailProductLotModel>, DetailProductLotDAO>();
            services.AddTransient<INoneUpdateDAO<DetailSaleEventModel>, DetailSaleEventDAO>();
            services.AddTransient<INoneUpdateDAO<DisposeProductModel>, DisposeProductDAO>();
            services.AddTransient<IDAO<DisposeReasonModel>, DisposeReasonDAO>();
            services.AddTransient<IDAO<EmployeeModel>, EmployeeDAO>();
            services.AddTransient<IDAO<InventoryModel>, InventoryDAO>();
            services.AddTransient<INoneUpdateDAO<InventoryMovementModel>, InventoryMovementDAO>();
            services.AddTransient<INoneUpdateDAO<InvoiceModel>, InvoiceDAO>();
            services.AddTransient<INoneUpdateDAO<InvoiceDiscountModel>, InvoiceDiscountDAO>();
            services.AddTransient<IDAO<LocationCityModel>, LocationCityDAO>();
            services.AddTransient<IDAO<LocationModel>, LocationDAO>();
            services.AddTransient<IDAO<LocationDistrictModel>, LocationDistrictDAO>();
            services.AddTransient<IDAO<LocationTypeModel>, LocationTypeDAO>();
            services.AddTransient<IDAO<LocationWardModel>, LocationWardDAO>();
            services.AddTransient<IDAO<ProductCategoriesModel>, ProductCateogriesDAO>();
            services.AddTransient<IDAO<ProductModel>, ProductDAO>();
            services.AddTransient<IDAO<ProductImageModel>, ProductImageDAO>();
            services.AddTransient<INoneUpdateDAO<ProductLotModel>, ProductLotDAO>();
            services.AddTransient<INoneUpdateDAO<ProductLotInventoryModel>, ProductLotInventoryDAO>();
            services.AddTransient<IDAO<RoleModel>, RoleDAO>();
            services.AddScoped<IDAO<RolesOfUserModel>, RoleOfUserDAO>();
            services.AddTransient<IDAO<SaleEventModel>, SaleEventDAO>();
            services.AddTransient<IDAO<SaleEventImageModel>, SaleEventImageDAO>();
            services.AddTransient<IDAO<SupplierModel>, SupplierDAO>();
            services.AddTransient<IDAO<UserPaymentMethodModel>, UserPaymentMethodDAO>();

            // Registering unique DAOs
            services.AddScoped<IAccountDAO<AccountModel>, AccountDAO>();
            services.AddTransient<IGetRelativeAsync<BankModel>, BankDAO>();
            services.AddTransient<IGetByStatusAsync<BankModel>, BankDAO>();
            services.AddTransient<ICartDAO<CartModel>, CartDAO>();
            services.AddTransient<IGetByStatusAsync<CategoryModel>, CategoryDAO>();
            services.AddTransient<IGetRelativeAsync<CategoryModel>, CategoryDAO>();
            services.AddTransient<ICustomerAddressDAO<CustomerAddressModel>, CustomerAddressDAO>();
            services.AddTransient<IPersonDAO<CustomerModel>, CustomerDAO>();
            services.AddTransient<IDetailCartDAO<DetailCartModel>, DetailCartDAO>();
            services.AddTransient<IDetailInventoryDAO<DetailInventoryModel>, DetailInventoryDAO>();
            services.AddTransient<IDetailInventoryMovementDAO<DetailInventoryMovementModel>, DetailInventoryMovementDAO>();
            services.AddTransient<IDetailInvoiceDAO<DetailInvoiceModel>, DetailInvoiceDAO>();
            services.AddTransient<IDetailProductLotDAO<DetailProductLotModel, DetailProductLotKey>, DetailProductLotDAO>();
            services.AddTransient<IDetailSaleEventDAO<DetailSaleEventModel>, DetailSaleEventDAO>();
            services.AddTransient<IDisposeProductDAO<DisposeProductModel>, DisposeProductDAO>();
            services.AddTransient<IGetRelativeAsync<DisposeReasonModel>, DisposeReasonDAO>();
            services.AddTransient<IEmployeeDAO<EmployeeModel>, EmployeeDAO>();
            services.AddTransient<IInventoryDAO<InventoryModel>, InventoryDAO>();
            services.AddTransient<IInventoryMovementDAO<InventoryMovementModel>, InventoryMovementDAO>();
            services.AddTransient<IInvoiceDAO<InvoiceModel>, InvoiceDAO>();
            services.AddTransient<IInvoiceDiscountDAO<InvoiceDiscountModel, InvoiceDiscountKey>, InvoiceDiscountDAO>();
            services.AddTransient<ILocationDAO<LocationModel>, LocationDAO>();
            services.AddTransient<IGetRelativeAsync<LocationCityModel>, LocationCityDAO>();
            services.AddTransient<IGetByStatusAsync<LocationCityModel>, LocationCityDAO>();
            services.AddTransient<IGetRelativeAsync<LocationDistrictModel>, LocationDistrictDAO>();
            services.AddTransient<IGetByStatusAsync<LocationDistrictModel>, LocationDistrictDAO>();
            services.AddTransient<IGetRelativeAsync<LocationTypeModel>, LocationTypeDAO>();
            services.AddTransient<IGetByStatusAsync<LocationTypeModel>, LocationTypeDAO>();
            services.AddTransient<IGetRelativeAsync<LocationWardModel>, LocationWardDAO>();
            services.AddTransient<IGetByStatusAsync<LocationWardModel>, LocationWardDAO>();
            services.AddTransient<IProductCategoriesDAO<ProductCategoriesModel, ProductCategoriesKey>, ProductCateogriesDAO>();
            services.AddTransient<IPersonDAO<EmployeeModel>, EmployeeDAO>();
            services.AddTransient<IPersonDAO<CustomerModel>, CustomerDAO>();
            services.AddTransient<IProductDAO<ProductModel>, ProductDAO>();
            services.AddTransient<IProductImageDAO<ProductImageModel>, ProductImageDAO>();
            services.AddTransient<IProductLotDAO<ProductLotModel>, ProductLotDAO>();
            services.AddTransient<IProductLotInventoryDAO<ProductLotInventoryModel, ProductLotInventoryKey>, ProductLotInventoryDAO>();
            services.AddTransient<IRoleDAO<RoleModel>, RoleDAO>();
            services.AddScoped<IRoleOfUserDAO<RolesOfUserModel, RolesOfUserKey>, RoleOfUserDAO>();
            services.AddTransient<ISaleEventDAO<SaleEventModel>, SaleEventDAO>();
            services.AddTransient<ISaleEventImageDAO<SaleEventImageModel>, SaleEventImageDAO>();
            services.AddTransient<ISupplierDAO<SupplierModel>, SupplierDAO>();
            services.AddTransient<IUserPaymentMethodDAO<UserPaymentMethodModel>, UserPaymentMethodDAO>();
            return services;
        }
    }
}
