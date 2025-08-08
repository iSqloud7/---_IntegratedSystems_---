using LibraryApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Borrowing> Borrowings { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<LibrarySection> LibrarySections { get; set; }
        public virtual DbSet<BookInSection> BooksInSections { get; set; }
    }
}
