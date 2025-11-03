using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.WebAPI.Controllers;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ControllerServiceConfiguration
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            // services.AddScoped<AccountController>();
            return services;
        }
    }
}
