using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using ProjectShop.Server.Core.Entities;
using ProjectShop.Server.Core.Interfaces.IContext;

namespace ProjectShop.Server.Core.Entities.Context;

public partial class FoodAndDrinkShopDbContext : DbContext, IFoodAndDrinkShopDbContext
{
    private IDbContextTransaction? _currentTransaction;

    public FoodAndDrinkShopDbContext()
    {
    }

    public FoodAndDrinkShopDbContext(DbContextOptions<FoodAndDrinkShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountAdditionalPermission> AccountAdditionalPermissions { get; set; }

    public virtual DbSet<AccountRole> AccountRoles { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<DetailCart> DetailCarts { get; set; }

    public virtual DbSet<DetailInventory> DetailInventories { get; set; }

    public virtual DbSet<DetailInventoryMovement> DetailInventoryMovements { get; set; }

    public virtual DbSet<DetailInvoice> DetailInvoices { get; set; }

    public virtual DbSet<DetailProductLot> DetailProductLots { get; set; }

    public virtual DbSet<DetailSaleEvent> DetailSaleEvents { get; set; }

    public virtual DbSet<DisposeProduct> DisposeProducts { get; set; }

    public virtual DbSet<DisposeReason> DisposeReasons { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<InventoryMovement> InventoryMovements { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationCity> LocationCities { get; set; }

    public virtual DbSet<LocationDistrict> LocationDistricts { get; set; }

    public virtual DbSet<LocationType> LocationTypes { get; set; }

    public virtual DbSet<LocationWard> LocationWards { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDrink> ProductDrinks { get; set; }

    public virtual DbSet<ProductFruit> ProductFruits { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductLot> ProductLots { get; set; }

    public virtual DbSet<ProductMeat> ProductMeats { get; set; }

    public virtual DbSet<ProductSnack> ProductSnacks { get; set; }

    public virtual DbSet<ProductVegetable> ProductVegetables { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    public virtual DbSet<SaleEvent> SaleEvents { get; set; }

    public virtual DbSet<SaleEventImage> SaleEventImages { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity
                .ToTable("account")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.AccountStatus, "idx_account_status");

            entity.HasIndex(e => e.UserName, "user_name").IsUnique();

            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.AccountCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("account_created_date");
            entity.Property(e => e.AccountLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("account_last_updated_date");
            entity.Property(e => e.AccountStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("account_status");
            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<AccountAdditionalPermission>(entity =>
        {
            entity.HasKey(e => e.AccountAdditionalPermissionId).HasName("PRIMARY");

            entity
                .ToTable("account_additional_permission")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.PermissionId, "permission_id");

            entity.HasIndex(e => new { e.AccountId, e.PermissionId }, "uq_account_additional_permission").IsUnique();

            entity.Property(e => e.AccountAdditionalPermissionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_additional_permission_id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.AdditionalPermissionAssignedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("additional_permission_assigned_date");
            entity.Property(e => e.AdditionalPermissionStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("additional_permission_status");
            entity.Property(e => e.IsGranted)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasComment("1=granted, 0=denied")
                .HasColumnName("is_granted");
            entity.Property(e => e.PermissionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("permission_id");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountAdditionalPermissions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_additional_permission_ibfk_1");

            entity.HasOne(d => d.Permission).WithMany(p => p.AccountAdditionalPermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_additional_permission_ibfk_2");
        });

        modelBuilder.Entity<AccountRole>(entity =>
        {
            entity.HasKey(e => e.AccountRoleId).HasName("PRIMARY");

            entity
                .ToTable("account_role")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.HasIndex(e => new { e.AccountId, e.RoleId }, "uq_account_role").IsUnique();

            entity.Property(e => e.AccountRoleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_role_id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.AccountRoleAssignedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("account_role_assigned_date");
            entity.Property(e => e.AccountRoleStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("account_role_status");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("role_id");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_role_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.AccountRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("account_role_ibfk_2");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.BankId).HasName("PRIMARY");

            entity
                .ToTable("bank")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.BankStatus, "idx_bank_status");

            entity.HasIndex(e => e.BankName, "uq_bank_name").IsUnique();

            entity.Property(e => e.BankId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("bank_id");
            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("bank_name");
            entity.Property(e => e.BankStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("bank_status");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PRIMARY");

            entity
                .ToTable("cart")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CustomerId, "idx_cart_customer_id");

            entity.Property(e => e.CartId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cart_id");
            entity.Property(e => e.CartTotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("cart_total_price");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_ibfk_1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity
                .ToTable("category")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CategoryStatus, "idx_category_status");

            entity.HasIndex(e => e.CategoryName, "uq_category_name").IsUnique();

            entity.Property(e => e.CategoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasDefaultValueSql("''")
                .HasColumnName("category_name");
            entity.Property(e => e.CategoryStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("category_status");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PRIMARY");

            entity
                .ToTable("country")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CountryCode, "uq_country_code").IsUnique();

            entity.HasIndex(e => e.CountryName, "uq_country_name").IsUnique();

            entity.Property(e => e.CountryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("country_id");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .HasDefaultValueSql("''")
                .HasColumnName("country_code");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("country_name");
            entity.Property(e => e.CountryStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("country_status");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity
                .ToTable("customer")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.PersonId, "person_id").IsUnique();

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerLoyaltyPoints)
                .HasPrecision(10, 2)
                .HasColumnName("customer_loyalty_points");
            entity.Property(e => e.CustomerRegistrationDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("customer_registration_date");
            entity.Property(e => e.PersonId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("person_id");

            entity.HasOne(d => d.Person).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_ibfk_1");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.CustomerAddressId).HasName("PRIMARY");

            entity
                .ToTable("customer_address")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CustomerCityId, "customer_city_id");

            entity.HasIndex(e => e.CustomerDistrictId, "customer_district_id");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.CustomerWardId, "customer_ward_id");

            entity.Property(e => e.CustomerAddressId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_address_id");
            entity.Property(e => e.CustomerAddressNumber)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("customer_address_number");
            entity.Property(e => e.CustomerAddressStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(1) unsigned")
                .HasColumnName("customer_address_status");
            entity.Property(e => e.CustomerCityId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_city_id");
            entity.Property(e => e.CustomerDistrictId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_district_id");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerStreet)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("customer_street");
            entity.Property(e => e.CustomerWardId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_ward_id");

            entity.HasOne(d => d.CustomerCity).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_address_ibfk_1");

            entity.HasOne(d => d.CustomerDistrict).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerDistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_address_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_address_ibfk_4");

            entity.HasOne(d => d.CustomerWard).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerWardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_address_ibfk_3");
        });

        modelBuilder.Entity<DetailCart>(entity =>
        {
            entity.HasKey(e => e.DetailCartId).HasName("PRIMARY");

            entity.ToTable("detail_cart");

            entity.HasIndex(e => e.CartId, "idx_detail_cart_cart_id");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_cart_product_barcode");

            entity.Property(e => e.DetailCartId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_cart_id");
            entity.Property(e => e.CartId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cart_id");
            entity.Property(e => e.DetailCartAddedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("detail_cart_added_date");
            entity.Property(e => e.DetailCartPrice)
                .HasPrecision(10, 2)
                .HasColumnName("detail_cart_price");
            entity.Property(e => e.DetailCartQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_cart_quantity");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");

            entity.HasOne(d => d.Cart).WithMany(p => p.DetailCarts)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_cart_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DetailCarts)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_cart_ibfk_2");
        });

        modelBuilder.Entity<DetailInventory>(entity =>
        {
            entity.HasKey(e => e.DetailInventoryId).HasName("PRIMARY");

            entity.ToTable("detail_inventory");

            entity.HasIndex(e => e.InventoryId, "idx_detail_inventory_inventory_id");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_inventory_product_barcode");

            entity.HasIndex(e => new { e.InventoryId, e.ProductBarcode }, "uq_inventory_product").IsUnique();

            entity.Property(e => e.DetailInventoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_inventory_id");
            entity.Property(e => e.DetailInventoryAddedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("detail_inventory_added_date");
            entity.Property(e => e.DetailInventoryLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("detail_inventory_last_updated_date");
            entity.Property(e => e.DetailInventoryQuantity)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_inventory_quantity");
            entity.Property(e => e.InventoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");

            entity.HasOne(d => d.Inventory).WithMany(p => p.DetailInventories)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_inventory_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DetailInventories)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_inventory_ibfk_2");
        });

        modelBuilder.Entity<DetailInventoryMovement>(entity =>
        {
            entity.HasKey(e => e.DetailInventoryMovementId).HasName("PRIMARY");

            entity.ToTable("detail_inventory_movement");

            entity.HasIndex(e => e.InventoryMovementId, "idx_detail_inventory_movement_inventory_movement_id");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_inventory_movement_product_barcode");

            entity.HasIndex(e => e.ProductLotId, "idx_product_lot_id");

            entity.Property(e => e.DetailInventoryMovementId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_inventory_movement_id");
            entity.Property(e => e.DetailInventoryMovementQuantity)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_inventory_movement_quantity");
            entity.Property(e => e.InventoryMovementId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_movement_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.ProductLotId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("product_lot_id");

            entity.HasOne(d => d.InventoryMovement).WithMany(p => p.DetailInventoryMovements)
                .HasForeignKey(d => d.InventoryMovementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_inventory_movement_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DetailInventoryMovements)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_inventory_movement_ibfk_2");

            entity.HasOne(d => d.ProductLot).WithMany(p => p.DetailInventoryMovements)
                .HasForeignKey(d => d.ProductLotId)
                .HasConstraintName("fk_detail_inventory_movement_product_lot");
        });

        modelBuilder.Entity<DetailInvoice>(entity =>
        {
            entity.HasKey(e => e.DetailInvoiceId).HasName("PRIMARY");

            entity.ToTable("detail_invoice");

            entity.HasIndex(e => e.InvoiceId, "idx_detail_invoice_invoice_id");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_invoice_product_barcode");

            entity.Property(e => e.DetailInvoiceId)
                .ValueGeneratedNever()
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_invoice_id");
            entity.Property(e => e.DetailInvoicePrice)
                .HasPrecision(10, 2)
                .HasColumnName("detail_invoice_price");
            entity.Property(e => e.DetailInvoiceQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_invoice_quantity");
            entity.Property(e => e.DetailInvoiceStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("detail_invoice_status");
            entity.Property(e => e.InvoiceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("invoice_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");

            entity.HasOne(d => d.Invoice).WithMany(p => p.DetailInvoices)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_invoice_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DetailInvoices)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_invoice_ibfk_2");
        });

        modelBuilder.Entity<DetailProductLot>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("detail_product_lot");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_product_lot_product_barcode");

            entity.HasIndex(e => e.ProductLotId, "product_lot_id");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.ProductLotExpDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_lot_exp_date");
            entity.Property(e => e.ProductLotId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("product_lot_id");
            entity.Property(e => e.ProductLotInitialQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("product_lot_initial_quantity");
            entity.Property(e => e.ProductLotMfgDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_lot_mfg_date");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany()
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_product_lot_ibfk_2");

            entity.HasOne(d => d.ProductLot).WithMany()
                .HasForeignKey(d => d.ProductLotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_product_lot_ibfk_1");
        });

        modelBuilder.Entity<DetailSaleEvent>(entity =>
        {
            entity.HasKey(e => e.DetailSaleEventId).HasName("PRIMARY");

            entity.ToTable("detail_sale_event");

            entity.HasIndex(e => e.ProductBarcode, "idx_detail_sale_event_product_barcode");

            entity.HasIndex(e => e.SaleEventId, "idx_detail_sale_event_sale_event_id");

            entity.Property(e => e.DetailSaleEventId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("detail_sale_event_id");
            entity.Property(e => e.DiscountAmount)
                .HasPrecision(10, 2)
                .HasColumnName("discount_amount");
            entity.Property(e => e.DiscountPercent)
                .HasPrecision(5, 2)
                .HasColumnName("discount_percent");
            entity.Property(e => e.DiscountType)
                .HasDefaultValueSql("'percent'")
                .HasColumnType("enum('percent','amount')")
                .HasColumnName("discount_type");
            entity.Property(e => e.MaxDiscountPrice)
                .HasPrecision(10, 2)
                .HasColumnName("max_discount_price");
            entity.Property(e => e.MinPriceToUse)
                .HasPrecision(10, 2)
                .HasColumnName("min_price_to_use");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.SaleEventId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("sale_event_id");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DetailSaleEvents)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_sale_event_ibfk_2");

            entity.HasOne(d => d.SaleEvent).WithMany(p => p.DetailSaleEvents)
                .HasForeignKey(d => d.SaleEventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detail_sale_event_ibfk_1");
        });

        modelBuilder.Entity<DisposeProduct>(entity =>
        {
            entity.HasKey(e => e.DisposeProductId).HasName("PRIMARY");

            entity.ToTable("dispose_product");

            entity.HasIndex(e => e.DisposeByEmployeeId, "idx_dispose_product_employee_id");

            entity.HasIndex(e => e.LocationId, "idx_dispose_product_location_id");

            entity.HasIndex(e => e.ProductBarcode, "idx_dispose_product_product_barcode");

            entity.HasIndex(e => e.DisposeReasonId, "idx_dispose_product_reason_id");

            entity.Property(e => e.DisposeProductId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dispose_product_id");
            entity.Property(e => e.DisposeByEmployeeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dispose_by_employee_id");
            entity.Property(e => e.DisposeQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dispose_quantity");
            entity.Property(e => e.DisposeReasonId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dispose_reason_id");
            entity.Property(e => e.DisposedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("disposed_date");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");

            entity.HasOne(d => d.DisposeByEmployee).WithMany(p => p.DisposeProducts)
                .HasForeignKey(d => d.DisposeByEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dispose_product_ibfk_3");

            entity.HasOne(d => d.DisposeReason).WithMany(p => p.DisposeProducts)
                .HasForeignKey(d => d.DisposeReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dispose_product_ibfk_4");

            entity.HasOne(d => d.Location).WithMany(p => p.DisposeProducts)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dispose_product_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.DisposeProducts)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("dispose_product_ibfk_2");
        });

        modelBuilder.Entity<DisposeReason>(entity =>
        {
            entity.HasKey(e => e.DisposeReasonId).HasName("PRIMARY");

            entity
                .ToTable("dispose_reason")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.Property(e => e.DisposeReasonId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("dispose_reason_id");
            entity.Property(e => e.DisposeReasonName)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasColumnName("dispose_reason_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity
                .ToTable("employee")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.EmployeeCityId, "employee_city_id");

            entity.HasIndex(e => e.EmployeeDistrictId, "employee_district_id");

            entity.HasIndex(e => e.EmployeeWardId, "employee_ward_id");

            entity.HasIndex(e => e.LocationId, "location_id");

            entity.HasIndex(e => e.PersonId, "person_id").IsUnique();

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("employee_id");
            entity.Property(e => e.EmployeeCityId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("employee_city_id");
            entity.Property(e => e.EmployeeDistrictId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("employee_district_id");
            entity.Property(e => e.EmployeeHireDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("employee_hire_date");
            entity.Property(e => e.EmployeeHouseNumber)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("employee_house_number");
            entity.Property(e => e.EmployeeSalary)
                .HasPrecision(10, 2)
                .HasColumnName("employee_salary");
            entity.Property(e => e.EmployeeStreet)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("employee_street");
            entity.Property(e => e.EmployeeWardId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("employee_ward_id");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_id");
            entity.Property(e => e.PersonId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("person_id");

            entity.HasOne(d => d.EmployeeCity).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeCityId)
                .HasConstraintName("employee_ibfk_5");

            entity.HasOne(d => d.EmployeeDistrict).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeDistrictId)
                .HasConstraintName("employee_ibfk_3");

            entity.HasOne(d => d.EmployeeWard).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeWardId)
                .HasConstraintName("employee_ibfk_2");

            entity.HasOne(d => d.Location).WithMany(p => p.Employees)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_4");

            entity.HasOne(d => d.Person).WithOne(p => p.Employee)
                .HasForeignKey<Employee>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_1");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PRIMARY");

            entity
                .ToTable("inventory")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationId, "idx_inventory_location_id");

            entity.HasIndex(e => e.InventoryStatus, "idx_inventory_status");

            entity.Property(e => e.InventoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_id");
            entity.Property(e => e.InventoryLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("inventory_last_updated_date");
            entity.Property(e => e.InventoryStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("inventory_status");
            entity.Property(e => e.LocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_id");

            entity.HasOne(d => d.Location).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_ibfk_1");
        });

        modelBuilder.Entity<InventoryMovement>(entity =>
        {
            entity.HasKey(e => e.InventoryMovementId).HasName("PRIMARY");

            entity
                .ToTable("inventory_movement")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.DestinationLocationId, "idx_inventory_movement_destination_location_id");

            entity.HasIndex(e => e.SourceLocationId, "idx_inventory_movement_source_location_id");

            entity.Property(e => e.InventoryMovementId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_movement_id");
            entity.Property(e => e.DestinationLocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("destination_location_id");
            entity.Property(e => e.InventoryMovementDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("inventory_movement_date");
            entity.Property(e => e.InventoryMovementQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_movement_quantity");
            entity.Property(e => e.InventoryMovementReason)
                .HasDefaultValueSql("'warehouse_to_store'")
                .HasColumnType("enum('warehouse_to_store','store_to_warehouse','warehouse_to_warehouse','store_to_store','supplier_to_warehouse','other')")
                .HasColumnName("inventory_movement_reason");
            entity.Property(e => e.SourceLocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("source_location_id");

            entity.HasOne(d => d.DestinationLocation).WithMany(p => p.InventoryMovementDestinationLocations)
                .HasForeignKey(d => d.DestinationLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_movement_ibfk_3");

            entity.HasOne(d => d.SourceLocation).WithMany(p => p.InventoryMovementSourceLocations)
                .HasForeignKey(d => d.SourceLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("inventory_movement_ibfk_1");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PRIMARY");

            entity
                .ToTable("invoice")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CustomerId, "idx_invoice_customer_id");

            entity.HasIndex(e => e.EmployeeId, "idx_invoice_employee_id");

            entity.HasIndex(e => e.InvoiceStatus, "idx_invoice_status");

            entity.HasIndex(e => e.PaymentMethodId, "payment_method_id");

            entity.Property(e => e.InvoiceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("invoice_id");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("employee_id");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("invoice_status");
            entity.Property(e => e.InvoiceTotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("invoice_total_price");
            entity.Property(e => e.PaymentMethodId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentType)
                .HasDefaultValueSql("'cod'")
                .HasColumnType("enum('cod','prepaid')")
                .HasColumnName("payment_type");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_ibfk_1");

            entity.HasOne(d => d.Employee).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoice_ibfk_2");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("invoice_ibfk_3");

            entity.HasMany(d => d.SaleEvents).WithMany(p => p.Invoices)
                .UsingEntity<Dictionary<string, object>>(
                    "InvoiceDiscount",
                    r => r.HasOne<SaleEvent>().WithMany()
                        .HasForeignKey("SaleEventId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("invoice_discount_ibfk_2"),
                    l => l.HasOne<Invoice>().WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("invoice_discount_ibfk_1"),
                    j =>
                    {
                        j.HasKey("InvoiceId", "SaleEventId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("invoice_discount")
                            .UseCollation("utf8mb4_uca1400_ai_ci");
                        j.HasIndex(new[] { "SaleEventId" }, "idx_invoice_discount_sale_event_id");
                        j.IndexerProperty<uint>("InvoiceId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("invoice_id");
                        j.IndexerProperty<uint>("SaleEventId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("sale_event_id");
                    });
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PRIMARY");

            entity
                .ToTable("location")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationCityId, "idx_location_city_id");

            entity.HasIndex(e => e.LocationDistrictId, "idx_location_district_id");

            entity.HasIndex(e => e.LocationStatus, "idx_location_status");

            entity.HasIndex(e => e.LocationTypeId, "idx_location_type_id");

            entity.HasIndex(e => e.LocationWardId, "idx_location_ward_id");

            entity.HasIndex(e => e.LocationEmail, "uq_location_email").IsUnique();

            entity.HasIndex(e => e.LocationPhone, "uq_location_phone").IsUnique();

            entity.Property(e => e.LocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_id");
            entity.Property(e => e.LocationCityId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_city_id");
            entity.Property(e => e.LocationDistrictId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_district_id");
            entity.Property(e => e.LocationEmail)
                .HasMaxLength(60)
                .HasDefaultValueSql("''")
                .HasColumnName("location_email");
            entity.Property(e => e.LocationHouseNumber)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("location_house_number");
            entity.Property(e => e.LocationName)
                .HasMaxLength(120)
                .HasDefaultValueSql("''")
                .HasColumnName("location_name");
            entity.Property(e => e.LocationPhone)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("location_phone");
            entity.Property(e => e.LocationStatus)
                .HasDefaultValueSql("'1'")
                .HasColumnName("location_status");
            entity.Property(e => e.LocationStreet)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("location_street");
            entity.Property(e => e.LocationTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_type_id");
            entity.Property(e => e.LocationWardId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_ward_id");

            entity.HasOne(d => d.LocationCity).WithMany(p => p.Locations)
                .HasForeignKey(d => d.LocationCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_ibfk_4");

            entity.HasOne(d => d.LocationDistrict).WithMany(p => p.Locations)
                .HasForeignKey(d => d.LocationDistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_ibfk_3");

            entity.HasOne(d => d.LocationType).WithMany(p => p.Locations)
                .HasForeignKey(d => d.LocationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_ibfk_1");

            entity.HasOne(d => d.LocationWard).WithMany(p => p.Locations)
                .HasForeignKey(d => d.LocationWardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("location_ibfk_2");
        });

        modelBuilder.Entity<LocationCity>(entity =>
        {
            entity.HasKey(e => e.LocationCityId).HasName("PRIMARY");

            entity
                .ToTable("location_city")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationCityStatus, "idx_location_city_status");

            entity.Property(e => e.LocationCityId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_city_id");
            entity.Property(e => e.LocationCityName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("location_city_name");
            entity.Property(e => e.LocationCityStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("location_city_status");
        });

        modelBuilder.Entity<LocationDistrict>(entity =>
        {
            entity.HasKey(e => e.LocationDistrictId).HasName("PRIMARY");

            entity
                .ToTable("location_district")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationDistrictStatus, "idx_location_district_status");

            entity.Property(e => e.LocationDistrictId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_district_id");
            entity.Property(e => e.LocationDistrictName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("location_district_name");
            entity.Property(e => e.LocationDistrictStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("location_district_status");
        });

        modelBuilder.Entity<LocationType>(entity =>
        {
            entity.HasKey(e => e.LocationTypeId).HasName("PRIMARY");

            entity
                .ToTable("location_type")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationTypeStatus, "idx_location_type_status");

            entity.HasIndex(e => e.LocationTypeName, "uq_location_type_name").IsUnique();

            entity.Property(e => e.LocationTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_type_id");
            entity.Property(e => e.LocationTypeName)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("location_type_name");
            entity.Property(e => e.LocationTypeStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("location_type_status");
        });

        modelBuilder.Entity<LocationWard>(entity =>
        {
            entity.HasKey(e => e.LocationWardId).HasName("PRIMARY");

            entity
                .ToTable("location_ward")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.LocationWardStatus, "idx_location_ward_status");

            entity.Property(e => e.LocationWardId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("location_ward_id");
            entity.Property(e => e.LocationWardName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("location_ward_name");
            entity.Property(e => e.LocationWardStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("location_ward_status");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity
                .ToTable("permission")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.PermissionName, "uq_permission_name").IsUnique();

            entity.Property(e => e.PermissionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("permission_id");
            entity.Property(e => e.PermissionCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("permission_created_date");
            entity.Property(e => e.PermissionDescription)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("permission_description");
            entity.Property(e => e.PermissionLastUpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("permission_last_updated_date");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
            entity.Property(e => e.PermissionStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("permission_status");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity
                .ToTable("person")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.AccountId, "account_id").IsUnique();

            entity.Property(e => e.PersonId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("person_id");
            entity.Property(e => e.AccountId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("account_id");
            entity.Property(e => e.PersonAvatarUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("person_avatar_url");
            entity.Property(e => e.PersonBirthday)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnName("person_birthday");
            entity.Property(e => e.PersonCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("person_created_date");
            entity.Property(e => e.PersonEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("person_email");
            entity.Property(e => e.PersonGender)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("person_gender");
            entity.Property(e => e.PersonLastUpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("person_last_updated_date");
            entity.Property(e => e.PersonName)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("person_name");
            entity.Property(e => e.PersonPhone)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("person_phone");
            entity.Property(e => e.PersonStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("person_status");

            entity.HasOne(d => d.Account).WithOne(p => p.Person)
                .HasForeignKey<Person>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("person_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CountryId, "country_id");

            entity.HasIndex(e => e.SupplierId, "supplier_id");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.CountryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("country_id");
            entity.Property(e => e.ProductAddedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_added_date");
            entity.Property(e => e.ProductBasePrice)
                .HasPrecision(10, 2)
                .HasColumnName("product_base_price");
            entity.Property(e => e.ProductBrand)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("product_brand");
            entity.Property(e => e.ProductLastUpdatedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_last_updated_date");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("product_name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ProductNetWeight)
                .HasPrecision(10, 2)
                .HasColumnName("product_net_weight");
            entity.Property(e => e.ProductRatingAge)
                .HasMaxLength(3)
                .HasDefaultValueSql("'0'")
                .HasColumnName("product_rating_age");
            entity.Property(e => e.ProductStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("product_status");
            entity.Property(e => e.ProductType)
                .HasDefaultValueSql("'drink'")
                .HasColumnType("enum('meat','drink','snack','vegetable','fruit')")
                .HasColumnName("product_type");
            entity.Property(e => e.ProductUnit)
                .HasDefaultValueSql("'gram'")
                .HasColumnType("enum('kg','gram','lb','oz','ml','liter','box','packet','bottle','set')")
                .HasColumnName("product_unit");
            entity.Property(e => e.SupplierId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("supplier_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Products)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_2");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_1");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product_categories");

            entity.HasIndex(e => e.ProductBarcode, "product_barcode");

            entity.HasIndex(e => new { e.CategoryId, e.ProductBarcode }, "uniq_category_product").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("category_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_categories_ibfk_1");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_categories_ibfk_2");
        });

        modelBuilder.Entity<ProductDrink>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product_drink");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.AlcoholContent)
                .HasPrecision(5, 2)
                .HasComment("Percentage")
                .HasColumnName("alcohol_content");
            entity.Property(e => e.BeverageType)
                .HasDefaultValueSql("'other'")
                .HasColumnType("enum('soft_drink','juice','tea','coffee','energy_drink','sports_drink','kombucha','infused_water','protein_shake','alcoholic','wine','beer','water','milk','plant_milk','yogurt_drink','smoothie','syrup','other')")
                .HasColumnName("beverage_type");
            entity.Property(e => e.VolumeMl)
                .HasPrecision(10, 2)
                .HasColumnName("volume_ml");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithOne(p => p.ProductDrink)
                .HasForeignKey<ProductDrink>(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_drink_ibfk_1");
        });

        modelBuilder.Entity<ProductFruit>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product_fruit");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.FruitType)
                .HasDefaultValueSql("'fresh'")
                .HasColumnType("enum('fresh','dried_fruit','canned_fruit','other')")
                .HasColumnName("fruit_type");
            entity.Property(e => e.IsOrganic).HasColumnName("is_organic");
            entity.Property(e => e.Season)
                .HasDefaultValueSql("'year_round'")
                .HasColumnType("enum('spring','summer','autumn','winter','year_round')")
                .HasColumnName("season");
            entity.Property(e => e.SweetnessLevel)
                .HasDefaultValueSql("'medium'")
                .HasColumnType("enum('very_low','low','medium','high','very_high')")
                .HasColumnName("sweetness_level");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithOne(p => p.ProductFruit)
                .HasForeignKey<ProductFruit>(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fruit_ibfk_1");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PRIMARY");

            entity.ToTable("product_image");

            entity.HasIndex(e => e.ProductBarcode, "idx_product_image_product_barcode");

            entity.Property(e => e.ProductImageId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("product_image_id");
            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.ProductImageCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_image_created_date");
            entity.Property(e => e.ProductImageLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_image_last_updated_date");
            entity.Property(e => e.ProductImagePriority)
                .HasColumnType("tinyint(10)")
                .HasColumnName("product_image_priority");
            entity.Property(e => e.ProductImageUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("product_image_url");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_image_ibfk_1");
        });

        modelBuilder.Entity<ProductLot>(entity =>
        {
            entity.HasKey(e => e.ProductLotId).HasName("PRIMARY");

            entity
                .ToTable("product_lot")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.InventoryId, "idx_product_lot_inventory_id");

            entity.Property(e => e.ProductLotId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("product_lot_id");
            entity.Property(e => e.InventoryId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("inventory_id");
            entity.Property(e => e.ProductLotCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("product_lot_created_date");

            entity.HasOne(d => d.Inventory).WithMany(p => p.ProductLots)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_lot_ibfk_1");
        });

        modelBuilder.Entity<ProductMeat>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product_meat");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.Grade)
                .HasDefaultValueSql("'standard'")
                .HasColumnType("enum('premium','standard','economy')")
                .HasColumnName("grade");
            entity.Property(e => e.IsFrozen).HasColumnName("is_frozen");
            entity.Property(e => e.MeatType)
                .HasDefaultValueSql("'other'")
                .HasColumnType("enum('beef','pork','chicken','fish','lamb','duck','shrimp','crab','goat','turkey','rabbit','shellfish','seafood','other')")
                .HasColumnName("meat_type");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithOne(p => p.ProductMeat)
                .HasForeignKey<ProductMeat>(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_meat_ibfk_1");
        });

        modelBuilder.Entity<ProductSnack>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product_snack");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.Flavor)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("flavor");
            entity.Property(e => e.IsGlutenFree).HasColumnName("is_gluten_free");
            entity.Property(e => e.SnackType)
                .HasDefaultValueSql("'other'")
                .HasColumnType("enum('chips','cookies','nuts','candy','chocolate','crackers','ice_cream','cheese','butter','yogurt','noodles','porridge','popcorn','dried_fruit','seaweed','rice_crackers','granola_bar','pudding','jelly','waffle','other')")
                .HasColumnName("snack_type");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithOne(p => p.ProductSnack)
                .HasForeignKey<ProductSnack>(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_snack_ibfk_1");
        });

        modelBuilder.Entity<ProductVegetable>(entity =>
        {
            entity.HasKey(e => e.ProductBarcode).HasName("PRIMARY");

            entity.ToTable("product_vegetable");

            entity.Property(e => e.ProductBarcode)
                .HasMaxLength(20)
                .HasColumnName("product_barcode");
            entity.Property(e => e.HarvestSeason)
                .HasDefaultValueSql("'year_round'")
                .HasColumnType("enum('spring','summer','autumn','winter','year_round')")
                .HasColumnName("harvest_season");
            entity.Property(e => e.IsOrganic).HasColumnName("is_organic");
            entity.Property(e => e.StorageMethod)
                .HasDefaultValueSql("'room_temperature'")
                .HasColumnType("enum('refrigerated','frozen','room_temperature','dry_storage')")
                .HasColumnName("storage_method");
            entity.Property(e => e.VegetableType)
                .HasDefaultValueSql("'other'")
                .HasColumnType("enum('leafy','root','tuber','fruit_vegetable','bulb','stem','flower','sprout','fungi','other')")
                .HasColumnName("vegetable_type");

            entity.HasOne(d => d.ProductBarcodeNavigation).WithOne(p => p.ProductVegetable)
                .HasForeignKey<ProductVegetable>(d => d.ProductBarcode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_vegetable_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity
                .ToTable("role")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.RoleStatus, "idx_role_status");

            entity.HasIndex(e => e.RoleName, "uq_role_name").IsUnique();

            entity.Property(e => e.RoleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("role_created_date");
            entity.Property(e => e.RoleLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("role_last_updated_date");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .HasColumnName("role_name");
            entity.Property(e => e.RoleStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("role_status");
        });

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.RolePermissionId).HasName("PRIMARY");

            entity
                .ToTable("role_permission")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.PermissionId, "permission_id");

            entity.HasIndex(e => new { e.RoleId, e.PermissionId }, "uq_role_permission").IsUnique();

            entity.Property(e => e.RolePermissionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("role_permission_id");
            entity.Property(e => e.PermissionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("permission_id");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("role_id");
            entity.Property(e => e.RolePermissionCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("role_permission_created_date");

            entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_ibfk_2");

            entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_permission_ibfk_1");
        });

        modelBuilder.Entity<SaleEvent>(entity =>
        {
            entity.HasKey(e => e.SaleEventId).HasName("PRIMARY");

            entity
                .ToTable("sale_event")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.SaleEventDiscountCode, "idx_sale_event_discount_code").IsUnique();

            entity.HasIndex(e => e.SaleEventStatus, "idx_sale_event_status");

            entity.Property(e => e.SaleEventId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("sale_event_id");
            entity.Property(e => e.SaleEventDescription)
                .HasDefaultValueSql("''")
                .HasColumnType("text")
                .HasColumnName("sale_event_description");
            entity.Property(e => e.SaleEventDiscountCode)
                .HasMaxLength(20)
                .HasColumnName("sale_event_discount_code");
            entity.Property(e => e.SaleEventEndDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("sale_event_end_date");
            entity.Property(e => e.SaleEventName)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("sale_event_name");
            entity.Property(e => e.SaleEventStartDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("sale_event_start_date");
            entity.Property(e => e.SaleEventStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("sale_event_status");
        });

        modelBuilder.Entity<SaleEventImage>(entity =>
        {
            entity.HasKey(e => e.SaleEventImageId).HasName("PRIMARY");

            entity
                .ToTable("sale_event_image")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.SaleEventId, "idx_sale_event_image_sale_event_id");

            entity.Property(e => e.SaleEventImageId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("sale_event_image_id");
            entity.Property(e => e.SaleEventId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("sale_event_id");
            entity.Property(e => e.SaleEventImageCreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("sale_event_image_created_date");
            entity.Property(e => e.SaleEventImageLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("sale_event_image_last_updated_date");
            entity.Property(e => e.SaleEventImageUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("sale_event_image_url");

            entity.HasOne(d => d.SaleEvent).WithMany(p => p.SaleEventImages)
                .HasForeignKey(d => d.SaleEventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_event_image_ibfk_1");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity
                .ToTable("supplier")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.CompanyLocationId, "idx_supplier_company_location_id");

            entity.HasIndex(e => e.SupplierStatus, "idx_supplier_status");

            entity.HasIndex(e => e.StoreLocationId, "idx_supplier_store_city_id");

            entity.HasIndex(e => e.SupplierEmail, "uq_supplier_email").IsUnique();

            entity.HasIndex(e => e.SupplierPhone, "uq_supplier_phone").IsUnique();

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("supplier_id");
            entity.Property(e => e.CompanyLocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("company_location_id");
            entity.Property(e => e.StoreLocationId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("store_location_id");
            entity.Property(e => e.SupplierCooperationDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("supplier_cooperation_date");
            entity.Property(e => e.SupplierEmail)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("supplier_email");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .HasDefaultValueSql("''")
                .HasColumnName("supplier_name");
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("supplier_phone");
            entity.Property(e => e.SupplierStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("supplier_status");

            entity.HasOne(d => d.CompanyLocation).WithMany(p => p.SupplierCompanyLocations)
                .HasForeignKey(d => d.CompanyLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_ibfk_1");

            entity.HasOne(d => d.StoreLocation).WithMany(p => p.SupplierStoreLocations)
                .HasForeignKey(d => d.StoreLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("supplier_ibfk_2");
        });

        modelBuilder.Entity<UserPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.UserPaymentMethodId).HasName("PRIMARY");

            entity
                .ToTable("user_payment_method")
                .UseCollation("utf8mb4_uca1400_ai_ci");

            entity.HasIndex(e => e.BankId, "bank_id");

            entity.HasIndex(e => e.CustomerId, "idx_user_payment_method_customer_id");

            entity.HasIndex(e => e.PaymentMethodStatus, "idx_user_payment_method_status");

            entity.HasIndex(e => new { e.CustomerId, e.PaymentMethodDisplayName }, "uq_customer_display_name").IsUnique();

            entity.HasIndex(e => e.PaymentMethodToken, "uq_payment_method_token").IsUnique();

            entity.Property(e => e.UserPaymentMethodId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("user_payment_method_id");
            entity.Property(e => e.BankId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("bank_id");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("customer_id");
            entity.Property(e => e.PaymentMethodAddedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("payment_method_added_date");
            entity.Property(e => e.PaymentMethodDisplayName)
                .HasMaxLength(50)
                .HasDefaultValueSql("''")
                .HasColumnName("payment_method_display_name");
            entity.Property(e => e.PaymentMethodExpiryMonth)
                .HasColumnType("int(11)")
                .HasColumnName("payment_method_expiry_month");
            entity.Property(e => e.PaymentMethodExpiryYear)
                .HasColumnType("int(11)")
                .HasColumnName("payment_method_expiry_year");
            entity.Property(e => e.PaymentMethodLastFourDigit)
                .HasMaxLength(4)
                .HasDefaultValueSql("''")
                .HasColumnName("payment_method_last_four_digit");
            entity.Property(e => e.PaymentMethodLastUpdatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("payment_method_last_updated_date");
            entity.Property(e => e.PaymentMethodStatus)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("payment_method_status");
            entity.Property(e => e.PaymentMethodToken)
                .HasDefaultValueSql("''")
                .HasColumnName("payment_method_token");
            entity.Property(e => e.PaymentMethodType)
                .HasColumnType("enum('visa_or_mastercard','banking','momo')")
                .HasColumnName("payment_method_type");

            entity.HasOne(d => d.Bank).WithMany(p => p.UserPaymentMethods)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("user_payment_method_ibfk_2");

            entity.HasOne(d => d.Customer).WithMany(p => p.UserPaymentMethods)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_payment_method_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    #region Transaction Management

    public IDbContextTransaction BeginTransaction()
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = Database.BeginTransaction();
        return _currentTransaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
        return _currentTransaction;
    }

    public IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = Database.BeginTransaction(isolationLevel);
        return _currentTransaction;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _currentTransaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return _currentTransaction;
    }

    public void CommitTransaction()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress to commit.");
        }

        try
        {
            _currentTransaction.Commit();
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress to commit.");
        }

        try
        {
            await _currentTransaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress to rollback.");
        }

        try
        {
            _currentTransaction.Rollback();
        }
        finally
        {
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            throw new InvalidOperationException("No transaction in progress to rollback.");
        }

        try
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public override void Dispose()
    {
        _currentTransaction?.Dispose();
        base.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
        }
        await base.DisposeAsync();
    }

    #endregion
}
