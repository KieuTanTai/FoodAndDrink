namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductImageDAO<TEntity> : IGetDataByDateTimeAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode);
        Task<IEnumerable<TEntity>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByLastUpdatedMonthAndYearAsync(int month, int year);
        Task<IEnumerable<TEntity>> GetByRangeLastUpdatedDateAsync<TCompareType>(DateTime firstTime, DateTime secondTime, TCompareType compareType) where TCompareType : Enum;
    }
}
