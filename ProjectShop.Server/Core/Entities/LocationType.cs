using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class LocationType
{
    public uint LocationTypeId { get; set; }

    public string LocationTypeName { get; set; } = null!;

    public bool? LocationTypeStatus { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
}
