// File: LocationType.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationTypeModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_type_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationTypeId { get; set; }

        // Corresponds to 'location_type_name' (VARCHAR(20))
        public string LocationTypeName { get; set; } = string.Empty;

        // Corresponds to 'location_type_status' (TINYINT(1))
        public bool LocationTypeStatus { get; set; }

        // Navigation property for 'Location'
        public ICollection<LocationModel> Locations { get; set; } = new List<LocationModel>();

        public LocationTypeModel(uint locationTypeId, string locationTypeName, bool locationTypeStatus)
        {
            LocationTypeId = locationTypeId;
            LocationTypeName = locationTypeName;
            LocationTypeStatus = locationTypeStatus;
        }

        public LocationTypeModel() { }

        public uint GetIdEntity() => LocationTypeId;
    }
}

