using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class LocationWard
{
    public uint LocationWardId { get; set; }

    public string LocationWardName { get; set; } = null!;

    public bool? LocationWardStatus { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
