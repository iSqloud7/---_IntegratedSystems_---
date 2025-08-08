using EShop.Web.Models.Domain;
using EShop.Web.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Web.Data;

public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }

    // fluent api
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Product>()
            .Property(z => z.ID)
            .ValueGeneratedOnAdd();

        builder.Entity<ShoppingCart>()
            .Property(z => z.ID)
            .ValueGeneratedOnAdd();

        builder.Entity<ProductInShoppingCart>()
            .HasKey(z => new { z.ProductID, z.ShoppingCartID });

        builder.Entity<ProductInShoppingCart>()
            .HasOne(z => z.Product)
            .WithMany(z => z.ProductInShoppingCarts)
            .HasForeignKey(z => z.ShoppingCartID);

        builder.Entity<ProductInShoppingCart>()
            .HasOne(z => z.ShoppingCart)
            .WithMany(z => z.ProductInShoppingCarts)
            .HasForeignKey(z => z.ProductID);

        builder.Entity<ShoppingCart>()
            .HasOne<EShopApplicationUser>(z => z.Owner)
            .WithOne(z => z.UserCart)
            .HasForeignKey<ShoppingCart>(z => z.OwnerID);
        
        /* [Required] Annotation
        builder.Entity<Product>()
            .Property(z => z.ProductName)
            .IsRequired();
        */
    }
}