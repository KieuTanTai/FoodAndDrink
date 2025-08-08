// File: LocationType.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationType : IGetIdEntity<uint>
    {
        // Corresponds to 'location_type_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationTypeId { get; private set; }

        // Corresponds to 'location_type_name' (VARCHAR(20))
        public string LocationTypeName { get; private set; }

        // Corresponds to 'location_type_status' (TINYINT(1))
        public bool LocationTypeStatus { get; private set; }

        // Navigation property for 'Location'
        public ICollection<Location> Locations { get; private set; } = new List<Location>();

        public LocationType(uint locationTypeId, string locationTypeName, bool locationTypeStatus)
        {
            LocationTypeId = locationTypeId;
            LocationTypeName = locationTypeName;
            LocationTypeStatus = locationTypeStatus;
        }

        public uint GetIdEntity() => LocationTypeId;
    }
}

