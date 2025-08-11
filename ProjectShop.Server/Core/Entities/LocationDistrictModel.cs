// File: LocationDistrict.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationDistrictModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_district_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationDistrictId { get; private set; }

        // Corresponds to 'location_district_name' (NVARCHAR(50))
        public string LocationDistrictName { get; private set; }

        // Corresponds to 'location_district_status' (TINYINT(1))
        public bool LocationDistrictStatus { get; private set; }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; private set; } = new List<LocationModel>();
        public ICollection<SupplierModel> CompanySuppliers { get; private set; } = new List<SupplierModel>();
        public ICollection<SupplierModel> StoreSuppliers { get; private set; } = new List<SupplierModel>();
        public ICollection<CustomerModel> Customers { get; private set; } = new List<CustomerModel>();
        public ICollection<EmployeeModel> Employees { get; private set; } = new List<EmployeeModel>();

        public LocationDistrictModel(uint locationDistrictId, string locationDistrictName, bool locationDistrictStatus)
        {
            LocationDistrictId = locationDistrictId;
            LocationDistrictName = locationDistrictName;
            LocationDistrictStatus = locationDistrictStatus;
        }

        public uint GetIdEntity() => LocationDistrictId;
    }
}

