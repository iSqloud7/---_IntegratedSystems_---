using System.ComponentModel.DataAnnotations;

namespace AthletesApplication.Domain.DomainModels
{
    public class Athlete : BaseEntity
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required DateOnly DateOfBirth { get; set; }
        [Required]
        public required int JerseyNumber { get; set; }
        public DateOnly DateJoined { get; set; }
        public virtual ICollection<Participation>? Participations { get; set; }
        public Team? Team { get; set; }
        public Guid TeamId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
