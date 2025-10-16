using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Country
{
    public uint CountryId { get; init; }

    public string CountryName { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool? CountryStatus { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}
