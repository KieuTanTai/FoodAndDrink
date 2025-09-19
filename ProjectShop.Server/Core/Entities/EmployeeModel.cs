// File: Employee.cs
namespace ProjectShop.Server.Core.Entities
{
    public class EmployeeModel : PersonModel
    {
        // Backing fields
        private uint _employeeId;
        private string _employeeHouseNumber = string.Empty;
        private string _employeeStreet = string.Empty;
        private uint? _employeeWardId;
        private uint? _employeeDistrictId;
        private uint? _employeeCityId;
        private uint _locationId;

        public uint EmployeeId
        {
            get => _employeeId;
            set => _employeeId = value;
        }

        public string EmployeeHouseNumber
        {
            get => _employeeHouseNumber;
            set => _employeeHouseNumber = value ?? string.Empty;
        }

        public string EmployeeStreet
        {
            get => _employeeStreet;
            set => _employeeStreet = value ?? string.Empty;
        }

        public uint? EmployeeWardId
        {
            get => _employeeWardId;
            set => _employeeWardId = value;
        }

        public uint? EmployeeDistrictId
        {
            get => _employeeDistrictId;
            set => _employeeDistrictId = value;
        }

        public uint? EmployeeCityId
        {
            get => _employeeCityId;
            set => _employeeCityId = value;
        }

        public uint LocationId
        {
            get => _locationId;
            set => _locationId = value;
        }

        // Navigation properties
        public LocationWardModel? EmployeeWard { get; set; }
        public LocationDistrictModel? EmployeeDistrict { get; set; }
        public LocationCityModel? EmployeeCity { get; set; }
        public LocationModel Location { get; set; } = null!;
        // End of navigation properties

        public EmployeeModel(
            uint employeeId,
            uint accountId,
            DateTime employeeBirthday,
            string employeePhone,
            string employeeName,
            string employeeHouseNumber,
            string employeeStreet,
            uint? employeeWardId,
            uint? employeeDistrictId,
            uint? employeeCityId,
            string employeeAvatarUrl,
            uint locationId,
            string employeeEmail,
            bool employeeGender,
            bool employeeStatus
        ) : base(accountId, employeeBirthday, employeePhone, employeeName, employeeEmail, employeeAvatarUrl, employeeGender, employeeStatus)
        {
            EmployeeId = employeeId;
            EmployeeHouseNumber = employeeHouseNumber;
            EmployeeStreet = employeeStreet;
            EmployeeWardId = employeeWardId;
            EmployeeDistrictId = employeeDistrictId;
            EmployeeCityId = employeeCityId;
        }

        public EmployeeModel() : base() { }

        public override uint GetIdEntity() => EmployeeId;
    }
}
