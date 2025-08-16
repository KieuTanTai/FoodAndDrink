namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductLotDAO<T> : IGetDataByDateTimeAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string barcode);
    }
}
