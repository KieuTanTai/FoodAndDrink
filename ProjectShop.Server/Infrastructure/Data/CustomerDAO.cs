using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Enums;
using ProjectShop.Server.Core.Interfaces.IData;
using ProjectShop.Server.Core.Interfaces.IData.IUniqueDAO;
using ProjectShop.Server.Core.Interfaces.IValidate;
using ProjectShop.Server.Infrastructure.Persistence;

namespace ProjectShop.Server.Infrastructure.Data
{
    public class CustomerDAO : BaseDAO<CustomerModel>, IPersonDAO<CustomerModel>
    {
        public CustomerDAO(
            IDbConnectionFactory connectionFactory,
            IColumnService colService,
            IStringConverter converter,
            IStringChecker checker)
            : base(connectionFactory, colService, converter, checker, "customer", "customer_id", string.Empty)
        {
        }

        protected override string GetAllQuery()
        {
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, customer_email AS Email,
                    customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName}";
        }

        protected override string GetDataQuery(string colIdName)
        {
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, customer_email AS Email,
                    customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName}
                WHERE {colIdName} = @Input";
        }

        protected override string GetInsertQuery()
        {
            return $@"
                INSERT INTO {TableName} (
                    account_id, customer_name, customer_birthday, customer_phone,
                    customer_email, customer_avatar_url,
                    customer_gender, customer_status
                )
                VALUES (
                    @AccountId, @Name, @Birthday, @Phone,
                    @Email, @AvatarUrl,
                    @Gender, @Status
                ); SELECT LAST_INSERT_ID();";
        }
        protected override string GetUpdateQuery()
        {
            string colIdName = Converter.SnakeCaseToPascalCase(ColumnIdName);
            return $@"
                UPDATE {TableName} SET
                    account_id = @AccountId,
                    customer_name = @Name,
                    customer_birthday = @Birthday,
                    customer_phone = @Phone,
                    customer_email = @Email,
                    customer_avatar_url = @AvatarUrl,
                    customer_gender = @Gender,
                    customer_status = @Status
                WHERE {ColumnIdName} = @{colIdName}";
        }

        protected override string RelativeQuery(string colName)
        {
            return $@"
                SELECT
                    customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                    customer_birthday AS Birthday, customer_phone AS Phone, 
                    customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                    customer_status AS Status
                FROM {TableName} WHERE {colName} LIKE @Input";
        }

        protected override string GetByMonthAndYear(string colName)
        {
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE YEAR({colName}) = @FirstTime AND MONTH({colName}) = @SecondTime";
        }

        protected override string GetByYear(string colName, ECompareType compareType)
        {
            string compareOperator = GetStringType(compareType);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE Year({colName}) {compareOperator} @Input";
        }

        protected override string GetByDateTimeRange(string colName)
        {
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE {colName} >= @FirstTime AND {colName} < DATE_ADD(@SecondTime, INTERVAL 1 DAY)";
        }

        protected override string GetByDateTime(string colName, ECompareType compareType)
        {
            string compareOperator = GetStringType(compareType);
            return $@"
                    SELECT
                        customer_id AS CustomerId, account_id AS AccountId, customer_name AS Name,
                        customer_birthday AS Birthday, customer_phone AS Phone, 
                        customer_email AS Email, customer_avatar_url AS AvatarUrl, customer_gender AS Gender,
                        customer_status AS Status
                    FROM {TableName} WHERE {colName} {compareOperator} DATE_ADD(@Input, INTERVAL 1 DAY)";
        }

        public async Task<CustomerModel?> GetByAccountIdAsync(uint accountId) => await GetSingleDataAsync(accountId.ToString(), "account_id");

        public async Task<IEnumerable<CustomerModel>> GetByLikeStringAsync(string input) => await GetByLikeStringAsync(input, "customer_name");

        public async Task<IEnumerable<CustomerModel>> GetByStatusAsync(bool status) => await GetByInputAsync(GetTinyIntString(status), "customer_status");

        public async Task<IEnumerable<CustomerModel>> GetByGenderAsync(bool isMale) => await GetByInputAsync(isMale.ToString(), "customer_gender");

        public async Task<IEnumerable<CustomerModel>> GetByGendersAsync(IEnumerable<bool> isMales) => await GetByInputsAsync(isMales.Select(isMale => isMale.ToString()), "customer_gender");

        public async Task<CustomerModel?> GetByPhoneAsync(string phone) => await GetSingleDataAsync(phone, "customer_phone");

        public async Task<CustomerModel?> GetByEmailAsync(string email) => await GetSingleDataAsync(email, "customer_email");

        public async Task<CustomerModel?> GetByNameAsync(string name) => await GetSingleDataAsync(name, "customer_name");

        public async Task<IEnumerable<CustomerModel>> GetByPhonesAsync(IEnumerable<string> phones) => await GetByInputsAsync(phones, "customer_phone");

        public async Task<IEnumerable<CustomerModel>> GetByEmailsAsync(IEnumerable<string> emails) => await GetByInputsAsync(emails, "customer_email");

        public async Task<IEnumerable<CustomerModel>> GetByNamesAsync(IEnumerable<string> names) => await GetByInputsAsync(names, "customer_name");

        public async Task<IEnumerable<CustomerModel>> GetByMonthAndYearAsync(int year, int month)
            => await GetByDateTimeAsync("customer_birthday", EQueryTimeType.MONTH_AND_YEAR, new Tuple<int, int>(year, month));

        public async Task<IEnumerable<CustomerModel>> GetByYearAsync<TEnum>(int year, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("customer_birthday", EQueryTimeType.YEAR, type, year);
        }

        public async Task<IEnumerable<CustomerModel>> GetByDateTimeRangeAsync(DateTime startDate, DateTime endDate)
            => await GetByDateTimeAsync("customer_birthday", EQueryTimeType.DATE_TIME_RANGE, new Tuple<DateTime, DateTime>(startDate, endDate));

        public async Task<IEnumerable<CustomerModel>> GetByDateTimeAsync<TEnum>(DateTime dateTime, TEnum compareType) where TEnum : Enum
        {
            if (compareType is not ECompareType type)
                throw new ArgumentException("Invalid compare type provided.");
            return await GetByDateTimeAsync("customer_birthday", EQueryTimeType.DATE_TIME, type, dateTime);
        }
    }
}
