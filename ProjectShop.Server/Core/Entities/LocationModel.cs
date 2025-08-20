// File: Location.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationId { get; set; }

        // Corresponds to 'location_type_id' (INT UNSIGNED)
        public uint LocationTypeId { get; set; }

        // Corresponds to 'house_number' (VARCHAR(20))
        public string LocationHouseNumber { get; set; } = string.Empty;

        // Corresponds to 'location_street' (NVARCHAR(40))
        public string LocationStreet { get; set; } = string.Empty;

        // Corresponds to 'location_ward_id' (INT UNSIGNED)
        public uint LocationWardId { get; set; }

        // Corresponds to 'location_district_id' (INT UNSIGNED)
        public uint LocationDistrictId { get; set; }

        // Corresponds to 'location_city_id' (INT UNSIGNED)
        public uint LocationCityId { get; set; }

        // Corresponds to 'location_phone' (VARCHAR(12))
        public string LocationPhone { get; set; } = string.Empty;

        // Corresponds to 'location_email' (VARCHAR(60))
        public string LocationEmail { get; set; } = string.Empty;

        // Corresponds to 'location_name' (VARCHAR(50))
        public string LocationName { get; set; } = string.Empty;

        // Corresponds to 'location_status' (TINYINT(1))
        public bool LocationStatus { get; set; }

        // Navigation properties
        public LocationDistrictModel LocationDistrict { get; set; } = null!;
        public LocationWardModel LocationWard { get; set; } = null!;
        public LocationTypeModel LocationType { get; set; } = null!;
        public LocationCityModel LocationCity { get; set; } = null!;
        public ICollection<InventoryModel> Inventories { get; set; } = new List<InventoryModel>();
        public ICollection<InventoryMovementModel> SourceInventoryMovements { get; set; } = new List<InventoryMovementModel>();
        public ICollection<InventoryMovementModel> DestinationInventoryMovements { get; set; } = new List<InventoryMovementModel>();
        public ICollection<DisposeProductModel> DisposeProducts { get; set; } = new List<DisposeProductModel>();
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

