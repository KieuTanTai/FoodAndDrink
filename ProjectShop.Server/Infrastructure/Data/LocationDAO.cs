using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class LocationDAO : BaseDAO<LocationModel>, ILocationDAO<LocationModel>
    {
        public LocationDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "location", "location_id", string.Empty)
        {
        }

        protected override string GetInsertQuery()
        {
            return $@"INSERT INTO {TableName} (location_type_id, location_house_number, location_street, location_ward_id, location_district_id,
                     location_city_id, location_phone, location_email, location_name, location_status)
                    VALUES (@LocationTypeId, @LocationHouseNumber, @LocationStreet, @LocationWardId,
                     @LocationDistrictId, @LocationCityId, @LocationPhone, @LocationEmail, @LocationName, @LocationStatus); SELECT LAST_INSERT_ID();";
        }

        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"UPDATE {TableName} SET 
                location_type_id = @LocationTypeId,
                location_house_number = @LocationHouseNumber,
                location_street = @LocationStreet,
                location_ward_id = @LocationWardId,
                location_district_id = @LocationDistrictId,
                location_city_id = @LocationCityId,
                location_phone = @LocationPhone,
                location_email = @LocationEmail,
                location_name = @LocationName,
                location_status = @LocationStatus
              WHERE {ColumnIdName} = @{colIdName}";
        }

        public async Task<LocationModel?> GetByLocationNameAsync(string locationName) => await GetSingleDataAsync(locationName, "location_name");

        public async Task<IEnumerable<LocationModel>> GetByLocationNamesAsync(IEnumerable<string> locationNames) => await GetByInputsAsync(locationNames, "location_name");

        public async Task<LocationModel?> GetByHouseNumberAsync(string houseNumber) => await GetSingleDataAsync(houseNumber, "location_house_number");

        public async Task<IEnumerable<LocationModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "location_name");

        public async Task<IEnumerable<LocationModel>> GetByLocationCitiesAsync(IEnumerable<uint> cityIds) => await GetByInputsAsync(cityIds.Select(locationCity => locationCity.ToString()), "location_city_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationCityAsync(uint cityId) => await GetByInputAsync(cityId.ToString(), "location_city_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationDistrictAsync(uint districtId) => await GetByInputAsync(districtId.ToString(), "location_district_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationDistrictsAsync(IEnumerable<uint> districtIds) => await GetByInputsAsync(districtIds.Select(locationDistrict => locationDistrict.ToString()), "location_district_id");

        public async Task<LocationModel?> GetByLocationEmailAsync(string email) => await GetSingleDataAsync(email, "location_email");

        public async Task<IEnumerable<LocationModel>> GetByLocationEmailsAsync(IEnumerable<string> emails) => await GetByInputsAsync(emails, "location_email");

        public async Task<LocationModel?> GetByLocationPhoneAsync(string phone) => await GetSingleDataAsync(phone, "location_phone");

        public async Task<IEnumerable<LocationModel>> GetByLocationPhonesAsync(IEnumerable<string> phones) => await GetByInputsAsync(phones, "location_phone");

        public async Task<IEnumerable<LocationModel>> GetByLocationTypeIdAsync(uint typeId) => await GetByInputAsync(typeId.ToString(), "location_type_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationTypeIdsAsync(IEnumerable<uint> typeIds) => await GetByInputsAsync(typeIds.Select(locationType => locationType.ToString()), "location_type_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationWardIdAsync(uint wardId) => await GetByInputAsync(wardId.ToString(), "location_ward_id");

        public async Task<IEnumerable<LocationModel>> GetByLocationWardIdsAsync(IEnumerable<uint> wardIds) => await GetByInputsAsync(wardIds.Select(locationWard => locationWard.ToString()), "location_ward_id");

        public async Task<IEnumerable<LocationModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "location_status");

        public async Task<IEnumerable<LocationModel>> GetByStreetAsync(string streetName) => await GetByInputAsync(streetName, "location_street");
    }
}
