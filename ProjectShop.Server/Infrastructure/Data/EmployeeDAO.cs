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
            IStringConverter converter,
            ILogService logger)
            : base(connectionFactory, converter, logger, "employee", "employee_id", string.Empty)
        {
        }

        protected override string GetByListInputQuery(string colName)
        {
            return $@"SELECT 
                employee_id AS EmployeeId,
                account_id AS AccountId,
                employee_birthday AS Birthday,
                employee_phone AS Phone,
                employee_name AS Name,
                employee_email AS Email,
                employee_avatar_url AS AvatarUrl,
                employee_gender AS Gender,
                employee_status AS Status
                FROM {TableName} WHERE {colName} IN @Inputs";
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

        public async Task<EmployeeModel?> GetByHouseNumberAsync(string houseNumber)
            => await GetSingleDataAsync(houseNumber, "employee_house_number");

        public async Task<IEnumerable<EmployeeModel>> GetByHouseNumbersAsync(IEnumerable<string> houseNumbers, int? maxGetCount)
            => await GetByInputsAsync(houseNumbers, "employee_house_number", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByStreetAsync(string street, int? maxGetCount)
            => await GetByInputAsync(street, "employee_street", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByStreetsAsync(IEnumerable<string> streets, int? maxGetCount)
            => await GetByInputsAsync(streets, "employee_street", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByCityAsync(uint city, int? maxGetCount)
            => await GetByInputAsync(city.ToString(), "employee_city_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByCitiesAsync(IEnumerable<uint> cities, int? maxGetCount)
            => await GetByInputsAsync(cities.Select(city => city.ToString()), "employee_city_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByWardIdAsync(uint wardId, int? maxGetCount)
            => await GetByInputAsync(wardId.ToString(), "employee_ward_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByWardIdsAsync(IEnumerable<uint> wardIds, int? maxGetCount)
            => await GetByInputsAsync(wardIds.Select(wardId => wardId.ToString()), "employee_ward_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByDistrictIdAsync(uint districtId, int? maxGetCount)
            => await GetByInputAsync(districtId.ToString(), "employee_district_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByDistrictIdsAsync(IEnumerable<uint> districtIds, int? maxGetCount)
            => await GetByInputsAsync(districtIds.Select(districtId => districtId.ToString()), "employee_district_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByLocationIdAsync(uint locationId, int? maxGetCount)
            => await GetByInputAsync(locationId.ToString(), "location_id", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByLocationIdsAsync(IEnumerable<uint> locationIds, int? maxGetCount)
            => await GetByInputsAsync(locationIds.Select(locationId => locationId.ToString()), "location_id", maxGetCount);

        public async Task<EmployeeModel?> GetByAccountIdAsync(uint accountId)
            => await GetSingleDataAsync(accountId.ToString(), "account_id");
        public async Task<IEnumerable<EmployeeModel>> GetByAccountIdsAsync(IEnumerable<uint> accountIds, int? maxGetCount)
            => await GetByInputsAsync(accountIds.Select(accountId => accountId.ToString()), "account_id", maxGetCount);
        public async Task<EmployeeModel?> GetByPhoneAsync(string phone)
            => await GetSingleDataAsync(phone, "employee_phone");

        public async Task<IEnumerable<EmployeeModel>> GetByPhonesAsync(IEnumerable<string> phones, int? maxGetCount)
            => await GetByInputsAsync(phones, "employee_phone", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByGenderAsync(bool isMale, int? maxGetCount)
            => await GetByInputAsync(isMale.ToString(), "employee_gender", maxGetCount);

        public async Task<EmployeeModel?> GetByEmailAsync(string email)
            => await GetSingleDataAsync(email, "employee_email");

        public async Task<IEnumerable<EmployeeModel>> GetByEmailsAsync(IEnumerable<string> emails, int? maxGetCount)
            => await GetByInputsAsync(emails, "employee_email", maxGetCount);

        public async Task<EmployeeModel?> GetByNameAsync(string name)
            => await GetSingleDataAsync(name, "employee_name");

        public async Task<IEnumerable<EmployeeModel>> GetByNamesAsync(IEnumerable<string> names, int? maxGetCount)
            => await GetByInputsAsync(names, "employee_name", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByStatusAsync(bool status, int? maxGetCount)
            => await GetByInputAsync(GetTinyIntString(status), "employee_status", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByLikeStringAsync(string input, int? maxGetCount)
            => await GetByLikeStringAsync(input, "employee_name", maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByMonthAndYearAsync(int year, int month, int? maxGetCount)
            => await GetByDateTimeAsync("employee_birthday", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month), maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByYearAsync<TEnum>(int year, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for year comparison.");
            return await GetByDateTimeAsync("employee_birthday", EQueryTimeType.YEAR, type, year, maxGetCount);
        }

        public async Task<IEnumerable<EmployeeModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate, int? maxGetCount)
            => await GetByDateTimeAsync("employee_birthday", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate), maxGetCount);

        public async Task<IEnumerable<EmployeeModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType, int? maxGetCount) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type for date time comparison.");
            return await GetByDateTimeAsync("employee_birthday", EQueryTimeType.DATE_TIME, type, dateTime, maxGetCount);
        }
    }
}
