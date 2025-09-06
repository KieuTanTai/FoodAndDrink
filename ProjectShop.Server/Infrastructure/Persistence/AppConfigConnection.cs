namespace ProjectShop.Server.Infrastructure.Persistence
{
    public class AppConfigConnection
    {
        private static readonly IConfigurationRoot configurationRoot;

        static AppConfigConnection()
        {
            try
            {
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../");
                Console.WriteLine($"[DEBUG] basePath: {basePath}");
                configurationRoot = new ConfigurationBuilder().SetBasePath(basePath)
                                        .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true).Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("AppConfigConnection static constructor error: " + ex);
                throw;
            }
        }
        public static string GetConnectionString(string name = "DefaultConnection")
        {
            var connectionString = configurationRoot.GetConnectionString(name);
            if (connectionString == null)
                connectionString = "";
            return connectionString;
        }
    }
}
