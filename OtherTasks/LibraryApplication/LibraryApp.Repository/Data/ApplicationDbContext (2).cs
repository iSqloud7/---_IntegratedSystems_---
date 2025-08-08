using LibraryApp.Domain.DomainModels;
using LibraryApp.Domain.Relationship;
using LibraryApp.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Repository.Data;

public class ApplicationDbContext : IdentityDbContext<LibraryApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<BooksInShoppingCart> BooksInShoppingCarts { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<BooksInOrder> BooksInOrders { get; set; }


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

            cart.HasOne(c => c.LibraryApplicationUser) // има еден LibraryApplicationUser
            .WithOne(lau => lau.UserShoppingCart) // кој исто така има една UserShoppingCart
            .HasForeignKey<ShoppingCart>(c => c.OwnerID);
        });


        builder.Entity<BooksInShoppingCart>(booksInCart =>
        {
            //booksInCart.HasKey(bc => new { bc.BookID, bc.ShoppingCartID });
            booksInCart.HasKey(bc => bc.ID);

            booksInCart.HasOne(b => b.Book) // една Book
            .WithMany(b => b.BooksInShoppingCart) // има повеќе ShoppingCarts
            .HasForeignKey(b => b.BookID);

            booksInCart.HasOne(c => c.ShoppingCart) // една ShoppingCart
            .WithMany(c => c.BooksInShoppingCart) // има повеќе Books
            .HasForeignKey(c => c.ShoppingCartID);
        });

        builder.Entity<Order>(order =>
        {
            order.HasKey(o => o.ID);

            order.HasOne(o => o.LibraryApplicationUser)
            .WithMany()
            .HasForeignKey(o => o.OwnerID);
        });

        builder.Entity<BooksInOrder>(booksInOrder =>
        {
            booksInOrder.HasKey(bo => bo.ID);

            booksInOrder.HasOne(b => b.Book)
            .WithMany()
            .HasForeignKey(b => b.BookID);

            booksInOrder.HasOne(o => o.Order)
            .WithMany(o => o.BooksInOrder)
            .HasForeignKey(o => o.OrderID);
        });
    }
}
