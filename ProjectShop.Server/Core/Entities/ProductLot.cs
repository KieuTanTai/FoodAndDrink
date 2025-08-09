// File: ProductLot.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class ProductLot : IGetIdEntity<uint>
    {
        // Corresponds to 'product_lot_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint ProductLotId { get; private set; }

        // Corresponds to 'product_barcode' (VARCHAR(20))
        public string ProductBarcode { get; private set; }

        // Corresponds to 'product_lot_mfg_date' (DATETIME)
        public DateTime ProductLotMfgDate { get; private set; }

        // Corresponds to 'product_lot_exp_date' (DATETIME)
        public DateTime ProductLotExpDate { get; private set; }

        // Corresponds to 'product_lot_initial_quantity' (INT)
        public int ProductLotInitialQuantity { get; private set; }

        // Navigation properties
        public ProductModel Product { get; private set; } = null!;
        public ICollection<InventoryModel> Inventories { get; private set; } = new List<InventoryModel>();
        public ICollection<DisposeProductModel> DisposeProducts { get; private set; } = new List<DisposeProductModel>();

        public ProductLot(uint productLotId, string productBarcode, DateTime productLotMfgDate, DateTime productLotExpDate, int productLotInitialQuantity)
        {
            ProductLotId = productLotId;
            ProductBarcode = productBarcode;
            ProductLotMfgDate = productLotMfgDate;
            ProductLotExpDate = productLotExpDate;
            ProductLotInitialQuantity = productLotInitialQuantity;
        }

        public uint GetIdEntity() => ProductLotId;
    }
}

