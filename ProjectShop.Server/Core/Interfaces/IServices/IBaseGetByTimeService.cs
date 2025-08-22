namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IBaseGetByTimeService<TEntity, TOptions>
        where TEntity : class
        where TOptions : class
    {
        Task<IEnumerable<TEntity>> GetByDateTimeGenericAsync<TCompareType>(
            Func<TCompareType, int?, Task<IEnumerable<TEntity>>> daoFunc,
            TCompareType compareType,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null) where TCompareType : Enum;

        Task<IEnumerable<TEntity>> GetByDateTimeRangeGenericAsync(
            Func<int?, Task<IEnumerable<TEntity>>> daoFunc,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null);

        Task<IEnumerable<TEntity>> GetByMonthAndYearGenericAsync(
            Func<int, int, int?, Task<IEnumerable<TEntity>>> daoFunc,
            int year,
            int month,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null);
    }
}
