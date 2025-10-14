using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class LocationDistrict
{
    public uint LocationDistrictId { get; init; }

    public string LocationDistrictName { get; set; } = null!;

    public bool? LocationDistrictStatus { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; init; } = [];

    public virtual ICollection<Employee> Employees { get; init; } = [];

    public virtual ICollection<Location> Locations { get; init; } = [];
}
