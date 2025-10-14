using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class CustomerAddress
{
    public uint CustomerAddressId { get; init; }

    public uint CustomerCityId { get; private set; }

    public uint CustomerDistrictId { get; private set; }

    public uint CustomerWardId { get; private set; }

    public uint CustomerId { get; private set; }

    public string CustomerStreet { get; set; } = null!;

    public string CustomerAddressNumber { get; set; } = null!;

    public byte CustomerAddressStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual LocationCity CustomerCity { get; set; } = null!;

    public virtual LocationDistrict CustomerDistrict { get; set; } = null!;

    public virtual LocationWard CustomerWard { get; set; } = null!;
}
