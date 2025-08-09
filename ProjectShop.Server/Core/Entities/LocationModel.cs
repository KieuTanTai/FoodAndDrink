// File: Location.cs
using ProjectShop.Server.Core.Interfaces.IEntities;

namespace ProjectShop.Server.Core.Entities
{
    public class LocationModel : IGetIdEntity<uint>
    {
        // Corresponds to 'location_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint LocationId { get; private set; }

        // Corresponds to 'location_type_id' (INT UNSIGNED)
        public uint LocationTypeId { get; private set; }

        // Corresponds to 'house_number' (VARCHAR(20))
        public string HouseNumber { get; private set; }

        // Corresponds to 'location_street' (NVARCHAR(40))
        public string LocationStreet { get; private set; }

        // Corresponds to 'location_ward_id' (INT UNSIGNED)
        public uint LocationWardId { get; private set; }

        // Corresponds to 'location_district_id' (INT UNSIGNED)
        public uint LocationDistrictId { get; private set; }

        // Corresponds to 'location_city_id' (INT UNSIGNED)
        public uint LocationCityId { get; private set; }

        // Corresponds to 'location_phone' (VARCHAR(12))
        public string LocationPhone { get; private set; }

        // Corresponds to 'location_email' (VARCHAR(60))
        public string LocationEmail { get; private set; }

        // Corresponds to 'location_name' (VARCHAR(50))
        public string LocationName { get; private set; }

        // Corresponds to 'location_status' (TINYINT(1))
        public bool LocationStatus { get; private set; }

        // Navigation properties
        public LocationDistrict LocationDistrict { get; private set; } = null!;
        public LocationWardModel LocationWard { get; private set; } = null!;
        public LocationTypeModel LocationType { get; private set; } = null!;
        public LocationCity LocationCity { get; private set; } = null!;
        public ICollection<InventoryModel> Inventories { get; private set; } = new List<InventoryModel>();
        public ICollection<InventoryMovementModel> SourceInventoryMovements { get; private set; } = new List<InventoryMovementModel>();
        public ICollection<InventoryMovementModel> DestinationInventoryMovements { get; private set; } = new List<InventoryMovementModel>();
        public ICollection<DisposeProductModel> DisposeProducts { get; private set; } = new List<DisposeProductModel>();

        public LocationModel(uint locationId, uint locationTypeId, string houseNumber, string locationStreet, uint locationWardId, uint locationDistrictId, uint locationCityId, string locationPhone, string locationEmail, string locationName, bool locationStatus)
        {
            LocationId = locationId;
            LocationTypeId = locationTypeId;
            HouseNumber = houseNumber;
            LocationStreet = locationStreet;
            LocationWardId = locationWardId;
            LocationDistrictId = locationDistrictId;
            LocationCityId = locationCityId;
            LocationPhone = locationPhone;
            LocationEmail = locationEmail;
            LocationName = locationName;
            LocationStatus = locationStatus;
        }

        public uint GetIdEntity() => LocationId;
    }
}

