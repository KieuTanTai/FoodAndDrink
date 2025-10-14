using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Employee
{
    public uint EmployeeId { get; init; }

    public uint PersonId { get; private set; }

    public string? EmployeeHouseNumber { get; set; }

    public string EmployeeStreet { get; set; } = null!;

    public uint? EmployeeWardId { get; private set; }

    public uint? EmployeeDistrictId { get; private set; }

    public uint? EmployeeCityId { get; private set; }

    public uint LocationId { get; private set; }

    public DateTime EmployeeHireDate { get; init; }

    public decimal EmployeeSalary { get; set; }

    public virtual ICollection<DisposeProduct> DisposeProducts { get; init; } = [];

    public virtual LocationCity? EmployeeCity { get; set; }

    public virtual LocationDistrict? EmployeeDistrict { get; set; }

    public virtual LocationWard? EmployeeWard { get; set; }

    public virtual ICollection<Invoice> Invoices { get; init; } = [];

    public virtual Location Location { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
