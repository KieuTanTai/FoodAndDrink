// File: LocationWard.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationWard : IGetIdEntity<uint>
    {
        // Corresponds to 'location_ward_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationWardId { get; private set; }

        // Corresponds to 'location_ward_name' (NVARCHAR(50))
        public string LocationWardName { get; private set; }

        // Corresponds to 'location_ward_status' (TINYINT(1))
        public bool LocationWardStatus { get; private set; }

        // Navigation properties
        public ICollection<Location> Locations { get; private set; } = new List<Location>();
        public ICollection<Supplier> CompanySuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Supplier> StoreSuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Customer> Customers { get; private set; } = new List<Customer>();
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        public LocationWard(uint locationWardId, string locationWardName, bool locationWardStatus)
        {
            LocationWardId = locationWardId;
            LocationWardName = locationWardName;
            LocationWardStatus = locationWardStatus;
        }

        public uint GetIdEntity() => LocationWardId;
    }
}

