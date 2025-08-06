using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectShop.Server.Infrastructure.Data;

namespace ProjectShop.Server.Core.Interfaces.IServices.ICategory
{
    public interface IProductCategoryManagementService<T, TKeys> : IBaseLinkingDataService<T, TKeys> where T : class where TKeys : class
    {
        Task<int> UpdateByOldKeyAsync(T productCategory, string oldCategoryId);
        Task<int> UpdateManyByOldKeyAsync(IEnumerable<T> productCategories, int oldCategoryId);
        Task<int> DeleteManyAsync(string id);
        Task<int> DeleteAsync(TKeys ids);
    }
}
