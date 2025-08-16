namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailSaleEventDAO<T> : IGetByEnumAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetBySaleEventIdAsync(uint saleEventId);
        Task<IEnumerable<T>> GetByProductBarcodeAsync(string productBarcode);
        Task<IEnumerable<T>> GetByMaxDiscountPriceAsync<TCompareType>(decimal maxDiscountPrice, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByRangeMaxDiscountPriceAsync(decimal minDiscountPrice, decimal maxDiscountPrice);
        Task<IEnumerable<T>> GetByMinPriceToUseAsync<TCompareType>(decimal minDiscountPrice, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByRangeMinPriceToUseAsync(decimal minDiscountPrice, decimal maxDiscountPrice);
        Task<IEnumerable<T>> GetByDiscountPercentAsync<TCompareType>(decimal discountPercent, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByRangeDiscountPercentAsync(decimal minDiscountPercent, decimal maxDiscountPercent);
        Task<IEnumerable<T>> GetByDiscountAmountAsync<TCompareType>(decimal discountAmount, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<T>> GetByRangeDiscountAmountAsync(decimal minDiscountAmount, decimal maxDiscountAmount);
    }
}
