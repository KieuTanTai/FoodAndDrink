using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class UserPaymentMethod
{
    public uint UserPaymentMethodId { get; init; }

    public string PaymentMethodType { get; set; } = null!;

    public uint? BankId { get; private set; }

    public uint CustomerId { get; private set; }

    public DateTime PaymentMethodAddedDate { get; init; }

    public DateTime PaymentMethodLastUpdatedDate { get; set; }

    public bool? PaymentMethodStatus { get; set; }

    public string PaymentMethodDisplayName { get; set; } = null!;

    public string PaymentMethodLastFourDigit { get; set; } = null!;

    public int PaymentMethodExpiryYear { get; set; }

    public int PaymentMethodExpiryMonth { get; set; }

    public string PaymentMethodToken { get; set; } = null!;

    public virtual Bank? Bank { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; init; } = [];
}
