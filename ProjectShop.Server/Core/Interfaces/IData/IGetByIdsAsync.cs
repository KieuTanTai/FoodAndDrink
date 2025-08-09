using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IGetByIdsAsync<T, TKey> where T : class where TKey : struct
    {
        //public string GetSingleDataString();
        Task<T> GetSingleByKeysAsync(TKey keys);
        Task<List<T>> GetAllByKeysAsync(IEnumerable<TKey> keys);
    }
}
