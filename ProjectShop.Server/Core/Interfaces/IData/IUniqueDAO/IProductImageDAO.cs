using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IProductImageDAO<T> : IGetDataByDateTimeAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string productBarcode);
        Task<IEnumerable<T>> GetByLastUpdatedDateAsync<TCompareType>(DateTime dateTime, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedYearAsync<TCompareType>(int year, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByLastUpdatedMonthAndYearAsync(int month, int year);
        Task<IEnumerable<T>> GetByRangeLastUpdatedDateAsync<TCompareType>(DateTime firstTime, DateTime secondTime, TCompareType compareType) where TCompareType : Enum;
    }
}
