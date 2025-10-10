using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Cart
{
    public uint CartId { get; set; }

    public uint CustomerId { get; set; }

    public decimal CartTotalPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<DetailCart> DetailCarts { get; set; } = new List<DetailCart>();
}
