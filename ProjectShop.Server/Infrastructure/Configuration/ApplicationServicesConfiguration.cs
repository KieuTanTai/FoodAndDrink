using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Entities.GetNavigationPropertyOptions;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISearchAccountService<AccountModel, AccountNavigationOptions>, SearchAccountService>();
            services.AddTransient<IUpdateAccountService, UpdateAccountService>();
            services.AddTransient<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<ILoginService<AccountModel, AccountNavigationOptions>, LoginService>();
            services.AddScoped<ISignupService<AccountModel>, SignupService>();
            return services;
        }
    }
}