using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Location
{
    public uint LocationId { get; init; }

    public uint LocationTypeId { get; private set; }

    public string LocationHouseNumber { get; set; } = null!;

    public string LocationStreet { get; set; } = null!;

    public uint LocationWardId { get; private set; }

    public uint LocationDistrictId { get; private set; }

    public uint LocationCityId { get; private set; }

    public string LocationPhone { get; set; } = null!;

    public string LocationEmail { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public bool? LocationStatus { get; set; }

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = [];

    public virtual ICollection<Employee> Employees { get; set; } = [];

    public virtual ICollection<Inventory> Inventories { get; set; } = [];

    public virtual ICollection<InventoryMovement> InventoryMovementDestinationLocations { get; set; } = [];

    public virtual ICollection<InventoryMovement> InventoryMovementSourceLocations { get; set; } = [];

    public virtual LocationCity LocationCity { get; set; } = null!;

    public virtual LocationDistrict LocationDistrict { get; set; } = null!;

    public virtual LocationType LocationType { get; set; } = null!;

    public virtual LocationWard LocationWard { get; set; } = null!;

    public virtual ICollection<Supplier> SupplierCompanyLocations { get; set; } = [];

    public virtual ICollection<Supplier> SupplierStoreLocations { get; set; } = [];
}
