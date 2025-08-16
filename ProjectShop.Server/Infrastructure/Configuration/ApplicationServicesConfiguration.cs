using ProjectShop.Server.Application.Services.Account;
using ProjectShop.Server.Application.Services.Account.ForgotPassword;
using ProjectShop.Server.Application.Services.Account.Login;
using ProjectShop.Server.Application.Services.Account.Signup;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount.IForgotPassword;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount.ILogin;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount.ISignup;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISearchAccountService<AccountModel>, SearchAccountService>();
            services.AddTransient<IUpdateAccountStatusService, UpdateAccountStatusService>();
            services.AddTransient<IForgotPasswordService, ForgotPasswordService>();
            services.AddTransient<ILoginService<AccountModel>, LoginService>();
            services.AddTransient<ISignupService<AccountModel>, SignupService>();
            return services;
        }
    }
}