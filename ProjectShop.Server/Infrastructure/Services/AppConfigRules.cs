using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class AppConfigRules
    {
        private static readonly IConfigurationRoot _configRules;

        static AppConfigRules()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../PlatformRules");
                _configRules = new ConfigurationBuilder().SetBasePath(path)
                                .AddJsonFile("platform-rules.json", optional: false, reloadOnChange: false).Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error when fetching file json! ", ex);
                throw;
            }
        }

        public static IDictionary<string, IConfigurationSection> GetCustomRules(string name = "custom-rules")
        {
            var CustomRules = _configRules.GetSection(name).GetChildren()
                                .ToDictionary(result => result.Key, result => result);
            return CustomRules;
        }
    }
}