using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Person
{
    public uint PersonId { get; set; }

    public uint AccountId { get; set; }

    public DateOnly PersonBirthday { get; set; }

    public string PersonPhone { get; set; } = null!;

    public string PersonName { get; set; } = null!;

    public string PersonEmail { get; set; } = null!;

    public string PersonAvatarUrl { get; set; } = null!;

    public bool? PersonGender { get; set; }

    public bool? PersonStatus { get; set; }

    public DateTime PersonCreatedDate { get; set; }

    public DateTime PersonLastUpdatedDate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
