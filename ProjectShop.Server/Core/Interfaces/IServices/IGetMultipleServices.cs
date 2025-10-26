using ProjectShop.Server.Core.ValueObjects;
using System.Runtime.CompilerServices;

namespace ProjectShop.Server.Core.Interfaces.IServices
{
    public interface IGetMultipleServices<TEntity, TServiceCall>
        where TEntity : class, new()
        where TServiceCall : class
    {
        Task<ServiceResults<TEntity>> GetManyAsync<TParam>(
            TParam param,
            Func<TParam, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken = default, [CallerMemberName] string? methodCall = null);

        Task<ServiceResults<TEntity>> GetManyAsync(
            Func<CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken = default, [CallerMemberName] string? methodCall = null);

        // Method lấy theo giá trị decimal đơn lẻ (ví dụ: price, weight, v.v...)
        Task<ServiceResults<TEntity>> GetByValueAsync(
            decimal value,
            Func<decimal, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken = default, [CallerMemberName] string? methodCall = null);

        // Method lấy theo khoảng giá trị (range), dùng tuple hoặc 2 tham số
        Task<ServiceResults<TEntity>> GetByRangeAsync(
            decimal minValue,
            decimal maxValue,
            Func<decimal, decimal, CancellationToken, Task<IEnumerable<TEntity>>> queryFunc,
            CancellationToken cancellationToken = default, [CallerMemberName] string? methodCall = null);
    }
}
