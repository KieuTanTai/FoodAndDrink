// File: DetailCart.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DetailCartModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _detailCartId;
        private uint _cartId;
        private string _productBarcode = string.Empty;
        private DateTime _detailCartAddedDate;
        private decimal _detailCartPrice;
        private uint _detailCartQuantity;

        // Corresponds to 'detail_cart_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DetailCartId
        {
            get => _detailCartId;
            set => _detailCartId = value;
        }

        // Corresponds to 'cart_id' (INT UNSIGNED)
        public uint CartId
        {
            get => _cartId;
            set => _cartId = value;
        }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'cart_add_date' (DATETIME)
        public DateTime DetailCartAddedDate
        {
            get => _detailCartAddedDate;
            set => _detailCartAddedDate = value;
        }

        // Corresponds to 'cart_price' (DECIMAL(10, 2))
        public decimal DetailCartPrice
        {
            get => _detailCartPrice;
            set => _detailCartPrice = value;
        }

        // Corresponds to 'cart_quantity' (INT UNSIGNED)
        public uint DetailCartQuantity
        {
            get => _detailCartQuantity;
            set => _detailCartQuantity = value;
        }

        // Navigation properties
        public CartModel Cart { get; set; } = null!;
        public ProductModel Product { get; set; } = null!;
        // End of navigation properties

        public DetailCartModel(uint detailCartId, uint cartId, string productBarcode, DateTime detailCartAddedDate, decimal detailCartPrice, uint detailCartQuantity)
        {
            DetailCartId = detailCartId;
            CartId = cartId;
            ProductBarcode = productBarcode;
            DetailCartAddedDate = detailCartAddedDate;
            DetailCartPrice = detailCartPrice;
            DetailCartQuantity = detailCartQuantity;
        }

        public DetailCartModel()
        {
        }

        public uint GetIdEntity() => DetailCartId;
    }
}

