// File: Employee.cs
using ProjectShop.Server.Core.Interfaces.IEntities;
using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities
{
    public class Employee : IGetIdEntity<uint>
    {
        // Corresponds to 'employee_id' (INT UNSIGNED AUTO_INCREMENT)
        public uint EmployeeId { get; private set; }

        // Corresponds to 'account_id' (INT UNSIGNED UNIQUE)
        public uint AccountId { get; private set; }
        public Account Account { get; private set; } = null!;

        // Corresponds to 'employee_birthday' (DATE)
        public DateTime EmployeeBirthday { get; private set; }

        // Corresponds to 'employee_phone' (VARCHAR(12))
        public string EmployeePhone { get; private set; }

        // Corresponds to 'employee_name' (NVARCHAR(100))
        public string EmployeeName { get; private set; }

        // Corresponds to 'employee_house_number' (VARCHAR(20))
        public string EmployeeHouseNumber { get; private set; }

        // Corresponds to 'employee_street' (NVARCHAR(40))
        public string EmployeeStreet { get; private set; }

        // Corresponds to 'employee_ward_id' (INT UNSIGNED)
        public uint? EmployeeWardId { get; private set; }
        public LocationWard? EmployeeWard { get; private set; }

        // Corresponds to 'employee_district_id' (INT UNSIGNED)
        public uint? EmployeeDistrictId { get; private set; }
        public LocationDistrict? EmployeeDistrict { get; private set; }

        // Corresponds to 'employee_city_id' (INT UNSIGNED)
        public uint? EmployeeCityId { get; private set; }
        public LocationCity? EmployeeCity { get; private set; }

        // Corresponds to 'employee_avatar_url' (VARCHAR(255))
        public string EmployeeAvatarUrl { get; private set; }

        // Corresponds to 'employee_gender' (TINYINT(1))
        public bool EmployeeGender { get; private set; }

        // Corresponds to 'employee_status' (TINYINT(1))
        public bool EmployeeStatus { get; private set; }

        // Navigation properties
        public ICollection<DisposeProduct> DisposeProducts { get; private set; } = new List<DisposeProduct>();
        public ICollection<News> News { get; private set; } = new List<News>();

        public Employee(uint employeeId, uint accountId, DateTime employeeBirthday, string employeePhone, string employeeName, string employeeHouseNumber, string employeeStreet, uint? employeeWardId, uint? employeeDistrictId, uint? employeeCityId, string employeeAvatarUrl, bool employeeGender, bool employeeStatus)
        {
            EmployeeId = employeeId;
            AccountId = accountId;
            EmployeeBirthday = employeeBirthday;
            EmployeePhone = employeePhone;
            EmployeeName = employeeName;
            EmployeeHouseNumber = employeeHouseNumber;
            EmployeeStreet = employeeStreet;
            EmployeeWardId = employeeWardId;
            EmployeeDistrictId = employeeDistrictId;
            EmployeeCityId = employeeCityId;
            EmployeeAvatarUrl = employeeAvatarUrl;
            EmployeeGender = employeeGender;
            EmployeeStatus = employeeStatus;
        }

        public uint GetIdEntity() => EmployeeId;
    }
}

