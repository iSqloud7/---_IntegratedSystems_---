using EventsApplication.Domain.DomainModels;
using EventsApplication.Domain.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventsApplication.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<EventsApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attendee> Attendees { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Speaker> Speakers { get; set; }
    }
}
