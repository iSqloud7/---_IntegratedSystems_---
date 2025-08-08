using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Athlete (FirstName, LastName, DateOfBirth, JerseyNumber, DateJoined)

namespace AthletesApplication.Domain.DomainModels
{
    public class Athlete
    {
        public Guid Id { get; set; }
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
    }
}
