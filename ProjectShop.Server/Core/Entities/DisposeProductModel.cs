// File: DisposeProduct.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeProductModel : IGetIdEntity<uint>
    {
        // Corresponds to 'dispose_product_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeProductId { get; set; }

        // Corresponds to 'product_barcode' (INT UNSIGNED)
        public uint ProductBarcode { get; set; }

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId { get; set; }

        // Corresponds to 'dispose_by_employee_id' (INT UNSIGNED)
        public uint DisposeByEmployeeId { get; set; }

        // Corresponds to 'dispose_reason_id' (INT UNSIGNED)
        public uint DisposeReasonId { get; set; }

        // Corresponds to 'dispose_quantity' (INT UNSIGNED)
        public int DisposeQuantity { get; set; }

        // Corresponds to 'dispose_date' (DATETIME)
        public DateTime DisposedDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public LocationModel Location { get; set; } = null!;
        public EmployeeModel DisposeByEmployee { get; set; } = null!;
        public DisposeReasonModel DisposeReason { get; set; } = null!;

        public DisposeProductModel(uint disposeProductId, uint productBarcode, uint locationId, uint disposeByEmployeeId,
                            uint disposeReasonId, int disposeQuantity, DateTime disposedDate)
        {
            DisposeProductId = disposeProductId;
            ProductBarcode = productBarcode;
            LocationId = locationId;
            DisposeByEmployeeId = disposeByEmployeeId;
            DisposeReasonId = disposeReasonId;
            DisposeQuantity = disposeQuantity;
            DisposedDate = disposedDate;
        }

        public DisposeProductModel() { }

        public uint GetIdEntity() => DisposeProductId;
    }
}

