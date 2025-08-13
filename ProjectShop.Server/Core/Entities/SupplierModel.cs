// File: Supplier.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class SupplierModel : IGetIdEntity<uint>
    {
        // Corresponds to 'supplier_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint SupplierId { get; private set; }

        // Corresponds to 'supplier_name' (NVARCHAR(100))
        public string SupplierName { get; private set; }

        // Corresponds to 'supplier_phone' (VARCHAR(12))
        public string SupplierPhone { get; private set; }

        // Corresponds to 'supplier_email' (VARCHAR(100))
        public string SupplierEmail { get; private set; }

        // Corresponds to 'company_location_id' (INT UNSIGNED)
        public uint? CompanyLocationId { get; private set; }
        public LocationModel CompanyLocation { get; private set; } = null!;

        // Corresponds to 'store_location_id' (INT UNSIGNED)
        public uint? StoreLocationId { get; private set; }
        public LocationModel StoreLocation { get; private set; } = null!;

        // Corresponds to 'supplier_status' (TINYINT(1))
        public bool SupplierStatus { get; private set; }

        // Corresponds to 'supplier_cooperation_date' (DATETIME)
        public DateTime SupplierCooperationDate { get; private set; }

        // Navigation property: Một supplier có thể cung cấp nhiều sản phẩm
        public ICollection<ProductModel> Products { get; private set; } = new List<ProductModel>();

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

        public uint GetIdEntity() => SupplierId;
    }
}
