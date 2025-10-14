using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Customer
{
    public uint CustomerId { get; init; }

    public uint PersonId { get; private set; }

    public decimal CustomerLoyaltyPoints { get; set; }

    public DateTime CustomerRegistrationDate { get; init; }

    public virtual ICollection<Cart> Carts { get; init; } = [];

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; init; } = [];

    public virtual ICollection<Invoice> Invoices { get; init; } = [];

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; init; } = [];
}
