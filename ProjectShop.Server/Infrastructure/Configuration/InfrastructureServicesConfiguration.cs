using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using ProjectShop.Server.Infrastructure.Persistence.Repositories;
using ProjectShop.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities.Context;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class InfrastructureServicesConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringChecker, StringChecker>();
            services.AddSingleton<IStringConverter, StringConverter>();
            services.AddSingleton<IHashPassword, HashPasswordService>();
            services.AddSingleton<IClock, SystemClockService>();
            services.AddSingleton<ILogService, LogService>();
            services.AddSingleton<IClock>(provider => new FakeClockService { UtcNow = new DateTime(2030, 12, 31) });
            services.AddSingleton<IMaxGetRecord>(provider => new MaxGetRecordService { MaxGetRecord = 500 });

            string connectionString = AppConfigConnection.GetConnectionString();
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string is incorrect or empty. Please check configuration.");

            services.AddSingleton<IDbConnectionFactory>(provider => new MySqlConnectionFactory(connectionString));

            // Add DbContext with connection string from configuration
            services.AddDbContext<FoodAndDrinkShopDbContext>(options =>
                options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("12.0.2-mariadb")));

            return services;
        }
    }
}
