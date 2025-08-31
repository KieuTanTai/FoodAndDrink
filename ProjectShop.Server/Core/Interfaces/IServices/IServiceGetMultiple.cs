using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IServiceGetMultiple<TEntity, TOptions, TServiceCall>
        where TEntity : class, new()
        where TOptions : class
        where TServiceCall : class
    {
        Task<ServiceResults<TEntity>> GetManyAsync<TParam>(
            TParam param,
            Func<TParam, int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null);

        Task<ServiceResults<TEntity>> GetManyAsync(
            Func<int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null,
            int? maxGetCount = null,
            [CallerMemberName] string? methodCall = null);

        // Method lấy theo giá trị decimal đơn lẻ (ví dụ: price, weight, v.v...)
        Task<ServiceResults<TEntity>> GetByValueAsync(
            decimal value,
            Func<decimal, int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null);

        // Method lấy theo khoảng giá trị (range), dùng tuple hoặc 2 tham số
        Task<ServiceResults<TEntity>> GetByRangeAsync(
            decimal minValue,
            decimal maxValue,
            Func<decimal, decimal, int?, Task<IEnumerable<TEntity>>> queryFunc,
            TOptions? options = null,
            int? maxGetCount = null, [CallerMemberName] string? methodCall = null);
    }
}
