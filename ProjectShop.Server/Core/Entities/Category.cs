using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Category
{
    public uint CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool? CategoryStatus { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
}
