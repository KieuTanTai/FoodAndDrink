namespace ProjectShop.Server.Infrastructure.Services
{
    public static class GetProviderService
    {
        public static IServiceProvider SystemServices { get; set; } = default!;
        public static void SetServiceProvider(IServiceProvider serviceProvider)
        {
            SystemServices = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider), "IServiceProvider cannot be null.");
        }
    }
}
