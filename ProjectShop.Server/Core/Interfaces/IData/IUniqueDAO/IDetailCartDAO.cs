namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailCartDAO<TEntity> : IGetDataByDateTimeAsync<TEntity>, IGetByRangePriceAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByCartId(uint cartId);
        Task<IEnumerable<TEntity>> GetByProductBarcode(string barcode);
    }
}
