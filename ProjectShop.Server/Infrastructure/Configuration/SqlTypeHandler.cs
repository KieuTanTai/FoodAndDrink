using Dapper;
using ProjectShop.Server.Core.Enums;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Configuration
{
    // Generic SQL Type Handler for Enums
    public class SqlTypeHandler<TEntity> : SqlMapper.TypeHandler<TEntity> where TEntity : Enum
    {
        public override TEntity? Parse(object value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            return (TEntity?)Enum.Parse(typeof(TEntity), value.ToString()!, ignoreCase: true);
        }
        public override void SetValue(IDbDataParameter parameter, TEntity? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            parameter.Value = value.ToString().ToLower();
        }
    }

    // Move type handler registration into a static class constructor or an explicit method
    public static class SqlTypeHandlerRegistration
    {
        public static void Register()
        {
            SqlMapper.AddTypeHandler(new SqlTypeHandler<EDiscountType>());
            SqlMapper.AddTypeHandler(new SqlTypeHandler<EInventoryMovementReason>());
            SqlMapper.AddTypeHandler(new SqlTypeHandler<EPaymentMethodType>());
        }
    }
}
