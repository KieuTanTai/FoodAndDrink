using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Interfaces.IServices;
using ProjectShop.Server.Core.ValueObjects;
using ProjectShop.Server.Core.ValueObjects.PlatformRules;

namespace ProjectShop.Server.Application.Services
{
    public class ReadPlatformRuleService : IReadPlatformRulesService<BasePlatformRules>
    {
        public async Task<ServiceResult<BasePlatformRules>> GetExpiryCookieRule()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BasePlatformRules>> GetExpiryFetchTimeoutRule()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BasePlatformRules>> GetExpiryMessageTimeoutRule()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<BasePlatformRules>> GetExpiryTryTimeoutRule()
        {
            throw new NotImplementedException();
        }
    }
}