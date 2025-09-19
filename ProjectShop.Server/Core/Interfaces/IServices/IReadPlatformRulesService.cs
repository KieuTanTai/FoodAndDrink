using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectShop.Server.Core.ValueObjects;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IReadPlatformRulesService<TEntity> where TEntity : class
    {
        Task<ServiceResult<TEntity>> GetExpiryCookieRule();
        Task<ServiceResult<TEntity>> GetExpiryTryTimeoutRule();
        Task<ServiceResult<TEntity>> GetExpiryFetchTimeoutRule();
        Task<ServiceResult<TEntity>> GetExpiryMessageTimeoutRule();
    }
}