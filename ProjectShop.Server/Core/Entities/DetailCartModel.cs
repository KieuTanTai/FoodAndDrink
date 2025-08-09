// File: DetailCart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailCartModel : IGetIdEntity<uint>
    {
        // Corresponds to 'detail_cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailCartId { get; private set; }

        // Corresponds to 'cart_id' (INT UNSIGNED)
        public uint CartId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }

        // Corresponds to 'cart_add_date' (DATETIME)
        public DateTime CartAddedDate { get; private set; }

        // Corresponds to 'cart_price' (DECIMAL(10, 2))
        public decimal CartPrice { get; private set; }

        // Corresponds to 'cart_quantity' (INT UNSIGNED)
        public uint CartQuantity { get; private set; }

        // Navigation property
        public CartModel Cart { get; private set; } = null!;
        public ProductModel Product { get; private set; } = null!;

        public DetailCartModel(uint detailCartId, uint cartId, string productBarcode, DateTime cartAddedDate, decimal cartPrice, uint cartQuantity)
        {
            DetailCartId = detailCartId;
            CartId = cartId;
            ProductBarcode = productBarcode;
            CartAddedDate = cartAddedDate;
            CartPrice = cartPrice;
            CartQuantity = cartQuantity;
        }

        public uint GetIdEntity() => DetailCartId;
    }
}

