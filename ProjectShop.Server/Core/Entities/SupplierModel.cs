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

        // Corresponds to 'supplier_company_house_number' (VARCHAR(20))
        public string SupplierCompanyHouseNumber { get; private set; }

        // Corresponds to 'supplier_company_street' (NVARCHAR(40))
        public string SupplierCompanyStreet { get; private set; }

        // Corresponds to 'supplier_company_ward_id' (INT UNSIGNED)
        public uint? SupplierCompanyWardId { get; private set; }

        // Corresponds to 'supplier_company_district_id' (INT UNSIGNED)
        public uint? SupplierCompanyDistrictId { get; private set; }

        // Corresponds to 'supplier_company_city_id' (INT UNSIGNED)
        public uint? SupplierCompanyCityId { get; private set; }

        // Corresponds to 'supplier_store_house_number' (VARCHAR(20))
        public string SupplierStoreHouseNumber { get; private set; }

        // Corresponds to 'supplier_store_street' (NVARCHAR(40))
        public string SupplierStoreStreet { get; private set; }

        // Corresponds to 'supplier_store_ward_id' (INT UNSIGNED)
        public uint? SupplierStoreWardId { get; private set; }

        // Corresponds to 'supplier_store_district_id' (INT UNSIGNED)
        public uint? SupplierStoreDistrictId { get; private set; }

        // Corresponds to 'supplier_store_city_id' (INT UNSIGNED)
        public uint? SupplierStoreCityId { get; private set; }

        // Corresponds to 'supplier_status' (TINYINT(1))
        public bool SupplierStatus { get; private set; }

        // Navigation properties
        public LocationWardModel? SupplierCompanyWard { get; private set; }
        public LocationDistrict? SupplierCompanyDistrict { get; private set; }
        public LocationCity? SupplierCompanyCity { get; private set; }
        public LocationWardModel? SupplierStoreWard { get; private set; }
        public LocationDistrict? SupplierStoreDistrict { get; private set; }
        public LocationCity? SupplierStoreCity { get; private set; }
        public ICollection<ProductModel> Products { get; private set; } = new List<ProductModel>();

        public SupplierModel(uint supplierId, string supplierName, string supplierPhone, string supplierEmail, string supplierCompanyHouseNumber, string supplierCompanyStreet, uint? supplierCompanyWardId, uint? supplierCompanyDistrictId, uint? supplierCompanyCityId, string supplierStoreHouseNumber, string supplierStoreStreet, uint? supplierStoreWardId, uint? supplierStoreDistrictId, uint? supplierStoreCityId, bool supplierStatus)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            SupplierPhone = supplierPhone;
            SupplierEmail = supplierEmail;
            SupplierCompanyHouseNumber = supplierCompanyHouseNumber;
            SupplierCompanyStreet = supplierCompanyStreet;
            SupplierCompanyWardId = supplierCompanyWardId;
            SupplierCompanyDistrictId = supplierCompanyDistrictId;
            SupplierCompanyCityId = supplierCompanyCityId;
            SupplierStoreHouseNumber = supplierStoreHouseNumber;
            SupplierStoreStreet = supplierStoreStreet;
            SupplierStoreWardId = supplierStoreWardId;
            SupplierStoreDistrictId = supplierStoreDistrictId;
            SupplierStoreCityId = supplierStoreCityId;
            SupplierStatus = supplierStatus;
        }

        public uint GetIdEntity() => SupplierId;
    }
}

