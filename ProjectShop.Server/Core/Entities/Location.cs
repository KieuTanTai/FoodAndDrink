using System;
using System.Collections.Generic;

namespace ProjectShop.Server.Core.Entities;

public partial class Location
{
    public uint LocationId { get; set; }

    public uint LocationTypeId { get; set; }

    public string LocationHouseNumber { get; set; } = null!;

    public string LocationStreet { get; set; } = null!;

    public uint LocationWardId { get; set; }

    public uint LocationDistrictId { get; set; }

    public uint LocationCityId { get; set; }

    public string LocationPhone { get; set; } = null!;

    public string LocationEmail { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public bool? LocationStatus { get; set; }

    public virtual ICollection<DisposeProduct> DisposeProducts { get; set; } = new List<DisposeProduct>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public virtual ICollection<InventoryMovement> InventoryMovementDestinationLocations { get; set; } = new List<InventoryMovement>();

    public virtual ICollection<InventoryMovement> InventoryMovementSourceLocations { get; set; } = new List<InventoryMovement>();

    public virtual LocationCity LocationCity { get; set; } = null!;

    public virtual LocationDistrict LocationDistrict { get; set; } = null!;

    public virtual LocationType LocationType { get; set; } = null!;

    public virtual LocationWard LocationWard { get; set; } = null!;

    public virtual ICollection<Supplier> SupplierCompanyLocations { get; set; } = new List<Supplier>();

    public virtual ICollection<Supplier> SupplierStoreLocations { get; set; } = new List<Supplier>();
}
