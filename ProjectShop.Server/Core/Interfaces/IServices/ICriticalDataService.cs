using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface ICriticalDataService
    {
        Task<int> SoftDeleteAsync<TEnum>(string id, TEnum status) where TEnum : Enum;
        Task<int> SoftDeleteManyAsync<TEnum>(IEnumerable<string> ids, TEnum status) where TEnum : Enum; 
    }
}
