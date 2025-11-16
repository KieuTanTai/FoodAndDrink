using Microsoft.EntityFrameworkCore;
using ProjectShop.Server.Core.Entities;

namespace ProjectShop.Server.Core.Interfaces.IContext;

public interface IFoodAndDrinkShopDbContext : IDbContextBase
{
    DbSet<Account> Accounts { get; set; }
    DbSet<AccountRole> AccountRoles { get; set; }
    DbSet<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }
    DbSet<Permission> Permissions { get; set; }

    DbSet<Person> People { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<CustomerAddress> CustomerAddresses { get; set; }
    DbSet<Employee> Employees { get; set; }

    DbSet<Product> Products { get; set; }
    DbSet<ProductCategory> ProductCategories { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<ProductLot> ProductLots { get; set; }
    DbSet<DetailProductLot> DetailProductLots { get; set; }

    DbSet<ProductDrink> ProductDrinks { get; set; }
    DbSet<ProductFruit> ProductFruits { get; set; }
    DbSet<ProductMeat> ProductMeats { get; set; }
    DbSet<ProductSnack> ProductSnacks { get; set; }
    DbSet<ProductVegetable> ProductVegetables { get; set; }

    DbSet<Inventory> Inventories { get; set; }
    DbSet<DetailInventory> DetailInventories { get; set; }
    DbSet<InventoryMovement> InventoryMovements { get; set; }
    DbSet<DetailInventoryMovement> DetailInventoryMovements { get; set; }

    DbSet<Invoice> Invoices { get; set; }
    DbSet<DetailInvoice> DetailInvoices { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<DetailCart> DetailCarts { get; set; }
    DbSet<SaleEvent> SaleEvents { get; set; }
    DbSet<SaleEventImage> SaleEventImages { get; set; }
    DbSet<DetailSaleEvent> DetailSaleEvents { get; set; }

    DbSet<DisposeProduct> DisposeProducts { get; set; }
    DbSet<DisposeReason> DisposeReasons { get; set; }

    DbSet<Location> Locations { get; set; }
    DbSet<LocationCity> LocationCities { get; set; }
    DbSet<LocationDistrict> LocationDistricts { get; set; }
    DbSet<LocationWard> LocationWards { get; set; }
    DbSet<LocationType> LocationTypes { get; set; }

    DbSet<Bank> Banks { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Country> Countries { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }
}