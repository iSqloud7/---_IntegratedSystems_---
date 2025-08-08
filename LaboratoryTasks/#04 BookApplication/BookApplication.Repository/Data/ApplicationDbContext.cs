using BookApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
    }
}
