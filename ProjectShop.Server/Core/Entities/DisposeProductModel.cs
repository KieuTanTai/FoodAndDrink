// File: DisposeProduct.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeProductModel : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_product_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeProductId { get; private set; }

        // Corresponds to 'product_lot_id' (INT UNSIGNED)
        public uint ProductLotId { get; private set; }

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId { get; private set; }

        // Corresponds to 'dispose_by_employee_id' (INT UNSIGNED)
        public uint DisposeByEmployeeId { get; private set; }

        // Corresponds to 'dispose_reason_id' (INT UNSIGNED)
        public uint DisposeReasonId { get; private set; }

        // Corresponds to 'dispose_quantity' (INT UNSIGNED)
        public uint DisposeQuantity { get; private set; }

        // Navigation properties
        public ProductLot ProductLot { get; private set; } = null!;
        public LocationModel Location { get; private set; } = null!;
        public EmployeeModel DisposeByEmployee { get; private set; } = null!;
        public DisposeReasonModel DisposeReason { get; private set; } = null!;

        public DisposeProductModel(uint disposeProductId, uint productLotId, uint locationId, uint disposeByEmployeeId, uint disposeReasonId, uint disposeQuantity)
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

