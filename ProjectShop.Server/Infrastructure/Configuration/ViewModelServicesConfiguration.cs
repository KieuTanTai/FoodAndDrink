using Microsoft.Extensions.DependencyInjection;
using ProjectShop.Server.Core.Interfaces.IServices.ICategory;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ViewModelServicesConfiguration
    {
        public static IServiceCollection AddViewModelServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
