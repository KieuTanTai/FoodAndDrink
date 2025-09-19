// File: Supplier.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SupplierModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _supplierId;
        private string _supplierName = string.Empty;
        private string _supplierPhone = string.Empty;
        private string _supplierEmail = string.Empty;
        private uint? _companyLocationId;
        private uint? _storeLocationId;
        private bool _supplierStatus;
        private DateTime _supplierCooperationDate;

        // Corresponds to 'supplier_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SupplierId
        {
            get => _supplierId;
            set => _supplierId = value;
        }

        // Corresponds to 'supplier_name' (NVARCHAR(100))
        public string SupplierName
        {
            get => _supplierName;
            set => _supplierName = value ?? string.Empty;
        }

        // Corresponds to 'supplier_phone' (VARCHAR(12))
        public string SupplierPhone
        {
            get => _supplierPhone;
            set => _supplierPhone = value ?? string.Empty;
        }

        // Corresponds to 'supplier_email' (VARCHAR(100))
        public string SupplierEmail
        {
            get => _supplierEmail;
            set => _supplierEmail = value ?? string.Empty;
        }

        // Corresponds to 'company_location_id' (INT UNSIGNED)
        public uint? CompanyLocationId
        {
            get => _companyLocationId;
            set => _companyLocationId = value;
        }

        // Corresponds to 'store_location_id' (INT UNSIGNED)
        public uint? StoreLocationId
        {
            get => _storeLocationId;
            set => _storeLocationId = value;
        }

        // Corresponds to 'supplier_status' (TINYINT(1))
        public bool SupplierStatus
        {
            get => _supplierStatus;
            set => _supplierStatus = value;
        }

        // Corresponds to 'supplier_cooperation_date' (DATETIME)
        public DateTime SupplierCooperationDate
        {
            get => _supplierCooperationDate;
            set => _supplierCooperationDate = value;
        }

        // Navigation properties
        public LocationModel StoreLocation { get; set; } = null!;
        public LocationModel CompanyLocation { get; set; } = null!;
        public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
        // End of navigation properties

        public SupplierModel(uint supplierId,
                        string supplierName,
                        string supplierPhone,
                        string supplierEmail,
                        uint? companyLocationId,
                        uint? storeLocationId,
                        bool supplierStatus,
                        DateTime supplierCooperationDate)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            SupplierPhone = supplierPhone;
            SupplierEmail = supplierEmail;
            CompanyLocationId = companyLocationId;
            StoreLocationId = storeLocationId;
            SupplierStatus = supplierStatus;
            SupplierCooperationDate = supplierCooperationDate;
        }

        public SupplierModel()
        {
        }

        public uint GetIdEntity() => SupplierId;
    }
}
