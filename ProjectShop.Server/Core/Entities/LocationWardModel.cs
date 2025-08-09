// File: LocationWard.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationWardModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_ward_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationWardId { get; private set; }

        // Corresponds to 'location_ward_name' (NVARCHAR(50))
        public string LocationWardName { get; private set; }

        // Corresponds to 'location_ward_status' (TINYINT(1))
        public bool LocationWardStatus { get; private set; }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; private set; } = new List<LocationModel>();
        public ICollection<SupplierModel> CompanySuppliers { get; private set; } = new List<SupplierModel>();
        public ICollection<SupplierModel> StoreSuppliers { get; private set; } = new List<SupplierModel>();
        public ICollection<CustomerModel> Customers { get; private set; } = new List<CustomerModel>();
        public ICollection<EmployeeModel> Employees { get; private set; } = new List<EmployeeModel>();

        public LocationWardModel(uint locationWardId, string locationWardName, bool locationWardStatus)
        {
            LocationWardId = locationWardId;
            LocationWardName = locationWardName;
            LocationWardStatus = locationWardStatus;
        }

        public uint GetIdEntity() => LocationWardId;
    }
}

