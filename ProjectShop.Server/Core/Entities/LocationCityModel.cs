// File: LocationCity.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationCityModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_city_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationCityId { get; set; }

        // Corresponds to 'location_city_name' (NVARCHAR(50))
        public string LocationCityName { get; set; } = string.Empty;

        // Corresponds to 'location_city_status' (TINYINT(1))
        public bool LocationCityStatus { get; set; }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public ICollection<SupplierModel> CompanySuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<SupplierModel> StoreSuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        // End of navigation properties

        public LocationCityModel(uint locationCityId, string locationCityName, bool locationCityStatus)
        {
            LocationCityId = locationCityId;
            LocationCityName = locationCityName;
            LocationCityStatus = locationCityStatus;
        }

        public LocationCityModel() { }

        public uint GetIdEntity() => LocationCityId;
    }
}

