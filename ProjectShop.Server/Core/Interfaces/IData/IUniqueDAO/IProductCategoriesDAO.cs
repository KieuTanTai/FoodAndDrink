using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductCategoriesDAO<T, TKey> : IGetByKeysAsync<T, TKey>, IDeleteByKeysAsync<TKey> where T : class where TKey : struct
    {
        Task<IEnumerable<T>> GetByCategoryIdAsync(uint categoryId);
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string productBarcode);
        Task<int> DeleteByCategoryIdAsync(uint categoryId);
        Task<int> DeleteByProductBarcodeAsync(string productBarcode);
    }
}
