using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectShop.Server.Core.Interfaces.IData;

namespace ProjectShop.Server.Core.Interfaces.IServices.User.Storage
{
    internal interface IDetailStorageManagementService<T, TKey> : IBaseLinkingDataService<T, TKey>, IBaseEnumTimeService<T> where T : class where TKey : class
    {
        Task<List<T>> GetByFavorated(bool isFavorated);
    }
}
