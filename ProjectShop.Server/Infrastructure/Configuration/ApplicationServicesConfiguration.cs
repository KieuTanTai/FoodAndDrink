using ProjectShop.Server.Application.Services;
using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Application.Services.Product;
using ProjectShop.Server.Application.Services.Roles;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;
using ProjectShop.Server.Core.ValueObjects.GetNavigationPropertyOptions;
using TLGames.Application.Services;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // NOTE: BASE SERVICES (OPEN GENERIC)
            services.AddTransient(typeof(IBaseAuthorizationServices), typeof(BaseAuthorizationService));
            services.AddTransient(typeof(IBaseHelperServices<>), typeof(BaseHelperService<>));
            services.AddTransient(typeof(IBaseGetByTimeServices<,>), typeof(BaseGetByTimeService<,>));
            services.AddTransient(typeof(IServiceResultFactory<>), typeof(ServiceResultFactory<>));
            services.AddTransient(typeof(IGetSingleServices<,,>), typeof(BaseGetResultService<,,>));
            services.AddTransient(typeof(IGetMultipleServices<,,>), typeof(BaseGetResultsService<,,>));

            // NOTE: ACCOUNT SERVICES
            services.AddTransient<ISearchAccountServices<AccountModel, AccountNavigationOptions>, SearchAccountService>();
            services.AddTransient<IUpdateAccountServices, UpdateAccountService>();
            services.AddTransient<IForgotPasswordServices, ForgotPasswordService>();
            services.AddScoped<ILoginServices<AccountModel, AccountNavigationOptions>, LoginService>();
            services.AddScoped<ISignupServices<AccountModel>, SignupService>();

            // NOTE: ROLES SERVICES
            services.AddTransient<IAddAccountRoleServices<RolesOfUserModel, RolesOfUserKey>, AddAccountRoleService>();
            services.AddTransient<IAddRoleServices<RoleModel>, AddRoleService>();
            services.AddTransient<IUpdateRoleServices, UpdateRoleService>();
            services.AddTransient<IDeleteAccountRoleServices<RolesOfUserKey>, DeleteAccountRoleService>();
            services.AddTransient<ISearchAccountRoleServices<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey>, SearchAccountRoleService>();
            services.AddTransient<ISearchRoleServices<RoleModel, RoleNavigationOptions>, SearchRoleService>();

            // NOTE: IGetNavigationPropertyService
            services.AddTransient<IBaseGetNavigationPropertyServices<AccountModel, AccountNavigationOptions>, BaseReturnAccountService>();
            services.AddTransient<IBaseGetNavigationPropertyServices<ProductModel, ProductNavigationOptions>, BaseReturnProductService>();
            services.AddTransient<IBaseGetNavigationPropertyServices<RolesOfUserModel, RolesOfUserNavigationOptions>, BaseReturnAccountRoleService>();
            services.AddTransient<IBaseGetNavigationPropertyServices<RoleModel, RoleNavigationOptions>, BaseReturnRoleService>();
            services.AddTransient<IBaseGetNavigationPropertyServices<ProductModel, ProductNavigationOptions>, BaseReturnProductService>();
            return services;
        }
    }
}