// File: Employee.cs
namespace ProjectShop.Server.Core.Entities
{
    public class EmployeeModel : PersonModel
    {
        public uint EmployeeId { get; set; }
        public string EmployeeHouseNumber { get; set; } = string.Empty;
        public string EmployeeStreet { get; set; } = string.Empty;
        public uint? EmployeeWardId { get; set; }
        public uint? EmployeeDistrictId { get; set; }
        public uint? EmployeeCityId { get; set; }
        public uint LocationId { get; set; }

        // Navigation properties
        public LocationWardModel? EmployeeWard { get; set; }
        public LocationDistrictModel? EmployeeDistrict { get; set; }
        public LocationCityModel? EmployeeCity { get; set; }
        public LocationModel Location { get; set; } = null!;

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
