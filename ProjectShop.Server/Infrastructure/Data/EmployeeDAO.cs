using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;
using System.Data;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class EmployeeDAO : BaseDAO<EmployeeModel>, IEmployeeDAO<EmployeeModel>
    {
        public EmployeeDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "employee", "employee_id", string.Empty)
        {
        }

        protected override string GetAllQuery()
        {
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                    employee_street, employee_ward_id, employee_district_id, location_id,
                    employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName}";
        }

        protected override string GetDataQuery(string colIdName)
        {
            string colIdNamePascal = Converter.SnakeCaseToPascalCase(colIdName);
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                    employee_street, employee_ward_id, employee_district_id, location_id,
                    employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName}
                WHERE {colIdName} = @Input";
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (
                    account_id, employee_name, employee_birthday, employee_phone,
                    employee_house_number, employee_street, employee_ward_id,
                    employee_district_id, employee_city_id, employee_avatar_url,
                    employee_gender, employee_status, location_id
                )
                VALUES (
                    @AccountId, @Name, @Birthday, @Phone,
                    @HouseNumber, @Street, @WardId,
                    @DistrictId, @CityId, @AvatarUrl,
                    @Gender, @Status, @LocationId
                ); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName} SET
                    account_id = @AccountId,
                    employee_name = @Name,
                    employee_birthday = @Birthday,
                    employee_phone = @Phone,
                    employee_house_number = @EmployeeHouseNumber,
                    employee_street = @EmployeeStreet,
                    employee_ward_id = @EmployeeWardId,
                    employee_district_id = @DEmployeeistrictId,
                    employee_city_id = @EmployeeCityId,
                    employee_avatar_url = @AvatarUrl,
                    employee_gender = @Gender,
                    employee_status = @Status,
                    employee_email = @Email,
                    location_id = @LocationId   
                WHERE {ColumnIdName} = @{colIdName}";
        }

        protected override string RelativeQuery(string colName)
        {
            return $@"
                SELECT
                    employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                    employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                    employee_street, employee_ward_id, employee_district_id, location_id,
                    employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender,
                    employee_status AS Status
                FROM {TableName} WHERE {colName} LIKE @Input";
        }

        protected override string GetByMonthAndYear(string colName)
        {
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email,
                        employee_house_number, employee_street, employee_ward_id, employee_district_id, location_id,
                        employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        protected override string GetByYear(string colName, ECompareType compareType)
        {
            string compareOperator = GetStringType(compareType);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                        employee_street, employee_ward_id, employee_district_id, location_id,
                        employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE Year({colName}) {compareOperator} @Input";
        }

        protected override string GetByDateTimeRange(string colName)
        {
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                        employee_street, employee_ward_id, employee_district_id, location_id,
                        employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        protected override string GetByDateTime(string colName, ECompareType compareType)
        {
            string compareOperator = GetStringType(compareType);
            return $@"
                    SELECT
                        employee_id AS EmployeeId, account_id AS AccountId, employee_name AS Name,
                        employee_birthday AS Birthday, employee_phone AS Phone, employee_email AS Email, employee_house_number,
                        employee_street, employee_ward_id, employee_district_id, location_id,
                        employee_city_id, employee_avatar_url AS AvatarUrl, employee_gender AS Gender, employee_status AS Status
                    FROM {TableName} WHERE {colName} {compareOperator} DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<EmployeeModel?> GetByHouseNumberAsync(string houseNumber) => await GetSingleDataAsync(houseNumber, "employee_house_number");

        public async Task<IEnumerable<EmployeeModel>> GetByHouseNumbersAsync(IEnumerable<string> houseNumbers) => await GetByInputsAsync(houseNumbers, "employee_house_number");

        public async Task<IEnumerable<EmployeeModel>> GetByStreetAsync(string street) => await GetByInputAsync(street, "employee_street");

        public async Task<IEnumerable<EmployeeModel>> GetByStreetsAsync(IEnumerable<string> streets) => await GetByInputsAsync(streets, "employee_street");

        public async Task<IEnumerable<EmployeeModel>> GetByCityAsync(uint city) => await GetByInputAsync(city.ToString(), "employee_city_id");

        public async Task<IEnumerable<EmployeeModel>> GetByCitiesAsync(IEnumerable<uint> cities) => await GetByInputsAsync(cities.Select(city => city.ToString()), "employee_city_id");

        public async Task<IEnumerable<EmployeeModel>> GetByWardIdAsync(uint wardId) => await GetByInputAsync(wardId.ToString(), "employee_ward_id");

        public async Task<IEnumerable<EmployeeModel>> GetByWardIdsAsync(IEnumerable<uint> wardIds) => await GetByInputsAsync(wardIds.Select(wardId => wardId.ToString()), "employee_ward_id");

        public async Task<IEnumerable<EmployeeModel>> GetByDistrictIdAsync(uint districtId) => await GetByInputAsync(districtId.ToString(), "employee_district_id");

        public async Task<IEnumerable<EmployeeModel>> GetByDistrictIdsAsync(IEnumerable<uint> districtIds) => await GetByInputsAsync(districtIds.Select(districtId => districtId.ToString()), "employee_district_id");

        public async Task<IEnumerable<EmployeeModel>> GetByLocationIdAsync(uint locationId) => await GetByInputAsync(locationId.ToString(), "location_id");

        public async Task<IEnumerable<EmployeeModel>> GetByLocationIdsAsync(IEnumerable<uint> locationIds) => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "location_id");

        public async Task<EmployeeModel?> GetByAccountIdAsync(uint accountId) => await GetSingleDataAsync(accountId.ToString(), "account_id");
        public async Task<IEnumerable<EmployeeModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds) 
            => await GetByInputsAsync(accountIds.Select(accountId => accountId.ToString()), "account_id");
        public async Task<EmployeeModel?> GetByPhoneAsync(string phone) => await GetSingleDataAsync(phone, "employee_phone");

        public async Task<IEnumerable<EmployeeModel>> GetByPhonesAsync(IEnumerable<string> phones) => await GetByInputsAsync(phones, "employee_phone");

        public async Task<IEnumerable<EmployeeModel>> GetByGenderAsync(bool isMale) => await GetByInputAsync(isMale.ToString(), "employee_gender");

        public async Task<IEnumerable<EmployeeModel>> GetByGendersAsync(IEnumerable<bool> isMales) => await GetByInputsAsync(isMales.Select(isMale => isMale.ToString()), "employee_gender");

        public async Task<EmployeeModel?> GetByEmailAsync(string email) => await GetSingleDataAsync(email, "employee_email");

        public async Task<IEnumerable<EmployeeModel>> GetByEmailsAsync(IEnumerable<string> emails) => await GetByInputsAsync(emails, "employee_email");

        public async Task<EmployeeModel?> GetByNameAsync(string name) => await GetSingleDataAsync(name, "employee_name");

        public async Task<IEnumerable<EmployeeModel>> GetByNamesAsync(IEnumerable<string> names) => await GetByInputsAsync(names, "employee_name");

        public async Task<IEnumerable<EmployeeModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "employee_status");

        public async Task<IEnumerable<EmployeeModel>> GetByStatusAsync(bool status, int maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "employee_status", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "employee_name");

        public async Task<IEnumerable<EmployeeModel>> GetByLikeStringAsync(string input, int maxGetCount)
            => await GetByLikeStringAsync(input, "employee_name", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByMonthAndYearAsync(int year, int month) => await GetByDateTimeAsync("employee_birthday", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<EmployeeModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for year comparison.");
            return await GetByDateTimeAsync("employee_birthday", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<EmployeeModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate) => await GetByDateTimeAsync("employee_birthday", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<EmployeeModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for date time comparison.");
            return await GetByDateTimeAsync("employee_birthday", EQueryTimeType.DATE_TIME, type, dateTime);
        }
    }
}
