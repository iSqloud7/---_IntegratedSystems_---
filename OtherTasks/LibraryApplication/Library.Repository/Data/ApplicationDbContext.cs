using LibraryApp.Web.Models.Domain;
using LibraryApp.Web.Models.Identity;
using LibraryApp.Web.Models.Relationship;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Web.Data;

public class ApplicationDbContext : IdentityDbContext<LibraryApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<BooksInShoppingCart> BooksInShoppingCarts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>(book =>
        {
            book.HasKey(b => b.ID);

            /*
            book.Property(b => b.Title)
            .HasMaxLength(50)
            .IsRequired();
            */
        });

        builder.Entity<ShoppingCart>(cart =>
        {
            cart.HasKey(c => c.ID);

            cart.HasOne(c => c.LibraryUser)
            .WithOne(lu => lu.UserShoppingCart)
            .HasForeignKey<ShoppingCart>(c => c.OwnerID);
        });

        builder.Entity<BooksInShoppingCart>(booksInCart =>
        {
            booksInCart.HasKey(bc => new {bc.BookID, bc.ShoppingCartID});

            booksInCart.HasOne(b => b.Book)
            .WithMany(b => b.BooksInShoppingCarts)
            .HasForeignKey(b => b.BookID);

            booksInCart.HasOne(c => c.ShoppingCart)
            .WithMany(c => c.BooksInShoppingCarts)
            .HasForeignKey(c => c.ShoppingCartID);
        });
    }
}
