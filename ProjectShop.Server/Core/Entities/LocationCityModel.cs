// File: LocationCity.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationCityModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _locationCityId;
        private string _locationCityName = string.Empty;
        private bool _locationCityStatus;

        // Corresponds to 'location_city_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationCityId
        {
            get => _locationCityId;
            set => _locationCityId = value;
        }

        // Corresponds to 'location_city_name' (NVARCHAR(50))
        public string LocationCityName
        {
            get => _locationCityName;
            set => _locationCityName = value ?? string.Empty;
        }

        // Corresponds to 'location_city_status' (TINYINT(1))
        public bool LocationCityStatus
        {
            get => _locationCityStatus;
            set => _locationCityStatus = value;
        }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; set; } = [];
        public ICollection<SupplierModel> CompanySuppliers { get; set; } = [];
        public ICollection<SupplierModel> StoreSuppliers { get; set; } = [];
        public ICollection<CustomerModel> Customers { get; set; } = [];
        public ICollection<EmployeeModel> Employees { get; set; } = [];
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

