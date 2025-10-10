using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Customer
{
    public uint CustomerId { get; set; }

    public uint PersonId { get; set; }

    public decimal CustomerLoyaltyPoints { get; set; }

    public DateTime CustomerRegistrationDate { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; set; } = new List<UserPaymentMethod>();
}
