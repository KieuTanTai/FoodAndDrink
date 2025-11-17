using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Infrastructure.Services
{
    public class ReadConfigRulesJson
    {
        private static readonly IConfigurationRoot _configurationRoot;

        static ReadConfigRulesJson()
        {
            try
            {
                var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../PlatformRules/");
                _configurationRoot = new ConfigurationBuilder().SetBasePath(basePath)
                                        .AddJsonFile("platform-rules.json", optional: false, reloadOnChange: true).Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadConfigRulesJson static constructor error: " + ex);
                throw;
            }
        }

        public static uint Get(EPlatformRules rule, string name = "")
        {
            return rule switch
            {
                EPlatformRules.MAX_GET_RECORDS => ReadConfigRulesJson.GetRulesMaxReturnRecords(name),
                EPlatformRules.COOKIE_EXPIRY_DAYS => ReadConfigRulesJson.GetCookieExpiryDays(name),
                EPlatformRules.DEFAULT_PAGE_SIZE => ReadConfigRulesJson.GetDefaultPageSize(name),
                EPlatformRules.RATE_LIMITING => ReadConfigRulesJson.GetRateLimiting(name),
                _ => 0,
            };
        }

        private static uint GetRulesMaxReturnRecords(string name = "custom-rules:max-return-records")
        {
            var maxRecords = _configurationRoot.GetSection(name).Value;
            return uint.TryParse(maxRecords, out var result) ? result : 0;
        }

        private static uint GetCookieExpiryDays(string name = "custom-rules:cookie-expiry-days")
        {
            var expiryDays = _configurationRoot.GetSection(name).Value;
            return uint.TryParse(expiryDays, out var result) ? result : 0;
        }

        private static uint GetDefaultPageSize(string name = "custom-rules:default-page-size")
        {
            var defaultPageSize = _configurationRoot.GetSection(name).GetValue<uint>("defaultPageSize");
            return defaultPageSize > 0 ? defaultPageSize : 10; // Default to 10 if not configured
        }

        private static uint GetRateLimiting(string name = "custom-rules:rate-limiting")
        {
            var maxRequests = _configurationRoot.GetSection(name).GetValue<uint>("maxRequestsPerMinute");
            return maxRequests > 0 ? maxRequests : 100; // Default to 100 if not configured
        }
    }
}