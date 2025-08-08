using AthletesApplication.Domain.DomainModels;
using AthletesApplication.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AthletesApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Athlete> Athletes { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<AthletesApplicationUser> Users { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<AthleteInTournament> AthletesInTournaments { get; set; }
    }
}
