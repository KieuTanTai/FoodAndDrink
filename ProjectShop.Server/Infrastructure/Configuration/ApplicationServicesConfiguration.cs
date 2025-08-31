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
            services.AddTransient(typeof(IBaseAuthorizationService), typeof(BaseAuthorizationService));
            services.AddTransient(typeof(IBaseHelperService<>), typeof(BaseHelperService<>));
            services.AddTransient(typeof(IBaseGetByTimeService<,>), typeof(BaseGetByTimeService<,>));
            services.AddTransient(typeof(IServiceResultFactory<>), typeof(ServiceResultFactory<>));
            services.AddTransient(typeof(IServiceGetSingle<,,>), typeof(BaseGetResultService<,,>));
            services.AddTransient(typeof(IServiceGetMultiple<,,>), typeof(BaseGetResultsService<,,>));

            // NOTE: ACCOUNT SERVICES
            services.AddTransient<ISearchAccountService<AccountModel, AccountNavigationOptions>, SearchAccountService>();
            services.AddTransient<IUpdateAccountService, UpdateAccountService>();
            services.AddTransient<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<ILoginService<AccountModel, AccountNavigationOptions>, LoginService>();
            services.AddScoped<ISignupService<AccountModel>, SignupService>();

            // NOTE: ROLES SERVICES
            services.AddTransient<IAddAccountRoleService<RolesOfUserModel, RolesOfUserKey>, AddAccountRoleService>();
            services.AddTransient<IAddRoleService<RoleModel>, AddRoleService>();
            services.AddTransient<IUpdateRoleService, UpdateRoleService>();
            services.AddTransient<IDeleteAccountRoleService<RolesOfUserKey>, DeleteAccountRoleService>();
            services.AddTransient<ISearchAccountRoleService<RolesOfUserModel, RolesOfUserNavigationOptions, RolesOfUserKey>, SearchAccountRoleService>();
            services.AddTransient<ISearchRoleService<RoleModel, RoleNavigationOptions>, SearchRoleService>();

            // NOTE: IGetNavigationPropertyService
            services.AddTransient<IBaseGetNavigationPropertyService<AccountModel, AccountNavigationOptions>, BaseReturnAccountService>();
            services.AddTransient<IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions>, BaseReturnProductService>();
            services.AddTransient<IBaseGetNavigationPropertyService<RolesOfUserModel, RolesOfUserNavigationOptions>, BaseReturnAccountRoleService>();
            services.AddTransient<IBaseGetNavigationPropertyService<RoleModel, RoleNavigationOptions>, BaseReturnRoleService>();
            services.AddTransient<IBaseGetNavigationPropertyService<ProductModel, ProductNavigationOptions>, BaseReturnProductService>();
            return services;
        }
    }
}