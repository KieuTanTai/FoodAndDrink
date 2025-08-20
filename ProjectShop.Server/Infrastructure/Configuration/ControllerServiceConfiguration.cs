namespace ProjectShop.Server.Infrastructure.Configuration
{
    public static class ControllerServiceConfiguration
    {
        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            //services.AddTransient<ICurrentAccountLogin<AccountModel, RolesOfUserModel>, CurrentAccountLoginService>();
            return services;
        }
    }
}
