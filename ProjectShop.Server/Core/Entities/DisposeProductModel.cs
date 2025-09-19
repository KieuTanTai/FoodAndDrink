// File: DisposeProduct.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class DisposeProductModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _disposeProductId;
        private string _productBarcode = string.Empty;
        private uint _locationId;
        private uint _disposeByEmployeeId;
        private uint _disposeReasonId;
        private int _disposeQuantity;
        private DateTime _disposedDate = DateTime.UtcNow;

        // Corresponds to 'dispose_product_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint DisposeProductId
        {
            get => _disposeProductId;
            set => _disposeProductId = value;
        }

        // Corresponds to 'product_barcode' (string)
        public string ProductBarcode
        {
            get => _productBarcode;
            set => _productBarcode = value ?? string.Empty;
        }

        // Corresponds to 'location_id' (INT UNSIGNED)
        public uint LocationId
        {
            get => _locationId;
            set => _locationId = value;
        }

        // Corresponds to 'dispose_by_employee_id' (INT UNSIGNED)
        public uint DisposeByEmployeeId
        {
            get => _disposeByEmployeeId;
            set => _disposeByEmployeeId = value;
        }

        // Corresponds to 'dispose_reason_id' (INT UNSIGNED)
        public uint DisposeReasonId
        {
            get => _disposeReasonId;
            set => _disposeReasonId = value;
        }

        // Corresponds to 'dispose_quantity' (INT UNSIGNED)
        public int DisposeQuantity
        {
            get => _disposeQuantity;
            set => _disposeQuantity = value;
        }

        // Corresponds to 'dispose_date' (DATETIME)
        public DateTime DisposedDate
        {
            get => _disposedDate;
            set => _disposedDate = value;
        }

        // Navigation properties
        public ProductModel Product { get; set; } = null!;
        public LocationModel Location { get; set; } = null!;
        public EmployeeModel DisposeByEmployee { get; set; } = null!;
        public DisposeReasonModel DisposeReason { get; set; } = null!;
        // End of navigation properties

        public DisposeProductModel(uint disposeProductId, string productBarcode, uint locationId, uint disposeByEmployeeId,
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

