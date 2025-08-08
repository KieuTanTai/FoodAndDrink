// File: DetailCart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailCart : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailCartId { get; private set; }

        // Corresponds to 'cart_id' (INT UNSIGNED)
        public uint CartId { get; private set; }
        public Cart Cart { get; private set; } = null!;

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }
        public Product Product { get; private set; } = null!;

        // Corresponds to 'cart_add_date' (DATETIME)
        public DateTime CartAddDate { get; private set; }

        // Corresponds to 'cart_price' (DECIMAL(10, 2))
        public decimal CartPrice { get; private set; }

        // Corresponds to 'cart_quantity' (INT UNSIGNED)
        public uint CartQuantity { get; private set; }

        public DetailCart(uint detailCartId, uint cartId, string productBarcode, DateTime cartAddDate, decimal cartPrice, uint cartQuantity)
        {
            DetailCartId = detailCartId;
            CartId = cartId;
            ProductBarcode = productBarcode;
            CartAddDate = cartAddDate;
            CartPrice = cartPrice;
            CartQuantity = cartQuantity;
        }

        public uint GetIdEntity() => DetailCartId;
    }
}

