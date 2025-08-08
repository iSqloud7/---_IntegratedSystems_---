using System.ComponentModel.DataAnnotations;

namespace Athletes.Domain.Models.Domain
{
    public class Athlete
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public int JerseyNumber { get; set; }

        public DateOnly DateJoined { get; set; }

        public virtual ICollection<Participation>? Participations { get; set; }

        public Guid TeamID { get; set; }

        public virtual Team? Team { get; set; }
    }
}
