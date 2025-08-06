namespace ProjectShop.Server.Infrastructure.Persistence
{
    public class AppConfigConnection
    {
        private static readonly IConfigurationRoot configurationRoot;

        static AppConfigConnection()
        {
            string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\");
            configurationRoot = new ConfigurationBuilder().SetBasePath(basePath)
                                    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true).Build();
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
