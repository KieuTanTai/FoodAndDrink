// File: LocationDistrict.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationDistrictModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _locationDistrictId;
        private string _locationDistrictName = string.Empty;
        private bool _locationDistrictStatus;

        // Corresponds to 'location_district_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationDistrictId
        {
            get => _locationDistrictId;
            set => _locationDistrictId = value;
        }

        // Corresponds to 'location_district_name' (NVARCHAR(50))
        public string LocationDistrictName
        {
            get => _locationDistrictName;
            set => _locationDistrictName = value ?? string.Empty;
        }

        // Corresponds to 'location_district_status' (TINYINT(1))
        public bool LocationDistrictStatus
        {
            get => _locationDistrictStatus;
            set => _locationDistrictStatus = value;
        }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public ICollection<SupplierModel> CompanySuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<SupplierModel> StoreSuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        // End of navigation properties

        public LocationDistrictModel(uint locationDistrictId, string locationDistrictName, bool locationDistrictStatus)
        {
            LocationDistrictId = locationDistrictId;
            LocationDistrictName = locationDistrictName;
            LocationDistrictStatus = locationDistrictStatus;
        }

        public LocationDistrictModel() { }

        public uint GetIdEntity() => LocationDistrictId;
    }
}

