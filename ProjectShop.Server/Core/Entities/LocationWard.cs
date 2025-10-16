using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class LocationWard
{
    public uint LocationWardId { get; init; }

    public string LocationWardName { get; set; } = null!;

    public bool? LocationWardStatus { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = [];

    public virtual ICollection<Employee> Employees { get; set; } = [];

    public virtual ICollection<Location> Locations { get; set; } = [];
}
