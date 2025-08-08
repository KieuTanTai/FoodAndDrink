// File: DisposeProduct.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeProduct : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_product_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeProductId { get; private set; }

        // Corresponds to 'product_lot_id' (INT UNSIGNED)
        public uint ProductLotId { get; private set; }
        public ProductLot ProductLot { get; private set; } = null!;

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId { get; private set; }
        public Location Location { get; private set; } = null!;

        // Corresponds to 'dispose_by_employee_id' (INT UNSIGNED)
        public uint DisposeByEmployeeId { get; private set; }
        public Employee DisposeByEmployee { get; private set; } = null!;

        // Corresponds to 'dispose_reason_id' (INT UNSIGNED)
        public uint DisposeReasonId { get; private set; }
        public DisposeReason DisposeReason { get; private set; } = null!;

        // Corresponds to 'dispose_quantity' (INT UNSIGNED)
        public uint DisposeQuantity { get; private set; }

        public DisposeProduct(uint disposeProductId, uint productLotId, uint locationId, uint disposeByEmployeeId, uint disposeReasonId, uint disposeQuantity)
        {
            DisposeProductId = disposeProductId;
            ProductLotId = productLotId;
            LocationId = locationId;
            DisposeByEmployeeId = disposeByEmployeeId;
            DisposeReasonId = disposeReasonId;
            DisposeQuantity = disposeQuantity;
        }

        public uint GetIdEntity() => DisposeProductId;
    }
}

