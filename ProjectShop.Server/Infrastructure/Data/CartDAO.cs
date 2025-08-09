using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CartDAO : BaseDAO<CartModel>
    {
        public CartDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "cart", "cart_id", string.Empty)
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
    }
}
