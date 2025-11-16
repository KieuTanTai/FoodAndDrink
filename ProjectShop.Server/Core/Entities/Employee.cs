using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Employee
{
    public uint EmployeeId { get; init; }

    public uint PersonId { get; private set; }

    public string EmployeeIdentificationCard { get; set; } = null!;

    public uint EmployeeWorkLocationId { get; private set; }

    public DateTime EmployeeHireDate { get; init; }

    public decimal EmployeeSalary { get; set; }

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = [];

    public virtual ICollection<Invoice> Invoices { get; set; } = [];

    public virtual Location EmployeeWorkLocation { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
