// File: LocationCity.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationCity : IGetIdEntity<uint>
    {
        // Corresponds to 'location_city_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationCityId { get; private set; }

        // Corresponds to 'location_city_name' (NVARCHAR(50))
        public string LocationCityName { get; private set; }

        // Corresponds to 'location_city_status' (TINYINT(1))
        public bool LocationCityStatus { get; private set; }

        // Navigation properties
        public ICollection<Location> Locations { get; private set; } = new List<Location>();
        public ICollection<Supplier> CompanySuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Supplier> StoreSuppliers { get; private set; } = new List<Supplier>();
        public ICollection<Customer> Customers { get; private set; } = new List<Customer>();
        public ICollection<Employee> Employees { get; private set; } = new List<Employee>();

        public LocationCity(uint locationCityId, string locationCityName, bool locationCityStatus)
        {
            LocationCityId = locationCityId;
            LocationCityName = locationCityName;
            LocationCityStatus = locationCityStatus;
        }

        public uint GetIdEntity() => LocationCityId;
    }
}

