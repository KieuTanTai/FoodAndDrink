using ProjectShop.Server.Application.Services;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.Interfaces.IServices._IBase;
using ProjectShop.Server.Application.Services._BaseServices;
using ProjectShop.Server.Core.Interfaces.IServices.IAccount;
using ProjectShop.Server.Application.Services.AccountServices;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ApplicationServicesConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // NOTE: BASE SERVICES (OPEN GENERIC)
            services.AddScoped(typeof(IBaseHelperServices<>), typeof(BaseHelperServices<>));
            services.AddScoped(typeof(IServiceResultFactory<>), typeof(ServiceResultFactory<>));
            services.AddScoped<IBasePasswordMappingServices, BasePasswordMappingServices>();
            // services.AddScoped(typeof(IGetSingleServices<,>), typeof(BaseGetResultServices<,>));
            // services.AddScoped(typeof(IGetMultipleServices<,>), typeof(BaseGetResultsServices<,>));

            // NOTE: Get Account Permission 
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<ISignupServices, SignupServices>();
            services.AddScoped<IUpdatePasswordServices, UpdatePasswordServices>();
            services.AddScoped<IUpdateAccountServices, UpdateAccountServices>();
            services.AddScoped<ISearchAccountServices, SearchAccountServices>();
            return services;
        }
    }
}