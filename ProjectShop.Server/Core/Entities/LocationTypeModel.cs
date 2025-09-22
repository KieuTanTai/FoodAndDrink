// File: LocationType.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationTypeModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _locationTypeId;
        private string _locationTypeName = string.Empty;
        private bool _locationTypeStatus;

        // Corresponds to 'location_type_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationTypeId
        {
            get => _locationTypeId;
            set => _locationTypeId = value;
        }

        // Corresponds to 'location_type_name' (VARCHAR(20))
        public string LocationTypeName
        {
            get => _locationTypeName;
            set => _locationTypeName = value ?? string.Empty;
        }

        // Corresponds to 'location_type_status' (TINYINT(1))
        public bool LocationTypeStatus
        {
            get => _locationTypeStatus;
            set => _locationTypeStatus = value;
        }

        // Navigation properties
        public ICollection<LocationModel> Locations { get; set; } = [];
        // End of navigation properties

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

