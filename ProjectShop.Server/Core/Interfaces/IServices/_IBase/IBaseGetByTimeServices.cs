using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices._IBase
{
    public interface IBaseGetByTimeServices<TEntity, TOptions>
        where TEntity : class
        where TOptions : class
    {
        Task<ServiceResults<TEntity>> GetByDateTimeGenericAsync<TCompareType>(
            Func<TCompareType, int?, Task<IEnumerable<TEntity>>> daoFunc,
            TCompareType compareType,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null) where TCompareType : Enum;

        Task<ServiceResults<TEntity>> GetByDateTimeRangeGenericAsync(
            Func<int?, Task<IEnumerable<TEntity>>> daoFunc,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null);

        Task<ServiceResults<TEntity>> GetByMonthAndYearGenericAsync(
            Func<int, int, int?, Task<IEnumerable<TEntity>>> daoFunc,
            int year,
            int month,
            TOptions? options,
            string errorMsg,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null);
    }
}
