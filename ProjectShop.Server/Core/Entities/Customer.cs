using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Customer
{
    public uint CustomerId { get; init; }

    public uint PersonId { get; private set; }

    public decimal CustomerLoyaltyPoints { get; set; }

    public DateTime CustomerRegistrationDate { get; init; }

    public virtual ICollection<Cart> Carts { get; set; } = [];

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = [];

    public virtual ICollection<Invoice> Invoices { get; set; } = [];

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; set; } = [];
}
