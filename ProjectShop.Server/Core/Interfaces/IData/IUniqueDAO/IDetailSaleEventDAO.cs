namespace ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO
{
    public interface IDetailSaleEventDAO<TEntity> : IGetByEnumAsync<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetBySaleEventIdAsync(uint saleEventId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetBySaleEventIdsAsync(IEnumerable<uint> saleEventId, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodeAsync(string productBarcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByProductBarcodesAsync(IEnumerable<string> productBarcode, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMaxDiscountPriceAsync<TCompareType>(decimal maxDiscountPrice, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeMaxDiscountPriceAsync(decimal minDiscountPrice, decimal maxDiscountPrice, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByMinPriceToUseAsync<TCompareType>(decimal minDiscountPrice, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeMinPriceToUseAsync(decimal minDiscountPrice, decimal maxDiscountPrice, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDiscountPercentAsync<TCompareType>(decimal discountPercent, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeDiscountPercentAsync(decimal minDiscountPercent, decimal maxDiscountPercent, int? maxGetCount = null);
        Task<IEnumerable<TEntity>> GetByDiscountAmountAsync<TCompareType>(decimal discountAmount, TCompareType compareType, int? maxGetCount = null) where TCompareType : Enum;
        Task<IEnumerable<TEntity>> GetByRangeDiscountAmountAsync(decimal minDiscountAmount, decimal maxDiscountAmount, int? maxGetCount = null);
    }
}
