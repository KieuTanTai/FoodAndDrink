using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Application.Services.Roles;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.Role;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
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
            return services;
        }
    }
}