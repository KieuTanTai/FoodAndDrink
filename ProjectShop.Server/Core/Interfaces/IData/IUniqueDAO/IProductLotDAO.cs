namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string barcode);
    }
}
