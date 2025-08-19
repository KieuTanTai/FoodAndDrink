namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailSaleEventDAO<TEntity> : IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetBySaleEventIdAsync(uint saleEventId);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode);
        Task<IEnumerable<TEntity>> GetByMaxDiscountPriceAsync<TCompareType>(decimal maxDiscountPrice, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeMaxDiscountPriceAsync(decimal minDiscountPrice, decimal maxDiscountPrice);
        Task<IEnumerable<TEntity>> GetByMinPriceToUseAsync<TCompareType>(decimal minDiscountPrice, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeMinPriceToUseAsync(decimal minDiscountPrice, decimal maxDiscountPrice);
        Task<IEnumerable<TEntity>> GetByDiscountPercentAsync<TCompareType>(decimal discountPercent, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeDiscountPercentAsync(decimal minDiscountPercent, decimal maxDiscountPercent);
        Task<IEnumerable<TEntity>> GetByDiscountAmountAsync<TCompareType>(decimal discountAmount, TCompareType compareType) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeDiscountAmountAsync(decimal minDiscountAmount, decimal maxDiscountAmount);
    }
}
