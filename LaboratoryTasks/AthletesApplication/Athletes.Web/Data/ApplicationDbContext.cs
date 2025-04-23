using Athletes.Domain.Models.Domain;
using Athletes.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Athletes.Web.Data;

public class ApplicationDbContext : IdentityDbContext<AthletesApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Athlete> Athletes { get; set; }
    public virtual DbSet<Competition> Competitions { get; set; }
    public virtual DbSet<Participation> Participations { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
}
