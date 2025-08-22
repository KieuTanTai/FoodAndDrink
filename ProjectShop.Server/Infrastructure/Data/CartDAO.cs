using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CartDAO : BaseDAO<CartModel>, ICartDAO<CartModel>
    {
        public CartDAO(
            IDbConnectionFactory connectionFactory,
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "cart", "cart_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (customer_id, cart_total_price)
                VALUES (@CustomerId, @CartTotalPrice); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName}
                SET cart_total_price = @CartTotalPrice
                WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<IEnumerable<CartModel>> GetByTotalPriceAsync(decimal minPrice, decimal maxPrice, int? maxGetCount) 
            => await GetByRangeDecimalAsync(minPrice, maxPrice, "cart_total_price", maxGetCount);

        public async Task<IEnumerable<CartModel>> GetByTotalPriceAsync<TEnum>(decimal price, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid comparison type provided.");
            return await GetByTotalPriceAsync(price, type, maxGetCount);
        }

    }
}
