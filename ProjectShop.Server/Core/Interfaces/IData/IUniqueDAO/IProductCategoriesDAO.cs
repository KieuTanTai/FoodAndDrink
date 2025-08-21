namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductCategoriesDAO<TEntity, TKey> : IGetByKeysAsync<TEntity, TKey>, IDeleteByKeysAsync<TKey> where TEntity : class where TKey : struct
    {
        Task<IEnumerable<TEntity>> GetByCategoryIdAsync(uint categoryId, int? getMaxCount = null);
        Task<IEnumerable<TEntity>> GetByCategoryIdsAsync(IEnumerable<uint> categoryIds, int? getMaxCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode, int? getMaxCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> productBarcodes, int? getMaxCount = null);
        Task<int> DeleteByCategoryIdAsync(uint categoryId);
        Task<int> DeleteByProductBarcodeAsync(string productBarcode);
    }
}
