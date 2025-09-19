// File: LocationWard.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationWardModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _locationWardId;
        private string _locationWardName = string.Empty;
        private bool _locationWardStatus;

        // Corresponds to 'location_ward_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationWardId
        {
            get => _locationWardId;
            set => _locationWardId = value;
        }

        // Corresponds to 'location_ward_name' (NVARCHAR(50))
        public string LocationWardName
        {
            get => _locationWardName;
            set => _locationWardName = value ?? string.Empty;
        }

        // Corresponds to 'location_ward_status' (TINYINT(1))
        public bool LocationWardStatus
        {
            get => _locationWardStatus;
            set => _locationWardStatus = value;
        }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; set; } = new List<LocationModel>();
        public ICollection<SupplierModel> CompanySuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<SupplierModel> StoreSuppliers { get; set; } = new List<SupplierModel>();
        public ICollection<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
        public ICollection<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        // End of navigation properties

        public LocationWardModel(uint locationWardId, string locationWardName, bool locationWardStatus)
        {
            LocationWardId = locationWardId;
            LocationWardName = locationWardName;
            LocationWardStatus = locationWardStatus;
        }

        public LocationWardModel() { }

        public uint GetIdEntity() => LocationWardId;
    }
}

