using IMS.Domain.Identity;
using IMS.Domain.Models;
using IMS.Domain.Relationship;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace IMS.Repository;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Warehouse> Warehouses { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Shipment> Shipments { get; set; }
    public virtual DbSet<OrderProducts> OrderProducts { get; set; }
    public virtual DbSet<CartProducts> CartProducts { get; set; }
    public virtual DbSet<WarehouseProducts> WarehouseProducts { get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Account>(entity => { entity.ToTable("Accounts"); });
        builder.Entity<Customer>(entity => { entity.ToTable("Customers"); });
        builder.Entity<Admin>(entity => { entity.ToTable("Admins"); });

        // <-- Product -->

        builder.Entity<Product>()
            .HasOne(x => x.ProductSupplier)
            .WithMany(x => x.SupplierProducts)
            .HasForeignKey(x => x.ProductSupplierId)
            .IsRequired(false);

        builder.Entity<Product>()
            .HasOne(x => x.ProductCategory)
            .WithMany(x => x.CategoryProducts)
            .HasForeignKey(x => x.ProductCategoryId)
            .IsRequired(false);

        // <-- Order -->

        builder.Entity<Order>()
            .HasOne(x => x.Customer)
            .WithMany(x => x.CustomerOrders)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired(true);

        // <-- Cart -->

        builder.Entity<Cart>()
            .HasOne(x => x.Customer)
            .WithOne(x => x.CustomerCart)
            .HasForeignKey<Cart>(x => x.CustomerId);

        // <-- Shipment -->

        builder.Entity<Shipment>()
            .HasOne(x => x.ShippingOrder)
            .WithOne(x => x.Shipment)
            .HasForeignKey<Shipment>(x => x.ShippingOrderId)
            .IsRequired(true);

        // <-- WarehouseProducts -->

        builder.Entity<WarehouseProducts>()
            .HasKey(x => new { x.WarehouseId, x.WarehouseProductId });

        builder.Entity<WarehouseProducts>()
            .HasOne(x => x.WarehouseProduct)
            .WithMany(x => x.WarehouseProducts)
            .HasForeignKey(x => x.WarehouseProductId);

        builder.Entity<WarehouseProducts>()
            .HasOne(x => x.Warehouse)
            .WithMany(x => x.WarehouseProducts)
            .HasForeignKey(x => x.WarehouseId);

        // <-- OrderProducts -->

        builder.Entity<OrderProducts>()
            .HasKey(x => new { x.OrderId, x.OrderProductId });

        builder.Entity<OrderProducts>()
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderProducts)
            .HasForeignKey(x => x.OrderId);

        builder.Entity<OrderProducts>()
            .HasOne(x => x.OrderProduct)
            .WithMany(x => x.OrderProducts)
            .HasForeignKey(x => x.OrderProductId);

        // <-- CartProducts -->

        builder.Entity<CartProducts>()
            .HasKey(x => new { x.CartId, x.CartProductId });

        builder.Entity<CartProducts>()
            .HasOne(x => x.Cart)
            .WithMany(x => x.CartProducts)
            .HasForeignKey(x => x.CartId);

        builder.Entity<CartProducts>()
            .HasOne(x => x.CartProduct)
            .WithMany(x => x.CartProducts)
            .HasForeignKey(x => x.CartProductId);
    }
}
