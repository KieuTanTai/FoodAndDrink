// File: Employee.cs
namespace ProjectShop.Server.Core.Entities
{
    public class EmployeeModel : PersonModel
    {
        public uint EmployeeId { get; private set; }

        // Các navigation properties riêng c?a Employee
        public ICollection<DisposeProductModel> DisposeProducts { get; private set; } = new List<DisposeProductModel>();
        public ICollection<NewsModel> News { get; private set; } = new List<NewsModel>();

        // Constructor s? d?ng `base(...)` ?? g?i constructor c?a l?p cha.
        public EmployeeModel(uint employeeId, uint accountId, DateTime employeeBirthday, string employeePhone, string employeeName, string employeeHouseNumber, string employeeStreet,
            uint? employeeWardId, uint? employeeDistrictId, uint? employeeCityId, string employeeAvatarUrl, bool employeeGender, bool employeeStatus)
            : base(accountId, employeeBirthday, employeePhone, employeeName, employeeHouseNumber, employeeStreet, employeeWardId, employeeDistrictId, employeeCityId, employeeAvatarUrl, employeeGender, employeeStatus)
        {
            EmployeeId = employeeId;
        }

        // Tri?n khai ph??ng th?c tr?u t??ng t? l?p cha.
        public override uint GetIdEntity() => EmployeeId;
    }
}

