using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class CustomerAddress
{
    public uint CustomerAddressId { get; set; }

    public uint CustomerCityId { get; set; }

    public uint CustomerDistrictId { get; set; }

    public uint CustomerWardId { get; set; }

    public uint CustomerId { get; set; }

    public string CustomerStreet { get; set; } = null!;

    public string CustomerAddressNumber { get; set; } = null!;

    public byte CustomerAddressStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual LocationCity CustomerCity { get; set; } = null!;

    public virtual LocationDistrict CustomerDistrict { get; set; } = null!;

    public virtual LocationWard CustomerWard { get; set; } = null!;
}
