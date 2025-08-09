// File: Cart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CartModel : IGetIdEntity<uint>
    {
        // Corresponds to 'cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CartId { get; private set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; private set; }

        // Corresponds to 'cart_total_price' (DECIMAL(10, 2))
        public decimal CartTotalPrice { get; private set; }

        // Navigation property
        public CustomerModel Customer { get; private set; } = null!;
        public ICollection<DetailCartModel> DetailCarts { get; private set; } = new List<DetailCartModel>();

        public CartModel(uint cartId, uint customerId, decimal cartTotalPrice)
        {
            CartId = cartId;
            CustomerId = customerId;
            CartTotalPrice = cartTotalPrice;
        }

        public uint GetIdEntity() => CartId;
    }
}

