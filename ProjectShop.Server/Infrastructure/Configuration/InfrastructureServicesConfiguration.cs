using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
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

            string connectionString = AppConfigConnection.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string is incorrect or empty. Please check configuration.");
            services.AddSingleton<IDbConnectionFactory>(provider => new MySqlConnectionFactory(connectionString));
            return services;
        }
    }
}
