namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductCategoriesDAO<TEntity, TKey> : IGetByKeysAsync<TEntity, TKey>, IDeleteByKeysAsync<TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByCategoryIdAsync(uint categoryId);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode);
        Task<int> DeleteByCategoryIdAsync(uint categoryId);
        Task<int> DeleteByProductBarcodeAsync(string productBarcode);
    }
}
