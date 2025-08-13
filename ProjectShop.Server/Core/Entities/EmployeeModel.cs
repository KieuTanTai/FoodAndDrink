// File: Employee.cs
namespace ProjectShop.Server.Core.Entities
{
    public class EmployeeModel : PersonModel
    {
        public uint EmployeeId { get; private set; }
        public string EmployeeHouseNumber { get; private set; }
        public string EmployeeStreet { get; private set; }
        public uint? EmployeeWardId { get; private set; }
        public uint? EmployeeDistrictId { get; private set; }
        public uint? EmployeeCityId { get; private set; }
        public uint LocationId { get; private set; }

        // Navigation properties
        public LocationWardModel? EmployeeWard { get; private set; }
        public LocationDistrictModel? EmployeeDistrict { get; private set; }
        public LocationCityModel? EmployeeCity { get; private set; }
        public LocationModel Location { get; private set; } = null!;

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

        public override uint GetIdEntity() => EmployeeId;
    }
}
