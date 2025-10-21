using ProjectShop.Server.Application.Services;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Application.Services._BaseServices;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // NOTE: BASE SERVICES (OPEN GENERIC)
            services.AddTransient<IBaseAuthorizationServices, BaseAuthorizationService>();
            services.AddTransient(typeof(IBaseHelperServices<>), typeof(BaseHelperService<>));
            services.AddTransient(typeof(IBaseGetByTimeServices<,>), typeof(BaseGetByTimeService<,>));
            services.AddTransient(typeof(IServiceResultFactory<>), typeof(ServiceResultFactory<>));
            services.AddTransient(typeof(IGetSingleServices<,,>), typeof(BaseGetResultService<,,>));
            services.AddTransient(typeof(IGetMultipleServices<,,>), typeof(BaseGetResultsService<,,>));
            services.AddTransient(typeof(IBaseHelperReturnTEntityService<>), typeof(BaseHelperReturnTEntityService<>));

            // NOTE: Get Account Permission 
            
            return services;
        }
    }
}