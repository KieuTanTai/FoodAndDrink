namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductImageDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> productBarcodes, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByRangeLastUpdatedDateAsync<TCompareType>(DateTime firstTime, DateTime secondTime, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
    }
}
