namespace ProjectShop.Server.Infrastructure.Persistence
{
    public class AppConfigConnection
    {
        private static readonly IConfigurationRoot _configurationRoot;

        static AppConfigConnection()
        {
            try
            {
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../");
                _configurationRoot = new ConfigurationBuilder().SetBasePath(basePath)
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
            var connectionString = _configurationRoot.GetConnectionString(name);
            connectionString ??= "";
            return connectionString;
        }
    }
}
