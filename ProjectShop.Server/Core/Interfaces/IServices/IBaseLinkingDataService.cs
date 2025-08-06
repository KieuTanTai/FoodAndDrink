using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseLinkingDataService<T, TKeys> where T : class where TKeys : class
    {
        Task<T> GetSingleByIdsAsync(TKeys ids);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllByIdAsync(string id, string colName);
        Task<int> InsertAsync(T productCategory);
        Task<int> InsertManyAsync(IEnumerable<T> productCategories);
    }
}
