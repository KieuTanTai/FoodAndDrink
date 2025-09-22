// File: Location.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationModel : IGetIdEntity<uint>
    {
        // Backing fields
        private uint _locationId;
        private uint _locationTypeId;
        private string _locationHouseNumber = string.Empty;
        private string _locationStreet = string.Empty;
        private uint _locationWardId;
        private uint _locationDistrictId;
        private uint _locationCityId;
        private string _locationPhone = string.Empty;
        private string _locationEmail = string.Empty;
        private string _locationName = string.Empty;
        private bool _locationStatus;

        // Corresponds to 'location_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationId
        {
            get => _locationId;
            set => _locationId = value;
        }

        // Corresponds to 'location_type_id' (INT UNSIGNED)
        public uint LocationTypeId
        {
            get => _locationTypeId;
            set => _locationTypeId = value;
        }

        // Corresponds to 'house_number' (VARCHAR(20))
        public string LocationHouseNumber
        {
            get => _locationHouseNumber;
            set => _locationHouseNumber = value ?? string.Empty;
        }

        // Corresponds to 'location_street' (NVARCHAR(40))
        public string LocationStreet
        {
            get => _locationStreet;
            set => _locationStreet = value ?? string.Empty;
        }

        // Corresponds to 'location_ward_id' (INT UNSIGNED)
        public uint LocationWardId
        {
            get => _locationWardId;
            set => _locationWardId = value;
        }

        // Corresponds to 'location_district_id' (INT UNSIGNED)
        public uint LocationDistrictId
        {
            get => _locationDistrictId;
            set => _locationDistrictId = value;
        }

        // Corresponds to 'location_city_id' (INT UNSIGNED)
        public uint LocationCityId
        {
            get => _locationCityId;
            set => _locationCityId = value;
        }

        // Corresponds to 'location_phone' (VARCHAR(12))
        public string LocationPhone
        {
            get => _locationPhone;
            set => _locationPhone = value ?? string.Empty;
        }

        // Corresponds to 'location_email' (VARCHAR(60))
        public string LocationEmail
        {
            get => _locationEmail;
            set => _locationEmail = value ?? string.Empty;
        }

        // Corresponds to 'location_name' (VARCHAR(50))
        public string LocationName
        {
            get => _locationName;
            set => _locationName = value ?? string.Empty;
        }

        // Corresponds to 'location_status' (TINYINT(1))
        public bool LocationStatus
        {
            get => _locationStatus;
            set => _locationStatus = value;
        }

        // Navigation properties
        public LocationDistrictModel LocationDistrict { get; set; } = null!;
        public LocationWardModel LocationWard { get; set; } = null!;
        public LocationTypeModel LocationType { get; set; } = null!;
        public LocationCityModel LocationCity { get; set; } = null!;
        public InventoryModel Inventory { get; set; } = null!;
        public ICollection<InventoryMovementModel> SourceInventoryMovements { get; set; } = [];
        public ICollection<InventoryMovementModel> DestinationInventoryMovements { get; set; } = [];
        public ICollection<DisposeProductModel> DisposeProducts { get; set; } = [];
        // End of navigation properties

        public LocationModel(uint locationId, uint locationTypeId, string locationHouseNumber, string locationStreet, uint locationWardId, uint locationDistrictId, uint locationCityId, string locationPhone, string locationEmail, string locationName, bool locationStatus)
        {
            LocationId = locationId;
            LocationTypeId = locationTypeId;
            LocationHouseNumber = locationHouseNumber;
            LocationStreet = locationStreet;
            LocationWardId = locationWardId;
            LocationDistrictId = locationDistrictId;
            LocationCityId = locationCityId;
            LocationPhone = locationPhone;
            LocationEmail = locationEmail;
            LocationName = locationName;
            LocationStatus = locationStatus;
        }

        public LocationModel() { }

        public uint GetIdEntity() => LocationId;
    }
}

