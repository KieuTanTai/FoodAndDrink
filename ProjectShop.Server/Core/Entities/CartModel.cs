// File: Cart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class CartModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _cartId;
        private uint _customerId;
        private decimal _cartTotalPrice;

        // Corresponds to 'cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CartId
        {
            get => _cartId;
            set => _cartId = value;
        }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId
        {
            get => _customerId;
            set => _customerId = value;
        }

        // Corresponds to 'cart_total_price' (DECIMAL(10, 2))
        public decimal CartTotalPrice
        {
            get => _cartTotalPrice;
            set => _cartTotalPrice = value;
        }

        // Navigation properties
        public CustomerModel Customer { get; set; } = null!;
        public ICollection<DetailCartModel> DetailCarts { get; set; } = new List<DetailCartModel>();
        // End of navigation properties

        public CartModel(uint cartId, uint customerId, decimal cartTotalPrice)
        {
            CartId = cartId;
            CustomerId = customerId;
            CartTotalPrice = cartTotalPrice;
        }

        public CartModel()
        {
        }

        public uint GetIdEntity() => CartId;
    }
}

