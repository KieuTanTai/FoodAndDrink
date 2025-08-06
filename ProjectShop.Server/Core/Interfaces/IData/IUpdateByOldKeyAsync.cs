using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IData
{
    public interface IUpdateByOldKeyAsync<T> where T : class
    {
        string GetUpdateWithOldKeyString();
        Task<int> UpdateAsync(T entity, string oldKey);
        Task<int> UpdateAsync(IEnumerable<T> entities, string oldKey);
    }
}
