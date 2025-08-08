// File: Cart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Cart : IGetIdEntity<uint>
    {
        // Corresponds to 'cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint CartId { get; private set; }

        // Corresponds to 'customer_id' (INT UNSIGNED)
        public uint CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;

        // Corresponds to 'cart_total_price' (DECIMAL(10, 2))
        public decimal CartTotalPrice { get; private set; }

        // Navigation property
        public ICollection<DetailCart> DetailCarts { get; private set; } = new List<DetailCart>();

        public Cart(uint cartId, uint customerId, decimal cartTotalPrice)
        {
            CartId = cartId;
            CustomerId = customerId;
            CartTotalPrice = cartTotalPrice;
        }

        public uint GetIdEntity() => CartId;
    }
}

