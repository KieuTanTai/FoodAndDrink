using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Invoice
{
    public uint InvoiceId { get; init; }

    public uint CustomerId { get; private set; }

    public uint EmployeeId { get; private set; }

    public string PaymentType { get; set; } = null!;

    public uint? PaymentMethodId { get; set; }

    public decimal InvoiceTotalPrice { get; set; }

    public DateTime InvoiceDate { get; init; }

    public bool? InvoiceStatus { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<DetailInvoice> DetailInvoices { get; set; } = [];

    public virtual Employee Employee { get; set; } = null!;

    public virtual UserPaymentMethod? PaymentMethod { get; set; }

    public virtual ICollection<SaleEvent> SaleEvents { get; set; } = [];
}
