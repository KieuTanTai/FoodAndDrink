using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Employee
{
    public uint EmployeeId { get; set; }

    public uint PersonId { get; set; }

    public string? EmployeeHouseNumber { get; set; }

    public string EmployeeStreet { get; set; } = null!;

    public uint? EmployeeWardId { get; set; }

    public uint? EmployeeDistrictId { get; set; }

    public uint? EmployeeCityId { get; set; }

    public uint LocationId { get; set; }

    public DateTime EmployeeHireDate { get; set; }

    public decimal EmployeeSalary { get; set; }

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = new List<DisposeProduct>();

    public virtual LocationCity? EmployeeCity { get; set; }

    public virtual LocationDistrict? EmployeeDistrict { get; set; }

    public virtual LocationWard? EmployeeWard { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Location Location { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
