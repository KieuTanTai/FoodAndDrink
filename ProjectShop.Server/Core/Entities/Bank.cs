using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Bank
{
    public uint BankId { get; init; }

    public string BankName { get; set; } = null!;

    public bool? BankStatus { get; set; }

    public virtual ICollection<UserPaymentMethod> UserPaymentMethods { get; init; } = [];
}
