using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class LocationCity
{
    public uint LocationCityId { get; set; }

    public string LocationCityName { get; set; } = null!;

    public bool? LocationCityStatus { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
