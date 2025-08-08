// File: LocationDistrict.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationDistrict : IGetIdEntity<uint>
    {
        // Corresponds to 'location_district_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationDistrictId { get; private set; }

        // Corresponds to 'location_district_name' (NVARCHAR(50))
        public string LocationDistrictName { get; private set; }

        // Corresponds to 'location_district_status' (TINYINT(1))
        public bool LocationDistrictStatus { get; private set; }

        // Navigation properties
        public ICollection<Location> Locations { get; private set; } = new List<Location>();
        public ICollection<Supplier> CompanySuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Supplier> StoreSuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Customer> Customers { get; private set; } = new List<Customer>();
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        public LocationDistrict(uint locationDistrictId, string locationDistrictName, bool locationDistrictStatus)
        {
            LocationDistrictId = locationDistrictId;
            LocationDistrictName = locationDistrictName;
            LocationDistrictStatus = locationDistrictStatus;
        }

        public uint GetIdEntity() => LocationDistrictId;
    }
}

